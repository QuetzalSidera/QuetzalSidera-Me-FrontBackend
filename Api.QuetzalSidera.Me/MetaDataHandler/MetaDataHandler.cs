using Api.QuetzalSidera.Me.Shared;
using Grpc.Client.Api;

namespace Api.QuetzalSidera.Me.MetaDataHandler;

public class MetaDataHandler
{
    public const string Route = $"{VersionHelper.VersionNum}/userNum";

    public static async Task<Result<long>> GetNum()
    {
        var service = new MetaDataService();
        try
        {
            var ret = await service.GetUserNumAsync();
            return new Result<long>()
            {
                Status = ErrorCode.Ok,
                Message = "Ok",
                Data = ret
            };
        }
        catch (Exception ex)
        {
           Console.WriteLine(ex.Message);
           Console.WriteLine(ex.StackTrace);
            return new Result<long>()
            {
                Status = ErrorCode.ServerError,
                Message = nameof(ErrorCode.ServerError),
                Data = 0
            };
        }
    }
}