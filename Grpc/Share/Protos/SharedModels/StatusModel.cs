using Protobuf.Shared.Status;

namespace Grpc.Share.Protos.SharedModels;

/// <summary>
/// Status的Model
/// </summary>
public class StatusModel
{
    public StatusCodeEnum StatusCode { get; set; }  = StatusCodeEnum.Ok;
    public string Message { get; set; }  = "Ok";
    public string AddOn { get; set; } =string.Empty;


    public static implicit operator Status(StatusModel model)
    {
        return new Status()
        {
            StatusCode = model.StatusCode,
            Message = model.Message,
            AddOn = model.AddOn
        };
    }

    public static implicit operator StatusModel(Status dto)
    {
        if (dto == null)
            return new StatusModel();
        return new StatusModel()
        {
            StatusCode = dto.StatusCode,
            Message = dto.Message,
            AddOn = dto.AddOn
        };
    }
}