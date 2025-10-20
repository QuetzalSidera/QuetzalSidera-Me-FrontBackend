using Grpc.Share.Protos.ChatModels;
using Protobuf.Chat;

namespace Backend.GrpcServer;

public static class ErrorMessage
{
    public static ChatMessageModel NewErrorMessage(string errorMessage)
    {
        return new ChatMessageModel()
        {
            MessageGuid = Guid.NewGuid().ToString(),
            Timestamp = DateTimeOffset.Now.ToUnixTimeSeconds(),
            Talker = Talker.Agent,
            Message =$"发生异常 Trace: Timestamp:{DateTimeOffset.Now.ToUnixTimeSeconds()}, Message:{errorMessage}",
        };
    }
}