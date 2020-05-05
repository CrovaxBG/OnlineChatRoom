using System.Collections.Generic;

namespace OnlineChatRoom.Common
{
    public static class GeneralExtensions
    {
        public static IEnumerable<T> AsEnumerable<T>(this T source)
        {
            yield return source;
        }

        public static string FirstLetterToUpper(this string str)
        {
            if (str == null)
            {
                return null;
            }

            if (str.Length > 1)
            {
                return char.ToUpper(str[0]) + str.Substring(1);
            }

            return str.ToUpper();
        }
    }
}