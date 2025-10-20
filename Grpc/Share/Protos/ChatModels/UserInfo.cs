using Protobuf.Chat;

namespace Grpc.Share.Protos.ChatModels;

public class UserInfoModel
{
    /// <summary>
    /// 邮箱
    /// </summary>
    public string MailBox { get; set; } = string.Empty;

    /// <summary>
    ///  用户Guid
    /// </summary>
    public string UserGuid { get; set; } = string.Empty;

    /// <summary>
    /// 用户昵称
    /// </summary>
    public string NickName { get; set; } = string.Empty;

    /// <summary>
    /// 哈希后密码(数据库表项)
    /// </summary>
    public string HashedPassword { get; set; } = string.Empty;


    /// <summary>
    /// 密码登录时前端传递到后端的明文密码
    /// </summary>
    public string Password { get; set; } = string.Empty;

    public static implicit operator UserInfoModel(UserInfo dto)
    {
        if(dto == null)
            return new UserInfoModel();
        var model = new UserInfoModel()
        {
            MailBox = dto.MailBox,
            UserGuid = dto.UserGuid,
            NickName = dto.NickName,
            HashedPassword = dto.HashedPassword,
            Password = dto.Password,
        };
        return model;
    }

    public static implicit operator UserInfo(UserInfoModel model)
    {
        var dto = new UserInfo()
        {
            MailBox = model.MailBox,
            UserGuid = model.UserGuid,
            NickName = model.NickName,
            HashedPassword = model.HashedPassword,
            Password = model.Password,
        };
        return dto;
    }
}

public class VerifyCodeModel
{
    public string MailBox { get; set; } = string.Empty;
    public string VerifyCode { get; set; } = string.Empty;
    public VerifyCodeTypeEnum VerifyCodeType { get; set; }

    public long CreateTimestamp { get; set; } =
        DateTimeOffset.Now.ToUnixTimeSeconds();

    public static implicit operator VerifyCodeModel(VerifyCode dto)
    {
        if(dto == null)
            return new VerifyCodeModel();
        var model = new VerifyCodeModel()
        {
            MailBox = dto.MailBox,
            VerifyCode = dto.VerifyCode_,
            VerifyCodeType = dto.VerifyCodeType,
            CreateTimestamp = dto.CreateTimestamp,
        };
        return model;
    }

    public static implicit operator VerifyCode(VerifyCodeModel model)
    {
        var dto = new VerifyCode()
        {
            MailBox = model.MailBox,
            VerifyCode_ = model.VerifyCode,
            VerifyCodeType = model.VerifyCodeType,
            CreateTimestamp = model.CreateTimestamp,
        };
        return dto;
    }
}

public class AuthTokenModel
{
    public string UserGuid { get; set; } = string.Empty;
    public string CookieString { get; set; } = string.Empty;
    public long CreateTimestamp { get; set; } = 0;
    public bool IsRegistered { get; set; } = false;
    public static implicit operator AuthToken(AuthTokenModel model)
    {
        var dto = new AuthToken()
        {
            UserGuid = model.UserGuid,
            CookieString = model.CookieString,
            IsValid = model.IsRegistered,
            CreateTimestamp = model.CreateTimestamp,
        };
        return dto;
    }

    public static implicit operator AuthTokenModel(AuthToken dto)
    {
        if(dto == null)
            return new AuthTokenModel();
        var model = new AuthTokenModel()
        {
            UserGuid = dto.UserGuid,
            CookieString = dto.CookieString,
            IsRegistered = dto.IsValid,
            CreateTimestamp = dto.CreateTimestamp,
        };
        return model;
    }
}