using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace MahdiHajian.Packages
{
    public class PersianDate
    {
        public static DateTime GetDateTime()
        {
            return DateTime.Now;
        }

        public static string GetMonth(DateTime dateTime)
        {
            var permonth = new string[] { "فروردین", "اردیبهشت", "خرداد", "تیر", "مرداد", "شهریور", "مهر", "آبان", "آذر", "دی", "بهمن", "اسفند" };
            var persian = new PersianCalendar();
            return permonth[persian.GetMonth(dateTime) - 1];
        }

        public static string GetDay(DateTime dateTime)
        {
            var day = new string[] { "یکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنجشنبه", "جمعه", "شنبه" };
            var persian = new PersianCalendar();
            return day[(int)persian.GetDayOfWeek(dateTime)];
        }

        public static int GetYear(DateTime dateTime)
        {
            var persian = new PersianCalendar();
            return persian.GetYear(dateTime);
        }

        public static string GetDayOfMonth(DateTime dateTime)
        {
            var persian = new PersianCalendar();
            return persian.GetDayOfMonth(dateTime).ToString();
        }

        public static DateTime ConvertPersianDateToGeorgian(string d)
        {
            var persianCalendar = new PersianCalendar();
            try
            {
                if (d.IndexOf(":") < 3)
                {
                    try
                    {
                        return new DateTime(int.Parse(d.Substring(9, 4)), int.Parse(d.Substring(14, 2)),
                        int.Parse(d.Substring(17, 2)), int.Parse(d.Substring(0, 2)), int.Parse(d.Substring(3, 2)), int.Parse(d.Substring(6, 2)), persianCalendar);
                    }
                    catch
                    {
                        return new DateTime(int.Parse(d.Substring(9, 4)), int.Parse(d.Substring(14, 2)),
                        int.Parse(d.Substring(17, 2)), persianCalendar);
                    }
                }
                else
                {
                    return new DateTime(int.Parse(d.Substring(0, 4)), int.Parse(d.Substring(5, 2)),
                        int.Parse(d.Substring(8, 2)), persianCalendar);
                }
            }
            catch
            {
                return DateTime.Now;
            }
        }

        public static DateTime ConvertPersianDateWithTimeToGeorgian(string d)
        {
            var persianCalendar = new PersianCalendar();
            try
            {
                if (d.IndexOf(":") < 3)
                {
                    try
                    {
                        return new DateTime(int.Parse(d.Substring(9, 4)), int.Parse(d.Substring(14, 2)),
                        int.Parse(d.Substring(17, 2)), int.Parse(d.Substring(0, 2)), int.Parse(d.Substring(3, 2)), int.Parse(d.Substring(6, 2)), persianCalendar);
                    }
                    catch
                    {
                        return new DateTime(int.Parse(d.Substring(9, 4)), int.Parse(d.Substring(14, 2)),
                        int.Parse(d.Substring(17, 2)), persianCalendar);
                    }
                }
                else
                {
                    return new DateTime(int.Parse(d.Substring(0, 4)), int.Parse(d.Substring(5, 2)),
                        int.Parse(d.Substring(8, 2)), persianCalendar);
                }
            }
            catch
            {
                return DateTime.Now;
            }
        }

        public static string ConvertToPersianDate(DateTime dateTime)
        {
            var persian = new PersianCalendar();

            return string.Format("{0}/{1}/{2}",
                                 persian.GetYear(dateTime),
                                 persian.GetMonth(dateTime) > 9 ? persian.GetMonth(dateTime).ToString() : "0" + persian.GetMonth(dateTime).ToString(),
                                 persian.GetDayOfMonth(dateTime) > 9 ? persian.GetDayOfMonth(dateTime).ToString() : "0" + persian.GetDayOfMonth(dateTime).ToString(),
                                 DateTimeFormatInfo.CurrentInfo.TimeSeparator);
        }

        public static string ConvertToPersianDateTime(DateTime dateTime)
        {
            var persian = new PersianCalendar();

            return string.Format("{3}{6}{4:D2}{6}{5:D2}-{0}/{1}/{2}",


                                 persian.GetYear(dateTime),
                                 persian.GetMonth(dateTime),
                                 persian.GetDayOfMonth(dateTime),
                                 persian.GetHour(dateTime),
                                 persian.GetMinute(dateTime),
                                 persian.GetSecond(dateTime),

                                 DateTimeFormatInfo.CurrentInfo.TimeSeparator);
        }

        public static string ConvertToPersianDateTime2(DateTime dateTime)
        {
            var persian = new PersianCalendar();

            return string.Format("{0} {1} {2} {3}",
                                 GetDay(dateTime),
                                 persian.GetDayOfMonth(dateTime),
                                 GetMonth(dateTime),
                                 persian.GetYear(dateTime),
                                 DateTimeFormatInfo.CurrentInfo.TimeSeparator);
        }


        public static string GetCreateDateWeekValue(DateTime model)
        {
            DayOfWeek todayWeek = DateTime.Today.DayOfWeek;
            string today = "";

            if (todayWeek == DayOfWeek.Saturday)
            {
                today = "شنبه";
            }
            else if (todayWeek == DayOfWeek.Sunday)
            {
                today = "یکشنبه";
            }
            else if (todayWeek == DayOfWeek.Monday)
            {
                today = "دوشنبه";
            }
            else if (todayWeek == DayOfWeek.Tuesday)
            {
                today = "سه شنبه";
            }
            else if (todayWeek == DayOfWeek.Wednesday)
            {
                today = "چهار شنبه";
            }
            else if (todayWeek == DayOfWeek.Thursday)
            {
                today = "پنج شنبه";
            }
            else if (todayWeek == DayOfWeek.Friday)
            {
                today = "جمعه";
            }

            string month = ConvertToPersianDateTime(model).Split('-')[1].Split('/')[1]
                .Replace("1", "فروردین")
                .Replace("2", "اردیبهشت")
                .Replace("3", "خرداد")
                .Replace("4", "تیر")
                .Replace("5", "مرداد")
                .Replace("6", "شهریور")
                .Replace("7", "مهر")
                .Replace("8", "آبان")
                .Replace("9", "آذر")
                .Replace("10", "دی")
                .Replace("11", "بهمن")
                .Replace("12", "اسفند");
            string date = ConvertToPersianDateTime(model).Split('-')[1];
            return today + " " + date.Split('/')[2] + " " + month;
        }
        public string GetCreateDateMonthValue(DateTime model)
        {
            string month = ConvertToPersianDateTime(model).Split('-')[1].Split('/')[1]
                    .Replace("1", "فروردین")
                    .Replace("2", "اردیبهشت")
                    .Replace("3", "خرداد")
                    .Replace("4", "تیر")
                    .Replace("5", "مرداد")
                    .Replace("6", "شهریور")
                    .Replace("7", "مهر")
                    .Replace("8", "آبان")
                    .Replace("9", "آذر")
                    .Replace("10", "دی")
                    .Replace("11", "بهمن")
                    .Replace("12", "اسفند");
            string date = ConvertToPersianDateTime(model).Split('-')[1];
            return date.Split('/')[2] + " " + month + " " + date.Split('/')[0];
        }
        public static string GetCreateDateTimeAgoValue(DateTime model)
        {
            string timeAgo = "";
            if (DateTime.Now - model > new TimeSpan(1, 0, 0, 0, 0))
            {
                timeAgo = (DateTime.Now - model).Days + " روز پیش";
            }
            else if (DateTime.Now - model < new TimeSpan(1, 0, 0))
            {
                timeAgo = "به تازگی";
            }
            else
            {
                timeAgo = (DateTime.Now - model).Hours + " ساعت پیش";
            }
            return timeAgo;
        }
        public static string GetCreateDateWeekAndMonthValue(DateTime model)
        {
            DayOfWeek todayWeek = DateTime.Today.DayOfWeek;
            string today = "";

            if (todayWeek == DayOfWeek.Saturday)
            {
                today = "شنبه";
            }
            else if (todayWeek == DayOfWeek.Sunday)
            {
                today = "یکشنبه";
            }
            else if (todayWeek == DayOfWeek.Monday)
            {
                today = "دوشنبه";
            }
            else if (todayWeek == DayOfWeek.Tuesday)
            {
                today = "سه شنبه";
            }
            else if (todayWeek == DayOfWeek.Wednesday)
            {
                today = "چهار شنبه";
            }
            else if (todayWeek == DayOfWeek.Thursday)
            {
                today = "پنج شنبه";
            }
            else if (todayWeek == DayOfWeek.Friday)
            {
                today = "جمعه";
            }

            string month = ConvertToPersianDateTime(model).Split('-')[1].Split('/')[1]
                .Replace("1", "فروردین")
                .Replace("2", "اردیبهشت")
                .Replace("3", "خرداد")
                .Replace("4", "تیر")
                .Replace("5", "مرداد")
                .Replace("6", "شهریور")
                .Replace("7", "مهر")
                .Replace("8", "آبان")
                .Replace("9", "آذر")
                .Replace("10", "دی")
                .Replace("11", "بهمن")
                .Replace("12", "اسفند");
            string date = ConvertToPersianDateTime(model).Split('-')[1];
            return today + " " + date.Split('/')[2] + " " + month + " " + date.Split('/')[0];
        }
    }
}
