using Grpc.Share.Protos.SharedModels;
using Protobuf.Www.Content;

namespace Grpc.Share.Protos.WwwModels.Content;

public class SubsectionModel
{
    public string Path { get; set; } = string.Empty;
    public TextModel SectionTitle { get; set; } = new();
    public TextModel SubsectionTitle { get; set; } = new();
    public string SubsectionPictureCss { get; set; } = string.Empty;
    public TextModel SubsectionComment { get; set; } = new();
    public CardType WrappedCardType { get; set; } = CardType.NavSectionUndefined;
    public List<CardModel> Cards { get; set; } = new();

    public static implicit operator SubsectionModel(Subsection dto)
    {
        if (dto == null)
            return new SubsectionModel();
        var model = new SubsectionModel()
        {
            Path = dto.Path,
            SectionTitle = dto.SectionTitle,
            SubsectionTitle = dto.SubsectionTitle,
            SubsectionPictureCss = dto.SubsectionPictureCss,
            SubsectionComment = dto.SubsectionComment,
            WrappedCardType = dto.CardType,
            Cards = dto.Cards.Select(c => (CardModel)c).ToList(),
        };
        return model;
    }

    public static implicit operator Subsection(SubsectionModel model)
    {
        var dto = new Subsection()
        {
            Path = model.Path,
            SectionTitle = model.SectionTitle,
            SubsectionTitle = model.SubsectionTitle,
            SubsectionPictureCss = model.SubsectionPictureCss,
            SubsectionComment = model.SubsectionComment,
            CardType = model.WrappedCardType,
        };
        dto.Cards.AddRange(model.Cards.Select(c => (Card)c).ToList());
        return dto;
    }
}