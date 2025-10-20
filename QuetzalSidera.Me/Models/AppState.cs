using System.ComponentModel;
using Protobuf.Shared.Text;

namespace QuetzalSidera.Me.Models;

public class AppState
{
    public LangType LangTypeInstance = LangType.ZhCn;
    public const string LangTypeKey = "user_lang";
    public bool IsFirstRendered { get; set; } = true;

    private static readonly List<LangType> SupportLang =
        [LangType.ZhCn, LangType.ZhHk, LangType.ZhTw, LangType.EnUs, LangType.EnGb, LangType.JaJp];

    private int _index = 0;

    public void ChangeLang()
    {
        _index = Array.IndexOf(SupportLang.ToArray(), LangTypeInstance);
        _index++;
        if (_index == SupportLang.Count)
            _index = 0;
        LangTypeInstance = SupportLang.ElementAt(_index);
        OnLanguageChanged.Invoke();
        // Console.WriteLine(_index);
        // Console.WriteLine(LangTypeInstance.ToString());
    }

    public event Action OnLanguageChanged = () => { };

    public void LangHasChanged()
    {
        OnLanguageChanged.Invoke();
    }
}