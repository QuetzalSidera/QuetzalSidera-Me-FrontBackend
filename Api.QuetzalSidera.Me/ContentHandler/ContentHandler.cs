using System.Text.Json;
using Api.QuetzalSidera.Me.Shared;
using Grpc.Client.Www;
using Grpc.Share.Protos.SharedModels;
using Grpc.Share.Protos.WwwModels.Content;
using Grpc.Share.Protos.WwwModels.Header;
using Protobuf.Api;
using Protobuf.Shared.Status;
using Protobuf.Shared.Text;
using CheckListService = Grpc.Client.Api.CheckListService;

namespace Api.QuetzalSidera.Me.ContentHandler;

public class FriendCardHandler
{
    public const string Route = $"{VersionHelper.VersionNum}/checkList";
    public const string RouteAll = $"{VersionHelper.VersionNum}/checkList/all";

    public static async Task<Result<bool>> CheckStatus(string guid)
    {
        try
        {
            if (string.IsNullOrEmpty(guid))
                return new Result<bool>()
                {
                    Status = ErrorCode.BadRequest,
                    Message = "Bad Request Guid",
                    Data = false,
                };
            FriendsCardGuid friendsCardGuid = new FriendsCardGuid()
            {
                Guid = guid
            };
            var service = new CheckListService();
            var ret = await service.CheckStatusAsync(friendsCardGuid);
            return new Result<bool>()
            {
                Status = ErrorCode.Ok,
                Message = ret.StatusCode == StatusCodeEnum.Ok ? "Finished" : "Checking",
                Data = ret.StatusCode == StatusCodeEnum.Ok,
            };
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return new Result<bool>()
            {
                Status = ErrorCode.ServerError,
                Message = nameof(ErrorCode.ServerError),
                Data = false,
            };
        }
    }

    public static async Task<Result<string>> AddCard(Param card)
    {
        var service = new CheckListService();
        try
        {
            var ret = await service.AddCheckListAsync(card);
            if (string.IsNullOrEmpty(ret.Guid))
                return new Result<string>()
                {
                    Status = ErrorCode.BadRequest,
                    Message = "BadRequest",
                    Data = null,
                };
            return new Result<string>()
            {
                Status = ErrorCode.Ok,
                Message = "Ok",
                Data = ret.Guid,
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return new Result<string>()
            {
                Status = ErrorCode.ServerError,
                Message = nameof(ErrorCode.ServerError),
                Data = ex.Message + "\n" + ex.StackTrace,
            };
        }
    }

    public static async Task<Result> PassCheck(string guid)
    {
        if (string.IsNullOrEmpty(guid))
            return new Result()
            {
                Status = ErrorCode.BadRequest,
                Message = "Bad Request Guid",
            };
        FriendsCardGuid friendsCardGuid = new FriendsCardGuid()
        {
            Guid = guid
        };
        var service = new CheckListService();
        var ret = await service.PassCheckAsync(friendsCardGuid);
        return new Result()
        {
            Status = ret.StatusCode == StatusCodeEnum.Ok ? ErrorCode.Ok : ErrorCode.BadRequest,
            Message = ret.StatusCode == StatusCodeEnum.Ok ? "Ok" : "BadRequest",
        };
    }

    public static async Task<Result> RejectCheck(string guid)
    {
        if (string.IsNullOrEmpty(guid))
            return new Result()
            {
                Status = ErrorCode.BadRequest,
                Message = "Bad Request Guid",
            };
        FriendsCardGuid friendsCardGuid = new FriendsCardGuid()
        {
            Guid = guid
        };
        var service = new CheckListService();
        var ret = await service.RejectCheckAsync(friendsCardGuid);
        return new Result()
        {
            Status = ret.StatusCode == StatusCodeEnum.Ok ? ErrorCode.Ok : ErrorCode.BadRequest,
            Message = ret.StatusCode == StatusCodeEnum.Ok ? "Ok" : "BadRequest",
        };
    }

    public static async Task<Result<List<Param>>> GetCheckList()
    {
        var service = new CheckListService();
        try
        {
            var result = await service.GetFriendCardsAsync();
            List<Param> param = result.CheckList.Select(f => (Param)f).ToList();
            return new Result<List<Param>>()
            {
                Status = ErrorCode.Ok,
                Message = "Ok",
                Data = param,
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return new Result<List<Param>>()
            {
                Status = ErrorCode.ServerError,
                Message = nameof(ErrorCode.ServerError),
                Data = [new Param { AddOn = ex.Message + "\n" + ex.StackTrace }]
            };
        }
    }

    public class Param
    {
        public string Guid { get; set; } = string.Empty;
        public string Link { get; set; } = string.Empty;
        public string PictureLink { get; set; } = string.Empty;
        public TextParam Title { get; set; } = new();
        public TextParam Comment { get; set; } = new();
        public string AddOn { get; set; } = string.Empty;

        public static implicit operator FriendCardCheckListItem(Param param)
        {
            FriendsCardModel card = new FriendsCardModel()
            {
                CardLink = param.Link,
                CardTitle = (TextModel)param.Title,
                CardComment = (TextModel)param.Comment,
                CardPictureUrl = param.PictureLink,
            };

            FriendCardCheckListItem item = new FriendCardCheckListItem()
            {
                FriendCard = card,
                AddOn = param.AddOn,
                Guid = param.Guid,
            };
            return item;
        }

        public static implicit operator Param(FriendCardCheckListItem item)
        {
            Param card = new Param()
            {
                Link = item.FriendCard.CardLink,
                PictureLink = item.FriendCard.CardPictureUrl,
                Title = (TextModel)item.FriendCard.CardTitle,
                Comment = (TextModel)item.FriendCard.CardComment,
                AddOn = item.AddOn,
                Guid = item.Guid,
            };
            return card;
        }
    }

    public class TextParam
    {
        public string ZhCn { get; set; } = string.Empty;
        public string EnUs { get; set; } = string.Empty;
        public string ZhHk { get; set; } = string.Empty;
        public string ZhTw { get; set; } = string.Empty;
        public string EnGb { get; set; } = string.Empty;
        public string JaJp { get; set; } = string.Empty;


        public static implicit operator TextModel(TextParam param)
        {
            var ret = new TextModel
            {
                TextDefault = param.ZhCn,
                TextEnUs = param.EnUs,
                TextDict = new Dictionary<LangType, string>()
                {
                    [LangType.ZhCn] = param.ZhCn,
                    [LangType.EnUs] = param.EnUs,
                    [LangType.ZhHk] = param.ZhHk,
                    [LangType.ZhTw] = param.ZhTw,
                    [LangType.EnGb] = param.EnGb,
                    [LangType.JaJp] = param.JaJp,
                }
            };
            return ret;
        }

        public static implicit operator TextParam(TextModel model)
        {
            var ret = new TextParam();
            model.TextDict.TryGetValue(LangType.ZhCn, out var zhCn);
            ret.ZhCn = zhCn ?? string.Empty;
            model.TextDict.TryGetValue(LangType.EnUs, out var enUs);
            ret.EnUs = enUs ?? string.Empty;
            model.TextDict.TryGetValue(LangType.ZhHk, out var zhHk);
            ret.ZhHk = zhHk ?? string.Empty;
            model.TextDict.TryGetValue(LangType.ZhTw, out var zhTw);
            ret.ZhTw = zhTw ?? string.Empty;
            model.TextDict.TryGetValue(LangType.EnGb, out var enGb);
            ret.EnGb = enGb ?? string.Empty;
            model.TextDict.TryGetValue(LangType.JaJp, out var jaJp);
            ret.JaJp = jaJp ?? string.Empty;
            return ret;
        }
    }
}

public class LocationHandler
{
    public const string Route = $"{VersionHelper.VersionNum}/myLocation";

    public static async Task<Result<string>> GetLocation()
    {
        var service = new HeaderWeatherDataService();
        try
        {
            var result = await service.GetWeatherAsync();
            Console.WriteLine(JsonSerializer.Serialize(result));
            var ret = result.Location.Location;
            return new Result<string>()
            {
                Status = ErrorCode.Ok,
                Message = "Ok",
                Data = ret,
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            return new Result<string>()
            {
                Status = ErrorCode.ServerError,
                Message = ex.Message,
                Data = null,
            };
        }
    }


    public static async Task<Result> ModifyLocation(string location)
    {
        LocationModel model = new LocationModel()
        {
            Location = location
        };
        var service = new HeaderWeatherDataService();
        try
        {
            await service.ModifyLocationAsync(model);
            return new Result()
            {
                Status = ErrorCode.Ok,
                Message = "Ok",
            };
        }
        catch (Exception ex)
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