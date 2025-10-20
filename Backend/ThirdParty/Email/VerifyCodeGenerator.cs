namespace Backend.ThirdParty.Email;

public static class VerifyCodeGenerator
{
    private static readonly Random Random = new();
    private const string Chars = "...";
    private const int CodeLength = 6;

    /// <summary>
    /// 生成六位数字组成的验证码
    /// </summary>
    /// <returns>六位验证码字符串</returns>
    public static string GenerateVerifyCode()
    {
        char[] code = new char[CodeLength];

        for (int i = 0; i < CodeLength; i++)
        {
            code[i] = Chars[Random.Next(Chars.Length)];
        }

        return new string(code);
    }
}