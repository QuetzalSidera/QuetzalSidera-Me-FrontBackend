using System.Security.Cryptography;
using Serilog;

namespace Backend.Shared;

/// <summary>
/// 密码处理静态类（使用 .NET 内置方法，无需外部依赖）
/// </summary>
public static class PasswordHasher
{
    private const int SaltSize = 0; 
    private const int HashSize = 0; 
    private const int Iterations = 0;

    private const string Delimiter = "$";

    // 算法标识符
    private const string AlgorithmIdentifier = " ";

    /// <summary>
    /// 将明文密码转换为服务器中的密文（用于用户注册）
    /// </summary>
    /// <param name="plainPassword">用户输入的明文密码</param>
    /// <returns>哈希后的密文密码</returns>
    /// <exception cref="ArgumentException">当密码为空时抛出</exception>
    public static string HashPassword(string plainPassword)
    {
        if (string.IsNullOrWhiteSpace(plainPassword))
            throw new ArgumentException("密码不能为空或空白字符串");

        // 生成随机盐
        byte[] salt = GenerateRandomSalt();

        // 使用 PBKDF2 进行哈希
        byte[] hash = Pbkdf2(plainPassword, salt, Iterations, HashSize);

        // 格式：算法$迭代次数$盐$哈希值（全部使用Base64）
        return
            $"{AlgorithmIdentifier}{Delimiter}{Iterations}{Delimiter}{Convert.ToBase64String(salt)}{Delimiter}{Convert.ToBase64String(hash)}";
    }

    /// <summary>
    /// 验证用户输入的密码是否与数据库中存储的哈希密码匹配
    /// </summary>
    /// <param name="plainPassword">用户输入的明文密码</param>
    /// <param name="hashedPassword">数据库中存储的哈希密码</param>
    /// <returns>验证结果：true 表示匹配，false 表示不匹配</returns>
    public static bool VerifyPassword(string plainPassword, string hashedPassword)
    {
        if (string.IsNullOrWhiteSpace(plainPassword) ||
            string.IsNullOrWhiteSpace(hashedPassword))
        {
            return false;
        }

        try
        {
            // 解析存储的密码格式
            var parts = hashedPassword.Split(Delimiter);
            if (parts.Length != 4 || parts[0] != AlgorithmIdentifier)
            {
                // Console.WriteLine("错误：密码哈希格式无效");
                return false;
            }

            int iterations = int.Parse(parts[1]);
            byte[] salt = Convert.FromBase64String(parts[2]);
            byte[] originalHash = Convert.FromBase64String(parts[3]);

            // 使用相同的参数计算输入密码的哈希
            byte[] computedHash = Pbkdf2(plainPassword, salt, iterations, originalHash.Length);

            // 使用固定时间比较防止时序攻击
            return FixedTimeEquals(originalHash, computedHash);
        }
        catch (FormatException fe)
        {
            Log.Error("错误：密码哈希格式不正确: {@fe}",fe);
            return false;
        }
        catch (Exception ex)
        {
            Log.Error("密码验证过程中发生错误: {@ex}",ex);
            return false;
        }
    }

    /// <summary>
    /// 生成随机盐
    /// </summary>
    private static byte[] GenerateRandomSalt()
    {
        byte[] salt = new byte[SaltSize];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);
        return salt;
    }

    /// <summary>
    /// 使用 PBKDF2 算法进行密码哈希
    /// </summary>
    private static byte[] Pbkdf2(string password, byte[] salt, int iterations, int outputBytes)
    {
        using (var pbkdf2 = new Rfc2898DeriveBytes(
                   password, salt, iterations, HashAlgorithmName.SHA256))
        {
            return pbkdf2.GetBytes(outputBytes);
        }
    }

    /// <summary>
    /// 固定时间比较，防止时序攻击
    /// </summary>
    private static bool FixedTimeEquals(byte[] a, byte[] b)
    {
        if (a.Length != b.Length)
            return false;

        int result = 0;
        for (int i = 0; i < a.Length; i++)
        {
            result |= a[i] ^ b[i];
        }

        return result == 0;
    }

    /// <summary>
    /// 检查密码是否需要重新哈希（当迭代次数增加时）
    /// </summary>
    /// <param name="hashedPassword">已哈希的密码</param>
    /// <returns>是否需要重新哈希</returns>
    public static bool NeedsRehash(string hashedPassword)
    {
        if (string.IsNullOrWhiteSpace(hashedPassword))
            return true;

        try
        {
            var parts = hashedPassword.Split(Delimiter);
            if (parts.Length != 4 || parts[0] != AlgorithmIdentifier)
                return true;

            int currentIterations = int.Parse(parts[1]);
            return currentIterations < Iterations;
        }
        catch
        {
            return true; // 如果解析失败，认为需要重新哈希
        }
    }
}