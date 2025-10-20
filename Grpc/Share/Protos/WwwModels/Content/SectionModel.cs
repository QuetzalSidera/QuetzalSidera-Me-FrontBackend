using Grpc.Share.Protos.SharedModels;
using Protobuf.Www.Content;

namespace Grpc.Share.Protos.WwwModels.Content;

public class SectionModel
{
    public string Path { get; set; }  = string.Empty;
    public TextModel SectionTitle  { get; set; } = new();
    public string SectionAnchor  { get; set; } = string.Empty;
    public string SectionPictureCss  { get; set; } = string.Empty;
    public TextModel SectionComment { get; set; }  = new();
    public ChildType WrappedChildType { get; set; }  = ChildType.Undefined;
    public CardType WrappedCardType  { get; set; } = CardType.NavSectionUndefined;
    public List<CardModel>? Cards { get; set; }  = new();
    public List<SubsectionModel>? Subsections { get; set; }  = new();


    public static implicit operator Section(SectionModel model)
    {
        var dto = new Section()
        {
            Path = model.Path,
            SectionTitle = model.SectionTitle,
            SectionAnchor = model.SectionAnchor,
            SectionPictureCss = model.SectionPictureCss,
            SectionComment = model.SectionComment,
            ChildType = model.WrappedChildType,
            CardType = model.WrappedCardType,
        };
        if (model.Cards != null)
        {
            dto.Cards.AddRange(model.Cards?.Select(c => (Card)c));
        }

        if (model.Subsections != null)
        {
            dto.Subsections.AddRange(model.Subsections?.Select(s => (Subsection)s));
        }

        return dto;
    }

    public static implicit operator SectionModel(Section dto)
    {
        if (dto == null)
            return new SectionModel();
        var model = new SectionModel()
        {
            Path = dto.Path,
            SectionTitle = dto.SectionTitle,
            SectionAnchor = dto.SectionAnchor,
            SectionPictureCss = dto.SectionPictureCss,
            SectionComment = dto.SectionComment,
            WrappedChildType = dto.ChildType,
            WrappedCardType = dto.CardType,
            Cards = dto.Cards?.Select(c => (CardModel)c).ToList(),
            Subsections = dto.Subsections?.Select(s => (SubsectionModel)s).ToList(),
        };
        return model;
    }
}