using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EShop.Core.Helpers
{
    public static class TempDataExtensions
    {
        public static void Put<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }

        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            object o;
            tempData.TryGetValue(key, out o);
            return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
        }
    }
    public static class Utility
    {
        public static string ToDisplay(this Enum value)
        {
            Assert.NotNull(value, nameof(value));

            var attribute = value.GetType().GetField(value.ToString())
                .GetCustomAttributes<DisplayAttribute>(false).FirstOrDefault();

            if (attribute == null)
                return value.ToString();

            var propValue = attribute.GetType().GetProperty(DisplayProperty.Name.ToString())?.GetValue(attribute, null);
            return propValue?.ToString();
        }
        public static List<string> Split(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize)).ToList();
        }
        public static string ToUrlFormat(this string param)
        {
            if (param == null)
                return null;
            var res = param.Trim().Replace(" ", "-");
            return res.ToLower();
        }
        public static string ToLowerUrl(this string param)
        {
            if (param == null)
                return null;
            var res = param;
            return res.ToLower();
        }
        public static string ToLazyLoadImage(this string param)
        {
            if (param == null)
                return null;
          //  var res = param.Replace(@""" src=""/uploads", @" lazy b-lazy"" src=""https://farnaa.com/images/bg/bg-rectangle.webp"" data-src=""https://farnaa.com/uploads");
            var res = param.Replace(@"class=""img-fluid rounded-15""", "").Replace(@"class=""img-fluid""", "").Replace(@"class=""Img-fluid""", "").Replace(@"class=""img-fluid""", "").Replace(@"src=""", @"class=""lazy b-lazy img-fluid rounded-15"" src=""https://farnaa.com/images/bg/bg-rectangle.webp"" data-src=""https://farnaa.com/");
          //remove iframe lazy
            res = res.Replace(@"scrolling=""no"" class=""lazy b-lazy img-fluid rounded-15"" src=""https://farnaa.com/images/bg/bg-rectangle.webp"" data-src=""https://farnaa.com/", "scrolling=\"no\" src=\"");
            return res;
        }
        public static string EnglishToPersian(this string Str)
        {

            Dictionary<char, char> LettersDictionary = new Dictionary<char, char>
            {
                ['0'] ='۰',
                ['1'] ='۱',
                ['2'] ='۲',
                ['3'] ='۳',
                ['4'] ='۴',
                ['5'] ='۵',
                ['6'] ='۶',
                ['7'] ='۷',
                ['8'] ='۸',
                ['9'] ='۹'
            };
            foreach (var item in Str)
            {
                try
                {
                    Str = Str.Replace(item, LettersDictionary[item]);
                }
                catch (Exception e)
                {
                }
            }


            return Str;
        }
        public static string PersianToEnglish(this string persianStr)
        {

            Dictionary<char, char> LettersDictionary = new Dictionary<char, char>
            {
                ['۰'] = '0',
                ['۱'] = '1',
                ['۲'] = '2',
                ['۳'] = '3',
                ['۴'] = '4',
                ['۵'] = '5',
                ['۶'] = '6',
                ['۷'] = '7',
                ['۸'] = '8',
                ['۹'] = '9'
            };
            foreach (var item in persianStr)
            {
                try
                {
                    persianStr = persianStr.Replace(item, LettersDictionary[item]);
                }
                catch (Exception e)
                {
                }
            }


            return persianStr;
        }
        public static Boolean IsValidNationalCode(this String nationalCode)
        {
            try
            {

                if (String.IsNullOrEmpty(nationalCode))
                    throw new Exception("لطفا کد ملی را صحیح وارد نمایید");
                if (nationalCode.Length != 10)
                    throw new Exception("طول کد ملی باید ده کاراکتر باشد");
                var regex = new Regex(@"\d{10}");
                if (!regex.IsMatch(nationalCode))
                    throw new Exception("کد ملی تشکیل شده از ده رقم عددی می‌باشد؛ لطفا کد ملی را صحیح وارد نمایید");
                var allDigitEqual = new[] { "0000000000", "1111111111", "2222222222", "3333333333", "4444444444", "5555555555", "6666666666", "7777777777", "8888888888", "9999999999" };
                if (allDigitEqual.Contains(nationalCode)) return false;
                var chArray = nationalCode.ToCharArray();
                var num0 = Convert.ToInt32(chArray[0].ToString()) * 10;
                var num2 = Convert.ToInt32(chArray[1].ToString()) * 9;
                var num3 = Convert.ToInt32(chArray[2].ToString()) * 8;
                var num4 = Convert.ToInt32(chArray[3].ToString()) * 7;
                var num5 = Convert.ToInt32(chArray[4].ToString()) * 6;
                var num6 = Convert.ToInt32(chArray[5].ToString()) * 5;
                var num7 = Convert.ToInt32(chArray[6].ToString()) * 4;
                var num8 = Convert.ToInt32(chArray[7].ToString()) * 3;
                var num9 = Convert.ToInt32(chArray[8].ToString()) * 2;
                var a = Convert.ToInt32(chArray[9].ToString());

                var b = (((((((num0 + num2) + num3) + num4) + num5) + num6) + num7) + num8) + num9;
                var c = b % 11;

                return (((c < 2) && (a == c)) || ((c >= 2) && ((11 - c) == a)));
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public static bool IsValidBDate(this string bDate)
        {
            bDate = bDate.PersianToEnglish();
            var BYear = bDate.ConvertShamsiToMiladi().Year;
            return BYear < DateTime.Now.Year - 5;
        }
        public static string ConvertMiladiToShamsi(this DateTime dateTime)
        {
             PersianCalendar pc = new PersianCalendar();
            var res = $"{pc.GetYear(dateTime)}/{pc.GetMonth(dateTime)}/{pc.GetDayOfMonth(dateTime)}";
            return res;
        }
        public static DateTime ConvertShamsiToMiladi(this string persianDate)
        {

            var splitDate = persianDate.Split('/');
            int year = Convert.ToInt32(splitDate[0]);
            int month = Convert.ToInt32(splitDate[1]);
            int day = Convert.ToInt32(splitDate[2]);
            DateTime georgianDateTime = new DateTime(year, month, day, new System.Globalization.PersianCalendar());
            return georgianDateTime;
        }

        public static string ReplaceSpecialChars(this string str,string newChar)
        {
            string[] chars = new string[] { ",", ".", "/", "!", "@", "#", "$", "%", "^", "&", "*", "'", "\"", ";", "_", "(", ")", ":", "|", "[", "]","  " };
            for (int i = 0; i < chars.Length; i++)
            {
                if (str.Contains(chars[i]))
                {
                    str = str.Replace(chars[i], newChar);
                }
            }
            return str;
        }


        public static bool IsPersianWord(this string str)
        {
            var myregex = new Regex(@"^[\u0600-\u06FF]+$");
            return myregex.IsMatch(str) ? true : false;

        }
        public static int RoundPrice(this int price)
        {
            var p = Math.Ceiling((decimal)(price / 1000)) * 1000;
            return (int)p;
        }
    }
}