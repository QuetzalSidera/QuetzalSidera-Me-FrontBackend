using System.Text.RegularExpressions;
using Google.Protobuf.WellKnownTypes;
using Grpc.Client.Base;
using Grpc.Share.Enum;
using Grpc.Share.Protos.ChatModels;
using Grpc.Share.Protos.SharedModels;
using Protobuf.Chat;

namespace Grpc.Client.Chat;

public class ChatMessageService
{
    private static readonly Protobuf.Chat.ChatMessageService.ChatMessageServiceClient Client =
        new(ChannelInitializer.Channel);

    public async IAsyncEnumerable<ChatMessageModel> PostMessageAsync(ChatMessagePostInfoModel message)
    {
        using var call = Client.PostMessage(message);
        while (await call.ResponseStream.MoveNext(CancellationToken.None).ConfigureAwait(false))
        {
            yield return call.ResponseStream.Current;
        }
    }
}

public class ChatSessionService
{
    private static readonly Protobuf.Chat.ChatSessionService.ChatSessionServiceClient Client =
        new(ChannelInitializer.Channel);

    public async Task<StatusModel> MergeHistoryAsync(ChatHistoryModel historyModel)
    {
        try
        {
            return await Client.MergeChatHistoryAsync(historyModel);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return StatusEnum.ServerError;
        }
    }

    /// <summary>
    ///  此接口在传入AuthTokenModel在数据库中无相应表项时返回Guid为string.Empty的ChatHistoryModel
    /// </summary>
    /// <param name="authToken"></param>
    /// <returns></returns>
    public async Task<ChatHistoryModel> GetChatHistoryAsync(AuthTokenModel authToken)
    {
        try
        {
            return await Client.GetChatHistoryAsync(authToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return new ChatHistoryModel();
        }
    }

    /// <summary>
    ///  此接口在传入AuthTokenModel在数据库中无相应表项时返回Guid为string.Empty的ChatSessionModel
    /// 创建失败同样返回Guid为string.Empty的ChatSessionModel
    /// </summary>
    /// <param name="authToken"></param>
    /// <returns></returns>
    public async Task<ChatSessionModel> CreateChatSessionAsync(AuthTokenModel authToken)
    {
        try
        {
            return await Client.CreateChatSessionAsync(authToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return new ChatSessionModel();
        }
    }
}

public class UserInfoService
{
    private static readonly Protobuf.Chat.UserInfoService.UserInfoServiceClient
        Client = new(ChannelInitializer.Channel);

    public async Task<StatusModel> DeleteAccountAsync(AuthTokenModel authToken)
    {
        try
        {
            if (string.IsNullOrEmpty(authToken.UserGuid) || string.IsNullOrEmpty(authToken.CookieString) ||
                !authToken.IsRegistered || authToken.CreateTimestamp == 0)
                return StatusEnum.BadRequest;
            return await Client.DeleteAccountAsync(authToken);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return StatusEnum.ServerError;
        }
    }

    public async Task<StatusModel> LogoutAccountAsync(AuthTokenModel authToken)
    {
        try
        {
            if (string.IsNullOrEmpty(authToken.UserGuid) || string.IsNullOrEmpty(authToken.CookieString) ||
                !authToken.IsRegistered || authToken.CreateTimestamp == 0)
                return StatusEnum.BadRequest;
            return await Client.LogoutAccountAsync(authToken);
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return StatusEnum.ServerError;
        }
    }

    public async Task<StatusModel> CheckAuthTokenAsync(AuthTokenModel authToken)
    {
        try
        {
            return await Client.CheckAuthTokenAsync(authToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return StatusEnum.ServerError;
        }
    }

    public async Task<AuthTokenModel> GetTempUserAsync()
    {
        try
        {
            return await Client.GetTempUserAsync(new Empty());
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return new AuthTokenModel();
        }
    }

    /// <summary>
    /// 此接口在传入AuthTokenModel在数据库中无相应表项时返回Guid为string.Empty的UserInfoModel
    /// </summary>
    /// <param name="authToken"></param>
    /// <returns></returns>
    public async Task<UserInfoModel> GetUserAsync(AuthTokenModel authToken)
    {
        try
        {
            return await Client.GetUserAsync(authToken);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return new UserInfoModel();
        }
    }

    public async Task<AuthTokenModel> PasswordLogin(UserInfoModel userInfoModel)
    {
        try
        {
            if (string.IsNullOrEmpty(userInfoModel.MailBox) || string.IsNullOrEmpty(userInfoModel.Password))
                return new AuthTokenModel()
                {
                    IsRegistered = false
                };
            return await Client.PasswordLoginAsync(userInfoModel);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return new AuthTokenModel();
        }
    }

    public async Task<AuthTokenModel> CreateUserAsync(UserInfoModel user)
    {
        try
        {
            return await Client.CreateUserAsync(user);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return new AuthTokenModel();
        }
    }

    public async Task<StatusModel> GetVerifyCodeAsync(string mailBox, VerifyCodeTypeEnum type)
    {
        try
        {
            if (!IsValidEmail(mailBox))
                return StatusEnum.InvalidMailBox;
            var user = new VerifyCodeModel()
            {
                MailBox = mailBox,
                VerifyCodeType = type,
            };
            return await Client.GetVerifyCodeAsync(user);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return new StatusModel();
        }
    }

    public async Task<StatusModel> PostVerifyCodeRegisterAsync(VerifyCodeModel verifyCodeModel)
    {
        try
        {
            if (!IsValidEmail(verifyCodeModel.MailBox))
                return StatusEnum.InvalidMailBox;
            if (verifyCodeModel.VerifyCodeType != VerifyCodeTypeEnum.Register)
            {
                var error = StatusEnum.BadRequest;
                error.Message = "InValid VerifyCodeType";
                return error;
            }

            return await Client.PostVerifyCodeRegisterAsync(verifyCodeModel);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return StatusEnum.ServerError;
        }
    }

    public async Task<AuthTokenModel> PostVerifyCodeVerifyCodeLoginAsync(VerifyCodeModel verifyCodeModel)
    {
        try
        {
            if (!IsValidEmail(verifyCodeModel.MailBox) || verifyCodeModel.VerifyCodeType != VerifyCodeTypeEnum.Login)
                return new AuthTokenModel()
                {
                    IsRegistered = false
                };

            return await Client.PostVerifyCodeVerifyCodeLoginAsync(verifyCodeModel);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return new AuthTokenModel();
        }
    }

    private static bool IsValidEmail(string mailBox)
    {
        if (string.IsNullOrWhiteSpace(mailBox))
            return false;

        // 常用的邮箱验证正则表达式
        string pattern =
            @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";

        return Regex.IsMatch(mailBox, pattern);
    }
}