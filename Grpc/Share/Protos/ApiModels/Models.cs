using Protobuf.Api;

namespace Grpc.Share.Protos.ApiModels;

public class ApiAuthTokenModel
{
    public string UserGuid { get; set; } = string.Empty;
    public string AuthToken { get; set; } = string.Empty;
    public long CreateTimestamp { get; set; }
    public bool IsAdmin { get; set; } = false;

    public static implicit operator ApiAuthTokenModel(ApiAuthToken dto)
    {
        var model = new ApiAuthTokenModel()
        {
            UserGuid = dto.UserGuid,
            AuthToken = dto.AuthToken,
            CreateTimestamp = dto.CreateTimestamp,
            IsAdmin = dto.IsAdmin
        };
        return model;
    }

    public static implicit operator ApiAuthToken(ApiAuthTokenModel model)
    {
        var dto = new ApiAuthToken()
        {
            UserGuid = model.UserGuid,
            AuthToken = model.AuthToken,
            CreateTimestamp = model.CreateTimestamp,
            IsAdmin = model.IsAdmin
        };
        return dto;
    }
}

public class AdminsModel
{
    public List<string> MailBoxes { get; set; } = new List<string>();

    public static implicit operator Admins(AdminsModel model)
    {
        var dto = new Admins();
        dto.MailBoxes.AddRange(model.MailBoxes);
        return dto;
    }

    public static implicit operator AdminsModel(Admins dto)
    {
        if (dto == null)
            return null;
        var model = new AdminsModel()
        {
            MailBoxes = dto.MailBoxes.ToList(),
        };
        return model;
    }
}