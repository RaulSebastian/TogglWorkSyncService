using System;

namespace TogglWorkSyncService.Helpers
{
    public static class Iso8601DateFormat
    {
        public static string Convert(DateTime date)
        {
            var utcOffsetHours = TimeZoneInfo.Local.BaseUtcOffset.Hours;
            string offset = $"{(utcOffsetHours > 0 ? "+" : "")}{utcOffsetHours:00}:00";

            return date.ToString("s") + offset;
        }
    }
}