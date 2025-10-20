using Grpc.Share.Protos.SharedModels;
using Protobuf.Shared.Text;
using Protobuf.Www.Content;


namespace Grpc.Share.Tools;

public static class CompleteStatusHelper
{
    /// <summary>
    /// 转化为多语言文本
    /// </summary>
    /// <param name="status"></param>
    /// <returns></returns>
    public static TextModel ToTextModel(this CompleteStatus status)
    {
        var text = new Text();
        switch (status)
        {
            case CompleteStatus.Aside:
                text.TextEnUs = "Aside";
                text.TextDefault = "已搁置";
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.ZhCn,
                    Text = "已搁置"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.ZhTw,
                    Text = "已擱置"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.ZhHk,
                    Text = "已擱置"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.EnUs,
                    Text = "Aside"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.EnGb,
                    Text = "Aside"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.JaJp,
                    Text = "保留した"
                });
                break;
            case CompleteStatus.Finished:
                text.TextEnUs = "Finished";
                text.TextDefault = "已完成";
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.ZhCn,
                    Text = "已完成"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.ZhTw,
                    Text = "已完成"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.ZhHk,
                    Text = "已完成"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.EnUs,
                    Text = "Finished"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.EnGb,
                    Text = "Finished"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.JaJp,
                    Text = "完了した"
                });
                break;
            case CompleteStatus.OnGoing:
                text.TextEnUs = "On Going";
                text.TextDefault = "进行中";
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.ZhCn,
                    Text = "进行中"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.ZhTw,
                    Text = "進行中"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.ZhHk,
                    Text = "進行中"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.EnUs,
                    Text = "On Going"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.EnGb,
                    Text = "On Going"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.JaJp,
                    Text = "進行中"
                });
                break;
            case CompleteStatus.Planning:
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
            case CompleteStatus.Dreaming:
                text.TextEnUs = "Dreaming";
                text.TextDefault = "幻想中";
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.ZhCn,
                    Text = "幻想中"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.ZhTw,
                    Text = "幻想中"
                });
                text.TextDict.Add(new DictionaryEntry()
                {
                    LangType = LangType.ZhHk,
                    Text = "幻想中"
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
    public static string ToCss(this CompleteStatus status)
    {
        return status switch
        {
            CompleteStatus.Aside => "project-status aside",
            CompleteStatus.Finished => "project-status finished",
            CompleteStatus.OnGoing => "project-status ongoing",
            CompleteStatus.Planning => "project-status planning",
            CompleteStatus.Dreaming => "project-status dreaming",
            _ => "project-status planning"
        };
    }
}