using Backend.ApiEntities;
using Backend.ChatEntities;
using Backend.Shared;
using Grpc.Core;
using Grpc.Share.Config.Www;
using Grpc.Share.Enum;
using Grpc.Share.Protos.ApiModels;
using Grpc.Share.Protos.SharedModels;
using Grpc.Share.Protos.WwwModels.Content;
using Microsoft.EntityFrameworkCore;
using Protobuf.Api;
using Protobuf.Shared.Status;
using Protobuf.Shared.Text;
using DatabaseHelper = Backend.ChatEntities.DatabaseHelper;
using Status = Protobuf.Shared.Status.Status;

namespace Backend.GrpcServer;

public class ApiAuthService : Protobuf.Api.ApiAuthService.ApiAuthServiceBase
{
    private static readonly ApiAuthTokenModel InvalidAuthToken = new()
    {
        AuthToken = string.Empty,
        CreateTimestamp = 0,
        UserGuid = string.Empty,
        IsAdmin = false,
    };

    public override Task<ApiAuthToken> GetApiAuth(Protobuf.Chat.UserInfo request, ServerCallContext context)
    {
        var password = request.Password;
        var mailBox = request.MailBox;
        if (string.IsNullOrWhiteSpace(mailBox) || string.IsNullOrEmpty(mailBox))
            return Task.FromResult((ApiAuthToken)InvalidAuthToken);

        using var db = new ChatContext();
        var user = db.Users.AsNoTracking().FirstOrDefault(user => user.MailBox == mailBox);
        if (user == null)
            return Task.FromResult((ApiAuthToken)InvalidAuthToken);

        string hashedPassword = user.HashedPassword;
        string userGuid = user.UserGuid;
        if (!PasswordHasher.VerifyPassword(password, hashedPassword))
            return Task.FromResult((ApiAuthToken)InvalidAuthToken);

        //验证通过，创建新ApiAuthTokenModel
        var apiAuthToken = ApiEntities.DatabaseHelper.CreateNewApiAuthToken(userGuid, mailBox);
        return Task.FromResult((ApiAuthToken)apiAuthToken);
    }

    public override Task<Status> CheckApiAuth(ApiAuthToken request,
        ServerCallContext context)
    {
        var result = ApiEntities.DatabaseHelper.CheckAuth((ApiAuthTokenModel)request);

        var ret = new Status()
        {
            StatusCode = StatusCodeEnum.NotFound,
        };
        if (result.IsAuth)
            ret.StatusCode = StatusCodeEnum.Ok;
        if (result.IsAdmin)
        {
            ret.StatusCode = StatusCodeEnum.Ok;
            ret.AddOn = "Admin";
        }

        return Task.FromResult(ret);
    }

    public override Task<Admins> GetAdmins(global::Google.Protobuf.WellKnownTypes.Empty request,
        ServerCallContext context)
    {
        return Task.FromResult((Admins)new AdminsModel
        {
            MailBoxes = ApiEntities.DatabaseHelper.GetAdmins(),
        });
    }

    public override Task<Status> AddAdmin(Admins request, ServerCallContext context)
    {
        try
        {
            ApiEntities.DatabaseHelper.AddAdmins(request.MailBoxes.ToList());
            return Task.FromResult((Status)(StatusEnum.Ok));
        }
        catch
        {
            return Task.FromResult((Status)StatusEnum.ServerError);
        }
    }


    public override Task<Status> RemoveAdmin(
        Admins request, ServerCallContext context)
    {
        try
        {
            ApiEntities.DatabaseHelper.RemoveAdmins(request.MailBoxes.ToList());
            return Task.FromResult((Status)(StatusEnum.Ok));
        }
        catch
        {
            return Task.FromResult((Status)StatusEnum.ServerError);
        }
    }
}

public class CheckListService : Protobuf.Api.CheckListService.CheckListServiceBase
{
    /// <summary>
    /// 添加失败返回string.Empty
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override Task<FriendsCardGuid> AddCheckList(FriendCardCheckListItem request,
        ServerCallContext context)
    {
        FriendsCardEntity entity = new()
        {
            CardLink = request.FriendCard.CardLink,
            CardPictureUrl = request.FriendCard.CardPictureUrl,
            CommentDict = ((TextModel)request.FriendCard.CardComment).TextDict,
            TitleDict = ((TextModel)request.FriendCard.CardTitle).TextDict,
            AddOn = request.AddOn
        };
        var ret = ApiEntities.DatabaseHelper.AddFriendCard(entity);
        return Task.FromResult(new FriendsCardGuid()
        {
            Guid = ret,
        });
    }

    /// <summary>
    /// 在检查列表中返回Checking，不在返回Ok
    /// </summary>
    /// <param name="request"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public override Task<Status> CheckStatus(FriendsCardGuid request, ServerCallContext context)
    {
        var ret = ApiEntities.DatabaseHelper.GetFriendsCard(request.Guid);
        return ret == null
            ? Task.FromResult((Status)StatusEnum.Ok)
            : Task.FromResult((Status)StatusEnum.Checking);
    }

    public override Task<Status> PassCheck(FriendsCardGuid request, ServerCallContext context)
    {
        try
        {
            FriendsCardModel? ret = ApiEntities.DatabaseHelper.GetFriendsCard(request.Guid);
            if (ret == null)
                return Task.FromResult((Status)StatusEnum.BadRequest);
            CardModel card = new CardModel()
            {
                Path = ConfigData.PathConfig.RootPath,
                SectionTitle = new TextModel()
                {
                    TextEnUs = ConfigData.Friends.SectionTitle[LangType.EnUs]
                },
                WrappedFriendsCard = ret,
            };
            WwwEntities.DatabaseHelper.AddCard(card);
            ApiEntities.DatabaseHelper.RemoveFriendCard(request.Guid);
            return Task.FromResult((Status)StatusEnum.Ok);
        }
        catch
        {
            return Task.FromResult((Status)StatusEnum.ServerError);
        }
    }

    public override Task<Status> RejectCheck(FriendsCardGuid request, ServerCallContext context)
    {
        return ApiEntities.DatabaseHelper.RemoveFriendCard(request.Guid)
            ? Task.FromResult((Status)StatusEnum.Ok)
            : Task.FromResult((Status)StatusEnum.BadRequest);
    }

    public override Task<FriendCardCheckList> GetFriendCards(global::Google.Protobuf.WellKnownTypes.Empty request,
        ServerCallContext context)
    {
        var result = ApiEntities.DatabaseHelper.GetFriendsCards();
        var ret = new FriendCardCheckList();
        ret.CheckList.AddRange(result.Select(f => (FriendCardCheckListItem)f).ToList());
        return Task.FromResult((FriendCardCheckList)ret);
    }
}

public class MetaDataService : Protobuf.Api.MetaDataService.MetaDataServiceBase
{
    public override Task<UserNum> GetUserNum(Google.Protobuf.WellKnownTypes.Empty request, ServerCallContext context)
    {
        using var db = new ChatContext();
        return Task.FromResult(new UserNum()
        {
            UserNum_ = db.Users.Count()
        });
    }
}