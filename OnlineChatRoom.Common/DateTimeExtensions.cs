using System;
using System.Globalization;

namespace OnlineChatRoom.Common
{
    public static class DateTimeExtensions
    {
        public static string FormatToTimestamp(this DateTime dateTime)
        {
            var dayText = "Днес";
            var dayDifference = (int)TimeSpan.FromTicks(DateTime.Now.Ticks - dateTime.Ticks).TotalDays;
            if (dayDifference == 1)
            {
                dayText = "Вчера";
            }
            else if (dayDifference >= 2 && dayDifference < 7)
            {
                dayText = CultureInfo.GetCultureInfo("bg-BG").DateTimeFormat.GetDayName(dateTime.DayOfWeek).FirstLetterToUpper();
            }
            else if (dayDifference >= 7)
            {
                return dateTime.ToShortDateString();
            }
            return dateTime.ToString($"{dayText} във HH:mm:ss tt", CultureInfo.InvariantCulture);
        }
    }
}