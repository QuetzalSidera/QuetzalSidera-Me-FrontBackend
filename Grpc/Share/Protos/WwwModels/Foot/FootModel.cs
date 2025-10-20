using Grpc.Share.Protos.SharedModels;
using Protobuf.Www.Foot;

namespace Grpc.Share.Protos.WwwModels.Foot;

public class FootModel
{
    public List<LinkSectionModel> Section  { get; set; } = new();
    public FootCommentModel FootComment  { get; set; } = new();

    public static implicit operator FootModel(FootDto dto)
    {
        var model = new FootModel()
        {
            Section = dto.Section.Select(s => (LinkSectionModel)s).ToList(),
            FootComment = dto.FootComment
        };
        return model;
    }

    public static implicit operator FootDto(FootModel model)
    {
        var dto = new FootDto()
        {
            FootComment = model.FootComment
        };
        dto.Section.AddRange(model.Section.Select(s => (LinkSectionDto)s));
        return dto;
    }
}

public class LinkSectionModel
{
    public SectionType Type { get; set; } 
    public TextModel SectionTitle { get; set; }  = new();
    public List<LinkItemModel> ItemList { get; set; }  = new();

    public static implicit operator LinkSectionModel(LinkSectionDto dto)
    {
        var model = new LinkSectionModel()
        {
            Type = dto.Type,
            SectionTitle = dto.SectionTitle,
            ItemList = dto.ItemList.Select(l => (LinkItemModel)l).ToList()
        };
        return model;
    }

    public static implicit operator LinkSectionDto(LinkSectionModel model)
    {
        var dto = new LinkSectionDto()
        {
            Type = model.Type,
            SectionTitle = model.SectionTitle,
        };
        dto.ItemList.AddRange(model.ItemList.Select(l => (LinkItemDto)l));
        return dto;
    }
}

public class LinkItemModel
{
    public TextModel SectionTitle { get; set; }  = new();
    public TextModel ItemTitle  { get; set; } = new();
    public string PictureCss  { get; set; } = string.Empty;
    public string Link  { get; set; } = string.Empty;

    public static implicit operator LinkItemModel(LinkItemDto dto)
    {
        if(dto == null)
            return new LinkItemModel();
        var model = new LinkItemModel()
        {
            SectionTitle = dto.SectionTitle,
            ItemTitle = dto.ItemTitle,
            PictureCss = dto.PictureCss,
            Link = dto.Link
        };
        return model;
    }

    public static implicit operator LinkItemDto(LinkItemModel model)
    {
        
        var dto = new LinkItemDto()
        {
            SectionTitle = model.SectionTitle,
            ItemTitle = model.ItemTitle,
            PictureCss = model.PictureCss,
            Link = model.Link
        };
        return dto;
    }
}

public class FootCommentModel
{
    public TextModel FootComment  { get; set; } = new();

    public string Link { get; set; }  = string.Empty;

    public static implicit operator FootCommentModel(FootCommentDto dto)
    {
        if(dto == null)
            return new FootCommentModel();
        var model = new FootCommentModel()
        {
            Link = dto.Link,
            FootComment = dto.FootComment
        };
        return model;
    }

    public static implicit operator FootCommentDto(FootCommentModel model)
    {
        var dto = new FootCommentDto()
        {
            Link = model.Link,
            FootComment = model.FootComment
        };
        return dto;
    }
}