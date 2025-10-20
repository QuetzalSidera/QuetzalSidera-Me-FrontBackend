namespace Grpc.Share.Tools;

public static class AnchorHelper
{
    /// <summary>
    /// 对含空格的url进行1编码
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static string GetAnchor(this string url)
    {
        return Uri.EscapeDataString(url);
    }
}