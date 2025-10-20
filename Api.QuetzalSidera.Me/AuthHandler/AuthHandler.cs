using Api.QuetzalSidera.Me.Shared;
using Grpc.Client.Api;
using Grpc.Share.Protos.ApiModels;
using Grpc.Share.Protos.ChatModels;
using Protobuf.Shared.Status;

namespace Api.QuetzalSidera.Me.AuthHandler;

public class AuthHandler
{
    public const string Route = $"{VersionHelper.VersionNum}/authToken";

    public static async Task<bool> IsAdmin(string userGuid, string authToken)
    {
        if (string.IsNullOrEmpty(userGuid) || string.IsNullOrEmpty(userGuid))
            return false;
        var service = new ApiAuthService();
        var result = await service.CheckApiAuthAsync(new ApiAuthTokenModel()
        {
            UserGuid = userGuid,
            AuthToken = authToken
        });

        if (result.StatusCode == StatusCodeEnum.Ok && result.AddOn == "Admin")
            return true;
        return false;
    }


    public static async Task<Result<ApiAuthTokenModel>> GetAuth(string mailBox, string password)
    {
        if (string.IsNullOrEmpty(mailBox) || string.IsNullOrEmpty(password))
            return new Result<ApiAuthTokenModel>()
            {
                Status = ErrorCode.BadRequest,
                Message = "The mailbox and password are required",
                Data = null
            };
        var service = new ApiAuthService();
        var userInfo = new UserInfoModel()
        {
            MailBox = mailBox,
            Password = password
        };
        var result = await service.GetApiAuthAsync(userInfo);
        if (string.IsNullOrEmpty(result.UserGuid) || string.IsNullOrEmpty(result.AuthToken))
            return new Result<ApiAuthTokenModel>()
            {
                Status = ErrorCode.EmailOrPasswordError,
                Message = nameof(ErrorCode.EmailOrPasswordError),
                Data = null,
            };
        return new Result<ApiAuthTokenModel>()
        {
            Status = ErrorCode.Ok,
            Message = nameof(ErrorCode.Ok),
            Data = result,
        };
    }

    public const string CheckAuthRoute = $"{VersionHelper.VersionNum}/authToken/check";

    public static async Task<Result> CheckAuth(string userGuid, string authToken)
    {
        if (string.IsNullOrEmpty(userGuid) || string.IsNullOrEmpty(authToken))
            return new Result()
            {
                Status = ErrorCode.BadRequest,
                Message = "The userGuid and authToken are required",
            };
        var service = new ApiAuthService();
        var result = await service.CheckApiAuthAsync(new ApiAuthTokenModel()
        {
            UserGuid = userGuid,
            AuthToken = authToken
        });
        if (result.StatusCode == StatusCodeEnum.Ok)
            return new Result()
            {
                Status = ErrorCode.Ok,
                Message = nameof(ErrorCode.Ok),
            };
        return new Result()
        {
            Status = ErrorCode.TokenExpired,
            Message = nameof(ErrorCode.TokenExpired),
        };
    }
}

public class AdminHandler
{
    public const string Route = $"{VersionHelper.VersionNum}/admins";

    public static async Task<Result<List<string>>> GetAdmins()
    {
        var service = new ApiAuthService();
        try
        {
            var ret = await service.GetAdminsAsync();
            return new Result<List<string>>()
            {
                Status = ErrorCode.Ok,
                Message = nameof(ErrorCode.Ok),
                Data = ret
            };
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return new Result<List<string>>()
            {
                Status = ErrorCode.ServerError,
                Message = nameof(ErrorCode.ServerError),
                Data =[ex.Message, ex.StackTrace??string.Empty],
            };
        }
    }

    public static async Task<Result> AddAdmin(string mailBox)
    {
        var service = new ApiAuthService();
        try
        {
            var ret = await service.AddAdminsAsync([mailBox]);
            if (ret.StatusCode == StatusCodeEnum.Ok)
            {
                return new Result()
                {
                    Status = ErrorCode.Ok,
                    Message = nameof(ErrorCode.Ok),
                };
            }

            return new Result()
            {
                Status = ErrorCode.ServerError,
                Message = nameof(ErrorCode.ServerError),
            };
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return new Result()
            {
                Status = ErrorCode.ServerError,
                Message = nameof(ErrorCode.ServerError),
            };
        }
    }

    public static async Task<Result> DeleteAdmin(string mailBox)
    {
        var service = new ApiAuthService();
        try
        {
            var ret = await service.RemoveAdminsAsync([mailBox]);
            if (ret.StatusCode == StatusCodeEnum.Ok)
            {
                return new Result()
                {
                    Status = ErrorCode.Ok,
                    Message = nameof(ErrorCode.Ok),
                };
            }

            return new Result()
            {
                Status = ErrorCode.ServerError,
                Message = nameof(ErrorCode.ServerError),
            };
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return new Result()
            {
                Status = ErrorCode.ServerError,
                Message = nameof(ErrorCode.ServerError),
            };
        }
    }
}