using Grpc.Share.Protos.SharedModels;
using Protobuf.Shared.Text;


namespace Grpc.Share.Tools;

public static class LangHelper
{
    /// <summary>
    /// 将枚举转化为标准的Html格式字符串
    /// </summary>
    /// <param name="lang"></param>
    /// <returns>zh-CN，en-US等</returns>
    public static string ToHtmlLang(this LangType lang)
    {
        return lang switch
        {
            LangType.ZhCn => "zh-CN", // 简体中文(中国)
            LangType.ZhTw => "zh-TW", // 繁体中文(台湾地区)
            LangType.ZhHk => "zh-HK", // 繁体中文(香港)
            LangType.EnHk => "en-HK", // 英语(香港)
            LangType.EnUs => "en-US", // 英语(美国)
            LangType.EnGb => "en-GB", // 英语(英国)
            LangType.EnWw => "en-WW", // 英语(全球)
            LangType.EnCa => "en-CA", // 英语(加拿大)
            LangType.EnAu => "en-AU", // 英语(澳大利亚)
            LangType.EnIe => "en-IE", // 英语(爱尔兰)
            LangType.EnFi => "en-FI", // 英语(芬兰)
            LangType.FiFi => "fi-FI", // 芬兰语(芬兰)
            LangType.EnDk => "en-DK", // 英语(丹麦)
            LangType.DaDk => "da-DK", // 丹麦语(丹麦)
            LangType.EnIl => "en-IL", // 英语(以色列)
            LangType.HeIl => "he-IL", // 希伯来语(以色列)
            LangType.EnZa => "en-ZA", // 英语(南非)
            LangType.EnIn => "en-IN", // 英语(印度)
            LangType.EnNo => "en-NO", // 英语(挪威)
            LangType.EnSg => "en-SG", // 英语(新加坡)
            LangType.EnNz => "en-NZ", // 英语(新西兰)
            LangType.EnId => "en-ID", // 英语(印度尼西亚)
            LangType.EnPh => "en-PH", // 英语(菲律宾)
            LangType.EnTh => "en-TH", // 英语(泰国)
            LangType.EnMy => "en-MY", // 英语(马来西亚)
            LangType.EnXa => "en-XA", // 英语(阿拉伯)
            LangType.KoKr => "ko-KR", // 韩文(韩国)
            LangType.JaJp => "ja-JP", // 日语(日本)
            LangType.NlNl => "nl-NL", // 荷兰语(荷兰)
            LangType.NlBe => "nl-BE", // 荷兰语(比利时)
            LangType.PtPt => "pt-PT", // 葡萄牙语(葡萄牙)
            LangType.PtBr => "pt-BR", // 葡萄牙语(巴西)
            LangType.FrFr => "fr-FR", // 法语(法国)
            LangType.FrLu => "fr-LU", // 法语(卢森堡)
            LangType.FrCh => "fr-CH", // 法语(瑞士)
            LangType.FrBe => "fr-BE", // 法语(比利时)
            LangType.FrCa => "fr-CA", // 法语(加拿大)
            LangType.EsLa => "es-LA", // 西班牙语(拉丁美洲)
            LangType.EsEs => "es-ES", // 西班牙语(西班牙)
            LangType.EsAr => "es-AR", // 西班牙语(阿根廷)
            LangType.EsUs => "es-US", // 西班牙语(美国)
            LangType.EsMx => "es-MX", // 西班牙语(墨西哥)
            LangType.EsCo => "es-CO", // 西班牙语(哥伦比亚)
            LangType.EsPr => "es-PR", // 西班牙语(波多黎各)
            LangType.DeDe => "de-DE", // 德语(德国)
            LangType.DeAt => "de-AT", // 德语(奥地利)
            LangType.DeCh => "de-CH", // 德语(瑞士)
            LangType.RuRu => "ru-RU", // 俄语(俄罗斯)
            LangType.ItIt => "it-IT", // 意大利语(意大利)
            LangType.ElGr => "el-GR", // 希腊语(希腊)
            LangType.NoNo => "no-NO", // 挪威语(挪威)
            LangType.HuHu => "hu-HU", // 匈牙利语(匈牙利)
            LangType.TrTr => "tr-TR", // 土耳其语(土耳其)
            LangType.CsCz => "cs-CZ", // 捷克语(捷克共和国)
            LangType.SlSl => "sl-SL", // 斯洛文尼亚语
            LangType.PlPl => "pl-PL", // 波兰语(波兰)
            LangType.SvSe => "sv-SE", // 瑞典语(瑞典)
            _ => "zh-CN" // 默认返回简体中文(中国)
        };
    }
    
    /// <summary>
    /// 将标准的Html格式字符串转化为枚举
    /// </summary>
    /// <param name="htmlLang">zh-CN，en-US等</param>
    /// <returns>对应的LangType枚举值</returns>
    public static LangType ToLangType(this string htmlLang)
    {
        return htmlLang switch
        {
            "zh-CN" => LangType.ZhCn, // 简体中文(中国)
            "zh-TW" => LangType.ZhTw, // 繁体中文(台湾地区)
            "zh-HK" => LangType.ZhHk, // 繁体中文(香港)
            "en-HK" => LangType.EnHk, // 英语(香港)
            "en-US" => LangType.EnUs, // 英语(美国)
            "en-GB" => LangType.EnGb, // 英语(英国)
            "en-WW" => LangType.EnWw, // 英语(全球)
            "en-CA" => LangType.EnCa, // 英语(加拿大)
            "en-AU" => LangType.EnAu, // 英语(澳大利亚)
            "en-IE" => LangType.EnIe, // 英语(爱尔兰)
            "en-FI" => LangType.EnFi, // 英语(芬兰)
            "fi-FI" => LangType.FiFi, // 芬兰语(芬兰)
            "en-DK" => LangType.EnDk, // 英语(丹麦)
            "da-DK" => LangType.DaDk, // 丹麦语(丹麦)
            "en-IL" => LangType.EnIl, // 英语(以色列)
            "he-IL" => LangType.HeIl, // 希伯来语(以色列)
            "en-ZA" => LangType.EnZa, // 英语(南非)
            "en-IN" => LangType.EnIn, // 英语(印度)
            "en-NO" => LangType.EnNo, // 英语(挪威)
            "en-SG" => LangType.EnSg, // 英语(新加坡)
            "en-NZ" => LangType.EnNz, // 英语(新西兰)
            "en-ID" => LangType.EnId, // 英语(印度尼西亚)
            "en-PH" => LangType.EnPh, // 英语(菲律宾)
            "en-TH" => LangType.EnTh, // 英语(泰国)
            "en-MY" => LangType.EnMy, // 英语(马来西亚)
            "en-XA" => LangType.EnXa, // 英语(阿拉伯)
            "ko-KR" => LangType.KoKr, // 韩文(韩国)
            "ja-JP" => LangType.JaJp, // 日语(日本)
            "nl-NL" => LangType.NlNl, // 荷兰语(荷兰)
            "nl-BE" => LangType.NlBe, // 荷兰语(比利时)
            "pt-PT" => LangType.PtPt, // 葡萄牙语(葡萄牙)
            "pt-BR" => LangType.PtBr, // 葡萄牙语(巴西)
            "fr-FR" => LangType.FrFr, // 法语(法国)
            "fr-LU" => LangType.FrLu, // 法语(卢森堡)
            "fr-CH" => LangType.FrCh, // 法语(瑞士)
            "fr-BE" => LangType.FrBe, // 法语(比利时)
            "fr-CA" => LangType.FrCa, // 法语(加拿大)
            "es-LA" => LangType.EsLa, // 西班牙语(拉丁美洲)
            "es-ES" => LangType.EsEs, // 西班牙语(西班牙)
            "es-AR" => LangType.EsAr, // 西班牙语(阿根廷)
            "es-US" => LangType.EsUs, // 西班牙语(美国)
            "es-MX" => LangType.EsMx, // 西班牙语(墨西哥)
            "es-CO" => LangType.EsCo, // 西班牙语(哥伦比亚)
            "es-PR" => LangType.EsPr, // 西班牙语(波多黎各)
            "de-DE" => LangType.DeDe, // 德语(德国)
            "de-AT" => LangType.DeAt, // 德语(奥地利)
            "de-CH" => LangType.DeCh, // 德语(瑞士)
            "ru-RU" => LangType.RuRu, // 俄语(俄罗斯)
            "it-IT" => LangType.ItIt, // 意大利语(意大利)
            "el-GR" => LangType.ElGr, // 希腊语(希腊)
            "no-NO" => LangType.NoNo, // 挪威语(挪威)
            "hu-HU" => LangType.HuHu, // 匈牙利语(匈牙利)
            "tr-TR" => LangType.TrTr, // 土耳其语(土耳其)
            "cs-CZ" => LangType.CsCz, // 捷克语(捷克共和国)
            "sl-SL" => LangType.SlSl, // 斯洛文尼亚语
            "pl-PL" => LangType.PlPl, // 波兰语(波兰)
            "sv-SE" => LangType.SvSe, // 瑞典语(瑞典)
            _ => LangType.ZhCn // 默认返回简体中文(中国)
        };
    }


    /// <summary>
    /// 将枚举转化为当地语言名称格式字符串（包含地区信息）
    /// </summary>
    /// <param name="lang"></param>
    /// <returns>简体中文（中国），English（US），Español（España）等</returns>
    public static string ToReadableString(this LangType lang)
    {
        return lang switch
        {
            LangType.ZhCn => "简体中文（中国）",
            LangType.ZhTw => "繁體中文（台灣）",
            LangType.ZhHk => "繁體中文（香港）",
            LangType.EnHk => "English（Hong Kong）",
            LangType.EnUs => "English（United States）",
            LangType.EnGb => "English（United Kingdom）",
            LangType.EnWw => "English（Worldwide）",
            LangType.EnCa => "English（Canada）",
            LangType.EnAu => "English（Australia）",
            LangType.EnIe => "English（Ireland）",
            LangType.EnFi => "English（Finland）",
            LangType.FiFi => "Suomi（Suomi）",
            LangType.EnDk => "English（Denmark）",
            LangType.DaDk => "Dansk（Danmark）",
            LangType.EnIl => "English（Israel）",
            LangType.HeIl => "עברית（ישראל）",
            LangType.EnZa => "English（South Africa）",
            LangType.EnIn => "English（India）",
            LangType.EnNo => "English（Norway）",
            LangType.EnSg => "English（Singapore）",
            LangType.EnNz => "English（New Zealand）",
            LangType.EnId => "English（Indonesia）",
            LangType.EnPh => "English（Philippines）",
            LangType.EnTh => "English（Thailand）",
            LangType.EnMy => "English（Malaysia）",
            LangType.EnXa => "English（Arab）",
            LangType.KoKr => "한국어（대한민국）",
            LangType.JaJp => "日本語（日本）",
            LangType.NlNl => "Nederlands（Nederland）",
            LangType.NlBe => "Nederlands（België）",
            LangType.PtPt => "Português（Portugal）",
            LangType.PtBr => "Português（Brasil）",
            LangType.FrFr => "Français（France）",
            LangType.FrLu => "Français（Luxembourg）",
            LangType.FrCh => "Français（Suisse）",
            LangType.FrBe => "Français（Belgique）",
            LangType.FrCa => "Français（Canada）",
            LangType.EsLa => "Español（América Latina）",
            LangType.EsEs => "Español（España）",
            LangType.EsAr => "Español（Argentina）",
            LangType.EsUs => "Español（Estados Unidos）",
            LangType.EsMx => "Español（México）",
            LangType.EsCo => "Español（Colombia）",
            LangType.EsPr => "Español（Puerto Rico）",
            LangType.DeDe => "Deutsch（Deutschland）",
            LangType.DeAt => "Deutsch（Österreich）",
            LangType.DeCh => "Deutsch（Schweiz）",
            LangType.RuRu => "Русский（Россия）",
            LangType.ItIt => "Italiano（Italia）",
            LangType.ElGr => "Ελληνικά（Ελλάδα）",
            LangType.NoNo => "Norsk（Norge）",
            LangType.HuHu => "Magyar（Magyarország）",
            LangType.TrTr => "Türkçe（Türkiye）",
            LangType.CsCz => "Čeština（Česká republika）",
            LangType.SlSl => "Slovenščina（Slovenija）",
            LangType.PlPl => "Polski（Polska）",
            LangType.SvSe => "Svenska（Sverige）",
            _ => "简体中文（中国）"
        };
    }

    /// <summary>
    /// 返回值无id
    /// </summary>
    /// <param name="dict"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static TextModel ToTextModel(this Dictionary<LangType, string> dict)
    {
        if (!dict.ContainsKey(LangType.EnUs))
            throw new Exception($"{nameof(dict)} should contain EnUs");

        var textModel = new TextModel()
        {
            TextEnUs = dict[LangType.EnUs],
            TextDefault = dict.TryGetValue(LangType.ZhCn, out var zhCn) ? zhCn : dict[LangType.EnUs]
        };
        textModel.TextDict = dict;
        return textModel;
    }

    /// <summary>
    /// 从<see cref="Text"/>中按照<see cref="LangType"/>选择相应语言，以字符串返回
    /// </summary>
    /// <returns>未发现默认语言返回TextDefault</returns>
    public static string ToLangString(this TextModel textModel, LangType lang, string append = "")
    {
        return (textModel.TextDict.TryGetValue(lang, out var text) ? text : textModel.TextDefault) + append;
    }
    
    public static string ToLangString(this Dictionary<LangType, string> dict, LangType lang)
    {
        return dict.ToTextModel().ToLangString(lang);
    }
}