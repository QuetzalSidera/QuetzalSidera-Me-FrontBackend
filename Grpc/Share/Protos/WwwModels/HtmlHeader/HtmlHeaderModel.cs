using Grpc.Share.Protos.SharedModels;
using Protobuf.Www.HtmlHeader.HtmlHeader;

namespace Grpc.Share.Protos.WwwModels.HtmlHeader;

public class HtmlHeaderModel
{
    public TextModel Title { get; set; } = new();
    public string IconLink { get; set; } = string.Empty;

    public static implicit operator HtmlHeaderModel(HtmlHeaderDto dto)
    {
        if(dto == null)
            return new HtmlHeaderModel();
        return new HtmlHeaderModel()
        {
            Title = dto.Title,
            IconLink = dto.IconLink
        };
    }

    public static implicit operator HtmlHeaderDto(HtmlHeaderModel model)
    {
        return new HtmlHeaderDto()
        {
            Title = model.Title,
            IconLink = model.IconLink
        };
    }
}