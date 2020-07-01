using System;

namespace ShopeeChat.Infrastructure.Extensions
{
    public static class EpochTimeExtensions
    {
        /// <summary>
        /// Converts the given date value to epoch time.
        /// </summary>
        public static long ToEpochTime(this DateTime dateTime)
        {
            var date = dateTime.ToUniversalTime();
            var ticks = date.Ticks - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).Ticks;
            var ts = ticks / TimeSpan.TicksPerSecond;
            return ts;
        }

        public static string ToNanoseconds(this DateTime dateTime)
        {
            DateTime zuluTime = DateTime.Now.ToUniversalTime();
            DateTime unixEpoch = new DateTime(1970, 1, 1);
            var ssNanoSeconds = ((Int32)(zuluTime.Subtract(unixEpoch)).TotalSeconds-10) + "000000000";
            return ssNanoSeconds;
        }
    }
}