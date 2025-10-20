using Grpc.Share.Protos.WwwModels.Content;
using Grpc.Share.Protos.WwwModels.Foot;
using Grpc.Share.Tools;
using Protobuf.Shared.Text;
using Protobuf.Www.Content;
using Protobuf.Www.Foot;

namespace Grpc.Share.Config.Www
{
    public static class ConfigData
    {
        public static class PathConfig
        {
            public const string RootPath = "/";
            public const string ThankYouPath = "/ThankYou";
        }

        public static class LangUtils
        {
            public static readonly Dictionary<LangType, string> MyLocation = new()
            {
                [LangType.ZhCn] = "我的位置",
                [LangType.ZhTw] = "我的位置",
                [LangType.ZhHk] = "我的位置",
                [LangType.EnUs] = "My Location",
                [LangType.EnGb] = "My Location",
                [LangType.JaJp] = "マイロケーション"
            };

            public static readonly Dictionary<LangType, string> SideNavTitle = new()
            {
                [LangType.ZhCn] = "页面导航",
                [LangType.ZhTw] = "頁面導航",
                [LangType.ZhHk] = "頁面導航",
                [LangType.EnUs] = "Page Navigation",
                [LangType.EnGb] = "Page Navigation",
                [LangType.JaJp] = "ページナビゲーション"
            };

            public static readonly Dictionary<LangType, string> CurrentLanguage = new()
            {
                [LangType.ZhCn] = "当前语言",
                [LangType.ZhTw] = "當前語言",
                [LangType.ZhHk] = "當前語言",
                [LangType.EnUs] = "Current Language",
                [LangType.EnGb] = "Current Language",
                [LangType.JaJp] = "現在の言語"
            };
        }

        #region HtmlHeader

        public static class HtmlHeader
        {
            public static readonly Dictionary<LangType, string> HtmlTitle = new()
            {
                [LangType.ZhCn] = "QuetzalSidera的个人博客",
                [LangType.ZhTw] = "QuetzalSidera的個人部落格",
                [LangType.ZhHk] = "QuetzalSidera的個人網誌",
                [LangType.EnUs] = "QuetzalSidera's Personal Blog",
                [LangType.EnGb] = "QuetzalSidera's Personal Blog",
                [LangType.JaJp] = "QuetzalSideraの個人ブログ"
            };

            public const string LogoPath = "img/HtmlHeader/Logo.png";
        }

        #endregion

        #region Header

        public static class Header
        {
            #region Title

            public static class Title
            {
                public const string Css = "fa fa-rss";

                public static readonly Dictionary<LangType, string> Text = new()
                {
                    [LangType.ZhCn] = "我的空间",
                    [LangType.ZhTw] = "我的空間",
                    [LangType.ZhHk] = "我的空間",
                    [LangType.EnUs] = "My Space",
                    [LangType.EnGb] = "My Space",
                    [LangType.JaJp] = "マイスペース"
                };
            }

            #endregion

            #region Weather

            public static class Weather
            {
                public const string LocationCss = "fas fa-location";
                public const string TempCss = "fas fa-thermometer-half";
                public const string HumidityCss = "fas fa-tint";
            }

            #endregion

            #region HeaderNav

            public static class HeaderNav
            {
                public const string GitHubCss = "fab fa-github";
                public const string GitHubLink = "https://github.com/QuetzalSidera/";

                public static readonly Dictionary<LangType, string> GitHub = new()
                {
                    [LangType.ZhCn] = "GitHub",
                    [LangType.ZhTw] = "GitHub",
                    [LangType.ZhHk] = "GitHub",
                    [LangType.EnUs] = "GitHub",
                    [LangType.EnGb] = "GitHub",
                    [LangType.JaJp] = "GitHub"
                };

                public const string AliceCss = "fas fa-robot";
                public const string AliceLink = "https://chat.quetzalsidera.me";

                public static readonly Dictionary<LangType, string> Alice = new()
                {
                    [LangType.ZhCn] = "爱丽丝",
                    [LangType.ZhTw] = "愛麗絲",
                    [LangType.ZhHk] = "愛麗絲",
                    [LangType.EnUs] = "Alice",
                    [LangType.EnGb] = "Alice",
                    [LangType.JaJp] = "アリス"
                };

                public const string OpenCss = "fa fa-share-nodes";
                public const string OpenLink = "https://open.quetzalsidera.me";

                public static readonly Dictionary<LangType, string> Open = new()
                {
                    [LangType.ZhCn] = "开放平台",
                    [LangType.ZhTw] = "開放平台",
                    [LangType.ZhHk] = "開放平台",
                    [LangType.EnUs] = "Open Platform",
                    [LangType.EnGb] = "Open Platform",
                    [LangType.JaJp] = "オープンプラットフォーム"
                };

                public const string ThankYouCss = ThankYou.SectionCss;
                public const string ThankYouLink = PathConfig.ThankYouPath;

                public static readonly Dictionary<LangType, string> ThankYouDict = new()
                {
                    [LangType.ZhCn] = "致谢",
                    [LangType.ZhTw] = "致謝",
                    [LangType.ZhHk] = "致謝",
                    [LangType.EnUs] = "Acknowledgments",
                    [LangType.EnGb] = "Acknowledgements",
                    [LangType.JaJp] = "謝辞"
                };
            }

            #endregion

            #region HeaderProfile

            public static class HeaderProfile
            {
                public const string HeaderPictureLink = "/img/Header/HeaderPict.PNG";

                public static readonly Dictionary<LangType, string> Name = new()
                {
                    [LangType.ZhCn] = "QuetzalSidera",
                    [LangType.ZhTw] = "QuetzalSidera",
                    [LangType.ZhHk] = "QuetzalSidera",
                    [LangType.EnUs] = "QuetzalSidera",
                    [LangType.EnGb] = "QuetzalSidera",
                    [LangType.JaJp] = "QuetzalSidera"
                };

                public static readonly Dictionary<LangType, string> Summary = new()
                {
                    [LangType.ZhCn] = "机器人 | 计科 | 二次元",
                    [LangType.ZhTw] = "機器人 | 資工 | 二次元",
                    [LangType.ZhHk] = "機械人 | 電腦科學 | 二次元",
                    [LangType.EnUs] = "Robotics | Computer Science | Anime",
                    [LangType.EnGb] = "Robotics | Computer Science | Anime",
                    [LangType.JaJp] = "ロボット | コンピュータ科学 | 二次元"
                };

                public static readonly Dictionary<LangType, string> Description = new()
                {
                    [LangType.ZhCn] = "欢迎来到我的个人博客，我在这里已经等了你很~久，很~~~久了",
                    [LangType.ZhTw] = "歡迎來到我的個人部落格，我在這裡已經等了你很~久，很~~~久了",
                    [LangType.ZhHk] = "歡迎來到我的個人網誌，我在這裡已經等了你很~久，很~~~久了",
                    [LangType.EnUs] = "Welcome to my personal blog, I've been waiting for you for so~ long, so~~~ long",
                    [LangType.EnGb] = "Welcome to my personal blog, I've been waiting for you for so~ long, so~~~ long",
                    [LangType.JaJp] = "私の個人ブログへようこそ、私はここであなたをとても~長く、とても~~~長く待っていました"
                };
            }

            #endregion
        }

        #endregion

        #region AboutMe

        public static class AboutMe
        {
            public const string SectionCss = "fas fa-user";

            public const string SectionAnchor = "AboutMe";

            public static readonly Dictionary<LangType, string> SectionTitle = new()
            {
                [LangType.ZhCn] = "关于我",
                [LangType.ZhTw] = "關於我",
                [LangType.ZhHk] = "關於我",
                [LangType.EnUs] = "About Me",
                [LangType.EnGb] = "About Me",
                [LangType.JaJp] = "私について"
            };

            public static readonly Dictionary<LangType, string> SectionComment = new()
            {
                [LangType.ZhCn] = "23级本科生，主修机器人工程，辅修工程力学，喜欢计科，在机械、力学与代码三重世界之间游走",
                [LangType.ZhTw] = "23級本科生，主修機器人工程，輔修工程力學，喜歡資工，在機械、力學與程式碼三重世界之間遊走",
                [LangType.ZhHk] = "23級本科生，主修機械人工程，輔修工程力學，喜歡電腦科學，在機械、力學與代碼三重世界之間遊走",
                [LangType.EnUs] =
                    "Undergraduate class of 2023, majoring in Robotics Engineering, minoring in Engineering Mechanics, passionate about Computer Science, navigating between the triple worlds of mechanics, dynamics, and code",
                [LangType.EnGb] =
                    "Undergraduate class of 2023, majoring in Robotics Engineering, minoring in Engineering Mechanics, passionate about Computer Science, navigating between the triple worlds of mechanics, dynamics, and code",
                [LangType.JaJp] = "2023年度本科生、ロボット工学を専攻、工程力学を副専攻、計算機科学が好き、機械、力学とコードの三重世界の間を遊走中"
            };

            #region Cards

            public static class Card1
            {
                public static readonly Dictionary<LangType, string> Description1 = new()
                {
                    [LangType.ZhCn] = "你好！我是一名对机器人工程、力学与计算机科学都有着浓厚的兴趣的探索者。我相信在机械、力学与代码的交汇处，正蕴藏着解决未来挑战的钥匙。",
                    [LangType.ZhTw] = "你好！我是一名對機器人工程、力學與計算機科學都有著濃厚興趣的探索者。我相信在機械、力學與程式碼的交匯處，正蘊藏著解決未來挑戰的鑰匙。",
                    [LangType.ZhHk] = "你好！我是一名對機械人工程、力學與電腦科學都有著濃厚興趣的探索者。我相信在機械、力學與代碼的交匯處，正蘊藏著解決未來挑戰的鑰匙。",
                    [LangType.EnUs] =
                        "Hello! I am an explorer with a strong interest in robotics engineering, mechanics, and computer science. I believe that at the intersection of machinery, mechanics, and code lies the key to solving future challenges.",
                    [LangType.EnGb] =
                        "Hello! I am an explorer with a strong interest in robotics engineering, mechanics, and computer science. I believe that at the intersection of machinery, mechanics, and code lies the key to solving future challenges.",
                    [LangType.JaJp] = "こんにちは！私はロボット工学、力学、コンピュータ科学に強い関心を持つ探求者です。機械、力学、コードの交差点に未来の課題を解決する鍵が隠されていると信じています。"
                };

                public static readonly Dictionary<LangType, string> Description2 = new()
                {
                    [LangType.ZhCn] = "我也喜欢二次元，是一个东方厨，MC玩家，最近在玩蔚蓝档案(成分复杂belike)，喜欢骑行阅读音乐等等...",
                    [LangType.ZhTw] = "我也喜歡二次元，是一個東方廚，MC玩家，最近在玩蔚藍檔案(成分複雜belike)，喜歡騎行閱讀音樂等等...",
                    [LangType.ZhHk] = "我也喜歡二次元，是一個東方廚，MC玩家，最近在玩蔚藍檔案(成分複雜belike)，喜歡騎行閱讀音樂等等...",
                    [LangType.EnUs] =
                        "I also like anime, am a Touhou fan, Minecraft player, recently playing Blue Archive (complex interests belike), enjoy cycling, reading, music, etc...",
                    [LangType.EnGb] =
                        "I also like anime, am a Touhou fan, Minecraft player, recently playing Blue Archive (complex interests belike), enjoy cycling, reading, music, etc...",
                    [LangType.JaJp] = "二次元も好きで、東方厨、MCプレイヤー、最近はブルーアーカイブをプレイしています（興味が複雑belike）、サイクリング、読書、音楽なども好きです..."
                };

                public static readonly Dictionary<LangType, string> Description3 = new()
                {
                    [LangType.ZhCn] = "最近时间我正在学习计网/C#/ASP.NET/Unity相关知识，未来可能会接触Blender/Avalonia",
                    [LangType.ZhTw] = "最近時間我正在學習計網/C#/ASP.NET/Unity相關知識，未來可能會接觸Blender/Avalonia",
                    [LangType.ZhHk] = "最近時間我正在學習計網/C#/ASP.NET/Unity相關知識，未來可能會接觸Blender/Avalonia",
                    [LangType.EnUs] =
                        "Recently I've been learning about computer networks/C#/ASP.NET/Unity, and may explore Blender/Avalonia in the future",
                    [LangType.EnGb] =
                        "Recently I've been learning about computer networks/C#/ASP.NET/Unity, and may explore Blender/Avalonia in the future",
                    [LangType.JaJp] = "最近はコンピュータネットワーク/C#/ASP.NET/Unity関連の知識を学んでおり、将来的にはBlender/Avaloniaにも触れるかもしれません"
                };
            }

            #endregion
        }

        #endregion

        #region Projects

        public static class Projects
        {
            public const string SectionCss = "fas fa-code";
            public const string SectionAnchor = "PersonalProjects";

            public static readonly Dictionary<LangType, string> SectionTitle = new()
            {
                [LangType.ZhCn] = "个人项目",
                [LangType.ZhTw] = "個人項目",
                [LangType.ZhHk] = "個人項目",
                [LangType.EnUs] = "Personal Projects",
                [LangType.EnGb] = "Personal Projects",
                [LangType.JaJp] = "個人プロジェクト"
            };

            public static readonly Dictionary<LangType, string> SectionComment = new()
            {
                [LangType.ZhCn] = "记录构建项目的足迹，从构思到落地。",
                [LangType.ZhTw] = "記錄構建項目的足跡，從構思到落地。",
                [LangType.ZhHk] = "記錄構建項目的足跡，從構思到落地。",
                [LangType.EnUs] = "Documenting the journey of building projects, from conception to implementation.",
                [LangType.EnGb] = "Documenting the journey of building projects, from conception to implementation.",
                [LangType.JaJp] = "プロジェクト構築の旅を記録、構想から実現まで。"
            };

            #region Cards

            public static List<ProjectsCardModel> Cards =
            [
                new()
                {
                    CompleteStatus = CompleteStatus.OnGoing,
                    CardPictureCss = "fas fa-rocket",
                    CardTitle = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "C# Bilibili前端SDK",
                        [LangType.ZhTw] = "C# Bilibili前端SDK",
                        [LangType.ZhHk] = "C# Bilibili前端SDK",
                        [LangType.EnUs] = "C# Bilibili Frontend SDK",
                        [LangType.EnGb] = "C# Bilibili Frontend SDK",
                        [LangType.JaJp] = "C# BilibiliフロントエンドSDK"
                    }.ToTextModel(),
                    CardContent = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "C#与.NET上的Bilibili前端SDK，参照第三方API文档编写，支持主站/开放平台",
                        [LangType.ZhTw] = "C#與.NET上的Bilibili前端SDK，參照第三方API文檔編寫，支援主站/開放平台",
                        [LangType.ZhHk] = "C#與.NET上的Bilibili前端SDK，參照第三方API文檔編寫，支援主站/開放平台",
                        [LangType.EnUs] =
                            "Bilibili frontend SDK on C# and .NET, written with reference to third-party API documentation, supporting main site/open platform",
                        [LangType.EnGb] =
                            "Bilibili frontend SDK on C# and .NET, written with reference to third-party API documentation, supporting main site/open platform",
                        [LangType.JaJp] = "C#と.NET上のBilibiliフロントエンドSDK、サードパーティAPIドキュメントを参照して作成、メインサイト/オープンプラットフォームをサポート"
                    }.ToTextModel(),
                    CardComment = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "C#, RESTful API, gRPC, HTTP协议",
                        [LangType.ZhTw] = "C#, RESTful API, gRPC, HTTP協定",
                        [LangType.ZhHk] = "C#, RESTful API, gRPC, HTTP協議",
                        [LangType.EnUs] = "C#, RESTful API, gRPC, HTTP Protocol",
                        [LangType.EnGb] = "C#, RESTful API, gRPC, HTTP Protocol",
                        [LangType.JaJp] = "C#, RESTful API, gRPC, HTTPプロトコル"
                    }.ToTextModel(),
                    CardLink = "https://github.com/QuetzalSidera/.NET-BilibiliSDK",
                },
                new()
                {
                    CompleteStatus = CompleteStatus.Planning,
                    CardPictureCss = "fas fa-dice",
                    CardTitle = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "2d横版肉鸽游戏",
                        [LangType.ZhTw] = "2D橫版肉鴿遊戲",
                        [LangType.ZhHk] = "2D橫版肉鴿遊戲",
                        [LangType.EnUs] = "2D Side-scrolling Roguelike Game",
                        [LangType.EnGb] = "2D Side-scrolling Roguelike Game",
                        [LangType.JaJp] = "2D横スクロールローグライクゲーム"
                    }.ToTextModel(),
                    CardContent = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "正在进行中的社团项目，为GameJam预热的Unity团队游戏",
                        [LangType.ZhTw] = "正在進行中的社團項目，為GameJam預熱的Unity團隊遊戲",
                        [LangType.ZhHk] = "正在進行中的社團項目，為GameJam預熱的Unity團隊遊戲",
                        [LangType.EnUs] = "Ongoing club project, Unity team game as warm-up for GameJam",
                        [LangType.EnGb] = "Ongoing club project, Unity team game as warm-up for GameJam",
                        [LangType.JaJp] = "進行中のクラブプロジェクト、GameJamに向けたウォームアップのUnityチームゲーム"
                    }.ToTextModel(),
                    CardComment = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "C#, Unity",
                        [LangType.ZhTw] = "C#, Unity",
                        [LangType.ZhHk] = "C#, Unity",
                        [LangType.EnUs] = "C#, Unity",
                        [LangType.EnGb] = "C#, Unity",
                        [LangType.JaJp] = "C#, Unity"
                    }.ToTextModel(),
                    CardLink = "", // 无Link
                },
                new()
                {
                    CompleteStatus = CompleteStatus.Dreaming,
                    CardPictureCss = "fas fa-tree",
                    CardTitle = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "独立游戏《树下心生》",
                        [LangType.ZhTw] = "獨立遊戲《樹下心生》",
                        [LangType.ZhHk] = "獨立遊戲《樹下心生》",
                        [LangType.EnUs] = "Independent Game 'A Seed's Dream'",
                        [LangType.EnGb] = "Independent Game 'A Seed's Dream'",
                        [LangType.JaJp] = "インディーゲーム『樹の下の心』"
                    }.ToTextModel(),
                    CardContent = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "讲述主人公走出心理低谷的科普/情感/治愈向独立游戏",
                        [LangType.ZhTw] = "講述主人公走出心理低谷的科普/情感/治愈向獨立遊戲",
                        [LangType.ZhHk] = "講述主人公走出心理低谷的科普/情感/治愈向獨立遊戲",
                        [LangType.EnUs] =
                            "Science/emotional/healing independent game about the protagonist overcoming psychological low ebb",
                        [LangType.EnGb] =
                            "Science/emotional/healing independent game about the protagonist overcoming psychological low ebb",
                        [LangType.JaJp] = "主人公が心理的な低谷から抜け出す科学/感情/癒し系インディーゲーム"
                    }.ToTextModel(),
                    CardComment = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "C#, Unity, Blender, 美术与作曲, 神经生物学与心理学",
                        [LangType.ZhTw] = "C#, Unity, Blender, 美術與作曲, 神經生物學與心理學",
                        [LangType.ZhHk] = "C#, Unity, Blender, 美術與作曲, 神經生物學與心理學",
                        [LangType.EnUs] = "C#, Unity, Blender, Art and Music Composition, Neurobiology and Psychology",
                        [LangType.EnGb] = "C#, Unity, Blender, Art and Music Composition, Neurobiology and Psychology",
                        [LangType.JaJp] = "C#, Unity, Blender, 美術と作曲, 神経生物学と心理学"
                    }.ToTextModel(),
                    CardLink = "", // 无Link
                },
                new()
                {
                    CompleteStatus = CompleteStatus.Finished,
                    CardPictureCss = "fas fa-robot",
                    CardTitle = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "QQ机器人",
                        [LangType.ZhTw] = "QQ機器人",
                        [LangType.ZhHk] = "QQ機械人",
                        [LangType.EnUs] = "QQ Bot",
                        [LangType.EnGb] = "QQ Bot",
                        [LangType.JaJp] = "QQボット"
                    }.ToTextModel(),
                    CardContent = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "基于NapCat框架的，用C#编写逻辑的非官方QQ机器人，接入DeepSeek API",
                        [LangType.ZhTw] = "基於NapCat框架的，用C#編寫邏輯的非官方QQ機器人，接入DeepSeek API",
                        [LangType.ZhHk] = "基於NapCat框架的，用C#編寫邏輯的非官方QQ機械人，接入DeepSeek API",
                        [LangType.EnUs] =
                            "Unofficial QQ bot based on NapCat framework, logic written in C#, integrated with DeepSeek API",
                        [LangType.EnGb] =
                            "Unofficial QQ bot based on NapCat framework, logic written in C#, integrated with DeepSeek API",
                        [LangType.JaJp] = "NapCatフレームワークに基づく、C#でロジックを書いた非公式QQボット、DeepSeek APIを統合"
                    }.ToTextModel(),
                    CardComment = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "C#, 计算机网络, NapCat",
                        [LangType.ZhTw] = "C#, 計算機網絡, NapCat",
                        [LangType.ZhHk] = "C#, 電腦網絡, NapCat",
                        [LangType.EnUs] = "C#, Computer Networks, NapCat",
                        [LangType.EnGb] = "C#, Computer Networks, NapCat",
                        [LangType.JaJp] = "C#, コンピュータネットワーク, NapCat"
                    }.ToTextModel(),
                    CardLink = "https://github.com/QuetzalSidera/QQBot"
                },
                new()
                {
                    CompleteStatus = CompleteStatus.Planning,
                    CardPictureCss = "fas fa-desktop",
                    CardTitle = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "Bilibili跨平台第三方客户端",
                        [LangType.ZhTw] = "Bilibili跨平台第三方客戶端",
                        [LangType.ZhHk] = "Bilibili跨平台第三方客戶端",
                        [LangType.EnUs] = "Bilibili Cross-platform Third-party Client",
                        [LangType.EnGb] = "Bilibili Cross-platform Third-party Client",
                        [LangType.JaJp] = "Bilibiliクロスプラットフォームサードパーティクライアント"
                    }.ToTextModel(),
                    CardContent = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "C#.NET与Avalonia构建的Bilibili跨平台第三方客户端，使用Bilibili前端SDK编写",
                        [LangType.ZhTw] = "C#.NET與Avalonia構建的Bilibili跨平台第三方客戶端，使用Bilibili前端SDK編寫",
                        [LangType.ZhHk] = "C#.NET與Avalonia構建的Bilibili跨平台第三方客戶端，使用Bilibili前端SDK編寫",
                        [LangType.EnUs] =
                            "Bilibili cross-platform third-party client built with C#.NET and Avalonia, using Bilibili frontend SDK",
                        [LangType.EnGb] =
                            "Bilibili cross-platform third-party client built with C#.NET and Avalonia, using Bilibili frontend SDK",
                        [LangType.JaJp] = "C#.NETとAvaloniaで構築されたBilibiliクロスプラットフォームサードパーティクライアント、BilibiliフロントエンドSDKを使用"
                    }.ToTextModel(),
                    CardComment = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "C# ,Avalonia, .NET",
                        [LangType.ZhTw] = "C# ,Avalonia, .NET",
                        [LangType.ZhHk] = "C# ,Avalonia, .NET",
                        [LangType.EnUs] = "C#, Avalonia, .NET",
                        [LangType.EnGb] = "C#, Avalonia, .NET",
                        [LangType.JaJp] = "C#, Avalonia, .NET"
                    }.ToTextModel(),
                    CardLink = ""// 无Link
                },
                new()
                {
                    CompleteStatus = CompleteStatus.OnGoing,
                    CardPictureCss = "fas fa-blog",
                    CardTitle = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "个人博客网页",
                        [LangType.ZhTw] = "個人部落格網頁",
                        [LangType.ZhHk] = "個人網誌網頁",
                        [LangType.EnUs] = "Personal Blog Website",
                        [LangType.EnGb] = "Personal Blog Website",
                        [LangType.JaJp] = "個人ブログウェブサイト"
                    }.ToTextModel(),
                    CardContent = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "你正在浏览的这个网页",
                        [LangType.ZhTw] = "你正在瀏覽的這個網頁",
                        [LangType.ZhHk] = "你正在瀏覽的這個網頁",
                        [LangType.EnUs] = "The webpage you are currently browsing",
                        [LangType.EnGb] = "The webpage you are currently browsing",
                        [LangType.JaJp] = "あなたが今閲覧しているウェブページ"
                    }.ToTextModel(),
                    CardComment = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] =
                            "C#, ASP.NET Core, EF Core(少量), SQLite(少量), Docker, gRPC, RESTful API, Linux(少量), Blazor, 计算机网络, HTML/CSS",
                        [LangType.ZhTw] =
                            "C#, ASP.NET Core, EF Core(少量), SQLite(少量), Docker, gRPC, RESTful API, Linux(少量), Blazor, 計算機網絡, HTML/CSS",
                        [LangType.ZhHk] =
                            "C#, ASP.NET Core, EF Core(少量), SQLite(少量), Docker, gRPC, RESTful API, Linux(少量), Blazor, 電腦網絡, HTML/CSS",
                        [LangType.EnUs] =
                            "C#, ASP.NET Core, EF Core (basic), SQLite (basic), Docker, gRPC, RESTful API, Linux (basic), Blazor, Computer Networks, HTML/CSS",
                        [LangType.EnGb] =
                            "C#, ASP.NET Core, EF Core (basic), SQLite (basic), Docker, gRPC, RESTful API, Linux (basic), Blazor, Computer Networks, HTML/CSS",
                        [LangType.JaJp] =
                            "C#, ASP.NET Core, EF Core(基礎), SQLite(基礎), Docker, gRPC, RESTful API, Linux(基礎), Blazor, コンピュータネットワーク, HTML/CSS"
                    }.ToTextModel(),
                    CardLink = "https://github.com/QuetzalSidera/QuetzalSidera-Me-FrontBackend"
                },
                new()
                {
                    CompleteStatus = CompleteStatus.Finished,
                    CardPictureCss = "fas fa-spider",
                    CardTitle = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "Bilibili视频爬虫",
                        [LangType.ZhTw] = "Bilibili影片爬蟲",
                        [LangType.ZhHk] = "Bilibili影片爬蟲",
                        [LangType.EnUs] = "Bilibili Video Crawler",
                        [LangType.EnGb] = "Bilibili Video Crawler",
                        [LangType.JaJp] = "Bilibiliビデオクローラー"
                    }.ToTextModel(),
                    CardContent = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "Python构建的，支持所有分区/合集/分集/大会员番剧/音乐爬取的Bilibili爬虫",
                        [LangType.ZhTw] = "Python構建的，支援所有分區/合集/分集/大會員番劇/音樂爬取的Bilibili爬蟲",
                        [LangType.ZhHk] = "Python構建的，支援所有分區/合集/分集/大會員番劇/音樂爬取的Bilibili爬蟲",
                        [LangType.EnUs] =
                            "Python-built Bilibili crawler supporting all zones/collections/episodes/premium anime/music crawling",
                        [LangType.EnGb] =
                            "Python-built Bilibili crawler supporting all zones/collections/episodes/premium anime/music crawling",
                        [LangType.JaJp] = "Pythonで構築されたBilibiliクローラー、すべてのゾーン/コレクション/エピソード/プレミアムアニメ/音楽のクロールをサポート"
                    }.ToTextModel(),
                    CardComment = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "Python Request, Html, Xml, XPath与正则表达式, ffmpeg应用(少量), HTTP协议(少量)",
                        [LangType.ZhTw] = "Python Request, Html, Xml, XPath與正則表達式, ffmpeg應用(少量), HTTP協定(少量)",
                        [LangType.ZhHk] = "Python Request, Html, Xml, XPath與正則表達式, ffmpeg應用(少量), HTTP協議(少量)",
                        [LangType.EnUs] =
                            "Python Request, Html, Xml, XPath and Regular Expressions, FFmpeg Application (basic), HTTP Protocol (basic)",
                        [LangType.EnGb] =
                            "Python Request, Html, Xml, XPath and Regular Expressions, FFmpeg Application (basic), HTTP Protocol (basic)",
                        [LangType.JaJp] = "Python Request, Html, Xml, XPathと正規表現, FFmpegアプリケーション(基礎), HTTPプロトコル(基礎)"
                    }.ToTextModel(),
                    CardLink = "https://github.com/QuetzalSidera/bilibili_project"
                },
                new()
                {
                    CompleteStatus = CompleteStatus.OnGoing,
                    CardPictureCss = "fas fa-comments",
                    CardTitle = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "和爱丽丝一起聊天",
                        [LangType.ZhTw] = "和愛麗絲一起聊天",
                        [LangType.ZhHk] = "和愛麗絲一起聊天",
                        [LangType.EnUs] = "Chat with Alice",
                        [LangType.EnGb] = "Chat with Alice",
                        [LangType.JaJp] = "アリスとチャット"
                    }.ToTextModel(),
                    CardContent = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "接入DeepSeek API的网页端AI对话项目，人设是来自《蔚蓝档案》游戏开发部的机器人爱丽丝",
                        [LangType.ZhTw] = "接入DeepSeek API的網頁端AI對話項目，人設是來自《蔚藍檔案》遊戲開發部的機器人愛麗絲",
                        [LangType.ZhHk] = "接入DeepSeek API的網頁端AI對話項目，人設是來自《蔚藍檔案》遊戲開發部的機械人愛麗絲",
                        [LangType.EnUs] =
                            "Web-based AI conversation project integrated with DeepSeek API, character is Alice the robot from Blue Archive Game Development Department",
                        [LangType.EnGb] =
                            "Web-based AI conversation project integrated with DeepSeek API, character is Alice the robot from Blue Archive Game Development Department",
                        [LangType.JaJp] = "DeepSeek APIを統合したウェブベースのAI会話プロジェクト、キャラクターは『ブルーアーカイブ』ゲーム開発部のロボットアリス"
                    }.ToTextModel(),
                    CardComment = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] =
                            "C#, ASP.NET Core, EF Core(少量), SQLite(少量), Docker, RESTful API, Linux(少量), Blazor, 计算机网络, HTML/CSS",
                        [LangType.ZhTw] =
                            "C#, ASP.NET Core, EF Core(少量), SQLite(少量), Docker, RESTful API, Linux(少量), Blazor, 計算機網絡, HTML/CSS",
                        [LangType.ZhHk] =
                            "C#, ASP.NET Core, EF Core(少量), SQLite(少量), Docker, RESTful API, Linux(少量), Blazor, 電腦網絡, HTML/CSS",
                        [LangType.EnUs] =
                            "C#, ASP.NET Core, EF Core (basic), SQLite (basic), Docker, RESTful API, Linux (basic), Blazor, Computer Networks, HTML/CSS",
                        [LangType.EnGb] =
                            "C#, ASP.NET Core, EF Core (basic), SQLite (basic), Docker, RESTful API, Linux (basic), Blazor, Computer Networks, HTML/CSS",
                        [LangType.JaJp] =
                            "C#, ASP.NET Core, EF Core(基礎), SQLite(基礎), Docker, RESTful API, Linux(基礎), Blazor, コンピュータネットワーク, HTML/CSS"
                    }.ToTextModel(),
                    CardLink = "https://chat.quetzalsidera.me/"
                },
            ];

            #endregion
        }

        #endregion

        #region TechStack

        public static class TechStack
        {
            public const string SectionCss = "fas fa-cogs";
            public const string SectionAnchor = "TechStack";

            public static readonly Dictionary<LangType, string> SectionTitle = new()
            {
                [LangType.ZhCn] = "技术栈",
                [LangType.ZhTw] = "技術棧",
                [LangType.ZhHk] = "技術棧",
                [LangType.EnUs] = "Tech Stack",
                [LangType.EnGb] = "Tech Stack",
                [LangType.JaJp] = "技術スタック"
            };

            public static readonly Dictionary<LangType, string> SectionComment = new()
            {
                [LangType.ZhCn] = "道路是曲折的，前途是光明的",
                [LangType.ZhTw] = "道路是曲折的，前途是光明的",
                [LangType.ZhHk] = "道路是曲折的，前途是光明的",
                [LangType.EnUs] = "The road is tortuous, the future is bright",
                [LangType.EnGb] = "The road is tortuous, the future is bright",
                [LangType.JaJp] = "道は曲がりくねっているが、未来は明るい"
            };

            #region Cards

            public static readonly List<TechStackCardModel> Cards =
            [
                new()
                {
                    CardTitle = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "C#",
                        [LangType.ZhTw] = "C#",
                        [LangType.ZhHk] = "C#",
                        [LangType.EnUs] = "C#",
                        [LangType.EnGb] = "C#",
                        [LangType.JaJp] = "C#",
                    }.ToTextModel(),
                    CardPictureCss = "fab fa-microsoft",
                    ManageStatus = ManageStatus.Expert,
                    CardLink = "https://dotnet.microsoft.com/zh-cn/languages/csharp"
                },
                new()
                {
                    CardTitle = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "计算机网络",
                        [LangType.ZhTw] = "計算機網絡",
                        [LangType.ZhHk] = "電腦網絡",
                        [LangType.EnUs] = "Computer Networks",
                        [LangType.EnGb] = "Computer Networks",
                        [LangType.JaJp] = "コンピュータネットワーク"
                    }.ToTextModel(),
                    CardPictureCss = "fas fa-network-wired",
                    ManageStatus = ManageStatus.Competent,
                    CardLink = "https://www.geeksforgeeks.org/aptitude/aptitude-questions-and-answers/"
                },
                new()
                {
                    CardTitle = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "C语言与嵌入式",
                        [LangType.ZhTw] = "C語言與嵌入式",
                        [LangType.ZhHk] = "C語言與嵌入式",
                        [LangType.EnUs] = "C Language and Embedded Systems",
                        [LangType.EnGb] = "C Language and Embedded Systems",
                        [LangType.JaJp] = "C言語と組み込みシステム"
                    }.ToTextModel(),
                    CardPictureCss = "fas fa-microchip",
                    ManageStatus = ManageStatus.Novice,
                    CardLink = "https://learn.microsoft.com/zh-cn/cpp/c-language/c-language-reference?view=msvc-170"
                },
                new()
                {
                    CardTitle = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "ASP.NET Core",
                        [LangType.ZhTw] = "ASP.NET Core",
                        [LangType.ZhHk] = "ASP.NET Core",
                        [LangType.EnUs] = "ASP.NET Core",
                        [LangType.EnGb] = "ASP.NET Core",
                        [LangType.JaJp] = "ASP.NET Core",
                    }.ToTextModel(),
                    CardPictureCss = "fab fa-windows",
                    ManageStatus = ManageStatus.Competent,
                    CardLink = "https://dotnet.microsoft.com/zh-cn/apps/aspnet"
                },
                new()
                {
                    CardTitle = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "EF Core",
                        [LangType.ZhTw] = "EF Core",
                        [LangType.ZhHk] = "EF Core",
                        [LangType.EnUs] = "EF Core",
                        [LangType.EnGb] = "EF Core",
                        [LangType.JaJp] = "EF Core",
                    }.ToTextModel(),
                    CardPictureCss = "fas fa-database",
                    ManageStatus = ManageStatus.Competent,
                    CardLink = "https://learn.microsoft.com/zh-cn/ef/core/"
                },
                new()
                {
                    CardTitle = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "Python",
                        [LangType.ZhTw] = "Python",
                        [LangType.ZhHk] = "Python",
                        [LangType.EnUs] = "Python",
                        [LangType.EnGb] = "Python",
                        [LangType.JaJp] = "Python",
                    }.ToTextModel(),
                    CardPictureCss = "fab fa-python",
                    ManageStatus = ManageStatus.Competent,
                    CardLink = "https://www.python.org/"
                },
                new()
                {
                    CardTitle = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "Unity",
                        [LangType.ZhTw] = "Unity",
                        [LangType.ZhHk] = "Unity",
                        [LangType.EnUs] = "Unity",
                        [LangType.EnGb] = "Unity",
                        [LangType.JaJp] = "Unity",
                    }.ToTextModel(),
                    CardPictureCss = "fas fa-gamepad",
                    ManageStatus = ManageStatus.Novice,
                    CardLink = "https://unity.com"
                },
                new()
                {
                    CardTitle = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "HTML/CSS/Javascript",
                        [LangType.ZhTw] = "HTML/CSS/Javascript",
                        [LangType.ZhHk] = "HTML/CSS/Javascript",
                        [LangType.EnUs] = "HTML/CSS/Javascript",
                        [LangType.EnGb] = "HTML/CSS/Javascript",
                        [LangType.JaJp] = "HTML/CSS/Javascript",
                    }.ToTextModel(),
                    CardPictureCss = "fab fa-html5",
                    ManageStatus = ManageStatus.Novice,
                    CardLink = "https://developer.mozilla.org/zh-CN/docs/Web"
                },
                new()
                {
                    CardTitle = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "C++",
                        [LangType.ZhTw] = "C++",
                        [LangType.ZhHk] = "C++",
                        [LangType.EnUs] = "C++",
                        [LangType.EnGb] = "C++",
                        [LangType.JaJp] = "C++",
                    }.ToTextModel(),
                    CardPictureCss = "fab fa-cuttlefish",
                    ManageStatus = ManageStatus.Competent,
                    CardLink = "https://isocpp.org/"
                },
                new()
                {
                    CardTitle = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "Docker",
                        [LangType.ZhTw] = "Docker",
                        [LangType.ZhHk] = "Docker",
                        [LangType.EnUs] = "Docker",
                        [LangType.EnGb] = "Docker",
                        [LangType.JaJp] = "Docker",
                    }.ToTextModel(),
                    CardPictureCss = "fab fa-docker",
                    ManageStatus = ManageStatus.Expert,
                    CardLink = "https://docs.docker.com"
                },
                new()
                {
                    CardTitle = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "Avalonia",
                        [LangType.ZhTw] = "Avalonia",
                        [LangType.ZhHk] = "Avalonia",
                        [LangType.EnUs] = "Avalonia",
                        [LangType.EnGb] = "Avalonia",
                        [LangType.JaJp] = "Avalonia",
                    }.ToTextModel(),
                    CardPictureCss = "fab fa-windows",
                    ManageStatus = ManageStatus.Planning,
                    CardLink = "https://docs.avaloniaui.net/zh-Hans/docs/welcome"
                },
                new()
                {
                    CardTitle = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "Blender/Fusion",
                        [LangType.ZhTw] = "Blender/Fusion",
                        [LangType.ZhHk] = "Blender/Fusion",
                        [LangType.EnUs] = "Blender/Fusion",
                        [LangType.EnGb] = "Blender/Fusion",
                        [LangType.JaJp] = "Blender/Fusion",
                    }.ToTextModel(),
                    CardPictureCss = "fa fa-cube",
                    ManageStatus = ManageStatus.Planning,
                    CardLink = "https://www.blender.org"
                },
            ];

            #endregion
        }

        #endregion

        #region Hobbies

        public static class Hobbies
        {
            public const string SectionCss = "fas fa-heart";
            public const string SectionAnchor = "CultureConstruction";

            public static readonly Dictionary<LangType, string> SectionTitle = new()
            {
                [LangType.ZhCn] = "文化建设",
                [LangType.ZhTw] = "文化建設",
                [LangType.ZhHk] = "文化建設",
                [LangType.EnUs] = "Culture Construction",
                [LangType.EnGb] = "Culture Construction",
                [LangType.JaJp] = "文化建設"
            };

            public static readonly Dictionary<LangType, string> SectionComment = new()
            {
                [LangType.ZhCn] = "这里是现实的缓冲带，也是心灵的栖身之所。记录所有曾照亮过我的，那些皎洁的月光。",
                [LangType.ZhTw] = "這裡是現實的緩衝帶，也是心靈的棲身之所。記錄所有曾照亮過我的，那些皎潔的月光。",
                [LangType.ZhHk] = "這裡是現實的緩衝帶，也是心靈的棲身之所。記錄所有曾照亮過我的，那些皎潔的月光。",
                [LangType.EnUs] =
                    "This is the buffer zone of reality and the sanctuary of the soul. Recording all the bright moonlight that has ever illuminated me.",
                [LangType.EnGb] =
                    "This is the buffer zone of reality and the sanctuary of the soul. Recording all the bright moonlight that has ever illuminated me.",
                [LangType.JaJp] = "ここは現実の緩衝地帯であり、心の安住の地でもあります。私を照らしてくれたすべての清らかな月光を記録します。"
            };

            #region Subsections

            public static class Subsection1
            {
                public static readonly Dictionary<LangType, string> SubsectionTitle = new()
                {
                    [LangType.ZhCn] = "游戏",
                    [LangType.ZhTw] = "遊戲",
                    [LangType.ZhHk] = "遊戲",
                    [LangType.EnUs] = "Games",
                    [LangType.EnGb] = "Games",
                    [LangType.JaJp] = "ゲーム"
                };

                public static readonly Dictionary<LangType, string> SubsectionComment = new()
                {
                    [LangType.ZhCn] = "成分复杂",
                    [LangType.ZhTw] = "成分複雜",
                    [LangType.ZhHk] = "成分複雜",
                    [LangType.EnUs] = "Complex composition",
                    [LangType.EnGb] = "Complex composition",
                    [LangType.JaJp] = "成分が複雑"
                };

                public const string SubsectionCss = "fas fa-gamepad";

                #region Cards

                public static readonly List<HobbiesCardModel> Cards =
                [
                    new()
                    {
                        CardTitle = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "蔚蓝档案",
                            [LangType.ZhTw] = "蔚藍檔案",
                            [LangType.ZhHk] = "蔚藍檔案",
                            [LangType.EnUs] = "Blue Archive",
                            [LangType.EnGb] = "Blue Archive",
                            [LangType.JaJp] = "ブルーアーカイブ"
                        }.ToTextModel(),
                        CardPictureUrl = "img/Hobbies/Games/蔚蓝档案.jpeg",
                        CardComment = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "一切奇迹的起点",
                            [LangType.ZhTw] = "一切奇蹟的起點",
                            [LangType.ZhHk] = "一切奇蹟的起點",
                            [LangType.EnUs] = "The starting point of all miracles",
                            [LangType.EnGb] = "The starting point of all miracles",
                            [LangType.JaJp] = "あまねく奇跡の始発点"
                        }.ToTextModel(),
                        CardPictureCss = "fa fa-paper-plane",
                        CardLink = "https://bluearchive-cn.com"
                    },
                    new()
                    {
                        CardTitle = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "我的世界",
                            [LangType.ZhTw] = "我的世界",
                            [LangType.ZhHk] = "我的世界",
                            [LangType.EnUs] = "Minecraft",
                            [LangType.EnGb] = "Minecraft",
                            [LangType.JaJp] = "マインクラフト"
                        }.ToTextModel(),
                        CardPictureUrl = "img/Hobbies/Games/我的世界.jpg",
                        CardComment = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "无限可能的沙盘",
                            [LangType.ZhTw] = "無限可能的沙盤",
                            [LangType.ZhHk] = "無限可能的沙盤",
                            [LangType.EnUs] = "Infinite Possibilities Sandbox",
                            [LangType.EnGb] = "Infinite Possibilities Sandbox",
                            [LangType.JaJp] = "無限の可能性のサンドボックス"
                        }.ToTextModel(),
                        CardPictureCss = "fa fa-cube",
                        CardLink = "https://www.minecraft.net/zh-hans"
                    },
                    new()
                    {
                        CardTitle = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "东方夜雀食堂",
                            [LangType.ZhTw] = "東方夜雀食堂",
                            [LangType.ZhHk] = "東方夜雀食堂",
                            [LangType.EnUs] = "Touhou Mystia's Izakaya",
                            [LangType.EnGb] = "Touhou Mystia's Izakaya",
                            [LangType.JaJp] = "東方夜雀食堂"
                        }.ToTextModel(),
                        CardPictureUrl = "img/Hobbies/Games/东方夜雀食堂.PNG",
                        CardComment = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "比菜更好吃的当然是小碎骨啦～",
                            [LangType.ZhTw] = "比菜更好吃的當然是小碎骨啦～",
                            [LangType.ZhHk] = "比菜更好吃的當然是小碎骨啦～",
                            [LangType.EnUs] = "Of course, what's better than the dishes is little Mystia～",
                            [LangType.EnGb] = "Of course, what's better than the dishes is little Mystia～",
                            [LangType.JaJp] = "料理より美味しいのはもちろんミスティアちゃんです～"
                        }.ToTextModel(),
                        CardPictureCss = "fa fa-wine-bottle",
                        CardLink = "https://store.steampowered.com/app/1584090/__Touhou_Mystias_Izakaya/?l=schinese"
                    },
                ];

                #endregion
            }

            public static class Subsection2
            {
                public static readonly Dictionary<LangType, string> SubsectionTitle = new()
                {
                    [LangType.ZhCn] = "书籍",
                    [LangType.ZhTw] = "書籍",
                    [LangType.ZhHk] = "書籍",
                    [LangType.EnUs] = "Books",
                    [LangType.EnGb] = "Books",
                    [LangType.JaJp] = "書籍"
                };

                public static readonly Dictionary<LangType, string> SubsectionComment = new()
                {
                    [LangType.ZhCn] = "历史上少数人的被保存下来的作品是人类最宝贵的财产",
                    [LangType.ZhTw] = "歷史上少數人的被保存下來的作品是人類最寶貴的財產",
                    [LangType.ZhHk] = "歷史上少數人的被保存下來的作品是人類最寶貴的財產",
                    [LangType.EnUs] =
                        "The preserved works of a few people in history are humanity's most precious property",
                    [LangType.EnGb] =
                        "The preserved works of a few people in history are humanity's most precious property",
                    [LangType.JaJp] = "歴史上の少数の人々によって保存された作品は、人類の最も貴重な財産です"
                };

                public const string SubsectionCss = "fas fa-book";

                #region Cards

                public static readonly List<HobbiesCardModel> Cards =
                [
                    new()
                    {
                        CardTitle = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "精神分析引论",
                            [LangType.ZhTw] = "精神分析引論",
                            [LangType.ZhHk] = "精神分析引論",
                            [LangType.EnUs] = "Introductory Lectures on Psychoanalysis",
                            [LangType.EnGb] = "Introductory Lectures on Psychoanalysis",
                            [LangType.JaJp] = "精神分析入門講義"
                        }.ToTextModel(),
                        CardPictureUrl = "img/Hobbies/Books/精神分析引论.jpeg",
                        CardComment = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "动力心理学的开山之作",
                            [LangType.ZhTw] = "動力心理學的開山之作",
                            [LangType.ZhHk] = "動力心理學的開山之作",
                            [LangType.EnUs] = "The pioneering work of dynamic psychology",
                            [LangType.EnGb] = "The pioneering work of dynamic psychology",
                            [LangType.JaJp] = "力動心理学の先駆的作品"
                        }.ToTextModel(),
                        CardPictureCss = "fa fa-graduation-cap",
                        CardLink = "https://weread.qq.com/web/reader/6d332150727c90136d3799b"
                    },
                    new()
                    {
                        CardTitle = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "发现的乐趣",
                            [LangType.ZhTw] = "發現的樂趣",
                            [LangType.ZhHk] = "發現的樂趣",
                            [LangType.EnUs] = "The Pleasure of Finding Things Out",
                            [LangType.EnGb] = "The Pleasure of Finding Things Out",
                            [LangType.JaJp] = "発見の喜び"
                        }.ToTextModel(),
                        CardPictureUrl = "img/Hobbies/Books/发现的乐趣.png",
                        CardComment = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "发现优秀的学者背后的精神内核",
                            [LangType.ZhTw] = "發現優秀的學者背後的精神內核",
                            [LangType.ZhHk] = "發現優秀的學者背後的精神內核",
                            [LangType.EnUs] = "Discovering the spiritual core behind excellent scholars",
                            [LangType.EnGb] = "Discovering the spiritual core behind excellent scholars",
                            [LangType.JaJp] = "優秀な学者の背後にある精神的核心を発見する"
                        }.ToTextModel(),
                        CardPictureCss = "fa fa-scroll",
                        CardLink = "https://weread.qq.com/web/reader/7af32ba05e01507af3447c7"
                    },
                    new()
                    {
                        CardTitle = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "逃避自由",
                            [LangType.ZhTw] = "逃避自由",
                            [LangType.ZhHk] = "逃避自由",
                            [LangType.EnUs] = "Escape from Freedom",
                            [LangType.EnGb] = "Escape from Freedom",
                            [LangType.JaJp] = "自由からの逃走"
                        }.ToTextModel(),
                        CardPictureUrl = "img/Hobbies/Books/逃避自由.jpeg",
                        CardComment = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "个人心理如何与社会文化相互作用",
                            [LangType.ZhTw] = "個人心理如何與社會文化相互作用",
                            [LangType.ZhHk] = "個人心理如何與社會文化相互作用",
                            [LangType.EnUs] = "How individual psychology interacts with social culture",
                            [LangType.EnGb] = "How individual psychology interacts with social culture",
                            [LangType.JaJp] = "個人の心理が社会文化とどのように相互作用するか"
                        }.ToTextModel(),
                        CardPictureCss = "fa fa-search",
                        CardLink = "https://weread.qq.com/web/bookDetail/f70322c0811e33942g014cf5"
                    },
                    new()
                    {
                        CardTitle = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "爱的艺术",
                            [LangType.ZhTw] = "愛的藝術",
                            [LangType.ZhHk] = "愛的藝術",
                            [LangType.EnUs] = "The Art of Loving",
                            [LangType.EnGb] = "The Art of Loving",
                            [LangType.JaJp] = "愛するということ"
                        }.ToTextModel(),
                        CardPictureUrl = "img/Hobbies/Books/爱的艺术.jpg",
                        CardComment = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "爱是最小单位的共产主义",
                            [LangType.ZhTw] = "愛是最小單位的共產主義",
                            [LangType.ZhHk] = "愛是最小單位的共產主義",
                            [LangType.EnUs] = "Love is the smallest unit of communism",
                            [LangType.EnGb] = "Love is the smallest unit of communism",
                            [LangType.JaJp] = "愛は最小単位の共産主義"
                        }.ToTextModel(),
                        CardPictureCss = "fa fa-heart",
                        CardLink = "https://weread.qq.com/web/reader/d7d32d70722dd429d7d723d"
                    },
                    new()
                    {
                        CardTitle = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "娱乐至死",
                            [LangType.ZhTw] = "娛樂至死",
                            [LangType.ZhHk] = "娛樂至死",
                            [LangType.EnUs] = "Amusing Ourselves to Death",
                            [LangType.EnGb] = "Amusing Ourselves to Death",
                            [LangType.JaJp] = "楽しんで死ぬ"
                        }.ToTextModel(),
                        CardPictureUrl = "img/Hobbies/Books/娱乐至死.jpeg",
                        CardComment = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "别骂了இдஇ",
                            [LangType.ZhTw] = "別罵了இдஇ",
                            [LangType.ZhHk] = "別罵了இдஇ",
                            [LangType.EnUs] = "Stop scolding meஇдஇ",
                            [LangType.EnGb] = "Stop scolding meஇдஇ",
                            [LangType.JaJp] = "叱らないでஇдஇ"
                        }.ToTextModel(),
                        CardPictureCss = "fa fa-newspaper",
                        CardLink = "https://weread.qq.com/web/reader/aef326f05d0f19aef085d2b"
                    },
                    new()
                    {
                        CardTitle = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "我的世界观",
                            [LangType.ZhTw] = "我的世界觀",
                            [LangType.ZhHk] = "我的世界觀",
                            [LangType.EnUs] = "My World View",
                            [LangType.EnGb] = "My World View",
                            [LangType.JaJp] = "私の世界観"
                        }.ToTextModel(),
                        CardPictureUrl = "img/Hobbies/Books/我的世界观.jpeg",
                        CardComment = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "伟大的物理学家同样有光辉的人格",
                            [LangType.ZhTw] = "偉大的物理學家同樣有光輝的人格",
                            [LangType.ZhHk] = "偉大的物理學家同樣有光輝的人格",
                            [LangType.EnUs] = "Great physicists also have brilliant personalities",
                            [LangType.EnGb] = "Great physicists also have brilliant personalities",
                            [LangType.JaJp] = "偉大な物理学者も輝かしい人格を持っている"
                        }.ToTextModel(),
                        CardPictureCss = "fa fa-brain",
                        CardLink = "https://weread.qq.com/web/bookDetail/83c329e07166cdd783c051d"
                    }
                ];

                #endregion
            }

            public static class Subsection3
            {
                public static readonly Dictionary<LangType, string> SubsectionTitle = new()
                {
                    [LangType.ZhCn] = "番剧",
                    [LangType.ZhTw] = "番劇",
                    [LangType.ZhHk] = "番劇",
                    [LangType.EnUs] = "Anime",
                    [LangType.EnGb] = "Anime",
                    [LangType.JaJp] = "アニメ"
                };

                public static readonly Dictionary<LangType, string> SubsectionComment = new()
                {
                    [LangType.ZhCn] = "动画既是消遣，也是借来的灵魂碎片与平行的无数人生",
                    [LangType.ZhTw] = "動畫既是消遣，也是借來的靈魂碎片與平行的無數人生",
                    [LangType.ZhHk] = "動畫既是消遣，也是借來的靈魂碎片與平行的無數人生",
                    [LangType.EnUs] =
                        "Animation is both entertainment and borrowed fragments of souls with countless parallel lives",
                    [LangType.EnGb] =
                        "Animation is both entertainment and borrowed fragments of souls with countless parallel lives",
                    [LangType.JaJp] = "アニメは娯楽であると同時に、借りてきた魂の断片と無数の平行人生でもあります"
                };

                public const string SubsectionCss = "fa fa-tv";

                public static readonly List<HobbiesCardModel> Cards =
                [
                    new()
                    {
                        CardTitle = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "星之梦-雪圈球",
                            [LangType.ZhTw] = "星之夢-雪圈球",
                            [LangType.ZhHk] = "星之夢-雪圈球",
                            [LangType.EnUs] = "Planetarian: Snow Globe",
                            [LangType.EnGb] = "Planetarian: Snow Globe",
                            [LangType.JaJp] = "planetarian～雪圏球～"
                        }.ToTextModel(),
                        CardPictureUrl = "img/Hobbies/Bangumi/星之梦.jpeg",
                        CardComment = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "个人认为最感人的电影，哭烂了(╥﹏╥)",
                            [LangType.ZhTw] = "個人認為最感人的電影，哭爛了(╥﹏╥)",
                            [LangType.ZhHk] = "個人認為最感人的電影，哭爛了(╥﹏╥)",
                            [LangType.EnUs] = "Personally think it's the most touching movie, cried badly(╥﹏╥)",
                            [LangType.EnGb] = "Personally think it's the most touching movie, cried badly(╥﹏╥)",
                            [LangType.JaJp] = "個人的に最も感動的な映画だと思います、めっちゃ泣きました(╥﹏╥)"
                        }.ToTextModel(),
                        CardPictureCss = "fa fa-face-sad-tear",
                        CardLink = "https://www.bilibili.com/bangumi/play/ep90842"
                    },
                    new()
                    {
                        CardTitle = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "可塑性记忆",
                            [LangType.ZhTw] = "可塑性記憶",
                            [LangType.ZhHk] = "可塑性記憶",
                            [LangType.EnUs] = "Plastic Memories",
                            [LangType.EnGb] = "Plastic Memories",
                            [LangType.JaJp] = "プラスティック・メモリーズ"
                        }.ToTextModel(),
                        CardPictureUrl = "img/Hobbies/Bangumi/可塑性记忆.png",
                        CardComment = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "时光流转，愿你能与重要的人重逢",
                            [LangType.ZhTw] = "時光流轉，願你能與重要的人重逢",
                            [LangType.ZhHk] = "時光流轉，願你能與重要的人重逢",
                            [LangType.EnUs] = "Time flows, may you reunite with important people",
                            [LangType.EnGb] = "Time flows, may you reunite with important people",
                            [LangType.JaJp] = "時は流れ、大切な人と再会できますように"
                        }.ToTextModel(),
                        CardPictureCss = "fas fa-heart",
                        CardLink = "https://www.imdb.com/title/tt4603222/"
                    },
                    new()
                    {
                        CardTitle = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "孤独摇滚",
                            [LangType.ZhTw] = "孤獨搖滾",
                            [LangType.ZhHk] = "孤獨搖滾",
                            [LangType.EnUs] = "Bocchi the Rock!",
                            [LangType.EnGb] = "Bocchi the Rock!",
                            [LangType.JaJp] = "ぼっち・ざ・ろっく！"
                        }.ToTextModel(),
                        CardPictureUrl = "img/Hobbies/Bangumi/孤独摇滚.JPG",
                        CardComment = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "青春真好啊",
                            [LangType.ZhTw] = "青春真好啊",
                            [LangType.ZhHk] = "青春真好啊",
                            [LangType.EnUs] = "Youth is really wonderful",
                            [LangType.EnGb] = "Youth is really wonderful",
                            [LangType.JaJp] = "青春って本当に素敵ですね"
                        }.ToTextModel(),
                        CardPictureCss = "fas fa-face-smile-beam",
                        CardLink = "https://www.bilibili.com/bangumi/play/ep693247"
                    },
                    new()
                    {
                        CardTitle = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "东方幼灵梦",
                            [LangType.ZhTw] = "東方幼靈夢",
                            [LangType.ZhHk] = "東方幼靈夢",
                            [LangType.EnUs] = "Touhou Young Reimu",
                            [LangType.EnGb] = "Touhou Young Reimu",
                            [LangType.JaJp] = "東方幼靈夢"
                        }.ToTextModel(),
                        CardPictureUrl = "img/Hobbies/Bangumi/东方幼灵梦.jpeg",
                        CardComment = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "灵梦尚幼，食人妖怪尚大时的故事",
                            [LangType.ZhTw] = "靈夢尚幼，食人妖怪尚大時的故事",
                            [LangType.ZhHk] = "靈夢尚幼，食人妖怪尚大時的故事",
                            [LangType.EnUs] =
                                "The story when Reimu was still young and the man-eating youkai was still big",
                            [LangType.EnGb] =
                                "The story when Reimu was still young and the man-eating youkai was still big",
                            [LangType.JaJp] = "霊夢がまだ幼く、人食い妖怪がまだ大きかった時の物語"
                        }.ToTextModel(),
                        CardPictureCss = "fas fa-face-sad-tear",
                        CardLink = "https://baike.baidu.com/item/东方幼灵梦/7904133"
                    },
                    new()
                    {
                        CardTitle = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "你的名字",
                            [LangType.ZhTw] = "你的名字",
                            [LangType.ZhHk] = "你的名字",
                            [LangType.EnUs] = "Your Name",
                            [LangType.EnGb] = "Your Name",
                            [LangType.JaJp] = "君の名は。"
                        }.ToTextModel(),
                        CardPictureUrl = "img/Hobbies/Bangumi/你的名字.jpg",
                        CardComment = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "我好像在哪里见过你",
                            [LangType.ZhTw] = "我好像在哪裡見過你",
                            [LangType.ZhHk] = "我好像在哪裡見過你",
                            [LangType.EnUs] = "I feel like I've seen you somewhere before",
                            [LangType.EnGb] = "I feel like I've seen you somewhere before",
                            [LangType.JaJp] = "どこかで会ったことがあるような"
                        }.ToTextModel(),
                        CardPictureCss = "fas fa-face-smile-beam",
                        CardLink = "https://www.imdb.com/title/tt5311514/"
                    },
                    new()
                    {
                        CardTitle = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "大鱼海棠",
                            [LangType.ZhTw] = "大魚海棠",
                            [LangType.ZhHk] = "大魚海棠",
                            [LangType.EnUs] = "Big Fish & Begonia",
                            [LangType.EnGb] = "Big Fish & Begonia",
                            [LangType.JaJp] = "大魚海棠"
                        }.ToTextModel(),
                        CardPictureUrl = "img/Hobbies/Bangumi/大鱼海棠.jpg",
                        CardComment = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "6年前看完记忆深刻的电影",
                            [LangType.ZhTw] = "6年前看完記憶深刻的電影",
                            [LangType.ZhHk] = "6年前看完記憶深刻的電影",
                            [LangType.EnUs] = "A movie that left a deep impression 6 years ago",
                            [LangType.EnGb] = "A movie that left a deep impression 6 years ago",
                            [LangType.JaJp] = "6年前に見て印象に残った映画"
                        }.ToTextModel(),
                        CardPictureCss = "fas fa-heart",
                        CardLink = "https://www.imdb.com/title/tt1920885/"
                    }
                ];
            }

            public static class Subsection4
            {
                public static readonly Dictionary<LangType, string> SubsectionTitle = new()
                {
                    [LangType.ZhCn] = "电影",
                    [LangType.ZhTw] = "電影",
                    [LangType.ZhHk] = "電影",
                    [LangType.EnUs] = "Movies",
                    [LangType.EnGb] = "Movies",
                    [LangType.JaJp] = "映画"
                };

                public static readonly Dictionary<LangType, string> SubsectionComment = new()
                {
                    [LangType.ZhCn] = "封存于胶片上的时代情绪与灵魂切片",
                    [LangType.ZhTw] = "封存於膠片上的時代情緒與靈魂切片",
                    [LangType.ZhHk] = "封存於膠片上的時代情緒與靈魂切片",
                    [LangType.EnUs] = "Era emotions and soul slices preserved on film",
                    [LangType.EnGb] = "Era emotions and soul slices preserved on film",
                    [LangType.JaJp] = "フィルムに封じ込められた時代の感情と魂の切片"
                };

                public const string SubsectionCss = "fas fa-film";

                public static readonly List<HobbiesCardModel> Cards =
                [
                    new()
                    {
                        CardTitle = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "冰雪奇缘",
                            [LangType.ZhTw] = "冰雪奇緣",
                            [LangType.ZhHk] = "冰雪奇緣",
                            [LangType.EnUs] = "Frozen",
                            [LangType.EnGb] = "Frozen",
                            [LangType.JaJp] = "アナと雪の女王"
                        }.ToTextModel(),
                        CardPictureUrl = "img/Hobbies/Movies/冰雪奇缘.jpg",
                        CardComment = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "有些人是值得为他融化的",
                            [LangType.ZhTw] = "有些人是值得為他融化的",
                            [LangType.ZhHk] = "有些人是值得為他融化的",
                            [LangType.EnUs] = "Some people are worth melting for",
                            [LangType.EnGb] = "Some people are worth melting for",
                            [LangType.JaJp] = "溶ける価値のある人がいる"
                        }.ToTextModel(),
                        CardPictureCss = "fas fa-heart",
                        CardLink = "https://www.bilibili.com/bangumi/play/ss46052"
                    },
                    new()
                    {
                        CardTitle = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "冰雪奇缘II",
                            [LangType.ZhTw] = "冰雪奇緣II",
                            [LangType.ZhHk] = "冰雪奇緣II",
                            [LangType.EnUs] = "Frozen II",
                            [LangType.EnGb] = "Frozen II",
                            [LangType.JaJp] = "アナと雪の女王2"
                        }.ToTextModel(),
                        CardPictureUrl = "img/Hobbies/Movies/冰雪奇缘II.jpeg",
                        CardComment = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "音乐太好听了",
                            [LangType.ZhTw] = "音樂太好聽了",
                            [LangType.ZhHk] = "音樂太好聽了",
                            [LangType.EnUs] = "The music is too good",
                            [LangType.EnGb] = "The music is too good",
                            [LangType.JaJp] = "音楽がとても素敵"
                        }.ToTextModel(),
                        CardPictureCss = "fas fa-music",
                        CardLink = "https://www.bilibili.com/bangumi/play/ss46062"
                    },
                    new()
                    {
                        CardTitle = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "头脑特工队",
                            [LangType.ZhTw] = "腦筋急轉彎",
                            [LangType.ZhHk] = "玩轉腦朋友",
                            [LangType.EnUs] = "Inside Out",
                            [LangType.EnGb] = "Inside Out",
                            [LangType.JaJp] = "インサイド・ヘッド"
                        }.ToTextModel(),
                        CardPictureUrl = "img/Hobbies/Movies/头脑特工队.jpg",
                        CardComment = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "有时候，快乐的回忆也会让你感到伤心",
                            [LangType.ZhTw] = "有時候，快樂的回憶也會讓你感到傷心",
                            [LangType.ZhHk] = "有時候，快樂的回憶也會讓你感到傷心",
                            [LangType.EnUs] = "Sometimes, happy memories can also make you feel sad",
                            [LangType.EnGb] = "Sometimes, happy memories can also make you feel sad",
                            [LangType.JaJp] = "時には、楽しい思い出もあなたを悲しくさせることがある"
                        }.ToTextModel(),
                        CardPictureCss = "fas fa-face-sad-tear",
                        CardLink = "https://www.bilibili.com/bangumi/play/ss46265"
                    },
                    new()
                    {
                        CardTitle = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "狮子王",
                            [LangType.ZhTw] = "獅子王",
                            [LangType.ZhHk] = "獅子王",
                            [LangType.EnUs] = "The Lion King",
                            [LangType.EnGb] = "The Lion King",
                            [LangType.JaJp] = "ライオン・キング"
                        }.ToTextModel(),
                        CardPictureUrl = "img/Hobbies/Movies/狮子王.jpeg",
                        CardComment = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "It's the Circle Of Life",
                            [LangType.ZhTw] = "It's the Circle Of Life",
                            [LangType.ZhHk] = "It's the Circle Of Life",
                            [LangType.EnUs] = "It's the Circle Of Life",
                            [LangType.EnGb] = "It's the Circle Of Life",
                            [LangType.JaJp] = "It's the Circle Of Life"
                        }.ToTextModel(),
                        CardPictureCss = "fas fa-paper-plane",
                        CardLink = "https://www.bilibili.com/bangumi/play/ss46258"
                    },
                    new()
                    {
                        CardTitle = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "心灵奇旅",
                            [LangType.ZhTw] = "靈魂急轉彎",
                            [LangType.ZhHk] = "靈魂奇遇記",
                            [LangType.EnUs] = "Soul",
                            [LangType.EnGb] = "Soul",
                            [LangType.JaJp] = "ソウルフル・ワールド"
                        }.ToTextModel(),
                        CardPictureUrl = "img/Hobbies/Movies/心灵奇旅.jpg",
                        CardComment = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "在你想要生活的那一刻，火花就被点亮了",
                            [LangType.ZhTw] = "在你想要生活的那一刻，火花就被點亮了",
                            [LangType.ZhHk] = "在你想要生活的那一刻，火花就被點亮了",
                            [LangType.EnUs] = "The moment you want to live, the spark is lit",
                            [LangType.EnGb] = "The moment you want to live, the spark is lit",
                            [LangType.JaJp] = "あなたが生きたいと思った瞬間、火花が灯される"
                        }.ToTextModel(),
                        CardPictureCss = "fas fa-fire",
                        CardLink = "https://www.bilibili.com/bangumi/play/ss46248"
                    },
                    new()
                    {
                        CardTitle = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "泰山",
                            [LangType.ZhTw] = "泰山",
                            [LangType.ZhHk] = "泰山",
                            [LangType.EnUs] = "Tarzan",
                            [LangType.EnGb] = "Tarzan",
                            [LangType.JaJp] = "ターザン"
                        }.ToTextModel(),
                        CardPictureUrl = "img/Hobbies/Movies/泰山.jpg",
                        CardComment = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "在两个世界的峡谷，有同一颗爱的心",
                            [LangType.ZhTw] = "在兩個世界的峽谷，有同一顆愛的心",
                            [LangType.ZhHk] = "在兩個世界的峽谷，有同一顆愛的心",
                            [LangType.EnUs] = "In the canyon between two worlds, there is the same loving heart",
                            [LangType.EnGb] = "In the canyon between two worlds, there is the same loving heart",
                            [LangType.JaJp] = "二つの世界の峡谷に、同じ愛の心がある"
                        }.ToTextModel(),
                        CardPictureCss = "fas fa-heart",
                        CardLink = "https://www.imdb.com/title/tt0120855"
                    }
                ];
            }

            public static class Subsection5
            {
                public static readonly Dictionary<LangType, string> SubsectionTitle = new()
                {
                    [LangType.ZhCn] = "音乐",
                    [LangType.ZhTw] = "音樂",
                    [LangType.ZhHk] = "音樂",
                    [LangType.EnUs] = "Music",
                    [LangType.EnGb] = "Music",
                    [LangType.JaJp] = "音楽"
                };

                public static readonly Dictionary<LangType, string> SubsectionComment = new()
                {
                    [LangType.ZhCn] = "偏好番剧电影OST, 钢琴, JPop, 国语经典等, 最喜欢的还是OST",
                    [LangType.ZhTw] = "偏好番劇電影OST, 鋼琴, JPop, 國語經典等, 最喜歡的還是OST",
                    [LangType.ZhHk] = "偏好番劇電影OST, 鋼琴, JPop, 國語經典等, 最喜歡的還是OST",
                    [LangType.EnUs] =
                        "Prefer anime/movie OSTs, piano, JPop, Mandarin classics, etc., but still like OSTs the most",
                    [LangType.EnGb] =
                        "Prefer anime/movie OSTs, piano, JPop, Mandarin classics, etc., but still like OSTs the most",
                    [LangType.JaJp] = "アニメ・映画OST、ピアノ、JPop、中国語クラシックなどを好みますが、一番好きなのはやはりOSTです"
                };

                public const string SubsectionCss = "fas fa-music";

                public static readonly List<HobbiesCardModel> Cards =
                [
                    new()
                    {
                        CardTitle = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "Ones' hope",
                            [LangType.ZhTw] = "Ones' hope",
                            [LangType.ZhHk] = "Ones' hope",
                            [LangType.EnUs] = "Ones' hope",
                            [LangType.EnGb] = "Ones' hope",
                            [LangType.JaJp] = "Ones' hope"
                        }.ToTextModel(),
                        CardPictureUrl = "img/Hobbies/Musics/Ones-hope.jpg",
                        CardComment = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "有一种电影落幕的悲伤感",
                            [LangType.ZhTw] = "有一種電影落幕的悲傷感",
                            [LangType.ZhHk] = "有一種電影落幕的悲傷感",
                            [LangType.EnUs] = "Has a sad feeling of a movie ending",
                            [LangType.EnGb] = "Has a sad feeling of a movie ending",
                            [LangType.JaJp] = "映画の終わり的な悲しみがある"
                        }.ToTextModel(),
                        CardPictureCss = "fas fa-headphones",
                        CardLink = "https://www.bilibili.com/video/BV1A14y1n7hK"
                    },
                    new()
                    {
                        CardTitle = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "温柔的回忆",
                            [LangType.ZhTw] = "溫柔的回憶",
                            [LangType.ZhHk] = "溫柔的回憶",
                            [LangType.EnUs] = "Memories of Kindness",
                            [LangType.EnGb] = "Memories of Kindness",
                            [LangType.JaJp] = "優しさの記憶"
                        }.ToTextModel(),
                        CardPictureUrl = "img/Hobbies/Musics/温柔的回忆.jpeg",
                        CardComment = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "蔚蓝档案最终章ED",
                            [LangType.ZhTw] = "蔚藍檔案最終章ED",
                            [LangType.ZhHk] = "蔚藍檔案最終章ED",
                            [LangType.EnUs] = "Blue Archive Final Chapter ED",
                            [LangType.EnGb] = "Blue Archive Final Chapter ED",
                            [LangType.JaJp] = "ブルーアーカイブ最終章ED"
                        }.ToTextModel(),
                        CardPictureCss = "fas fa-music",
                        CardLink = "https://www.bilibili.com/video/BV1Ag4y1b7pa"
                    },
                    new()
                    {
                        CardTitle = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "Show Yourself",
                            [LangType.ZhTw] = "Show Yourself",
                            [LangType.ZhHk] = "Show Yourself",
                            [LangType.EnUs] = "Show Yourself",
                            [LangType.EnGb] = "Show Yourself",
                            [LangType.JaJp] = "Show Yourself"
                        }.ToTextModel(),
                        CardPictureUrl = "img/Hobbies/Musics/ShowYourself.jpeg",
                        CardComment = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "冰雪奇缘II的OST百听不厌",
                            [LangType.ZhTw] = "冰雪奇緣II的OST百聽不厭",
                            [LangType.ZhHk] = "冰雪奇緣II的OST百聽不厭",
                            [LangType.EnUs] = "Frozen II OST never gets old",
                            [LangType.EnGb] = "Frozen II OST never gets old",
                            [LangType.JaJp] = "アナと雪の女王2のOSTは何度聞いても飽きない"
                        }.ToTextModel(),
                        CardPictureCss = "fas fa-music",
                        CardLink = "https://www.bilibili.com/video/BV1oh4y1Z72f"
                    },
                    new()
                    {
                        CardTitle = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "像风一样自由",
                            [LangType.ZhTw] = "像風一樣自由",
                            [LangType.ZhHk] = "像風一樣自由",
                            [LangType.EnUs] = "Free as the Wind",
                            [LangType.EnGb] = "Free as the Wind",
                            [LangType.JaJp] = "風のように自由"
                        }.ToTextModel(),
                        CardPictureUrl = "img/Hobbies/Musics/像风一样自由.jpeg",
                        CardComment = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "高三的时候学校广播经常放这首",
                            [LangType.ZhTw] = "高三的時候學校廣播經常放這首",
                            [LangType.ZhHk] = "高三的時候學校廣播經常放這首",
                            [LangType.EnUs] = "School broadcast often played this song during senior year",
                            [LangType.EnGb] = "School broadcast often played this song during senior year",
                            [LangType.JaJp] = "高校3年生の時、学校放送でよくこの曲を流していた"
                        }.ToTextModel(),
                        CardPictureCss = "fas fa-headphones",
                        CardLink = "https://www.bilibili.com/video/BV1Ba4y1Q7NE"
                    },
                    new()
                    {
                        CardTitle = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "Greensleeves",
                            [LangType.ZhTw] = "Greensleeves",
                            [LangType.ZhHk] = "Greensleeves",
                            [LangType.EnUs] = "Greensleeves",
                            [LangType.EnGb] = "Greensleeves",
                            [LangType.JaJp] = "グリーンスリーブス"
                        }.ToTextModel(),
                        CardPictureUrl = "img/Hobbies/Musics/Greensleeves.jpg",
                        CardComment = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "理查德克莱德曼翻弹的英国民谣",
                            [LangType.ZhTw] = "理查德克萊德曼翻彈的英國民謠",
                            [LangType.ZhHk] = "理查德克萊德曼翻彈的英國民謠",
                            [LangType.EnUs] = "British folk song covered by Richard Clayderman",
                            [LangType.EnGb] = "British folk song covered by Richard Clayderman",
                            [LangType.JaJp] = "リチャード・クレイダーマンがカバーしたイギリス民謡"
                        }.ToTextModel(),
                        CardPictureCss = "fas fa-music",
                        CardLink = "https://www.bilibili.com/video/BV1P8411o7PL"
                    },
                    new()
                    {
                        CardTitle = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "Time for Miracles",
                            [LangType.ZhTw] = "Time for Miracles",
                            [LangType.ZhHk] = "Time for Miracles",
                            [LangType.EnUs] = "Time for Miracles",
                            [LangType.EnGb] = "Time for Miracles",
                            [LangType.JaJp] = "Time for Miracles"
                        }.ToTextModel(),
                        CardPictureUrl = "img/Hobbies/Musics/Time-for-Miracles.jpeg",
                        CardComment = new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = "电影《2012》的OST",
                            [LangType.ZhTw] = "電影《2012》的OST",
                            [LangType.ZhHk] = "電影《2012》的OST",
                            [LangType.EnUs] = "OST from the movie '2012'",
                            [LangType.EnGb] = "OST from the movie '2012'",
                            [LangType.JaJp] = "映画『2012』のOST"
                        }.ToTextModel(),
                        CardPictureCss = "fas fa-headphones",
                        CardLink = "https://www.bilibili.com/video/BV1EzHDzLE6R/"
                    }
                ];
            }

            #endregion
        }

        #endregion

        #region Quotes

        public static class Quotes
        {
            public const string SectionCss = "fas fa-lightbulb";
            public const string SectionAnchor = "BookQuotes";

            public static readonly Dictionary<LangType, string> SectionTitle = new()
            {
                [LangType.ZhCn] = "书摘",
                [LangType.ZhTw] = "書摘",
                [LangType.ZhHk] = "書摘",
                [LangType.EnUs] = "Book Quotes",
                [LangType.EnGb] = "Book Quotes",
                [LangType.JaJp] = "書籍引用"
            };

            public static readonly Dictionary<LangType, string> SectionComment = new()
            {
                [LangType.ZhCn] = "采撷字句星光，照亮思想一隅",
                [LangType.ZhTw] = "採擷字句星光，照亮思想一隅",
                [LangType.ZhHk] = "採擷字句星光，照亮思想一隅", 
                [LangType.EnUs] = "Gleaning starlight from words, to illuminate a corner of the mind",
                [LangType.EnGb] = "Gleaning starlight from words, to illuminate a corner of the mind",
                [LangType.JaJp] = "言葉のきらめきを集め、心の一角を照らす"
            };


            #region Cards

            public static readonly List<QuotesCardModel> Cards =
            [
                new()
                {
                    CardContent = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] =
                            "完成同样一件工作对学生产生的教育方面的影响可能有很大不同，这取决于使他完成这件工作的内因究竟是害怕受伤害、利己主义的情感，还是获得喜悦和满足感。工作最重要的动机是工作中的乐趣、工作所得到的成果的乐趣，以及对该成果的社会价值的感知。",
                        [LangType.ZhTw] =
                            "完成同樣一件工作對學生產生的教育方面的影響可能有很大不同，這取決於使他完成這件工作的內因究竟是害怕受傷害、利己主義的情感，還是獲得喜悅和滿足感。工作最重要的動機是工作中的樂趣、工作所得到的成果的樂趣，以及對該成果的社會價值的感知。",
                        [LangType.ZhHk] =
                            "完成同樣一件工作對學生產生的教育方面的影響可能有很大不同，這取決於使他完成這件工作的內因究竟是害怕受傷害、利己主義的情感，還是獲得喜悅和滿足感。工作最重要的動機是工作中的樂趣、工作所得到的成果的樂趣，以及對該成果的社會價值的感知。",
                        [LangType.EnUs] =
                            "The educational impact of completing the same work on students may vary greatly, depending on whether the internal motivation is fear of harm, egoistic emotions, or the joy and satisfaction gained. The most important motivations for work are the pleasure in the work itself, the pleasure in the results achieved, and the perception of the social value of those results.",
                        [LangType.EnGb] =
                            "The educational impact of completing the same work on students may vary greatly, depending on whether the internal motivation is fear of harm, egoistic emotions, or the joy and satisfaction gained. The most important motivations for work are the pleasure in the work itself, the pleasure in the results achieved, and the perception of the social value of those results.",
                        [LangType.JaJp] =
                            "同じ仕事を完成させることの学生に対する教育的影響は大きく異なる可能性があり、それはその仕事を完成させる内的要因が危害を受ける恐れ、利己主義的感情であるか、喜びと満足感を得ることであるかによって決まります。仕事の最も重要な動機は、仕事自体の楽しみ、仕事によって得られる成果の楽しみ、そしてその成果の社会的価値に対する認識です。"
                    }.ToTextModel(),
                    CardComment = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "爱因斯坦《论教育》",
                        [LangType.ZhTw] = "愛因斯坦《論教育》",
                        [LangType.ZhHk] = "愛因斯坦《論教育》",
                        [LangType.EnUs] = "Einstein 'On Education'",
                        [LangType.EnGb] = "Einstein 'On Education'",
                        [LangType.JaJp] = "アインシュタイン『教育について』"
                    }.ToTextModel(),
                    CardLink = "https://weread.qq.com/web/reader/83c329e07166cdd783c051d",
                },
                new()
                {
                    CardContent = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "爱是一种创造性的关系，爱是创造爱的能力，无爱则不能创造爱。",
                        [LangType.ZhTw] = "愛是一種創造性的關係，愛是創造愛的能力，無愛則不能創造愛。",
                        [LangType.ZhHk] = "愛是一種創造性的關係，愛是創造愛的能力，無愛則不能創造愛。",
                        [LangType.EnUs] =
                            "Love is a creative relationship, love is the ability to create love, without love one cannot create love.",
                        [LangType.EnGb] =
                            "Love is a creative relationship, love is the ability to create love, without love one cannot create love.",
                        [LangType.JaJp] = "愛は創造的な関係であり、愛は愛を創造する能力であり、愛がなければ愛を創造することはできません。"
                    }.ToTextModel(),
                    CardComment = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "弗洛姆《爱的艺术》",
                        [LangType.ZhTw] = "弗洛姆《愛的藝術》",
                        [LangType.ZhHk] = "弗洛姆《愛的藝術》",
                        [LangType.EnUs] = "Fromm 'The Art of Loving'",
                        [LangType.EnGb] = "Fromm 'The Art of Loving'",
                        [LangType.JaJp] = "フロム『愛するということ』"
                    }.ToTextModel(),
                    CardLink = "https://weread.qq.com/web/reader/d7d32d70722dd429d7d723d",
                },
                new()
                {
                    CardContent = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] =
                            "自由是人存在的特征，而且，其含义会随人把自身作为一个独立和分离的存在物加以认识和理解的程度不同而有所变化。一旦个体化全部完成，个人从这些始发纽带中解放出来，他又面临新的任务：他必须自我定位，在这个世界上扎下根，寻找不同于其前个体存在状态所具有的更安全的保护方式。",
                        [LangType.ZhTw] =
                            "自由是人存在的特徵，而且，其含義會隨人把自身作為一個獨立和分離的存在物加以認識和理解的程度不同而有所變化。一旦個體化全部完成，個人從這些始發紐帶中解放出來，他又面臨新的任務：他必須自我定位，在這個世界上紮下根，尋找不同於其前個體存在狀態所具有的更安全的保護方式。",
                        [LangType.ZhHk] =
                            "自由是人存在的特徵，而且，其含義會隨人把自身作為一個獨立和分離的存在物加以認識和理解的程度不同而有所變化。一旦個體化全部完成，個人從這些始發紐帶中解放出來，他又面臨新的任務：他必須自我定位，在這個世界上紮下根，尋找不同於其前個體存在狀態所具有的更安全的保護方式。",
                        [LangType.EnUs] =
                            "Freedom is a characteristic of human existence, and its meaning changes according to the degree to which people recognize and understand themselves as independent and separate beings. Once individuation is fully completed and the individual is liberated from these primary bonds, they face a new task: they must position themselves, take root in this world, and find safer ways of protection different from those in their pre-individual existence state.",
                        [LangType.EnGb] =
                            "Freedom is a characteristic of human existence, and its meaning changes according to the degree to which people recognize and understand themselves as independent and separate beings. Once individuation is fully completed and the individual is liberated from these primary bonds, they face a new task: they must position themselves, take root in this world, and find safer ways of protection different from those in their pre-individual existence state.",
                        [LangType.JaJp] =
                            "自由は人間存在の特徴であり、その意味は、人が自分自身を独立した分離した存在物として認識し理解する程度によって変化します。個人化が完全に完了し、個人がこれらの原初的絆から解放されると、彼は新たな課題に直面します：自己を位置づけ、この世界に根を下ろし、個人化以前の存在状態が持っていたものとは異なるより安全な保護方法を見つけなければなりません。"
                    }.ToTextModel(),
                    CardComment = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "弗洛姆《逃避自由》",
                        [LangType.ZhTw] = "弗洛姆《逃避自由》",
                        [LangType.ZhHk] = "弗洛姆《逃避自由》",
                        [LangType.EnUs] = "Fromm 'Escape from Freedom'",
                        [LangType.EnGb] = "Fromm 'Escape from Freedom'",
                        [LangType.JaJp] = "フロム『自由からの逃走』"
                    }.ToTextModel(),
                    CardLink = "https://weread.qq.com/web/reader/679328a0813ab8004g01640f",
                },
                new()
                {
                    CardContent = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "这种对先辈的缅怀确实不容忽视，尤其是对往日盛事的回忆，有助于鼓舞今天善良的人去勇敢地奋斗。",
                        [LangType.ZhTw] = "這種對先輩的緬懷確實不容忽視，尤其是對往日盛事的回憶，有助於鼓舞今天善良的人去勇敢地奮鬥。",
                        [LangType.ZhHk] = "這種對先輩的緬懷確實不容忽視，尤其是對往日盛事的回憶，有助於鼓舞今天善良的人去勇敢地奮鬥。",
                        [LangType.EnUs] =
                            "This remembrance of predecessors should indeed not be overlooked, especially the recollection of past glorious events, which helps inspire good people today to strive bravely.",
                        [LangType.EnGb] =
                            "This remembrance of predecessors should indeed not be overlooked, especially the recollection of past glorious events, which helps inspire good people today to strive bravely.",
                        [LangType.JaJp] = "この先人への追憶は確かに見過ごすべきではなく、特に過去の輝かしい出来事の回想は、今日の善良な人々が勇敢に奮闘するよう鼓舞するのに役立ちます。"
                    }.ToTextModel(),
                    CardComment = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "爱因斯坦",
                        [LangType.ZhTw] = "愛因斯坦",
                        [LangType.ZhHk] = "愛因斯坦",
                        [LangType.EnUs] = "Einstein",
                        [LangType.EnGb] = "Einstein",
                        [LangType.JaJp] = "アインシュタイン"
                    }.ToTextModel(),
                    CardLink = "https://weread.qq.com/web/reader/83c329e07166cdd783c051d",
                },
                new()
                {
                    CardContent = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "道路是曲折的，前途是光明的",
                        [LangType.ZhTw] = "道路是曲折的，前途是光明的",
                        [LangType.ZhHk] = "道路是曲折的，前途是光明的",
                        [LangType.EnUs] = "The road is tortuous, the future is bright",
                        [LangType.EnGb] = "The road is tortuous, the future is bright",
                        [LangType.JaJp] = "道は曲がりくねっているが、未来は明るい"
                    }.ToTextModel(),
                    CardComment = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "毛主席",
                        [LangType.ZhTw] = "毛主席",
                        [LangType.ZhHk] = "毛主席",
                        [LangType.EnUs] = "Chairman Mao",
                        [LangType.EnGb] = "Chairman Mao",
                        [LangType.JaJp] = "毛主席"
                    }.ToTextModel(),
                    CardLink = "https://zhuanlan.zhihu.com/p/1935129455052984765",
                },
                new()
                {
                    CardContent = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "生活往往不尽如人意，但人在失意的时候不要失态",
                        [LangType.ZhTw] = "生活往往不盡如人意，但人在失意的時候不要失態",
                        [LangType.ZhHk] = "生活往往不盡如人意，但人在失意的時候不要失態",
                        [LangType.EnUs] =
                            "Life often doesn't go as desired, but one should not lose composure when frustrated.",
                        [LangType.EnGb] =
                            "Life often doesn't go as desired, but one should not lose composure when frustrated.",
                        [LangType.JaJp] = "生活は往々にして思い通りになりませんが、人は失意のときでも品格を失ってはいけません。"
                    }.ToTextModel(),
                    CardComment = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "知乎回答",
                        [LangType.ZhTw] = "知乎回答",
                        [LangType.ZhHk] = "知乎回答",
                        [LangType.EnUs] = "Zhihu Answer",
                        [LangType.EnGb] = "Zhihu Answer",
                        [LangType.JaJp] = "Zhihu回答"
                    }.ToTextModel(),
                    CardLink = "https://www.zhihu.com/question/62598434/answer/3306472075",
                },
                new()
                {
                    CardContent = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "你知道这鸟的名字，就算你会用世界上所有的语言去称呼它，你其实对这鸟还是一无所知。你所知道的，仅仅是不同地方的人怎么称呼这种鸟而已。",
                        [LangType.ZhTw] = "你知道這鳥的名字，就算你會用世界上所有的語言去稱呼它，你其實對這鳥還是一無所知。你所知道的，僅僅是不同地方的人怎麼稱呼這種鳥而已。",
                        [LangType.ZhHk] = "你知道這鳥的名字，就算你會用世界上所有的語言去稱呼它，你其實對這鳥還是一無所知。你所知道的，僅僅是不同地方的人怎麼稱呼這種鳥而已。",
                        [LangType.EnUs] =
                            "You know the name of this bird, even if you can call it in all the languages of the world, you actually know nothing about this bird. All you know is just how people in different places call this bird.",
                        [LangType.EnGb] =
                            "You know the name of this bird, even if you can call it in all the languages of the world, you actually know nothing about this bird. All you know is just how people in different places call this bird.",
                        [LangType.JaJp] =
                            "あなたはこの鳥の名前を知っていますが、たとえ世界中のすべての言語でそれを呼ぶことができても、実際にはこの鳥について何も知りません。あなたが知っているのは、単に異なる場所の人々がこの鳥をどう呼ぶかだけです。"
                    }.ToTextModel(),
                    CardComment = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "费曼《知道某事物的名称和知道事物之间的区别》",
                        [LangType.ZhTw] = "費曼《知道某事物的名稱和知道事物之間的區別》",
                        [LangType.ZhHk] = "費曼《知道某事物的名稱和知道事物之間的區別》",
                        [LangType.EnUs] =
                            "Feynman 'The Difference Between Knowing the Name of Something and Knowing Something'",
                        [LangType.EnGb] =
                            "Feynman 'The Difference Between Knowing the Name of Something and Knowing Something'",
                        [LangType.JaJp] = "ファインマン『物事の名前を知ることと物事を知ることの違い』"
                    }.ToTextModel(),
                    CardLink = "https://weread.qq.com/web/bookDetail/7af32ba05e01507af3447c7",
                }
            ];

            #endregion
        }

        #endregion

        #region Friends

        public static class Friends
        {
            public const string SectionCss = "fas fa-link";
            public const string SectionAnchor = "FriendLinks";

            public static readonly Dictionary<LangType, string> SectionTitle = new()
            {
                [LangType.ZhCn] = "友链",
                [LangType.ZhTw] = "友鏈",
                [LangType.ZhHk] = "友鏈",
                [LangType.EnUs] = "Friend Links",
                [LangType.EnGb] = "Friend Links",
                [LangType.JaJp] = "友達リンク"
            };

            public static readonly Dictionary<LangType, string> SectionComment = new()
            {
                [LangType.ZhCn] = "相关项目与朋友们",
                [LangType.ZhTw] = "相關項目與朋友們",
                [LangType.ZhHk] = "相關項目與朋友們",
                [LangType.EnUs] = "Related Projects and Friends",
                [LangType.EnGb] = "Related Projects and Friends",
                [LangType.JaJp] = "関連プロジェクトと友達"
            };


            public static readonly List<FriendsCardModel> Cards =
            [
                new()
                {
                    CardTitle = new Dictionary<LangType, string>
                    {
                        [LangType.ZhCn] = "天童爱丽丝",
                        [LangType.ZhTw] = "天童愛麗絲",
                        [LangType.ZhHk] = "天童愛麗絲",
                        [LangType.EnUs] = "Tendou Alice",
                        [LangType.EnGb] = "Tendou Alice",
                        [LangType.JaJp] = "天童アリス"
                    }.ToTextModel(),
                    CardPictureUrl = "img/Friends/爱丽丝.jpeg",
                    CardComment = new Dictionary<LangType, string>
                    {
                        [LangType.ZhCn] = "实时AI聊天机器人",
                        [LangType.ZhTw] = "即時AI聊天機器人",
                        [LangType.ZhHk] = "實時AI聊天機械人",
                        [LangType.EnUs] = "Real-time AI Chatbot",
                        [LangType.EnGb] = "Real-time AI Chatbot",
                        [LangType.JaJp] = "リアルタイムAIチャットボット"
                    }.ToTextModel(),
                    CardLink = "https://chat.quetzalsidera.me",
                },
                new()
                {
                    CardTitle = new Dictionary<LangType, string>
                    {
                        [LangType.ZhCn] = "开放平台",
                        [LangType.ZhTw] = "開放平台",
                        [LangType.ZhHk] = "開放平台",
                        [LangType.EnUs] = "Open Platform",
                        [LangType.EnGb] = "Open Platform",
                        [LangType.JaJp] = "オープンプラットフォーム"
                    }.ToTextModel(),
                    CardPictureUrl = "img/Friends/开放平台.jpeg",
                    CardComment = new Dictionary<LangType, string>
                    {
                        [LangType.ZhCn] = "网页源码与API",
                        [LangType.ZhTw] = "網頁源碼與API",
                        [LangType.ZhHk] = "網頁源碼與API",
                        [LangType.EnUs] = "Web Source Code and API",
                        [LangType.EnGb] = "Web Source Code and API",
                        [LangType.JaJp] = "ウェブソースコードとAPI"
                    }.ToTextModel(),
                    CardLink = "https://open.quetzalsidera.me",
                }
            ];
        }

        #endregion

        #region ThankYou

        public static class ThankYou
        {
            public const string SectionCss = "fas fa-heart";
            public const string SectionAnchor = "ThankYou";

            public static Dictionary<LangType, string> SectionTitle = new Dictionary<LangType, string>()
            {
                [LangType.ZhCn] = "致谢",
                [LangType.ZhTw] = "致謝",
                [LangType.ZhHk] = "致謝",
                [LangType.EnUs] = "Acknowledgments",
                [LangType.EnGb] = "Acknowledgements",
                [LangType.JaJp] = "謝辞"
            };

            public static Dictionary<LangType, string> SectionComment = new Dictionary<LangType, string>()
            {
                [LangType.ZhCn] = "这是我最珍贵的宝物",
                [LangType.ZhTw] = "這是我最珍貴的寶物",
                [LangType.ZhHk] = "這是我最珍貴的寶物",
                [LangType.EnUs] = "This is my most precious treasure",
                [LangType.EnGb] = "This is my most precious treasure",
                [LangType.JaJp] = "これは私の最も大切な宝物です",
            };


            public const string CardTitle = "生命中最重要的三件事";

            public static readonly List<string> CardContent =
            [
                "在大一下学期，我有幸选到了心理中心华丹老师的“自我与生命探索”课程。这门课排课很少，考核也很松，一周一次课，期末一篇论文。这样的排课与考核，就算是放在最无足轻重的课程里面也显得过于“水”了。但就是这样一门“水”课，却成为了我大学生活中直到现在最为印象深刻的课程之一。",
                "在课上，正如华老师第一次课上所说的：“将每周四下午的这门课作为你们的一个‘避风港’，无论你平时有多忙，都请你暂时停下来，给心灵一个喘息的机会”，我们看完了《心灵奇旅》、《狮子王》、《我与死亡侃侃而谈》，谈论生命的美与死亡的痛，追寻生活的价值与人生的意义。那段时间正好在读爱因斯坦写的《我的世界观》，书中有这样一句话：“完成同样一件工作对学生产生的教育方面的影响可能有很大不同，这取决于使他完成这件工作的内因究竟是害怕受伤害、利己主义的情感，还是获得喜悦和满足感”。生活的表象总是千篇一律的，我们上着同样的课，参加同样的考试，在可预见的未来同样是升学/就业等大同小异的结果。但完成同样的事情的背后有什么东西在驱动着自己，在过着每一天的生活的同时，心中又留存着如何的希冀，也许便是人与人之间最大的差异了吧。",
                "那么生活背后的动力是什么？生命的意义又是什么？追求千篇一律的人生显然不是，日复一日的枯燥生活更不是。人无论在生前还是死后都是自然间的无生命之物，若是在这短暂的几十年间不将生命的珍贵与独特表达出来，那便如病枝上的枯叶一般，悄无声息地就消逝了。“每个人死去，天空中便会划过一颗流星”，茫茫夜空之中，只有冲破大气层那短暂的瞬间才让流星于茫茫星海中独立出来，创造源于她的，独特的美；短暂的人生，也同样是我们作为一个血肉身躯，所拥有的唯一可以创造独特，展现自我创造力的机会。",
                "弗洛姆曾言：“爱是一种创造性的情感”。诚然，在健康的关系中，一方将自己的动能转化为创造力，将创造的结果一部分留给自己，另一部分给予对方。爱因斯坦所言“个人自由而又负责地发展，从而可以在服务全人类的过程中自由而快乐地发挥自己的能力”，大概也是这个意思。人的表达欲是不可压抑的，正如《精神分析引论》所言，“压抑的情感不会消失，而只是从意识中暂时抹去了，退居潜意识发挥不可忽视的作用”。表达与创造，确实是生命的最大价值之一，个人在创造的过程中自由地展现着自己的天性，同时又使自己的创造服务于社会，为社会做出贡献的同时又在这自然中留下了自己的印记，因此跨越了生死的边界而具有了超个人的意义。",
                "自由地表达与创造，将创造物的诞生作为劳动的唯一动机，个人便与自己的创造物相互联系，“人的最深切的需要就是克服分离，从而使他从孤独的囚牢中解脱出来”。在创造性的表达中，个人与创造物相联系，与社会相联系。而在社会达尔文主义之下，个人劳动的目的不再是劳动的结果本身，而是借助劳动的结果获取经济价值，创造物与个人相互分离，创造的目的不再是表达，而成了一个毫不相干的外界目标。而个体竞争又使得人脱离本应该归属于的环境，在“社会规则”这样一个新型权威面前，孤立的个体毫无力量，深感焦虑不安。",
                "弗洛伊德在一个世纪之前曾说，“精神分析揭露了人的原始冲动，与道德和美育的成见相冲突，因此，精神分析的理论是要受到非难的，是要被视为丑恶的、不道德的，或是危险的”，人的表达同样如此。但精神分析在揭露人心中的恶的同时，并没有否定人心中的善意。我一直持有这样一个观点，真正对于人的心理建设有价值的创造，应该是以非压抑的表达为目的的创造，创造若是脱离了表达本身便是空虚的，因为创造物并不和创造他的人相互关联——若得到一个创造物既不是我劳动的目标，创造的过程又并非属于我自己的表达，创造得到的结果也是千篇一律的模版作品，那我又有什么理由要去热爱我的创造物呢。",
                "那么回到之前的问题，生活背后的动力是什么，生活的意义是什么？对于后一个问题，暂时按下不表，生活仍旧很长，有大把的时间值得去追问这个问题，而不必在一段路途开始前就划定终点。对于前一个问题，我的回答是，生活的动力在于在创造的过程中自由地表达。在这样的的过程中，我与我的创造物相联系，我与社会，与世界紧密联系，我服务着社会，同时又自由地展现着个人的天性与创造力。用略带浪漫主义色彩的话来说，生命通过他所创造的东西而得以延续，因此跨越了生死的边界而具有了超个人的意义。",
                "而以表达为目的的创造，必然离不开一些重要的人与事物。在我的生命中，最重要的三件事物便是爱，知识与自由。",
                "正如弗洛姆所说：“爱是一种创造性的情感”。真挚的爱本身就是一种创造性的表达，我将自己的天性融入于创造物中，将创造物同时给予自己与对方，不仅能充分地发挥个人的创造性，还能不断地感受到在“表达—创造—给予”过程中的快乐，坚定一直克服困难走下去的决心。而这世间最为美好的情感如此之少，以至于难以寻得。感谢我的爸爸妈妈，我的亲人，以及我已经逝世的爷爷婆婆，能够使我时时刻刻生活在这样的情感之中。有的人虽然已经远去，但总有人会带着他们的爱，带着这独一份的珍贵的回忆活下去。别离不是永恒的结局，时间不是永恒的结局，死亡也不是永恒的结局。在最强壮的人面前，世间没有不可登上的山峰，在充满好奇的民族面前，世界上没有跨越不过的大洋，在最为真挚的情感面前，也不曾存在不可逾越的鸿沟。经历是真的，回忆是真的，感觉是真的，爱便是真的。在悲伤的现状与乐观的幻想之间，我宁愿去相信幻想。逝去的人仍然活着，只是不在这个世界；没有生命的人仍然是有生命的，只是不同于血肉之躯。无论所爱之人是否仍然在这个世界上，我都愿意相信他们仍然存在，每次回忆起过去的点滴的时候，无论是喜还是悲，是平淡还是热烈，也不论当下是在低谷还是在高峰，我都觉得他们仍然陪在我的身边。我愿相信造物主是有感情的，在两个不同的世界之间，总会留有一道桥梁将已经分别的心再次沟通。",
                "而要将思想上的表达转化为实际的创造，将最为真挚的情感转化为行动，知识与自由是必不可少的。知识使得人能够独立于自然，自由使得人能够独立于他人与社会；知识使得人知道应该怎样做，而自由使得人能够这样做。对于自由而言，引用普京在采访中的一句话“自由的边界是不侵犯他人的自由”，独立性始终是重要的，个人的自由若以牺牲对方同等的自由为代价，又有何“独立”可言呢。",
                "爱将独立的个人相互联系，使得个人的创造物能够有发挥价值的机会，知识将人与创造的手段相联系，自由使得创造能够以表达为内核。在此三者之上，创造性的表达将个人与社会相联系，将个人与创造物相联系，个人由此挣脱孤立的牢笼，获得自我的解放。",
                "正如爱因斯坦所说，“浮华之辞向来是通向毁灭的道路”，思想需要时刻实践才能永远熠熠生辉。为此，我也应尽自己的绵薄之力。",
            ];

            public static List<ThankYouCardModel> Cards =
            [
                new()
                {
                    CardTitle = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = CardTitle,
                        [LangType.ZhTw] = CardTitle,
                        [LangType.ZhHk] = CardTitle,
                        [LangType.EnUs] = CardTitle,
                        [LangType.EnGb] = CardTitle,
                        [LangType.JaJp] = CardTitle
                    }.ToTextModel(),
                    CardContent = CardContent.Select(paragraph =>
                    {
                        return new Dictionary<LangType, string>()
                        {
                            [LangType.ZhCn] = paragraph,
                            [LangType.ZhTw] = paragraph,
                            [LangType.ZhHk] = paragraph,
                            [LangType.EnUs] = paragraph,
                            [LangType.EnGb] = paragraph,
                            [LangType.JaJp] = paragraph
                        }.ToTextModel();
                    }).ToList(),
                    CardContentComment = new Dictionary<LangType, string>()
                    {
                        [LangType.ZhCn] = "初稿于2025年10月13日",
                        [LangType.ZhTw] = "初稿於2025年10月13日",
                        [LangType.ZhHk] = "初稿於2025年10月13日",
                        [LangType.EnUs] = "First draft on Oct 13, 2025",
                        [LangType.EnGb] = "First draft on Oct 13, 2025",
                        [LangType.JaJp] = "初稿 2025年10月13日"
                    }.ToTextModel(),
                }
            ];
        }

        #endregion

        #region Footer

        public static class Footer
        {
            #region Sections

            public static class Links
            {
                public const SectionType Type = SectionType.Normal;

                public static readonly Dictionary<LangType, string> SectionTitle = new()
                {
                    [LangType.ZhCn] = "链接",
                    [LangType.ZhTw] = "連結",
                    [LangType.ZhHk] = "連結",
                    [LangType.EnUs] = "Links",
                    [LangType.EnGb] = "Links",
                    [LangType.JaJp] = "リンク"
                };

                public static readonly Dictionary<LangType, string> FootHome = new()
                {
                    [LangType.ZhCn] = "首页",
                    [LangType.ZhTw] = "首頁",
                    [LangType.ZhHk] = "首頁",
                    [LangType.EnUs] = "Home",
                    [LangType.EnGb] = "Home",
                    [LangType.JaJp] = "ホーム"
                };

                public static readonly List<LinkItemModel> Cards =
                [
                    new()
                    {
                        SectionTitle = Links.SectionTitle.ToTextModel(),
                        ItemTitle = FootHome.ToTextModel(),
                        PictureCss = "fas fa-home",
                        Link = PathConfig.RootPath + "#" + AboutMe.SectionAnchor,
                    },
                    new()
                    {
                        SectionTitle = Links.SectionTitle.ToTextModel(),
                        ItemTitle = AboutMe.SectionTitle.ToTextModel(),
                        PictureCss = AboutMe.SectionCss,
                        Link = PathConfig.RootPath + "#" + AboutMe.SectionAnchor
                    },
                    new()
                    {
                        SectionTitle = Links.SectionTitle.ToTextModel(),
                        ItemTitle = Projects.SectionTitle.ToTextModel(),
                        PictureCss = Projects.SectionCss,
                        Link = PathConfig.RootPath + "#" + Projects.SectionAnchor
                    },
                    new()
                    {
                        SectionTitle = Links.SectionTitle.ToTextModel(),
                        ItemTitle = TechStack.SectionTitle.ToTextModel(),
                        PictureCss = TechStack.SectionCss,
                        Link = PathConfig.RootPath + "#" + TechStack.SectionAnchor
                    },
                    new()
                    {
                        SectionTitle = Links.SectionTitle.ToTextModel(),
                        ItemTitle = Hobbies.SectionTitle.ToTextModel(),
                        PictureCss = Hobbies.SectionCss,
                        Link = PathConfig.RootPath + "#" + Hobbies.SectionAnchor
                    },
                    new()
                    {
                        SectionTitle = Links.SectionTitle.ToTextModel(),
                        ItemTitle = Quotes.SectionTitle.ToTextModel(),
                        PictureCss = Quotes.SectionCss,
                        Link = PathConfig.RootPath + "#" + Quotes.SectionAnchor
                    },
                    new()
                    {
                        SectionTitle = Links.SectionTitle.ToTextModel(),
                        ItemTitle = Friends.SectionTitle.ToTextModel(),
                        PictureCss = Friends.SectionCss,
                        Link = PathConfig.RootPath + "#" + Friends.SectionAnchor
                    },
                ];
            }

            /// <summary>
            /// 备案信息
            /// </summary>
            public static class Record
            {
                public const SectionType Type = SectionType.Normal;

                public static readonly Dictionary<LangType, string> SectionTitle = new()
                {
                    [LangType.ZhCn] = "备案信息",
                    [LangType.ZhTw] = "備案信息",
                    [LangType.ZhHk] = "備案信息",
                    [LangType.EnUs] = "Record Information",
                    [LangType.EnGb] = "Record Information",
                    [LangType.JaJp] = "記録情報"
                };

                public static readonly List<LinkItemModel> Cards =
                [
                    new()
                    {
                        SectionTitle = Record.SectionTitle.ToTextModel(),
                        ItemTitle = new Dictionary<LangType, string>
                        {
                            [LangType.ZhCn] = "公安备案",
                            [LangType.ZhTw] = "公安備案",
                            [LangType.ZhHk] = "公安備案",
                            [LangType.EnUs] = "Public Security Record",
                            [LangType.EnGb] = "Public Security Record",
                            [LangType.JaJp] = "公安記録"
                        }.ToTextModel(),
                        PictureCss = "fas fa-shield-alt",
                        Link = "https://beian.miit.gov.cn/#/Integrated/index",
                    },
                    new()
                    {
                        SectionTitle = Record.SectionTitle.ToTextModel(),
                        ItemTitle = new Dictionary<LangType, string>
                        {
                            [LangType.ZhCn] = "网站源码",
                            [LangType.ZhTw] = "網站源碼",
                            [LangType.ZhHk] = "網站源碼",
                            [LangType.EnUs] = "Website Source",
                            [LangType.EnGb] = "Website Source",
                            [LangType.JaJp] = "ウェブサイトソース"
                        }.ToTextModel(),
                        PictureCss = "fas fa-code-branch",
                        Link = "https://github.com/QuetzalSidera/QuetzalSidera-Me-FrontBackend/",
                    },
                    new()
                    {
                        SectionTitle = Record.SectionTitle.ToTextModel(),
                        ItemTitle = new Dictionary<LangType, string>
                        {
                            [LangType.ZhCn] = "意见反馈",
                            [LangType.ZhTw] = "意見反饋",
                            [LangType.ZhHk] = "意見反饋",
                            [LangType.EnUs] = "Feedback",
                            [LangType.EnGb] = "Feedback",
                            [LangType.JaJp] = "フィードバック"
                        }.ToTextModel(),
                        PictureCss = "fas fa-comment-dots",
                        Link = "mailto:1969154690@qq.com",
                    },
                    new()
                    {
                        SectionTitle = Record.SectionTitle.ToTextModel(),
                        ItemTitle = Header.HeaderNav.ThankYouDict.ToTextModel(),
                        PictureCss = ThankYou.SectionCss,
                        Link = PathConfig.ThankYouPath,
                    },
                ];
            }

            public static class Follow
            {
                public const SectionType Type = SectionType.Sns;

                public static readonly Dictionary<LangType, string> SectionTitle = new()
                {
                    [LangType.ZhCn] = "关注",
                    [LangType.ZhTw] = "關注",
                    [LangType.ZhHk] = "關注",
                    [LangType.EnUs] = "Follow",
                    [LangType.EnGb] = "Follow",
                    [LangType.JaJp] = "フォロー"
                };

                public static readonly List<LinkItemModel> Cards =
                [
                    new()
                    {
                        SectionTitle = Record.SectionTitle.ToTextModel(),
                        PictureCss = "fab fa-github",
                        Link = "https://github.com/QuetzalSidera/",
                    },
                    new()
                    {
                        SectionTitle = Record.SectionTitle.ToTextModel(),
                        PictureCss = "fab fa-zhihu",
                        Link = "https://www.zhihu.com/people/33-80-62-74-84",
                    },
                    new()
                    {
                        SectionTitle = Record.SectionTitle.ToTextModel(),
                        PictureCss = "fab fa-bilibili",
                        Link = "https://space.bilibili.com/327475434",
                    },
                    new()
                    {
                        SectionTitle = Record.SectionTitle.ToTextModel(),
                        PictureCss = "fab fa-qq",
                        Link = "https://user.qzone.qq.com/1969154690/main",
                    },
                    new()
                    {
                        SectionTitle = Record.SectionTitle.ToTextModel(),
                        PictureCss = "fab fa-facebook-f",
                        Link = "https://www.facebook.com/share/1Lkuiugn5b/?mibextid=wwXIfr",
                    },
                    new()
                    {
                        SectionTitle = Record.SectionTitle.ToTextModel(),
                        PictureCss = "fab fa-instagram",
                        Link = "https://www.instagram.com/eternal.hearted?igsh=bTNoOWN2ZjZldXM4&utm_source=qr",
                    },
                    new()
                    {
                        SectionTitle = Record.SectionTitle.ToTextModel(),
                        PictureCss = "fas fa-envelope",
                        Link = "mailto:1969154690@qq.com",
                    },
                ];
            }

            #endregion

            #region FootComment

            public static class FootComment
            {
                public const string Link = "https://beian.miit.gov.cn/#/Integrated/index";

                public static readonly Dictionary<LangType, string> Text = new()
                {
                    [LangType.ZhCn] = "© 版权所有 2025 QianShuang | 粤ICP备2025477459号 | 粤公网安备44030002008220号 | 天气数据来自和风天气",
                    [LangType.ZhTw] = "© 版權所有 2025 QianShuang | 粵ICP備2025477459號 | 粵公網安備44030002008220號 | 天氣數據來自和風天氣",
                    [LangType.ZhHk] = "© 版權所有 2025 QianShuang | 粵ICP備2025477459號 | 粵公網安備44030002008220號 |天氣數據來自和風天氣",
                    [LangType.EnUs] =
                        "© All Rights Reserved 2025 QianShuang | Guangdong ICP No. 2025477459 | Guangdong PSB Record No. 44030002008220 | Weather data from HeWeather",
                    [LangType.EnGb] =
                        "© All Rights Reserved 2025 QianShuang | Guangdong ICP No. 2025477459 | Guangdong PSB Record No. 44030002008220 |Weather data from HeWeather",
                    [LangType.JaJp] = "© 著作権所有 2025 QianShuang | 広東省ICP第2025477459号 | 広東公安ネット記録 44030002008220\" | 天気データはHeWeatherから提供"
                };
            }
            
            #endregion
        }

        #endregion
    }
}