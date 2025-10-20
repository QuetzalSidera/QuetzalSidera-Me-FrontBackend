using Protobuf.Chat;

namespace Grpc.Share.Protos.ChatModels;

public class ChatHistoryModel
{
    public string UserGuid { get; set; } = string.Empty; //用户Guid
    public List<ChatSessionModel> History  { get; set; } = new(); //历史会话列表

    public static implicit operator ChatHistoryModel(ChatHistory dto)
    {
        if(dto == null)
            return new ChatHistoryModel();
        var model = new ChatHistoryModel()
        {
            UserGuid = dto.UserGuid,
            History = dto.History.Select(s => (ChatSessionModel)s).ToList()
        };
        return model;
    }

    public static implicit operator ChatHistory(ChatHistoryModel model)
    {
        var dto = new ChatHistory()
        {
            UserGuid = model.UserGuid,
        };
        dto.History.AddRange(model.History.Select(s => (ChatSession)s));
        return dto;
    }
}

public class ChatSessionModel
{
    public long CreateTimestamp { get; set; } 
    public string SessionGuid { get; set; }  = string.Empty; //会话Guid
    public string Title { get; set; }  = string.Empty; //会话标题
    public List<ChatMessageModel> Content { get; set; }  = new();

    public static implicit operator ChatSessionModel(ChatSession dto)
    {
        if(dto == null)
            return new ChatSessionModel();
        var model = new ChatSessionModel()
        {
            CreateTimestamp = dto.CreateTimestamp,
            SessionGuid = dto.SessionGuid,
            Title = dto.Title,
            Content = dto.Content.Select(c=>(ChatMessageModel)c).ToList()
        };
        return model;
    }

    public static implicit operator ChatSession(ChatSessionModel model)
    {
        var dto = new ChatSession()
        {
            CreateTimestamp = model.CreateTimestamp,
            SessionGuid = model.SessionGuid,
            Title = model.Title,
        };
        dto.Content.AddRange(model.Content.Select(c=>(ChatMessage)c));
        return dto;
    }
}