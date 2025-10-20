using Grpc.Share.Protos.SharedModels;
using Protobuf.Www.Header.Title;

namespace Grpc.Share.Protos.WwwModels.Header;

public class TitleModel
{
    public string TitlePictureCss { get; set; }  = string.Empty;
    public TextModel Title { get; set; }  = new();

    public static implicit operator TitleModel(TitleDto dto)
    {
        if(dto == null)
            return new TitleModel();
        var model = new TitleModel()
        {
            TitlePictureCss = dto.TitlePictureCss,
            Title = dto.Title
        };
        return model;
    }

    public static implicit operator TitleDto(TitleModel model)
    {
        var dto = new TitleDto()
        {
            TitlePictureCss = model.TitlePictureCss,
            Title = model.Title
        };
        return dto;
    }
}