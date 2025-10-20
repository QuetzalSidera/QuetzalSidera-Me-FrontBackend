namespace Chat.QuetzalSidera.Me.Models;

public class PasswordLoginInfo
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    /// <summary>
    /// 注销时使用 注册时使用
    /// </summary>
    public bool Confirm { get; set; }
}

public class VerifyCodeLoginInfo
{
    public string Email { get; set; } = string.Empty;
    public string VerifyCode { get; set; } = string.Empty;

    /// <summary>
    /// 注销时使用 注册时使用
    /// </summary>
    public bool Confirm { get; set; }
}

public class RegisterInfo
{
    public string NickName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string PasswordVerify { get; set; } = string.Empty;
    public string VerifyCode { get; set; } = string.Empty;

    /// <summary>
    /// 注册时使用
    /// </summary>
    public bool Confirm { get; set; }
}