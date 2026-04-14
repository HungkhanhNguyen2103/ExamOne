using ExamOne.Entity;

namespace ExamOne.Helper
{
    public class Constant
    {
        public static int RetryCount = 3;
        public static DateTime GetDateTimeVN()
        {
            var timezone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var nowUtcPlus7 = TimeZoneInfo.ConvertTime(DateTime.UtcNow, timezone);
            return nowUtcPlus7;
        }

        public static DateTime GetDateTimeFromMongo(DateTime dateTime)
        {
            var timezone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
            var dtLocal = TimeZoneInfo.ConvertTime(dateTime, timezone);
            return dtLocal;
        }

        public static string GetLocation(string branchName)
        {
            //, phường Hồng An, thành phố Hải Phòng
            return $"{branchName}";
        }
    }
}
