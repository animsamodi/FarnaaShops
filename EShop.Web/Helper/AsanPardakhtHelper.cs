using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EShop.Web.Helper
{
    public static class AsanPardakhtHelper
    {
        public static string GetUsername()
        {
            try
            {
                return ConfigurationManager.AppSetting["AsanPardakht:User"];
            }
            catch (Exception e)
            {
                return "";
            }
        } 
        
        public static string GetPassword()
        {
            try
            {
                return ConfigurationManager.AppSetting["AsanPardakht:Password"];
            }
            catch (Exception e)
            {
                return "";
            }
        }
        public static long GetMerchantConfigurationId()
        {
            try
            {
                return Convert.ToInt64(ConfigurationManager.AppSetting["AsanPardakht:MerchantConfigurationId"]);
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public static long GetServiceTypeId()
        {
            try
            {
                return Convert.ToInt64(ConfigurationManager.AppSetting["AsanPardakht:ServiceTypeId"]);
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public static string GetIban()
        {
            try
            {
                return ConfigurationManager.AppSetting["AsanPardakht:Iban"];
            }
            catch (Exception e)
            {
                return "";
            }
        }   
        public static string GetCallBackUrl()
        {
            try
            {
                return ConfigurationManager.AppSetting["AsanPardakht:CallBackUrl"];
            }
            catch (Exception e)
            {
                return "";
            }
        }
        public static string GetTimeUrl()
        {
            try
            {
                return ConfigurationManager.AppSetting["AsanPardakht:Time"];
            }
            catch (Exception e)
            {
                return "";
            }
        }
        public static string GetTokenUrl()
        {
            try
            {
                return ConfigurationManager.AppSetting["AsanPardakht:Token"];
            }
            catch (Exception e)
            {
                return "";
            }
        }
        public static string GetTranResultUrl()
        {
            try
            {
                return ConfigurationManager.AppSetting["AsanPardakht:TranResult"];
            }
            catch (Exception e)
            {
                return "";
            }
        }
        public static string GetVerifyUrl()
        {
            try
            {
                return ConfigurationManager.AppSetting["AsanPardakht:Verify"];
            }
            catch (Exception e)
            {
                return "";
            }
        }
        public static string GetSettlementUrl()
        {
            try
            {
                return ConfigurationManager.AppSetting["AsanPardakht:Settlement"];
            }
            catch (Exception e)
            {
                return "";
            }
        }
        public static string GetAsanPardakhtUrl()
        {
            try
            {
                return ConfigurationManager.AppSetting["AsanPardakht:AsanPardakht"];
            }
            catch (Exception e)
            {
                return "";
            }
        }
    
    }
}
