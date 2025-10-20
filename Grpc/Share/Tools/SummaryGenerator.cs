namespace Grpc.Share.Tools;

public static class SummaryHelper
{
    private const int AverageLength = 25;
    private const int Delta = 5;
    private static readonly char[] Separators = ['，', '。', '、', ',', '.', ';', '；', '!', '！', '?', '？'];

    /// <summary>
    /// 将文本裁剪为指定长度的摘要，优先在标点符号处断开。
    /// </summary>
    /// <param name="text">原始文本</param>
    /// <returns>摘要文本</returns>
    public static string GenerateSummary(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return string.Empty;

        if (text.Length <= AverageLength - Delta)
            return text;

        int maxLength = AverageLength + Delta;
        if (text.Length < maxLength)
            maxLength = text.Length;
        char[] interval = text[(AverageLength - Delta)..(maxLength)].ToCharArray();
        int[] indexes = interval.Where(i => Separators.Contains(i)).Select(i => Array.IndexOf(interval, i)).ToArray();
        if (indexes.Length != 0)
        {
            var index = indexes.MinBy(i => Math.Abs(i - AverageLength)) + AverageLength - Delta;
            return text[..index] + "...";
        }
        else
        {
            return text[..AverageLength]+ "...";
        }
    }
}