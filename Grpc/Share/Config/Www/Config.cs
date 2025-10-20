using Grpc.Share.Protos.SharedModels;
using Grpc.Share.Protos.WwwModels.Content;
using Grpc.Share.Protos.WwwModels.Foot;
using Grpc.Share.Protos.WwwModels.Header;
using Grpc.Share.Protos.WwwModels.HtmlHeader;
using Grpc.Share.Tools;
using Protobuf.Www.Content;

namespace Grpc.Share.Config.Www;

public static class Config
{
    public static LayoutModel ConfigRootLayoutModel;
    public static LayoutModel ConfigThankYouLayoutModel;
    public static HtmlHeaderModel ConfigHtmlHeaderModel;
    public static LocationModel ConfigLocationModel;
    public static TitleModel ConfigTitleModel;
    public static WeatherModel ConfigWeatherModel;
    public static NavModel ConfigNavModel;
    public static ProfileModel ConfigProfileModel;
    public static FootModel ConfigFootModel;


    static Config()
    {
        //HtmlHeader
        ConfigHtmlHeaderModel = new HtmlHeaderModel()
        {
            Title = ConfigData.HtmlHeader.HtmlTitle.ToTextModel(),
            IconLink = ConfigData.HtmlHeader.LogoPath
        };

        //Header
        ConfigTitleModel = new TitleModel()
        {
            TitlePictureCss = ConfigData.Header.Title.Css,
            Title = ConfigData.Header.Title.Text.ToTextModel(),
        };

        ConfigLocationModel = new LocationModel()
        {
            Location = "深圳"
        };
        ConfigWeatherModel = new WeatherModel()
        {
            Location = ConfigLocationModel,
            WeatherInfo = "晴朗",
            WeatherPictureCss = "qi-100",
            Temp = "26°C",
            Humidity = "50%",
            WindSpeed = "3m/s",
        };
        ConfigNavModel = new NavModel()
        {
            NavItems =
            [
                new NavItemModel()
                {
                    Link = ConfigData.Header.HeaderNav.GitHubLink,
                    Name = ConfigData.Header.HeaderNav.GitHub.ToTextModel(),
                    PictureCss = ConfigData.Header.HeaderNav.GitHubCss
                },

                new NavItemModel()
                {
                    Link = ConfigData.Header.HeaderNav.AliceLink,
                    Name = ConfigData.Header.HeaderNav.Alice.ToTextModel(),
                    PictureCss = ConfigData.Header.HeaderNav.AliceCss
                },
                new NavItemModel()
                {
                    Link = ConfigData.Header.HeaderNav.OpenLink,
                    Name = ConfigData.Header.HeaderNav.Open.ToTextModel(),
                    PictureCss = ConfigData.Header.HeaderNav.OpenCss
                },
            ]
        };

        ConfigProfileModel = new ProfileModel()
        {
            HeaderPictureUrl = ConfigData.Header.HeaderProfile.HeaderPictureLink,
            Name = ConfigData.Header.HeaderProfile.Name.ToTextModel(),
            Summary = ConfigData.Header.HeaderProfile.Summary.ToTextModel(),
            Description = ConfigData.Header.HeaderProfile.Description.ToTextModel(),
        };

        //RootLayout
        //AboutMe
        SectionModel aboutMeModel = new SectionModel()
        {
            Path = ConfigData.PathConfig.RootPath,
            SectionTitle = ConfigData.AboutMe.SectionTitle.ToTextModel(),
            SectionAnchor = ConfigData.AboutMe.SectionAnchor,
            SectionPictureCss = ConfigData.AboutMe.SectionCss,
            SectionComment = ConfigData.AboutMe.SectionComment.ToTextModel(),
            WrappedChildType = ChildType.Card,
            WrappedCardType = CardType.NavSectionAbout,
            Cards =
            [
                new CardModel()
                {
                    Path = ConfigData.PathConfig.RootPath,
                    SectionTitle = ConfigData.AboutMe.SectionTitle.ToTextModel(),
                    SubsectionTitle = null,
                    CardTitle = new TextModel(),
                    WrappedCardType = CardType.NavSectionAbout,
                    WrappedAboutCard = new AboutCardModel()
                    {
                        CardContent =
                        [
                            ConfigData.AboutMe.Card1.Description1.ToTextModel(),
                            ConfigData.AboutMe.Card1.Description2.ToTextModel(),
                            ConfigData.AboutMe.Card1.Description3.ToTextModel(),
                        ]
                    }
                }
            ]
        };

        //Projects
        SectionModel projectsModel = new SectionModel()
        {
            Path = ConfigData.PathConfig.RootPath,
            SectionTitle = ConfigData.Projects.SectionTitle.ToTextModel(),
            SectionAnchor = ConfigData.Projects.SectionAnchor,
            SectionPictureCss = ConfigData.Projects.SectionCss,
            SectionComment = ConfigData.Projects.SectionComment.ToTextModel(),
            WrappedChildType = ChildType.Card,
            WrappedCardType = CardType.NavSectionProjects,
            Cards = ConfigData.Projects.Cards.Select(c => (CardModel)c).ToList(),
        };
        //TechStack
        SectionModel techStackModel = new SectionModel()
        {
            Path = ConfigData.PathConfig.RootPath,
            SectionTitle = ConfigData.TechStack.SectionTitle.ToTextModel(),
            SectionAnchor = ConfigData.TechStack.SectionAnchor,
            SectionPictureCss = ConfigData.TechStack.SectionCss,
            SectionComment = ConfigData.TechStack.SectionComment.ToTextModel(),
            WrappedChildType = ChildType.Card,
            WrappedCardType = CardType.NavSectionTechStack,
            Cards = ConfigData.TechStack.Cards.Select(c => (CardModel)c).ToList(),
        };
        //Hobbies
        SectionModel hobbiesModel = new SectionModel()
        {
            Path = ConfigData.PathConfig.RootPath,
            SectionTitle = ConfigData.Hobbies.SectionTitle.ToTextModel(),
            SectionAnchor = ConfigData.Hobbies.SectionAnchor,
            SectionPictureCss = ConfigData.Hobbies.SectionCss,
            SectionComment = ConfigData.Hobbies.SectionComment.ToTextModel(),
            WrappedChildType = ChildType.Subsection,
            WrappedCardType = CardType.NavSectionHobbies,
            Subsections =
            [
                new SubsectionModel()
                {
                    Path = ConfigData.PathConfig.RootPath,
                    SectionTitle = ConfigData.Hobbies.SectionTitle.ToTextModel(),
                    SubsectionTitle = ConfigData.Hobbies.Subsection1.SubsectionTitle.ToTextModel(),
                    SubsectionPictureCss = ConfigData.Hobbies.Subsection1.SubsectionCss,
                    SubsectionComment = ConfigData.Hobbies.Subsection1.SubsectionComment.ToTextModel(),
                    WrappedCardType = CardType.NavSectionHobbies,
                    Cards = ConfigData.Hobbies.Subsection1.Cards.Select(c => (CardModel)c).ToList(),
                },
                new SubsectionModel()
                {
                    Path = ConfigData.PathConfig.RootPath,
                    SectionTitle = ConfigData.Hobbies.SectionTitle.ToTextModel(),
                    SubsectionTitle = ConfigData.Hobbies.Subsection2.SubsectionTitle.ToTextModel(),
                    SubsectionPictureCss = ConfigData.Hobbies.Subsection2.SubsectionCss,
                    SubsectionComment = ConfigData.Hobbies.Subsection2.SubsectionComment.ToTextModel(),
                    WrappedCardType = CardType.NavSectionHobbies,
                    Cards = ConfigData.Hobbies.Subsection2.Cards.Select(c => (CardModel)c).ToList(),
                },
                new SubsectionModel()
                {
                    Path = ConfigData.PathConfig.RootPath,
                    SectionTitle = ConfigData.Hobbies.SectionTitle.ToTextModel(),
                    SubsectionTitle = ConfigData.Hobbies.Subsection3.SubsectionTitle.ToTextModel(),
                    SubsectionPictureCss = ConfigData.Hobbies.Subsection3.SubsectionCss,
                    SubsectionComment = ConfigData.Hobbies.Subsection3.SubsectionComment.ToTextModel(),
                    WrappedCardType = CardType.NavSectionHobbies,
                    Cards = ConfigData.Hobbies.Subsection3.Cards.Select(c => (CardModel)c).ToList(),
                },
                new SubsectionModel()
                {
                    Path = ConfigData.PathConfig.RootPath,
                    SectionTitle = ConfigData.Hobbies.SectionTitle.ToTextModel(),
                    SubsectionTitle = ConfigData.Hobbies.Subsection4.SubsectionTitle.ToTextModel(),
                    SubsectionPictureCss = ConfigData.Hobbies.Subsection4.SubsectionCss,
                    SubsectionComment = ConfigData.Hobbies.Subsection4.SubsectionComment.ToTextModel(),
                    WrappedCardType = CardType.NavSectionHobbies,
                    Cards = ConfigData.Hobbies.Subsection4.Cards.Select(c => (CardModel)c).ToList(),
                },
                new SubsectionModel()
                {
                    Path = ConfigData.PathConfig.RootPath,
                    SectionTitle = ConfigData.Hobbies.SectionTitle.ToTextModel(),
                    SubsectionTitle = ConfigData.Hobbies.Subsection5.SubsectionTitle.ToTextModel(),
                    SubsectionPictureCss = ConfigData.Hobbies.Subsection5.SubsectionCss,
                    SubsectionComment = ConfigData.Hobbies.Subsection5.SubsectionComment.ToTextModel(),
                    WrappedCardType = CardType.NavSectionHobbies,
                    Cards = ConfigData.Hobbies.Subsection5.Cards.Select(c => (CardModel)c).ToList(),
                },
            ]
        };

        //Quotes
        SectionModel quotesModel = new SectionModel()
        {
            Path = ConfigData.PathConfig.RootPath,
            SectionTitle = ConfigData.Quotes.SectionTitle.ToTextModel(),
            SectionAnchor = ConfigData.Quotes.SectionAnchor,
            SectionPictureCss = ConfigData.Quotes.SectionCss,
            SectionComment = ConfigData.Quotes.SectionComment.ToTextModel(),
            WrappedChildType = ChildType.Card,
            WrappedCardType = CardType.NavSectionQuotes,
            Cards = ConfigData.Quotes.Cards.Select(c => (CardModel)c).ToList(),
        };

        //Friends
        SectionModel friendsModel = new SectionModel()
        {
            Path = ConfigData.PathConfig.RootPath,
            SectionTitle = ConfigData.Friends.SectionTitle.ToTextModel(),
            SectionAnchor = ConfigData.Friends.SectionAnchor,
            SectionPictureCss = ConfigData.Friends.SectionCss,
            SectionComment = ConfigData.Friends.SectionComment.ToTextModel(),
            WrappedChildType = ChildType.Card,
            WrappedCardType = CardType.NavSectionFriends,
            Cards = ConfigData.Friends.Cards.Select(c => (CardModel)c).ToList(),
        };

        ConfigRootLayoutModel = new LayoutModel()
        {
            Path = ConfigData.PathConfig.RootPath,
            Sections =
            [
                aboutMeModel,
                projectsModel,
                techStackModel,
                hobbiesModel,
                quotesModel,
                friendsModel,
            ]
        };

        SectionModel thankYouModel = new SectionModel()
        {
            Path = ConfigData.PathConfig.ThankYouPath,
            SectionTitle = ConfigData.ThankYou.SectionTitle.ToTextModel(),
            SectionAnchor = ConfigData.ThankYou.SectionAnchor,
            SectionPictureCss = ConfigData.ThankYou.SectionCss,
            SectionComment = ConfigData.ThankYou.SectionComment.ToTextModel(),
            WrappedChildType = ChildType.Card,
            WrappedCardType = CardType.NavSectionThankYou,
            Cards = ConfigData.ThankYou.Cards.Select(c => (CardModel)c).ToList(),
        };
        ConfigThankYouLayoutModel = new LayoutModel()
        {
            Path = ConfigData.PathConfig.ThankYouPath,
            Sections = [thankYouModel]
        };

        ConfigFootModel = new FootModel()
        {
            Section =
            [
                new LinkSectionModel()
                {
                    Type = ConfigData.Footer.Links.Type,
                    SectionTitle = ConfigData.Footer.Links.SectionTitle.ToTextModel(),
                    ItemList = ConfigData.Footer.Links.Cards,
                },
                new LinkSectionModel()
                {
                    Type = ConfigData.Footer.Record.Type,
                    SectionTitle = ConfigData.Footer.Record.SectionTitle.ToTextModel(),
                    ItemList = ConfigData.Footer.Record.Cards,
                },
                new LinkSectionModel()
                {
                    Type = ConfigData.Footer.Follow.Type,
                    SectionTitle = ConfigData.Footer.Follow.SectionTitle.ToTextModel(),
                    ItemList = ConfigData.Footer.Follow.Cards,
                }
            ],
            FootComment = new FootCommentModel()
            {
                Link = ConfigData.Footer.FootComment.Link,
                FootComment = ConfigData.Footer.FootComment.Text.ToTextModel(),
            }
        };
        ConfigRootLayoutModel = ConfigRootLayoutModel.LayoutIdConfig();
        ConfigThankYouLayoutModel = ConfigThankYouLayoutModel.LayoutIdConfig();
        ConfigNavModel = ConfigNavModel.NavIdConfig();
    }
}

public static class GuidHelper
{
    public static NavModel NavIdConfig(this NavModel navModel)
    {
        foreach (var navItem in navModel.NavItems)
        {
            navItem.Name.Id = Guid.NewGuid().ToString();
        }

        return navModel;
    }

    /// <summary>
    /// 为LayoutModel中所有元素设置guid
    /// </summary>
    /// <param name="layoutModel"></param>
    /// <returns></returns>
    public static LayoutModel LayoutIdConfig(this LayoutModel layoutModel)
    {
        foreach (var sectionModel in layoutModel.Sections)
        {
            var sectionGuid = Guid.NewGuid().ToString();

            sectionModel.SectionTitle.Id = sectionGuid;
            foreach (var cardModel in sectionModel.Cards ?? [])
            {
                cardModel.SubsectionTitle = null;
                cardModel.SectionTitle.Id = sectionGuid;
                cardModel.CardTitle.Id = Guid.NewGuid().ToString();
            }

            foreach (var subsectionModel in sectionModel.Subsections ?? [])
            {
                subsectionModel.SectionTitle.Id = sectionGuid;
                var subSectionGuid = Guid.NewGuid().ToString();
                subsectionModel.SubsectionTitle.Id = subSectionGuid;

                foreach (var cardModel in subsectionModel.Cards)
                {
                    cardModel.SubsectionTitle ??= new TextModel();
                    cardModel.SubsectionTitle.Id = subSectionGuid;
                    cardModel.CardTitle.Id = Guid.NewGuid().ToString();
                }
            }
        }

        return layoutModel;
    }

    public static FootModel FootIdConfig(this FootModel footModel)
    {
        foreach (var linkSectionModel in footModel.Section)
        {
            var sectionGuid = Guid.NewGuid().ToString();
            linkSectionModel.SectionTitle.Id = sectionGuid;
            foreach (var linkItem in linkSectionModel.ItemList)
            {
                linkItem.SectionTitle.Id = sectionGuid;
                linkItem.ItemTitle.Id = Guid.NewGuid().ToString();
            }
        }

        return footModel;
    }
}