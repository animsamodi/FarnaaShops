using System;
using System.Globalization;

namespace EShop.Core.ExtensionMethods
{
    public static class DateTimeEx
    {
        public enum PersianMonth{فرودین=1 ,اردیبهشت,خرداد,تیر,مرداد,شهریور,مهر, آبان, آذر, دی, بهمن, اسفند};
        public static string GetMonthPersian(this DateTime dt)
        {
            try
            {
                PersianCalendar pc = new PersianCalendar();
                string month = ((PersianMonth)pc.GetMonth(dt)).ToString();
                return pc.GetDayOfMonth(dt) + " " + month + " " + pc.GetYear(dt);
            }
            catch (Exception e)
            {
                return "";
            }
        
        }
        public static long GetNumberShamsiDate(this DateTime dt)
        {
            try
            {
                PersianCalendar pc = new PersianCalendar();
                  return Convert.ToInt64(pc.GetYear(dt).ToString("0000")  + pc.GetMonth(dt).ToString("00")  + pc.GetDayOfMonth(dt).ToString("00")); 
            }
            catch (Exception e)
            {
                return 0;
            }
        
        }
        public static string GetShamsiDate(this DateTime dt)
        {
            try
            {
                PersianCalendar pc = new PersianCalendar();
                  return pc.GetYear(dt).ToString("0000") + "/" + pc.GetMonth(dt).ToString("00") + "/" + pc.GetDayOfMonth(dt).ToString("00"); 
            }
            catch (Exception e)
            {
                return "";
            }
        
        }
        public static string GetSellerRegisterDate(this DateTime value)
        {
            var betweentospan = DateTime.Now - value;
            var betweendate = new DateTime(1) + betweentospan;
            var year = betweendate.Year - 1;
            var month = betweendate.Month - 1;
            var week = (betweendate.Day - 1) / 7;
            var day = (betweendate.Day - 1) % 7;
            var time = year > 0 ? year + " سال" + (month > 0 ? "," + month + " ماه" : week > 0 ? "," + week + " هفته" : day > 0 ? "," + day + " روز" : "")
                : month > 0 ? "," + month + " ماه" + (week > 0 ? "," + week + " هفته" : day > 0 ? "," + day + " روز" : "")
                : week > 0 ? "," + week + " هفته" + (day > 0 ? "," + day + " روز" : "") : "";
            return time;
        }


        public static string GetSendDate(this DateTime dt)
        {
            PersianCalendar pc = new PersianCalendar();
            string month = ((PersianMonth)pc.GetMonth(dt)).ToString();
            return pc.GetDayOfMonth(dt) + " " + month ;
        }

        public static string GetDateForChart(this DateTime dt)
        {
            PersianCalendar pc = new PersianCalendar();
            string month = ((PersianMonth)pc.GetMonth(dt)).ToString();
            return pc.GetDayOfMonth(dt) + " " + month ;
        }
    }
}
