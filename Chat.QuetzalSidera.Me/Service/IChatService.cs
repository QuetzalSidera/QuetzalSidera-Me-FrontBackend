using Grpc.Share.Enum;
using Grpc.Share.Protos.ChatModels;
using Grpc.Share.Protos.SharedModels;
using Protobuf.Shared.Status;
using Protobuf.Chat;
using ChatMessageService = Grpc.Client.Chat.ChatMessageService;
using ChatSessionService = Grpc.Client.Chat.ChatSessionService;
using UserInfoService = Grpc.Client.Chat.UserInfoService;

namespace Chat.QuetzalSidera.Me.Service;

/// <summary>
/// 聊天数据与用户数据接口
/// </summary>
public interface IChatService
{
    //用户相关
    Task CheckAuthTokenAsync();
    Task GetTempUserAsync();
    Task<UserInfoModel> GetUserInfoAsync();
    Task<(AuthTokenModel ReturnAuthToken, bool IsValidReturn)> RegisterAsync(string verifyCode, UserInfoModel userInfo);
    Task<(AuthTokenModel ReturnAuthToken, bool IsValidReturn)> PasswordLoginAsync(UserInfoModel userInfo);

    Task<(AuthTokenModel ReturnAuthToken, bool IsValidReturn)>
        VerifyCodeLoginAsync(string verifyCode, UserInfoModel userInfo);

    Task<StatusModel> GetVerifyCodeRegisterAsync(string mailBox);
    Task<StatusModel> GetVerifyCodeVerifyCodeLoginAsync(string mailBox);

    Task<StatusModel> DeleteAccountAsync(AuthTokenModel authToken);

    Task<StatusModel> LogoutAccountAsync(AuthTokenModel authToken);

    //内容相关
    Task<List<ChatSessionModel>> SyncChatHistoryAsync();
    Task<ChatSessionModel> CreateChatSessionAsync();
    IAsyncEnumerable<ChatMessageModel> SendMessageAsync(string sessionGuid, string message);
}

/// <summary>
/// 聊天数据与用户数据服务类
/// </summary>
public class ChatService(
    ChatMessageService chatMessageService,
    ChatSessionService chatSessionService,
    UserInfoService userInfoService) : IChatService
{
    public bool IsFirstRendered = true;

    public void ReInit()
    {
        IsFirstRendered = true;
        IsAuthed = false;
        LocalHistory.History.Clear();
        AuthToken = new AuthTokenModel()
        {
            CreateTimestamp = 0,
            IsRegistered = false,
            CookieString = string.Empty,
            UserGuid = string.Empty,
        };
    }

    /// <summary>
    /// 在用户本地的数据存储 对于无效的AuthToken 后端不会存储数据，所有数据均存储于前端内存中，用户离开即丢失
    /// </summary>
    /// 用户未登陆-对话->注册/登陆后要将此Temp传到数据库保存
    public readonly ChatHistoryModel LocalHistory = new ();

    /// <summary>
    /// 获取临时用户
    /// </summary>
    /// <returns></returns>
    public async Task GetTempUserAsync()
    {
        var result = await userInfoService.GetTempUserAsync();
        AuthToken.IsRegistered = false;
        AuthToken.CookieString = result.CookieString;
        AuthToken.UserGuid = result.UserGuid;
        AuthToken.CreateTimestamp = result.CreateTimestamp;
        IsAuthed = true;
    }

    public async Task<UserInfoModel> GetUserInfoAsync()
    {
#if DEBUG
        //纪念一下第一次测试时用的用户
        // return (new UserInfoModel
        // { 
        //     UserGuid = Guid.NewGuid().ToString(),
        //     HashedPassword = "HashedPassword",
        //     MailBox = "example@example.com",
        //     NickName = "测试用户"
        // }, true);
#endif
        UserInfoModel user = await userInfoService.GetUserAsync(AuthToken);
        return user;
    }

    public async Task<(AuthTokenModel ReturnAuthToken, bool IsValidReturn)> RegisterAsync(string verifyCode,
        UserInfoModel userInfo)
    {
        //验证验证码
        VerifyCodeModel verifyCodeModel = new VerifyCodeModel()
        {
            MailBox = userInfo.MailBox,
            VerifyCode = verifyCode,
            VerifyCodeType = VerifyCodeTypeEnum.Register,
            CreateTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds(),
        };
        var verifyResult = await userInfoService.PostVerifyCodeRegisterAsync(verifyCodeModel);
        switch (verifyResult.StatusCode)
        {
            case StatusCodeEnum.Ok:
                //验证通过，加入用户，返回AuthToken
                AuthTokenModel returnAuthToken = await userInfoService.CreateUserAsync(userInfo);
                return (returnAuthToken, returnAuthToken.IsRegistered);
            case StatusCodeEnum.VerifyCodeError:
            case StatusCodeEnum.InvalidMaliBox:
            default:
                return (new AuthTokenModel()
                {
                    IsRegistered = false
                }, false);
        }
    }

    public async Task<(AuthTokenModel ReturnAuthToken, bool IsValidReturn)> PasswordLoginAsync(UserInfoModel userInfo)
    {
        if (string.IsNullOrEmpty(userInfo.MailBox) || string.IsNullOrEmpty(userInfo.Password))
        {
            return (new AuthTokenModel()
            {
                IsRegistered = false
            }, false);
        }

        AuthTokenModel returnAuthToken = await userInfoService.PasswordLogin(userInfo);
        return (returnAuthToken, returnAuthToken.IsRegistered);
    }

    public async Task<(AuthTokenModel ReturnAuthToken, bool IsValidReturn)> VerifyCodeLoginAsync(string verifyCode,
        UserInfoModel userInfo)
    {
        if (string.IsNullOrEmpty(userInfo.MailBox) || string.IsNullOrEmpty(verifyCode))
        {
            return (new AuthTokenModel()
            {
                IsRegistered = false
            }, false);
        }

        VerifyCodeModel verifyCodeModel = new VerifyCodeModel()
        {
            CreateTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds(),
            VerifyCode = verifyCode,
            VerifyCodeType = VerifyCodeTypeEnum.Login,
            MailBox = userInfo.MailBox,
        };
        AuthTokenModel returnAuthToken = await userInfoService.PostVerifyCodeVerifyCodeLoginAsync(verifyCodeModel);
        return (returnAuthToken, returnAuthToken.IsRegistered);
    }

    public async Task<StatusModel> GetVerifyCodeRegisterAsync(string mailBox)
    {
        if (string.IsNullOrEmpty(mailBox))
        {
            return StatusEnum.InvalidMailBox;
        }

        return await userInfoService.GetVerifyCodeAsync(mailBox, VerifyCodeTypeEnum.Register);
    }

    public async Task<StatusModel> GetVerifyCodeVerifyCodeLoginAsync(string mailBox)
    {
        if (string.IsNullOrEmpty(mailBox))
        {
            return StatusEnum.InvalidMailBox;
        }

        return await userInfoService.GetVerifyCodeAsync(mailBox, VerifyCodeTypeEnum.Login);
    }

    public async Task<StatusModel> DeleteAccountAsync(AuthTokenModel authToken)
    {
        if (string.IsNullOrEmpty(authToken.UserGuid) || string.IsNullOrEmpty(authToken.CookieString) ||
            !authToken.IsRegistered || authToken.CreateTimestamp == 0)
            return StatusEnum.BadRequest;
        return await userInfoService.DeleteAccountAsync(authToken);
    }

    public async Task<StatusModel> LogoutAccountAsync(AuthTokenModel authToken)
    {
        if (string.IsNullOrEmpty(authToken.UserGuid) || string.IsNullOrEmpty(authToken.CookieString) ||
            !authToken.IsRegistered || authToken.CreateTimestamp == 0)
            return StatusEnum.BadRequest;
        return await userInfoService.LogoutAccountAsync(authToken);
    }

    public async Task<List<ChatSessionModel>> SyncChatHistoryAsync()
    {
        var history = await chatSessionService.GetChatHistoryAsync(AuthToken);

        //对history与_localHistory以SessionGuid去重
        history.History = history.History.GroupBy(session => session.SessionGuid).Select(group => group.First())
            .ToList();
        LocalHistory.History = LocalHistory.History.GroupBy(session => session.SessionGuid)
            .Select(group => group.First()).ToList();

        if (AuthToken.IsRegistered && LocalHistory.History.Count != 0)
        {
            LocalHistory.UserGuid = AuthToken.UserGuid;
            await chatSessionService.MergeHistoryAsync(LocalHistory);
        }

        foreach (var session in history.History)
        {
            LocalHistory.History.Add(session);
            LocalHistory.History = LocalHistory.History.GroupBy(s => s.SessionGuid)
                .Select(group => group.First()).ToList();
            LocalHistory.History.Sort((a, b) => b.CreateTimestamp.CompareTo(a.CreateTimestamp));
        }

        return LocalHistory.History;
    }

    /// <summary>
    /// 服务端创建失败返回ChatSessionModel的Guid为空串
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public async Task<ChatSessionModel> CreateChatSessionAsync()
    {
        var result = await chatSessionService.CreateChatSessionAsync(AuthToken);
        return result;
    }


    /// <summary>
    /// 服务端若请求API失败 返回Message的Guid为空串
    /// </summary>
    public async IAsyncEnumerable<ChatMessageModel> SendMessageAsync(string sessionGuid, string message)
    {
        // 构建发送消息
        var postInfo = new ChatMessagePostInfoModel
        {
            SessionGuid = sessionGuid,
            Message = new ChatMessageModel
            {
                Timestamp = DateTimeOffset.Now.ToUnixTimeSeconds(),
                Talker = Talker.User,
                Message = message
            },
            AuthToken = AuthToken
        };
        // 发送消息
        var responses = chatMessageService.PostMessageAsync(postInfo);
        await foreach (var response in responses)
        {
            yield return response;
        }
    }

    public AuthTokenModel AuthToken = new()
    {
        IsRegistered = false
    };

    /// <summary>
    /// 是否已认证(游客/已注册均为true)
    /// </summary>
    public bool IsAuthed = false;

    public async Task CheckAuthTokenAsync()
    {
        var result = await userInfoService.CheckAuthTokenAsync(AuthToken);
        switch (result.StatusCode)
        {
            case StatusCodeEnum.Traveller:
                IsAuthed = true;
                AuthToken.IsRegistered = false;
                break;
            case StatusCodeEnum.Ok:
                IsAuthed = true;
                AuthToken.IsRegistered = true;
                break;
            case StatusCodeEnum.ServerError:
                IsAuthed = true;
                AuthToken.IsRegistered = false;
                break;
            case StatusCodeEnum.NotFound:
            default:
                IsAuthed = false;
                AuthToken.IsRegistered = false;
                break;
        }
    }
}