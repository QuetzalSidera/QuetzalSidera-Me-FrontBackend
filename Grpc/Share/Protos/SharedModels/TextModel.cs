using Protobuf.Shared.Text;

namespace Grpc.Share.Protos.SharedModels;

public class TextModel
{
    public string Id  { get; set; } = string.Empty;
    public string TextDefault  { get; set; } = string.Empty;
    public string TextEnUs  { get; set; } = string.Empty;
    public Dictionary<LangType, string> TextDict { get; set; }  = new();

    public static implicit operator Text(TextModel model)
    {
        var dto = new Text()
        {
            Id = model.Id,
            TextDefault = model.TextDefault,
            TextEnUs = model.TextEnUs,
        };
        foreach (var item in model.TextDict)
        {
            dto.TextDict.Add(new DictionaryEntry()
            {
                LangType = item.Key,
                Text = item.Value
            });
        }

        return dto;
    }

    public static implicit operator TextModel(Text dto)
    {
        if (dto == null)
            return new TextModel();
        var model = new TextModel()
        {
            Id = dto.Id,
            TextDefault = dto.TextDefault,
            TextEnUs = dto.TextEnUs,
        };
        foreach (var item in dto.TextDict)
        {
            model.TextDict[item.LangType] = item.Text;
        }

        return model;
    }
}