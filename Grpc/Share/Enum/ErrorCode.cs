using Grpc.Share.Protos.SharedModels;
using Protobuf.Shared.Status;


namespace Grpc.Share.Enum;

public static class StatusEnum
{
    public static readonly StatusModel Ok = StatusHelper.NewStatusModel(StatusCodeEnum.Ok, "Ok");

    public static readonly StatusModel ParameterMissing =
        StatusHelper.NewStatusModel(StatusCodeEnum.ParameterMissing, "Parameter missing");

    public static readonly StatusModel InvalidParameter =
        StatusHelper.NewStatusModel(StatusCodeEnum.InvalidParameter, "Invalid parameter");

    public static readonly StatusModel InvalidMailBox =
        StatusHelper.NewStatusModel(StatusCodeEnum.InvalidMaliBox, "Invalid mail box");

    public static readonly StatusModel VerifyCodeError =
        StatusHelper.NewStatusModel(StatusCodeEnum.VerifyCodeError, "Verify code error");

    public static readonly StatusModel BadRequest =
        StatusHelper.NewStatusModel(StatusCodeEnum.BadRequest, "Bad Request");

    public static readonly StatusModel NotFound =
        StatusHelper.NewStatusModel(StatusCodeEnum.NotFound, "NotFound");

    public static readonly StatusModel VerifyCodeSendError =
        StatusHelper.NewStatusModel(StatusCodeEnum.VerifyCodeSendError, "SendError");
    public static readonly StatusModel ServerError =
        StatusHelper.NewStatusModel(StatusCodeEnum.ServerError, "ServerError");

    public static readonly StatusModel Traveller =
        StatusHelper.NewStatusModel(StatusCodeEnum.Traveller, "Traveller");
    
    public static readonly StatusModel Checking =
        StatusHelper.NewStatusModel(StatusCodeEnum.Checking, "Checking");
    
}

public static class StatusHelper
{
    public static StatusModel NewStatusModel(StatusCodeEnum statusCode = StatusCodeEnum.Ok, string message = "Ok",
        string addOn = "")
    {
        return new StatusModel
        {
            StatusCode = statusCode,
            Message = message,
            AddOn = addOn
        };
    }
}