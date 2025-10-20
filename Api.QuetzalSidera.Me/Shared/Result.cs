namespace Api.QuetzalSidera.Me.Shared;

public class Result<T>
{
    public ErrorCode Status { get; set; }
    public string Message { get; set; } = string.Empty;
    public T? Data { get; set; }
}

public class Result
{
    public ErrorCode Status { get; set; }
    public string Message { get; set; } = string.Empty;
}

public enum ErrorCode
{
    /// <summary>
    /// 成功
    /// </summary>
    Ok = 0,

    /// <summary>
    /// 权限类
    /// </summary>
    EmailOrPasswordError = 1001,
    TokenExpired = 1002,
    Forbidden = 1003,

    /// <summary>
    /// 请求错误类
    /// </summary>
    BadRequest = 4001,

    /// <summary>
    /// 响应错误类
    /// </summary>
    ServerError = 5001,
}