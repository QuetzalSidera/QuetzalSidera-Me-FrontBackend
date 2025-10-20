using Grpc.Share.Protos.SharedModels;
using Protobuf.Shared.Text;
using Protobuf.Www.Content;


namespace Grpc.Share.Tools;

public static class ManageStatusHelper
{
    /// <summary>
    /// 转化为多语言文本
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    public static TextModel ToTextModel(this ManageStatus status)
    {
        var text = new Text();
        switch (status)
        {
            case ManageStatus.Proficient:
                text.TextEnUs = "Proficient";
                text.TextDefault = "精通";
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.ZhCn,
                    Text = "精通"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.ZhTw,
                    Text = "精通"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.ZhHk,
                    Text = "精通"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.EnUs,
                    Text = "Proficient"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.EnGb,
                    Text = "Proficient"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.JaJp,
                    Text = "熟練"
                });
                break;
            case ManageStatus.Expert:
                text.TextEnUs = "Expert";
                text.TextDefault = "熟练";
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.ZhCn,
                    Text = "熟练"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.ZhTw,
                    Text = "熟練"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.ZhHk,
                    Text = "熟練"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.EnUs,
                    Text = "Expert"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.EnGb,
                    Text = "Expert"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.JaJp,
                    Text = "熟達"
                });
                break;
            case ManageStatus.Competent:
                text.TextEnUs = "Competent";
                text.TextDefault = "熟悉";
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.ZhCn,
                    Text = "熟悉"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.ZhTw,
                    Text = "熟悉"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.ZhHk,
                    Text = "熟悉"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.EnUs,
                    Text = "Competent"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.EnGb,
                    Text = "Competent"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.JaJp,
                    Text = "熟知"
                });
                break;
            case ManageStatus.Novice:
                text.TextEnUs = "Novice";
                text.TextDefault = "了解";
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.ZhCn,
                    Text = "了解"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.ZhTw,
                    Text = "了解"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.ZhHk,
                    Text = "了解"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.EnUs,
                    Text = "Novice"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.EnGb,
                    Text = "Novice"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.JaJp,
                    Text = "理解"
                });
                break;
            case ManageStatus.Planning:
                text.TextEnUs = "Planning";
                text.TextDefault = "计划中";
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.ZhCn,
                    Text = "计划中"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.ZhTw,
                    Text = "計劃中"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.ZhHk,
                    Text = "計劃中"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.EnUs,
                    Text = "Planning"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.EnGb,
                    Text = "Planning"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.JaJp,
                    Text = "計画中"
                });
                break;
            case ManageStatus.Dreaming:
                text.TextEnUs = "Dreaming";
                text.TextDefault = "幻想时间";
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.ZhCn,
                    Text = "幻想时间"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.ZhTw,
                    Text = "幻想時間"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.ZhHk,
                    Text = "幻想時間"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.EnUs,
                    Text = "Dreaming"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.EnGb,
                    Text = "Dreaming"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.JaJp,
                    Text = "幻想中"
                });
                break;
        }

        return text;
    }

    /// <summary>
    /// 转化为css类
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    public static string ToCss(this ManageStatus status)
    {
        return status switch
        {
            ManageStatus.Proficient => "tech-stack-status proficient",
            ManageStatus.Expert => "tech-stack-status expert",
            ManageStatus.Competent => "tech-stack-status competent",
            ManageStatus.Novice => "tech-stack-status novice",
            ManageStatus.Planning => "tech-stack-status planning",
            ManageStatus.Dreaming => "tech-stack-status dreaming",
            _ => "tech-stack-status planning",
        };
    }
}