using Grpc.Share.Tools;
using Protobuf.Shared.Text;

namespace Grpc.Share.Config.Chat;

public static class ConfigData
{
    public static class LangUtils
    {
        public static readonly Dictionary<LangType, string> CurrentLanguage = new()
        {
            [LangType.ZhCn] = "当前语言",
            [LangType.ZhTw] = "當前語言",
            [LangType.ZhHk] = "當前語言",
            [LangType.EnUs] = "Current Language",
            [LangType.EnGb] = "Current Language",
            [LangType.JaJp] = "現在の言語"
        };

        public static readonly Dictionary<LangType, string> Title = new()
        {
            [LangType.ZhCn] = "A.L.I.C.E",
            [LangType.ZhTw] = "A.L.I.C.E",
            [LangType.ZhHk] = "A.L.I.C.E",
            [LangType.EnUs] =  "A.L.I.C.E",
            [LangType.EnGb] =  "A.L.I.C.E",
            [LangType.JaJp] =  "A.L.I.C.E",
        };
        
        
        public static readonly Dictionary<LangType, string> TendouAlice = new()
        {
            [LangType.ZhCn] = "天童爱丽丝",
            [LangType.ZhTw] = "天童愛麗絲",
            [LangType.ZhHk] = "天童愛麗絲",
            [LangType.EnUs] = "Alice Tendou",
            [LangType.EnGb] = "Alice Tendou",
            [LangType.JaJp] = "天童アリス"
        };

        public static readonly Dictionary<LangType, string> BangBangKaBang = new()
        {
            [LangType.ZhCn] = "邦邦卡邦",
            [LangType.ZhTw] = "邦邦卡邦",
            [LangType.ZhHk] = "邦邦卡邦",
            [LangType.EnUs] = "Bang Bang Ka Bang",
            [LangType.EnGb] = "Bang Bang Ka Bang",
            [LangType.JaJp] = "バンバンカバン"
        };

        public static readonly Dictionary<LangType, string> TeacherWelcome = new()
        {
            [LangType.ZhCn] = "老师！你可算来基沃托斯啦！",
            [LangType.ZhTw] = "老師！你終於來到基沃托斯囉！",
            [LangType.ZhHk] = "老師！你終於嚟到基沃托斯喇！",
            [LangType.EnUs] = "Sensei! Welcome to Kivotos",
            [LangType.EnGb] = "Sensei! Welcome to Kivotos",
            [LangType.JaJp] = "先生！キヴォトスに着いたね！"
        };

        public static readonly Dictionary<LangType, string> NewConversation = new()
        {
            [LangType.ZhCn] = "新建对话",
            [LangType.ZhTw] = "新建對話",
            [LangType.ZhHk] = "新建對話",
            [LangType.EnUs] = "New Conversation",
            [LangType.EnGb] = "New Conversation",
            [LangType.JaJp] = "新規会話"
        };

        public static readonly Dictionary<LangType, string> NewConversationTitle = new()
        {
            [LangType.ZhCn] = "新对话",
            [LangType.ZhTw] = "新對話",
            [LangType.ZhHk] = "新對話",
            [LangType.EnUs] = "New Chat",
            [LangType.EnGb] = "New Chat",
            [LangType.JaJp] = "新規チャット"
        };

        public static readonly Dictionary<LangType, string> ConversationHistory = new()
        {
            [LangType.ZhCn] = "对话历史",
            [LangType.ZhTw] = "對話歷史",
            [LangType.ZhHk] = "對話歷史",
            [LangType.EnUs] = "Conversation History",
            [LangType.EnGb] = "Conversation History",
            [LangType.JaJp] = "会話履歴"
        };

        public static readonly Dictionary<LangType, string> NoConversationRecords = new()
        {
            [LangType.ZhCn] = "暂无对话记录",
            [LangType.ZhTw] = "暫無對話記錄",
            [LangType.ZhHk] = "暫無對話記錄",
            [LangType.EnUs] = "No Conversation Records",
            [LangType.EnGb] = "No Conversation Records",
            [LangType.JaJp] = "会話記録がありません"
        };

        public static readonly Dictionary<LangType, string> LetsChat = new()
        {
            [LangType.ZhCn] = "聊点什么吧~",
            [LangType.ZhTw] = "聊聊吧～",
            [LangType.ZhHk] = "傾下偈啦～",
            [LangType.EnUs] = "Let's talk!",
            [LangType.EnGb] = "Fancy a chat?",
            [LangType.JaJp] = "こんにちは！"
        };

        public static readonly Dictionary<LangType, string> UserAvatar = new()
        {
            [LangType.ZhCn] = "用户头像",
            [LangType.ZhTw] = "用戶頭像",
            [LangType.ZhHk] = "用戶頭像",
            [LangType.EnUs] = "Avatar",
            [LangType.EnGb] = "Avatar",
            [LangType.JaJp] = "アバター"
        };

        public static readonly Dictionary<LangType, string> AiAssistant = new()
        {
            [LangType.ZhCn] = "AI助手",
            [LangType.ZhTw] = "AI助手",
            [LangType.ZhHk] = "AI助手",
            [LangType.EnUs] = "AI Assistant",
            [LangType.EnGb] = "AI Assistant",
            [LangType.JaJp] = "AIアシスタント"
        };

        public static readonly Dictionary<LangType, string> SyncAfterLogin = new()
        {
            [LangType.ZhCn] = "登录后同步对话记录",
            [LangType.ZhTw] = "登入後同步對話記錄",
            [LangType.ZhHk] = "登入後同步對話記錄",
            [LangType.EnUs] = "Login to Sync Records",
            [LangType.EnGb] = "Login to Sync Records",
            [LangType.JaJp] = "ログインで同期"
        };

        public static readonly Dictionary<LangType, string> LoginRegister = new()
        {
            [LangType.ZhCn] = "登录/注册",
            [LangType.ZhTw] = "登入/註冊",
            [LangType.ZhHk] = "登入/註冊",
            [LangType.EnUs] = "Login/Register",
            [LangType.EnGb] = "Login/Register",
            [LangType.JaJp] = "ログイン/登録"
        };

        public static readonly Dictionary<LangType, string> LogoutAccount = new()
        {
            [LangType.ZhCn] = "登出",
            [LangType.ZhTw] = "登出",
            [LangType.ZhHk] = "登出",
            [LangType.EnUs] = "Logout",
            [LangType.EnGb] = "Logout",
            [LangType.JaJp] = "ログアウト"
        };

        public static readonly Dictionary<LangType, string> DeleteAccount = new()
        {
            [LangType.ZhCn] = "注销",
            [LangType.ZhTw] = "註銷",
            [LangType.ZhHk] = "註銷",
            [LangType.EnUs] = "Delete Account",
            [LangType.EnGb] = "Delete Account",
            [LangType.JaJp] = "アカウント削除"
        };

        public static readonly Dictionary<LangType, string> DeleteAccountTitle = new()
        {
            [LangType.ZhCn] = "注销账户",
            [LangType.ZhTw] = "註銷帳戶",
            [LangType.ZhHk] = "註銷帳戶",
            [LangType.EnUs] = "Delete Account",
            [LangType.EnGb] = "Delete Account",
            [LangType.JaJp] = "アカウント削除"
        };

        public static readonly Dictionary<LangType, string> LeavingMessage = new()
        {
            [LangType.ZhCn] = "要走了吗 (´；ω；`)",
            [LangType.ZhTw] = "要走了嗎 (´；ω；`)",
            [LangType.ZhHk] = "要走了嗎 (´；ω；`)",
            [LangType.EnUs] = "Are you leaving? (´；ω；`)",
            [LangType.EnGb] = "Are you leaving? (´；ω；`)",
            [LangType.JaJp] = "もう行っちゃうの？(´；ω；`)"
        };

        public static readonly Dictionary<LangType, string> Loading = new()
        {
            [LangType.ZhCn] = "加载中...",
            [LangType.ZhTw] = "載入中...",
            [LangType.ZhHk] = "載入中...",
            [LangType.EnUs] = "Loading...",
            [LangType.EnGb] = "Loading...",
            [LangType.JaJp] = "読み込み中..."
        };

        public static readonly Dictionary<LangType, string> Loading2 = new()
        {
            [LangType.ZhCn] = "加载中",
            [LangType.ZhTw] = "載入中",
            [LangType.ZhHk] = "載入中",
            [LangType.EnUs] = "Loading",
            [LangType.EnGb] = "Loading",
            [LangType.JaJp] = "読み込み中"
        };
        public static readonly Dictionary<LangType, string> CreatedAt = new()
        {
            [LangType.ZhCn] = "创建于",
            [LangType.ZhTw] = "建立於",
            [LangType.ZhHk] = "建立於",
            [LangType.EnUs] = "Created at",
            [LangType.EnGb] = "Created at",
            [LangType.JaJp] = "作成日時"
        };

        public static readonly Dictionary<LangType, string> CopyrightInfo = new()
        {
            [LangType.ZhCn] = "© 版权所有 2025 QianShuang",
            [LangType.ZhTw] = "© 版權所有 2025 QianShuang",
            [LangType.ZhHk] = "© 版權所有 2025 QianShuang ",
            [LangType.EnUs] = "© Copyright 2025 QianShuang",
            [LangType.EnGb] = "© Copyright 2025 QianShuang",
            [LangType.JaJp] = "© 著作権所有 2025 QianShuang"
        };
        
        public static readonly Dictionary<LangType, string> IcpRecordInfo = new()
        {
            [LangType.ZhCn] = "粤ICP备2025477459号",
            [LangType.ZhTw] = "粵ICP備2025477459號",
            [LangType.ZhHk] = "粵ICP備2025477459號",
            [LangType.EnUs] = "Guangdong ICP No. 2025477459",
            [LangType.EnGb] = "Guangdong ICP No. 2025477459",
            [LangType.JaJp] = "広東省ICP No. 2025477459"
        };
        
        public static readonly Dictionary<LangType, string> PsbRecordInfo = new()
        {
            [LangType.ZhCn] = "粤公网安备44030002008220号",
            [LangType.ZhTw] = "粵公網安備44030002008220號",
            [LangType.ZhHk] = "粵公網安備44030002008220號",
            [LangType.EnUs] = "Guangdong PSB Record No. 44030002008220",
            [LangType.EnGb] = "Guangdong PSB Record No. 44030002008220",
            [LangType.JaJp] = "広東公安ネット記録 44030002008220"
        };
        public static readonly Dictionary<LangType, string> PrivacyDeclare = new()
        {
            [LangType.ZhCn] = "用户协议 | 隐私声明",
            [LangType.ZhTw] = "用戶協議 | 隱私聲明",
            [LangType.ZhHk] = "用戶協議 | 隱私聲明",
            [LangType.EnUs] = "Terms | Privacy",
            [LangType.EnGb] = "Terms | Privacy",
            [LangType.JaJp] = "利用規約 | プライバシー"
        };

        public static readonly Dictionary<LangType, string> CopyrightAddition = new()
        {
            [LangType.ZhCn] = "© 人物及故事情节著作权归NEXON Games所有",
            [LangType.ZhTw] = "© 人物及故事情節著作權歸NEXON Games所有",
            [LangType.ZhHk] = "© 人物及故事情節著作權歸NEXON Games所有",
            [LangType.EnUs] = "© Characters & Story NEXON Games",
            [LangType.EnGb] = "© Characters & Story NEXON Games",
            [LangType.JaJp] = "© キャラクター及びストーリーの著作権はNEXON Gamesに帰属"
        };

        public static readonly Dictionary<LangType, string> AiGeneratedContentNotice = new()
        {
            [LangType.ZhCn] = "AI生成内容仅供参考",
            [LangType.ZhTw] = "AI生成內容僅供參考",
            [LangType.ZhHk] = "AI生成內容僅供參考",
            [LangType.EnUs] = "AI-generated content is for reference only",
            [LangType.EnGb] = "AI-generated content is for reference only",
            [LangType.JaJp] = "AI生成コンテンツは参考までにご利用ください"
        };

        public static readonly Dictionary<LangType, string> WelcomeTitle = new()
        {
            [LangType.ZhCn] = "AL-1S连接已建立",
            [LangType.ZhTw] = "AL-1S連接已建立",
            [LangType.ZhHk] = "AL-1S連接已建立",
            [LangType.EnUs] = "AL-1S Online",
            [LangType.EnGb] = "AL-1S Online",
            [LangType.JaJp] = "AL-1S接続完了"
        };

        public static readonly Dictionary<LangType, string> WelcomeIdentity = new()
        {
            [LangType.ZhCn] = "千禧学园·游戏开发部·爱丽丝",
            [LangType.ZhTw] = "千禧學園·遊戲開發部·愛麗絲",
            [LangType.ZhHk] = "千禧學園·遊戲開發部·愛麗絲",
            [LangType.EnUs] = "Alice - Game Dev Club",
            [LangType.EnGb] = "Alice - Game Dev Club",
            [LangType.JaJp] = "ミレニアム・ゲーム開発部・アリス"
        };

        public static readonly Dictionary<LangType, string> WelcomeGreeting = new()
        {
            [LangType.ZhCn] = "邦邦卡邦~",
            [LangType.ZhTw] = "邦邦卡邦~",
            [LangType.ZhHk] = "邦邦卡邦~",
            [LangType.EnUs] = "Hello Sensei!",
            [LangType.EnGb] = "Hello Sensei!",
            [LangType.JaJp] = "先生、こんにちは！"
        };

        public static readonly Dictionary<LangType, string> StartFirstChat = new()
        {
            [LangType.ZhCn] = "开始第一条对话",
            [LangType.ZhTw] = "開始第一條對話",
            [LangType.ZhHk] = "開始第一條對話",
            [LangType.EnUs] = "Start First Chat",
            [LangType.EnGb] = "Start First Chat",
            [LangType.JaJp] = "最初の会話を始める"
        };

        public static readonly Dictionary<LangType, string> StatusDeepSeek = new()
        {
            [LangType.ZhCn] = "深度求索 API",
            [LangType.ZhTw] = "深度求索 API",
            [LangType.ZhHk] = "深度求索 API",
            [LangType.EnUs] = "Deepseek API",
            [LangType.EnGb] = "Deepseek API",
            [LangType.JaJp] = "Deepseek API"
        };

        public static readonly Dictionary<LangType, string> StatusStreaming = new()
        {
            [LangType.ZhCn] = "流式输出",
            [LangType.ZhTw] = "串流輸出",
            [LangType.ZhHk] = "串流輸出",
            [LangType.EnUs] = "Streaming",
            [LangType.EnGb] = "Streaming",
            [LangType.JaJp] = "ストリーミング"
        };

        public static readonly Dictionary<LangType, string> StatusMemory = new()
        {
            [LangType.ZhCn] = "记忆存档",
            [LangType.ZhTw] = "記憶存檔",
            [LangType.ZhHk] = "記憶存檔",
            [LangType.EnUs] = "Memory",
            [LangType.EnGb] = "Memory",
            [LangType.JaJp] = "メモリ"
        };

        public static readonly Dictionary<LangType, string> StatusMillennium = new()
        {
            [LangType.ZhCn] = "千禧科技",
            [LangType.ZhTw] = "千禧科技",
            [LangType.ZhHk] = "千禧科技",
            [LangType.EnUs] = "Millennium",
            [LangType.EnGb] = "Millennium",
            [LangType.JaJp] = "ミレニアム"
        };

        public static readonly Dictionary<LangType, string> LoginAccount = new()
        {
            [LangType.ZhCn] = "登录账户",
            [LangType.ZhTw] = "登入帳戶",
            [LangType.ZhHk] = "登入帳戶",
            [LangType.EnUs] = "Login",
            [LangType.EnGb] = "Login",
            [LangType.JaJp] = "ログイン"
        };

        public static readonly Dictionary<LangType, string> RegisterAccount = new()
        {
            [LangType.ZhCn] = "注册账户",
            [LangType.ZhTw] = "註冊帳戶",
            [LangType.ZhHk] = "註冊帳戶",
            [LangType.EnUs] = "Register",
            [LangType.EnGb] = "Register",
            [LangType.JaJp] = "新規登録"
        };

        public static readonly Dictionary<LangType, string> VerifyCodeLogin = new()
        {
            [LangType.ZhCn] = "验证码登录",
            [LangType.ZhTw] = "驗證碼登入",
            [LangType.ZhHk] = "驗證碼登入",
            [LangType.EnUs] = "Code Login",
            [LangType.EnGb] = "Code Login",
            [LangType.JaJp] = "コード認証"
        };

        public static readonly Dictionary<LangType, string> Email = new()
        {
            [LangType.ZhCn] = "邮箱",
            [LangType.ZhTw] = "信箱",
            [LangType.ZhHk] = "電郵",
            [LangType.EnUs] = "Email",
            [LangType.EnGb] = "Email",
            [LangType.JaJp] = "メール"
        };

        public static readonly Dictionary<LangType, string> EnterEmail = new()
        {
            [LangType.ZhCn] = "请输入您的邮箱",
            [LangType.ZhTw] = "請輸入您的信箱",
            [LangType.ZhHk] = "請輸入您的電郵",
            [LangType.EnUs] = "Enter Email",
            [LangType.EnGb] = "Enter Email",
            [LangType.JaJp] = "メールを入力"
        };

        public static readonly Dictionary<LangType, string> InvalidEmail = new()
        {
            [LangType.ZhCn] = "邮箱格式不正确",
            [LangType.ZhTw] = "信箱格式不正確",
            [LangType.ZhHk] = "電郵格式不正確",
            [LangType.EnUs] = "Invalid Email",
            [LangType.EnGb] = "Invalid Email",
            [LangType.JaJp] = "メール形式エラー"
        };

        public static readonly Dictionary<LangType, string> Password = new()
        {
            [LangType.ZhCn] = "密码",
            [LangType.ZhTw] = "密碼",
            [LangType.ZhHk] = "密碼",
            [LangType.EnUs] = "Password",
            [LangType.EnGb] = "Password",
            [LangType.JaJp] = "パスワード"
        };

        public static readonly Dictionary<LangType, string> RequiredField = new()
        {
            [LangType.ZhCn] = "此项为必填",
            [LangType.ZhTw] = "此項為必填",
            [LangType.ZhHk] = "此項為必填",
            [LangType.EnUs] = "Required",
            [LangType.EnGb] = "Required",
            [LangType.JaJp] = "必須項目"
        };

        public static readonly Dictionary<LangType, string> LoginError = new()
        {
            [LangType.ZhCn] = "用户名或密码错误",
            [LangType.ZhTw] = "用戶名或密碼錯誤",
            [LangType.ZhHk] = "用戶名或密碼錯誤",
            [LangType.EnUs] = "Login Failed",
            [LangType.EnGb] = "Login Failed",
            [LangType.JaJp] = "ログイン失敗"
        };

        public static readonly Dictionary<LangType, string> EmailOrCodeError = new()
        {
            [LangType.ZhCn] = "邮箱或验证码有误",
            [LangType.ZhTw] = "信箱或驗證碼有誤",
            [LangType.ZhHk] = "電郵或驗證碼有誤",
            [LangType.EnUs] = "Invalid Email or Code",
            [LangType.EnGb] = "Invalid Email or Code",
            [LangType.JaJp] = "メールまたはコードエラー"
        };

        public static readonly Dictionary<LangType, string> LoginButton = new()
        {
            [LangType.ZhCn] = "登录",
            [LangType.ZhTw] = "登入",
            [LangType.ZhHk] = "登入",
            [LangType.EnUs] = "Login",
            [LangType.EnGb] = "Login",
            [LangType.JaJp] = "ログイン"
        };

        public static readonly Dictionary<LangType, string> EnterPassword = new()
        {
            [LangType.ZhCn] = "请输入您的密码",
            [LangType.ZhTw] = "請輸入您的密碼",
            [LangType.ZhHk] = "請輸入您的密碼",
            [LangType.EnUs] = "Enter Password",
            [LangType.EnGb] = "Enter Password",
            [LangType.JaJp] = "パスワードを入力"
        };

        public static readonly Dictionary<LangType, string> Nickname = new()
        {
            [LangType.ZhCn] = "昵称",
            [LangType.ZhTw] = "暱稱",
            [LangType.ZhHk] = "暱稱",
            [LangType.EnUs] = "Nickname",
            [LangType.EnGb] = "Nickname",
            [LangType.JaJp] = "ニックネーム"
        };

        public static readonly Dictionary<LangType, string> EnterNickname = new()
        {
            [LangType.ZhCn] = "请输入您的昵称",
            [LangType.ZhTw] = "請輸入您的暱稱",
            [LangType.ZhHk] = "請輸入您的暱稱",
            [LangType.EnUs] = "Enter Nickname",
            [LangType.EnGb] = "Enter Nickname",
            [LangType.JaJp] = "ニックネームを入力"
        };

        public static readonly Dictionary<LangType, string> ConfirmPassword = new()
        {
            [LangType.ZhCn] = "确认密码",
            [LangType.ZhTw] = "確認密碼",
            [LangType.ZhHk] = "確認密碼",
            [LangType.EnUs] = "Confirm",
            [LangType.EnGb] = "Confirm",
            [LangType.JaJp] = "確認"
        };

        public static readonly Dictionary<LangType, string> PleaseConfirmYourPassword = new()
        {
            [LangType.ZhCn] = "请确认您的密码",
            [LangType.ZhTw] = "請確認您的密碼",
            [LangType.ZhHk] = "請確認您的密碼",
            [LangType.EnUs] = "Confirm Password",
            [LangType.EnGb] = "Confirm Password",
            [LangType.JaJp] = "パスワードを確認"
        };

        public static readonly Dictionary<LangType, string> PasswordMismatch = new()
        {
            [LangType.ZhCn] = "两次密码不相同",
            [LangType.ZhTw] = "兩次密碼不相同",
            [LangType.ZhHk] = "兩次密碼不相同",
            [LangType.EnUs] = "Passwords Mismatch",
            [LangType.EnGb] = "Passwords Mismatch",
            [LangType.JaJp] = "パスワード不一致"
        };

        public static readonly Dictionary<LangType, string> VerificationCode = new()
        {
            [LangType.ZhCn] = "邮箱验证码",
            [LangType.ZhTw] = "信箱驗證碼",
            [LangType.ZhHk] = "電郵驗證碼",
            [LangType.EnUs] = "Verification Code",
            [LangType.EnGb] = "Verification Code",
            [LangType.JaJp] = "認証コード"
        };

        public static readonly Dictionary<LangType, string> EnterVerificationCode = new()
        {
            [LangType.ZhCn] = "请输入验证码",
            [LangType.ZhTw] = "請輸入驗證碼",
            [LangType.ZhHk] = "請輸入驗證碼",
            [LangType.EnUs] = "Enter Verify Code",
            [LangType.EnGb] = "Enter Verify Code",
            [LangType.JaJp] = "認証コードを入力"
        };

        public static readonly Dictionary<LangType, string> ResendCode = new()
        {
            [LangType.ZhCn] = "重发",
            [LangType.ZhTw] = "重發",
            [LangType.ZhHk] = "重發",
            [LangType.EnUs] = "Resend",
            [LangType.EnGb] = "Resend",
            [LangType.JaJp] = "再送信"
        };

        public static readonly Dictionary<LangType, string> SendCode = new()
        {
            [LangType.ZhCn] = "发送验证码",
            [LangType.ZhTw] = "發送驗證碼",
            [LangType.ZhHk] = "發送驗證碼",
            [LangType.EnUs] = "Send Code",
            [LangType.EnGb] = "Send Code",
            [LangType.JaJp] = "コード送信"
        };

        public static readonly Dictionary<LangType, string> RegisterButton = new()
        {
            [LangType.ZhCn] = "注册",
            [LangType.ZhTw] = "註冊",
            [LangType.ZhHk] = "註冊",
            [LangType.EnUs] = "Register",
            [LangType.EnGb] = "Register",
            [LangType.JaJp] = "登録"
        };

        public static readonly Dictionary<LangType, string> NoAccountRegister = new()
        {
            [LangType.ZhCn] = "没有账户？立即注册",
            [LangType.ZhTw] = "沒有帳戶？立即註冊",
            [LangType.ZhHk] = "沒有帳戶？立即註冊",
            [LangType.EnUs] = "No account? Register",
            [LangType.EnGb] = "No account? Register",
            [LangType.JaJp] = "アカウント登録"
        };

        public static readonly Dictionary<LangType, string> HaveAccountLogin = new()
        {
            [LangType.ZhCn] = "已有账户？立即登录",
            [LangType.ZhTw] = "已有帳戶？立即登入",
            [LangType.ZhHk] = "已有帳戶？立即登入",
            [LangType.EnUs] = "Have account? Login",
            [LangType.EnGb] = "Have account? Login",
            [LangType.JaJp] = "ログイン"
        };

        public static readonly Dictionary<LangType, string> PasswordLogin = new()
        {
            [LangType.ZhCn] = "密码登录",
            [LangType.ZhTw] = "密碼登入",
            [LangType.ZhHk] = "密碼登入",
            [LangType.EnUs] = "Password Login",
            [LangType.EnGb] = "Password Login",
            [LangType.JaJp] = "パスワード認証"
        };

        public static readonly Dictionary<LangType, string> CodeLogin = new()
        {
            [LangType.ZhCn] = "验证码登录",
            [LangType.ZhTw] = "驗證碼登入",
            [LangType.ZhHk] = "驗證碼登入",
            [LangType.EnUs] = "Code Login",
            [LangType.EnGb] = "Code Login",
            [LangType.JaJp] = "コード認証"
        };

        public static readonly Dictionary<LangType, string> InputYourQuestion = new()
        {
            [LangType.ZhCn] = "输入您的问题",
            [LangType.ZhTw] = "輸入您的問題",
            [LangType.ZhHk] = "輸入您的問題",
            [LangType.EnUs] = "Ask a question",
            [LangType.EnGb] = "Ask a question",
            [LangType.JaJp] = "質問を入力"
        };

        public static readonly Dictionary<LangType, string> Thinking = new()
        {
            [LangType.ZhCn] = "爱丽丝正在思考...",
            [LangType.ZhTw] = "愛麗絲正在思考...",
            [LangType.ZhHk] = "愛麗絲正在思考...",
            [LangType.EnUs] = "Alice is thinking...",
            [LangType.EnGb] = "Alice is thinking...",
            [LangType.JaJp] = "アリスは考え中..."
        };

        public static readonly Dictionary<LangType, string> ConfirmDelete = new()
        {
            [LangType.ZhCn] = "确认注销",
            [LangType.ZhTw] = "確認註銷",
            [LangType.ZhHk] = "確認註銷",
            [LangType.EnUs] = "Confirm Delete",
            [LangType.EnGb] = "Confirm Delete",
            [LangType.JaJp] = "削除を確認"
        };

        public static readonly Dictionary<LangType, string> DeleteWarning = new()
        {
            [LangType.ZhCn] = "警告：此操作无法撤销，您将永久失去账户中的所有数据，包括个人资料、设置和历史记录",
            [LangType.ZhTw] = "警告：此操作無法撤銷，您將永久失去帳戶中的所有數據，包括個人資料、設定和歷史記錄",
            [LangType.ZhHk] = "警告：此操作無法撤銷，您將永久失去帳戶中的所有數據，包括個人資料、設定和歷史記錄",
            [LangType.EnUs] =
                "Warning: This action cannot be undone. You will permanently lose all data in your account, including profile, settings and history",
            [LangType.EnGb] =
                "Warning: This action cannot be undone. You will permanently lose all data in your account, including profile, settings and history",
            [LangType.JaJp] = "警告：この操作は取り消せません。プロフィール、設定、履歴を含むアカウント内の全データを永久に失います"
        };

        public static readonly Dictionary<LangType, string> Acknowledge = new()
        {
            [LangType.ZhCn] = "我已知悉",
            [LangType.ZhTw] = "我已知悉",
            [LangType.ZhHk] = "我已知悉",
            [LangType.EnUs] = "I Understand",
            [LangType.EnGb] = "I Understand",
            [LangType.JaJp] = "理解しました"
        };

        public static readonly Dictionary<LangType, string> AgreementRead = new()
        {
            [LangType.ZhCn] = "已阅读并同意",
            [LangType.ZhTw] = "已閱讀並同意",
            [LangType.ZhHk] = "已閱讀並同意",
            [LangType.EnUs] = "I have read and agree to the",
            [LangType.EnGb] = "I have read and agree to the",
            [LangType.JaJp] = "を読み同意しました"
        };

        public static readonly Dictionary<LangType, string> UserAgreementPrivacy = new()
        {
            [LangType.ZhCn] = "用户协议与隐私声明",
            [LangType.ZhTw] = "用戶協議與隱私聲明",
            [LangType.ZhHk] = "用戶協議與隱私聲明",
            [LangType.EnUs] = "User Agreement and Privacy Policy",
            [LangType.EnGb] = "User Agreement and Privacy Policy",
            [LangType.JaJp] = "利用規約とプライバシーポリシー"
        };
    }
}