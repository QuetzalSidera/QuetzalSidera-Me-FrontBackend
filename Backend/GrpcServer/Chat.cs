using Backend.ChatEntities;
using Backend.ThirdParty.Assistant;
using Backend.ThirdParty.Email;
using Grpc.Core;
using Grpc.Share.Enum;
using Protobuf.Chat;
using ChatMessage = Protobuf.Chat.ChatMessage;
using Grpc.Share.Protos.ChatModels;
using Grpc.Share.Tools;
using Serilog;
using Status = Protobuf.Shared.Status.Status;

namespace Backend.GrpcServer;

public class ChatMessageService : Protobuf.Chat.ChatMessageService.ChatMessageServiceBase
{
    public override async Task PostMessage(ChatMessagePostInfo request, IServerStreamWriter<ChatMessage> responseStream,
        ServerCallContext context)
    {
        //读取相关参数
        AuthTokenModel authToken = request.AuthToken;
        string sessionGuid = request.SessionGuid;
        ChatMessageModel message = new ChatMessageModel()
        {
            Message = request.Message.Message,
            MessageGuid = Guid.NewGuid().ToString(),
            Timestamp = request.Message.Timestamp == 0
                ? DateTimeOffset.Now.ToUnixTimeSeconds()
                : request.Message.Timestamp,
            Talker = Talker.User,
        };
        //查询数据库鉴权
        if (DatabaseHelper.IsAuthed(authToken))
        {
            try
            {
                DeepSeekClient deepSeekClient = new DeepSeekClient();
                //查询数据库表项
                var session = DatabaseHelper.LookUpForSession(authToken, sessionGuid);
                ArgumentNullException.ThrowIfNull(session);
                List<ChatMessageModel> messages = session.Content;
                string? title = null;
                if (session.Content.Count == 0)
                {
                    title = SummaryHelper.GenerateSummary(message.Message);
                }

                //将当前对话插入上下文
                messages.Add(message);

                //调用API，获取回复
                string responseGuid = Guid.NewGuid().ToString();
                bool firstResponsePiece = true;
                long responseTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
                string responseMessage = string.Empty;
                await foreach (string responsePiece in deepSeekClient.SendMessageAsync(messages))
                {
                    if (firstResponsePiece)
                    {
                        responseTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
                        firstResponsePiece = false;
                    }

                    responseMessage += responsePiece;
                    await responseStream.WriteAsync(new ChatMessageModel()
                    {
                        MessageGuid = responseGuid,
                        Timestamp = responseTimestamp,
                        Talker = Talker.Agent,
                        Message = responsePiece,
                    });
                }

                //回复成功，将回复插入上下文
                var response = new ChatMessageModel()
                {
                    MessageGuid = responseGuid,
                    Timestamp = responseTimestamp,
                    Talker = Talker.Agent,
                    Message = responseMessage,
                };
                //将数据保存到数据库

                Log.Information("Chat request: AuthToken:{@authToken}, sessionGuid:{sessionGuid},message: {@message}",authToken, sessionGuid, message);
                Log.Information("Chat response:AuthToken:{@authToken}, sessionGuid:{sessionGuid},message: {@response}", authToken,sessionGuid, response);
                Log.Information("将数据保存到数据库");
                DatabaseHelper.UpdateSession(authToken, session.SessionGuid, [message, response], title);
                Log.Information("保存成功");
            }
            catch (Exception e)
            {
                Log.Error(e, "操作错误Backend.GrpcServer.ChatMessageService.PostMessage");
#if DEBUG

                await responseStream.WriteAsync(ErrorMessage.NewErrorMessage("发生异常"));
                await responseStream.WriteAsync(
                    ErrorMessage.NewErrorMessage($"Message: {e.Message}, StackTrace: {e.StackTrace}"));
#else
                await responseStream.WriteAsync(ErrorMessage.NewErrorMessage("登录已过期，"));
                await responseStream.WriteAsync(ErrorMessage.NewErrorMessage("请刷新页面重新登录"));
#endif
            }
        }
        else
        {
            //在未查询到登录记录的情况下，不将记录插入到数据库中
            Log.Information("登录过期AuthToken: {@authToken}", authToken);
            await responseStream.WriteAsync(ErrorMessage.NewErrorMessage("登录已过期，"));
            await responseStream.WriteAsync(ErrorMessage.NewErrorMessage("请刷新页面重新登录"));
        }
    }
}

public class UserInfoService : Protobuf.Chat.UserInfoService.UserInfoServiceBase
{
    public override Task<Status> CheckAuthToken(AuthToken request, ServerCallContext context)
    {
        if (string.IsNullOrEmpty(request.UserGuid) || string.IsNullOrEmpty(request.CookieString) ||
            request.CreateTimestamp == 0)
            return Task.FromResult((Status)StatusEnum.NotFound);

        AuthTokenModel authToken = new AuthTokenModel()
        {
            UserGuid = request.UserGuid,
            CookieString = request.CookieString,
            CreateTimestamp = request.CreateTimestamp,
            IsRegistered = false
        };
        var result = DatabaseHelper.CheckAuthType(authToken);
        switch (result)
        {
            case AuthTokenType.Login:
                return Task.FromResult((Status)StatusEnum.Ok);
            case AuthTokenType.Traveler:
                return Task.FromResult((Status)StatusEnum.Traveller);
            case AuthTokenType.None:
                return Task.FromResult((Status)StatusEnum.NotFound);
            default:
                return Task.FromResult((Status)StatusEnum.NotFound);
        }
    }

    public override Task<AuthToken> GetTempUser(
        global::Google.Protobuf.WellKnownTypes.Empty request, ServerCallContext context)
    {
        AuthTokenModel authTokenModel = DatabaseHelper.CreateTempAuthToken();
        DatabaseHelper.CreateHistory(authTokenModel.UserGuid);
        return Task.FromResult((AuthToken)authTokenModel);
    }

    public override Task<UserInfo> GetUser(AuthToken request, ServerCallContext context)
    {
        return Task.FromResult((UserInfo)(DatabaseHelper.LookUpForUser(request) ?? new UserInfoModel()));
    }

    public override Task<AuthToken> PasswordLogin(UserInfo request, ServerCallContext context)
    {
        return Task.FromResult((AuthToken)(DatabaseHelper.PasswordLogin(request) ?? new AuthTokenModel()));
    }

    public override Task<AuthToken> CreateUser(UserInfo request, ServerCallContext context)
    {
        var newUser = DatabaseHelper.CreateUser(request);
        if (newUser == null)
        {
            return Task.FromResult((AuthToken)new AuthTokenModel()
            {
                IsRegistered = false,
            });
        }

        var authToken = DatabaseHelper.CreateAuthToken(newUser.UserGuid);
        if (authToken == null)
        {
            return Task.FromResult((AuthToken)(authToken ?? new AuthTokenModel()
            {
                IsRegistered = false,
            }));
        }

        DatabaseHelper.CreateHistory(authToken.UserGuid);
        return Task.FromResult((AuthToken)authToken);
    }

    public override async Task<Status> GetVerifyCode(VerifyCode request, ServerCallContext context)
    {
        if (string.IsNullOrEmpty(request.MailBox))
            return (Status)(StatusEnum.BadRequest);
        string mailBox = request.MailBox;
        VerifyCodeTypeEnum verifyCodeType = request.VerifyCodeType;
        string verifyCode = VerifyCodeGenerator.GenerateVerifyCode();
        VerifyCodeModel verifyCodeModel = new VerifyCodeModel()
        {
            MailBox = mailBox,
            VerifyCode = verifyCode,
            CreateTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds(),
            VerifyCodeType = verifyCodeType,
        };
        if (!(await Mail163.SendEmail(request.MailBox, verifyCode)))
            return (Status)(StatusEnum.VerifyCodeSendError);
        DatabaseHelper.InsertVerifyCode(verifyCodeModel);
        return (Status)(StatusEnum.Ok);
    }

    public override Task<Status> PostVerifyCodeRegister(VerifyCode request,
        ServerCallContext context)
    {
        if (string.IsNullOrEmpty(request.MailBox) || string.IsNullOrWhiteSpace(request.VerifyCode_))
            return Task.FromResult((Status)(StatusEnum.VerifyCodeError));
        VerifyCodeModel verifyCodeModel = new VerifyCodeModel()
        {
            MailBox = request.MailBox,
            VerifyCode = request.VerifyCode_,
            VerifyCodeType = request.VerifyCodeType,
            CreateTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds(),
        };
        return DatabaseHelper.VerifyVerifyCode(verifyCodeModel)
            ? Task.FromResult((Status)(StatusEnum.Ok))
            : Task.FromResult((Status)(StatusEnum.VerifyCodeError));
    }

    public override Task<AuthToken> PostVerifyCodeVerifyCodeLogin(VerifyCode request, ServerCallContext context)
    {
        //空邮箱 空验证码 返回空AuthToken
        if (string.IsNullOrEmpty(request.MailBox) ||
            string.IsNullOrWhiteSpace(request.VerifyCode_))
            return Task.FromResult((AuthToken)new AuthTokenModel());
        //未注册邮箱 返回空AuthToken
        var user = DatabaseHelper.LookUpForUser(request.MailBox);
        if (user == null)
            return Task.FromResult((AuthToken)new AuthTokenModel());

        VerifyCodeModel verifyCodeModel = new VerifyCodeModel()
        {
            MailBox = request.MailBox,
            VerifyCode = request.VerifyCode_,
            VerifyCodeType = request.VerifyCodeType,
            CreateTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds(),
        };
        if (!DatabaseHelper.VerifyVerifyCode(verifyCodeModel))
            return Task.FromResult((AuthToken)new AuthTokenModel());
        var authToken = DatabaseHelper.CreateAuthToken(user.UserGuid);
        return Task.FromResult((AuthToken)(authToken ?? new AuthTokenModel()));
    }

    /// <summary>
    /// 删除帐户同时删除所有AuthToken与对话历史 用户信息
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override Task<Status> DeleteAccount(AuthToken request, ServerCallContext context)
    {
        if (string.IsNullOrEmpty(request.UserGuid) || string.IsNullOrEmpty(request.CookieString) ||
            !request.IsValid || request.CreateTimestamp == 0)
            return Task.FromResult((Status)StatusEnum.BadRequest);
        AuthTokenModel authTokenModel = new AuthTokenModel()
        {
            UserGuid = request.UserGuid,
            CookieString = request.CookieString,
            CreateTimestamp = request.CreateTimestamp,
            IsRegistered = request.IsValid,
        };
        return DatabaseHelper.DeleteAccount(authTokenModel)
            ? Task.FromResult((Status)StatusEnum.Ok)
            : Task.FromResult((Status)StatusEnum.BadRequest);
    }

    /// <summary>
    /// 登出帐户同时只删除此AuthToken
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override Task<Status> LogoutAccount(AuthToken request, ServerCallContext context)
    {
        if (string.IsNullOrEmpty(request.UserGuid) || string.IsNullOrEmpty(request.CookieString) ||
            !request.IsValid || request.CreateTimestamp == 0)
            return Task.FromResult((Status)StatusEnum.BadRequest);
        AuthTokenModel authTokenModel = new AuthTokenModel()
        {
            UserGuid = request.UserGuid,
            CookieString = request.CookieString,
            CreateTimestamp = request.CreateTimestamp,
            IsRegistered = request.IsValid,
        };
        return DatabaseHelper.RemoveAuthToken(authTokenModel)
            ? Task.FromResult((Status)StatusEnum.Ok)
            : Task.FromResult((Status)StatusEnum.BadRequest);
    }
}

public class ChatSessionService : Protobuf.Chat.ChatSessionService.ChatSessionServiceBase
{
    public override Task<Status> MergeChatHistory(
        ChatHistory request, ServerCallContext context)
    {
        if (string.IsNullOrEmpty(request.UserGuid))
            return Task.FromResult((Status)(StatusEnum.BadRequest));
        if (request.History.Count == 0 || DatabaseHelper.MergeHistory(request))
            return Task.FromResult((Status)(StatusEnum.Ok));
        return Task.FromResult((Status)(StatusEnum.ServerError));
    }

    public override Task<ChatHistory> GetChatHistory(AuthToken request, ServerCallContext context)
    {
        return Task.FromResult((ChatHistory)(DatabaseHelper.LookUpForHistory(request) ?? new ChatHistoryModel()));
    }

    public override Task<ChatSession> CreateChatSession(AuthToken request, ServerCallContext context)
    {
        return Task.FromResult((ChatSession)(DatabaseHelper.CreateSession(request) ?? new ChatSessionModel()
        {
            Title = "新对话",
            Content = [],
            SessionGuid = string.Empty,
            CreateTimestamp = 0,
        }));
    }
}