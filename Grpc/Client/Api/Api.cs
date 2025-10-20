using Google.Protobuf.WellKnownTypes;
using Grpc.Client.Base;
using Grpc.Share.Protos.ApiModels;
using Grpc.Share.Protos.ChatModels;
using Grpc.Share.Protos.SharedModels;
using Protobuf.Api;
using Protobuf.Shared.Status;

namespace Grpc.Client.Api;

public class ApiAuthService
{
    private static readonly Protobuf.Api.ApiAuthService.ApiAuthServiceClient Client = new(ChannelInitializer.Channel);

    public async Task<StatusModel> CheckApiAuthAsync(ApiAuthTokenModel apiAuthTokenModel)
    {
        try
        {
            return await Client.CheckApiAuthAsync(apiAuthTokenModel);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return new StatusModel()
            {
                StatusCode = StatusCodeEnum.ServerError,
                Message = "ServerError"
            };
        }
    }

    public async Task<ApiAuthTokenModel> GetApiAuthAsync(UserInfoModel userInfoModel)
    {
        try
        {
            return await Client.GetApiAuthAsync(userInfoModel);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return new ApiAuthTokenModel();
        }
    }

    public async Task<List<string>> GetAdminsAsync()
    {
        return (await Client.GetAdminsAsync(new Empty())).MailBoxes.ToList();
    }

    public async Task<StatusModel> AddAdminsAsync(List<string> mailBoxes)
    {
        try
        {
            return await Client.AddAdminAsync(new AdminsModel()
            {
                MailBoxes = mailBoxes
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return new StatusModel()
            {
                StatusCode = StatusCodeEnum.ServerError,
                Message = "ServerError",
                AddOn = ex.Message + "\n" + ex.StackTrace
            };
        }
    }

    public async Task<StatusModel> RemoveAdminsAsync(List<string> mailBoxes)
    {
        try
        {
            return await Client.RemoveAdminAsync(new AdminsModel()
            {
                MailBoxes = mailBoxes
            });
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return new StatusModel()
            {
                StatusCode = StatusCodeEnum.ServerError,
                Message = "ServerError"
            };
        }
    }
}

public class CheckListService
{
    private static readonly Protobuf.Api.CheckListService.CheckListServiceClient Client =
        new(ChannelInitializer.Channel);

    public async Task<FriendsCardGuid> AddCheckListAsync(FriendCardCheckListItem item)
    {
        return await Client.AddCheckListAsync(item);
    }

    public async Task<StatusModel> CheckStatusAsync(FriendsCardGuid guid)
    {
        try
        {
            return await Client.CheckStatusAsync(guid);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return new StatusModel()
            {
                StatusCode = StatusCodeEnum.ServerError,
                Message = "ServerError"
            };
        }
    }

    public async Task<StatusModel> PassCheckAsync(FriendsCardGuid guid)
    {
        try
        {
            return await Client.PassCheckAsync(guid);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return new StatusModel()
            {
                StatusCode = StatusCodeEnum.ServerError,
                Message = "ServerError"
            };
        }
    }

    public async Task<StatusModel> RejectCheckAsync(FriendsCardGuid guid)
    {
        try
        {
            return await Client.RejectCheckAsync(guid);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return new StatusModel()
            {
                StatusCode = StatusCodeEnum.ServerError,
                Message = "ServerError"
            };
        }
    }

    public async Task<FriendCardCheckList> GetFriendCardsAsync()
    {
        return await Client.GetFriendCardsAsync(new Empty());
    }
}

public class MetaDataService
{
    private static readonly Protobuf.Api.MetaDataService.MetaDataServiceClient Client =
        new(ChannelInitializer.Channel);

    public async Task<long> GetUserNumAsync()
    {
        try
        {
            return (await Client.GetUserNumAsync(new Empty())).UserNum_;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return 0;
        }
    }
}