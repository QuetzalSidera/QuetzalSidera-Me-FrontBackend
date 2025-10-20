namespace Chat.QuetzalSidera.Me.Tools;

public static class DateTimeHelper
{
    public static DateTime ToLocalTime(this DateTime dateTime)
    {
        return TimeZoneInfo.ConvertTimeFromUtc(dateTime, TimeZoneInfo.Local);
    }
}