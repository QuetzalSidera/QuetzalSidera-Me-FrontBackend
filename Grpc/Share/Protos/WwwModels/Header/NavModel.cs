using Protobuf.Www.Header.Nav;

namespace Grpc.Share.Protos.WwwModels.Header;

using SharedModels;

public class NavModel
{
    public List<NavItemModel> NavItems  { get; set; } = new();

    public static implicit operator NavModel(NavDto dto)
    {
        if(dto == null)
            return new NavModel();
        var model = new NavModel()
        {
            NavItems = dto.NavItems.Select(n => (NavItemModel)n).ToList(),
        };
        return model;
    }

    public static implicit operator NavDto(NavModel model)
    {
        var dto = new NavDto();
        dto.NavItems.AddRange(model.NavItems.Select(n => (NavItemDto)n));
        return dto;
    }
}

public class NavItemModel
{
    public string Link  { get; set; } = string.Empty;
    public TextModel Name  { get; set; } = new();
    public string PictureCss { get; set; }  = string.Empty;

    public static implicit operator NavItemModel(NavItemDto dto)
    {
        if(dto == null)
            return new NavItemModel();
        var model = new NavItemModel()
        {
            Link = dto.Link,
            Name = dto.Name,
            PictureCss = dto.PictureCss,
        };
        return model;
    }

    public static implicit operator NavItemDto(NavItemModel model)
    {
        var dto = new NavItemDto()
        {
            Link = model.Link,
            Name = model.Name,
            PictureCss = model.PictureCss,
        };
        return dto;
    }
}