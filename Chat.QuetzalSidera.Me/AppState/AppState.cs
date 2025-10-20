using Protobuf.Shared.Text;

namespace Chat.QuetzalSidera.Me.AppState;

public class AppState
{
    public const string UserGuidKey = "user_guid";
    public const string AuthTokenKey = "auth_token";
    public const string CreateTimestampKey = "create_timestamp";
    public const string UserLangKey = "user_lang";
    public DialogEnum ShowLoginDialog { get; set; } = DialogEnum.None;
    public LangType CurrentLang = LangType.ZhCn;

    public enum DialogEnum
    {
        None,
        Login,
        Logout,
        DeleteConfirm,
    }

    private static readonly List<LangType> SupportLang =
        [LangType.ZhCn, LangType.ZhHk, LangType.ZhTw, LangType.EnUs, LangType.EnGb, LangType.JaJp];

    private int _index = 0;

    public void ChangeLang()
    {
        _index = Array.IndexOf(SupportLang.ToArray(), CurrentLang); //index可以为-1
        _index++;
        if (_index == SupportLang.Count)
            _index = 0;
        CurrentLang = SupportLang.ElementAt(_index);
        OnLanguageChanged?.Invoke();
    }

    public void LangHasChanged()
    {
        OnLanguageChanged?.Invoke();
    }

    public event Func<Task>? OnLanguageChanged;
}