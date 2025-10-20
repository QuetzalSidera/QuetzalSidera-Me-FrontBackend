using Grpc.Share.Config.Www;
using Grpc.Share.Protos.SharedModels;
using Grpc.Share.Tools;
using Protobuf.Shared.Text;
using Protobuf.Www.Content;

namespace Grpc.Share.Protos.WwwModels.Content
{
    public class CardModel
    {
        public string Path { get; set; } = string.Empty;
        public TextModel SectionTitle { get; set; } = new();
        public TextModel? SubsectionTitle { get; set; } = new();
        public TextModel CardTitle { get; set; } = new();
        public CardType WrappedCardType { get; set; } = CardType.NavSectionUndefined;
        public AboutCardModel WrappedAboutCard { get; set; } = new ();
        public ProjectsCardModel WrappedProjectsCard { get; set; } = new();
        public TechStackCardModel WrappedTechStackCard { get; set; } = new();
        public HobbiesCardModel WrappedHobbiesCard { get; set; } = new();
        public QuotesCardModel WrappedQuotesCard { get; set; } = new();
        public FriendsCardModel WrappedFriendsCard { get; set; } = new();
        public ThankYouCardModel WrappedThankYouCard { get; set; } = new();
        public List<string> AddOns { get; set; } = new();


        public static implicit operator Card(CardModel model)
        {
            var dto = new Card
            {
                Path = model.Path,
                SectionTitle = model.SectionTitle,
                SubsectionTitle = model.SubsectionTitle ?? new TextModel(),
                CardTitle = model.CardTitle ?? new TextModel(),
                CardType = model.WrappedCardType,
                AboutCard = model.WrappedAboutCard ?? new AboutCardModel(),
                ProjectCard = model.WrappedProjectsCard ?? new ProjectsCardModel(),
                TechStackCard = model.WrappedTechStackCard ?? new TechStackCardModel(),
                HobbiesCard = model.WrappedHobbiesCard ?? new HobbiesCardModel(),
                QuotesCard = model.WrappedQuotesCard ?? new QuotesCardModel(),
                FriendsCard = model.WrappedFriendsCard ?? new FriendsCardModel(),
                ThankYouCard = model.WrappedThankYouCard ?? new ThankYouCardModel(),
            };
            dto.AddOns.AddRange(model.AddOns);
            return dto;
        }


        public static implicit operator CardModel(Card dto)
        {
            if (dto == null)
                return new CardModel();
            var model = new CardModel
            {
                Path = dto.Path,
                SectionTitle = dto.SectionTitle ?? new Text(),
                SubsectionTitle = dto.SubsectionTitle ?? new Text(),
                CardTitle = dto.CardTitle ?? new TextModel(),
                WrappedCardType = dto.CardType,
                WrappedAboutCard = dto.AboutCard ?? new AboutCard(),
                WrappedProjectsCard = dto.ProjectCard ?? new ProjectsCard(),
                WrappedTechStackCard = dto.TechStackCard ?? new TechStackCard(),
                WrappedHobbiesCard = dto.HobbiesCard ?? new HobbiesCard(),
                WrappedQuotesCard = dto.QuotesCard ?? new QuotesCard(),
                WrappedFriendsCard = dto.FriendsCard ?? new FriendsCard(),
                WrappedThankYouCard = dto.ThankYouCard ?? new ThankYouCard(),
                AddOns = dto.AddOns.ToList(),
            };
            return model;
        }
    }

    public class AboutCardModel
    {
        public List<TextModel> CardContent { get; set; } = new();

        public static implicit operator AboutCard(AboutCardModel model)
        {
            var dto = new AboutCard();
            dto.CardContent.AddRange(model.CardContent.Select(c => (Text)c));
            return dto;
        }

        public static implicit operator AboutCardModel(AboutCard dto)
        {
            if (dto == null)
                return new AboutCardModel();
            var model = new AboutCardModel()
            {
                CardContent = dto.CardContent.Select(c => (TextModel)c).ToList()
            };
            return model;
        }

        public static explicit operator CardModel(AboutCardModel aboutCardModel)
        {
            var cardModel = new CardModel()
            {
                Path = ConfigData.PathConfig.RootPath,
                SectionTitle = ConfigData.AboutMe.SectionTitle.ToTextModel(),
                SubsectionTitle = null,
                CardTitle = new TextModel(),
                WrappedCardType = CardType.NavSectionAbout,
                WrappedAboutCard = aboutCardModel,
            };
            return cardModel;
        }
    }

    public class ProjectsCardModel
    {
        public CompleteStatus CompleteStatus { get; set; } = CompleteStatus.Undefined;
        public string CardPictureCss { get; set; } = string.Empty;
        public TextModel CardTitle { get; set; } = new();
        public TextModel CardContent { get; set; } = new();
        public TextModel CardComment { get; set; } = new();
        public string CardLink { get; set; } = string.Empty;

        public static implicit operator ProjectsCard(ProjectsCardModel model)
        {
            var dto = new ProjectsCard()
            {
                CompleteStatus = model.CompleteStatus,
                CardPictureCss = model.CardPictureCss,
                CardTitle = model.CardTitle,
                CardContent = model.CardContent,
                CardComment = model.CardComment,
                CardLink = model.CardLink,
            };

            return dto;
        }

        public static implicit operator ProjectsCardModel(ProjectsCard dto)
        {
            if (dto == null)
                return new ProjectsCardModel();
            var model = new ProjectsCardModel()
            {
                CompleteStatus = dto.CompleteStatus,
                CardPictureCss = dto.CardPictureCss,
                CardTitle = dto.CardTitle,
                CardContent = dto.CardContent,
                CardComment = dto.CardComment,
                CardLink = dto.CardLink,
            };
            return model;
        }

        public static explicit operator CardModel(ProjectsCardModel projectsCardModel)
        {
            var cardModel = new CardModel()
            {
                Path = ConfigData.PathConfig.RootPath,
                SectionTitle = ConfigData.Projects.SectionTitle.ToTextModel(),
                SubsectionTitle = null,
                CardTitle = projectsCardModel.CardTitle,
                WrappedCardType = CardType.NavSectionAbout,
                WrappedProjectsCard = projectsCardModel,
            };
            return cardModel;
        }
    }

    public class TechStackCardModel
    {
        public TextModel CardTitle { get; set; } = new();
        public string CardPictureCss { get; set; } = string.Empty;
        public ManageStatus ManageStatus { get; set; } = ManageStatus.Undefined;
        public string CardLink { get; set; } = string.Empty;

        public static implicit operator TechStackCard(TechStackCardModel model)
        {
            var dto = new TechStackCard()
            {
                CardTitle = model.CardTitle,
                CardPictureCss = model.CardPictureCss,
                ManageStatus = model.ManageStatus,
                CardLink = model.CardLink,
            };
            return dto;
        }

        public static implicit operator TechStackCardModel(TechStackCard dto)
        {
            if (dto == null)
                return new TechStackCardModel();
            var model = new TechStackCardModel()
            {
                CardTitle = dto.CardTitle,
                CardPictureCss = dto.CardPictureCss,
                ManageStatus = dto.ManageStatus,
                CardLink = dto.CardLink,
            };
            return model;
        }

        public static explicit operator CardModel(TechStackCardModel techStackCardModel)
        {
            var cardModel = new CardModel()
            {
                Path = ConfigData.PathConfig.RootPath,
                SectionTitle = ConfigData.TechStack.SectionTitle.ToTextModel(),
                SubsectionTitle = null,
                CardTitle = techStackCardModel.CardTitle,
                WrappedCardType = CardType.NavSectionTechStack,
                WrappedTechStackCard = techStackCardModel,
            };
            return cardModel;
        }
    }

    public class HobbiesCardModel
    {
        public TextModel CardTitle { get; set; } = new();
        public string CardPictureUrl { get; set; } = string.Empty;
        public TextModel CardComment { get; set; } = new();
        public string? CardPictureCss { get; set; } = string.Empty;
        public string CardLink { get; set; } = string.Empty;

        public static implicit operator HobbiesCard(HobbiesCardModel model)
        {
            var dto = new HobbiesCard()
            {
                CardTitle = model.CardTitle,
                CardPictureUrl = model.CardPictureUrl,
                CardComment = model.CardComment,
                CardPictureCss = model.CardPictureCss,
                CardLink = model.CardLink,
            };
            return dto;
        }

        public static implicit operator HobbiesCardModel(HobbiesCard dto)
        {
            if (dto == null)
                return new HobbiesCardModel();
            var model = new HobbiesCardModel()
            {
                CardTitle = dto.CardTitle,
                CardPictureUrl = dto.CardPictureUrl,
                CardComment = dto.CardComment,
                CardPictureCss = dto.CardPictureCss,
                CardLink = dto.CardLink,
            };
            return model;
        }

        public static explicit operator CardModel(HobbiesCardModel hobbiesCardModel)
        {
            var cardModel = new CardModel()
            {
                Path = ConfigData.PathConfig.RootPath,
                SectionTitle = ConfigData.Hobbies.SectionTitle.ToTextModel(),
                SubsectionTitle = null,
                CardTitle = hobbiesCardModel.CardTitle,
                WrappedCardType = CardType.NavSectionHobbies,
                WrappedHobbiesCard = hobbiesCardModel,
            };
            return cardModel;
        }
    }

    public class QuotesCardModel
    {
        public TextModel CardContent { get; set; } = new();
        public TextModel CardComment { get; set; } = new();
        public string CardLink { get; set; } = string.Empty;

        public static implicit operator QuotesCard(QuotesCardModel model)
        {
            var dto = new QuotesCard()
            {
                CardContent = model.CardContent,
                CardComment = model.CardComment,
                CardLink = model.CardLink,
            };
            return dto;
        }

        public static implicit operator QuotesCardModel(QuotesCard dto)
        {
            if (dto == null)
                return new QuotesCardModel();
            var model = new QuotesCardModel()
            {
                CardContent = dto.CardContent,
                CardComment = dto.CardComment,
                CardLink = dto.CardLink,
            };
            return model;
        }

        public static explicit operator CardModel(QuotesCardModel quotesCardModel)
        {
            var cardModel = new CardModel()
            {
                Path = ConfigData.PathConfig.RootPath,
                SectionTitle = ConfigData.Hobbies.SectionTitle.ToTextModel(),
                SubsectionTitle = null,
                CardTitle = new TextModel(),
                WrappedCardType = CardType.NavSectionQuotes,
                WrappedQuotesCard = quotesCardModel,
            };
            return cardModel;
        }
    }

    public class FriendsCardModel
    {
        public TextModel CardTitle { get; set; } = new();
        public string CardPictureUrl { get; set; } = string.Empty;
        public TextModel CardComment { get; set; } = new();
        public string CardLink { get; set; } = string.Empty;

        public static implicit operator FriendsCard(FriendsCardModel model)
        {
            var dto = new FriendsCard()
            {
                CardTitle = model.CardTitle,
                CardPictureUrl = model.CardPictureUrl,
                CardComment = model.CardComment,
                CardLink = model.CardLink,
            };
            return dto;
        }

        public static implicit operator FriendsCardModel(FriendsCard dto)
        {
            if (dto == null)
                return new FriendsCardModel();
            var model = new FriendsCardModel()
            {
                CardTitle = dto.CardTitle,
                CardPictureUrl = dto.CardPictureUrl,
                CardComment = dto.CardComment,
                CardLink = dto.CardLink,
            };
            return model;
        }

        public static explicit operator CardModel(FriendsCardModel friendsCardModel)
        {
            var cardModel = new CardModel()
            {
                Path = ConfigData.PathConfig.RootPath,
                SectionTitle = ConfigData.Friends.SectionTitle.ToTextModel(),
                SubsectionTitle = null,
                CardTitle = friendsCardModel.CardTitle,
                WrappedCardType = CardType.NavSectionFriends,
                WrappedFriendsCard = friendsCardModel,
            };
            return cardModel;
        }
    }

    public class ThankYouCardModel
    {
        public TextModel CardTitle { get; set; } = new();
        public List<TextModel> CardContent { get; set; } = new();
        public TextModel CardContentComment { get; set; } = new();

        public static implicit operator ThankYouCard(ThankYouCardModel model)
        {
            var dto = new ThankYouCard()
            {
                CardTitle = model.CardTitle,
                CardContentComment = model.CardContentComment,
            };
            dto.CardContent.AddRange(model.CardContent.Select(c => (Text)c));
            return dto;
        }

        public static implicit operator ThankYouCardModel(ThankYouCard dto)
        {
            if (dto == null)
                return new ThankYouCardModel();
            var model = new ThankYouCardModel()
            {
                CardTitle = dto.CardTitle,
                CardContent = dto.CardContent.Select(c => (TextModel)c).ToList(),
                CardContentComment = dto.CardContentComment,
            };
            return model;
        }

        public static explicit operator CardModel(ThankYouCardModel thankYouCardModel)
        {
            var cardModel = new CardModel()
            {
                Path = ConfigData.PathConfig.ThankYouPath,
                SectionTitle = new TextModel(),
                SubsectionTitle = null,
                CardTitle = thankYouCardModel.CardTitle,
                WrappedCardType = CardType.NavSectionThankYou,
                WrappedThankYouCard = thankYouCardModel,
            };
            return cardModel;
        }
    }
}