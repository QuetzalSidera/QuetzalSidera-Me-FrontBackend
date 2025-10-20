using Protobuf.Www.Content;

namespace Grpc.Share.Protos.WwwModels.Content;

public class LayoutModel
{
    public string Path { get; set; } = string.Empty;
    public List<SectionModel> Sections { get; set; } = new();


    public static implicit operator LayoutModel(Layout dto)
    {
        if (dto == null)
            return new LayoutModel();
        var model = new LayoutModel()
        {
            Path = dto.Path,
            Sections = dto.Sections.Select(s => (SectionModel)s).ToList(),
        };
        return model;
    }

    public static implicit operator Layout(LayoutModel model)
    {
        var dto = new Layout()
        {
            Path = model.Path,
        };
        dto.Sections.AddRange(model.Sections.Select(s => (Section)s).ToList());
        return dto;
    }
}