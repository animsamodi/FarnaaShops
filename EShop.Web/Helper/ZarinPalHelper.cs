using System;

namespace EShop.Web.Helper
{
    public static class ZarinPalHelper
    {
        public static string GetMerchant()
        {
            try
            {
                return ConfigurationManager.AppSetting["ZarinPal:Merchant"];
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
                return ConfigurationManager.AppSetting["ZarinPal:CallBackUrl"];
            }
            catch (Exception e)
            {
                return "";
            }
        }
        public static string GetGateWayUrl()
        {
            try
            {
                return ConfigurationManager.AppSetting["ZarinPal:GateWayUrl"];
            }
            catch (Exception e)
            {
                return "";
            }
        }
        public static string GetRequestUrl()
        {
            try
            {
                return ConfigurationManager.AppSetting["ZarinPal:RequestUrl"];
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
                return ConfigurationManager.AppSetting["ZarinPal:VerifyUrl"];
            }
            catch (Exception e)
            {
                return "";
            }
        }   
         
    
    }
}