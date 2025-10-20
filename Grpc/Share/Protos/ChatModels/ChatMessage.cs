using Protobuf.Chat;

namespace Grpc.Share.Protos.ChatModels;

public class ChatMessageModel
{
    public string MessageGuid { get; set; } = string.Empty;
    public long Timestamp { get; set; }
    public Talker Talker { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool IsThinkMessage{ get; set; } = false;

    public static implicit operator ChatMessageModel(ChatMessage dto)
    {
        if(dto == null)
            return new ChatMessageModel();
        var model = new ChatMessageModel()
        {
            MessageGuid = dto.MessageGuid,
            Timestamp = dto.Timestamp,
            Talker = dto.Talker,
            Message = dto.Message,
        };
        return model;
    }

    public static implicit operator ChatMessage(ChatMessageModel model)
    {
        var dto = new ChatMessage()
        {
            MessageGuid = model.MessageGuid,
            Timestamp = model.Timestamp,
            Talker = model.Talker,
            Message = model.Message
        };
        return dto;
    }
}

public class ChatMessagePostInfoModel
{
    public string SessionGuid { get; set; } = string.Empty;
    public ChatMessageModel Message { get; set; } = new();
    public AuthTokenModel AuthToken { get; set; } = new();

    public static implicit operator ChatMessagePostInfoModel(ChatMessagePostInfo dto)
    {
        if(dto == null)
            return new ChatMessagePostInfoModel();
        var model = new ChatMessagePostInfoModel()
        {
            Message = dto.Message,
            SessionGuid = dto.SessionGuid,
            AuthToken = dto.AuthToken,
        };
        return model;
    }

    public static implicit operator ChatMessagePostInfo(ChatMessagePostInfoModel model)
    {
        var dto = new ChatMessagePostInfo()
        {
            Message = model.Message,
            SessionGuid = model.SessionGuid,
            AuthToken = model.AuthToken
        };
        return dto;
    }
}