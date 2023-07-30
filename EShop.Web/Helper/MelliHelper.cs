using System;

namespace EShop.Web.Helper
{
    public static class MelliHelper
    {
        public static string GetTerminalId()
        {
            try
            {
                return ConfigurationManager.AppSetting["BankMelli:TerminalId"];
            }
            catch (Exception e)
            {
                return "";
            }
        } 
        public static string GetMerchantId()
        {
            try
            {
                return ConfigurationManager.AppSetting["BankMelli:MerchantId"];
            }
            catch (Exception e)
            {
                return "";
            }
        } 
        
        public static string GetMerchantKey()
        {
            try
            {
                return ConfigurationManager.AppSetting["BankMelli:MerchantKey"];
            }
            catch (Exception e)
            {
                return "";
            }
        }
        public static string GetPaymentRequestUrl()
        {
            try
            {
                return ConfigurationManager.AppSetting["BankMelli:PaymentRequestUrl"];
            }
            catch (Exception e)
            {
                return "";
            }
        }
        public static string GetPurchasePage()
        {
            try
            {
                return ConfigurationManager.AppSetting["BankMelli:PurchasePage"];
            }
            catch (Exception e)
            {
                return "";
            }
        }
        public static string GetIndexTokenUrl()
        {
            try
            {
                return ConfigurationManager.AppSetting["BankMelli:IndexTokenUrl"];
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
                return ConfigurationManager.AppSetting["BankMelli:CallBackUrl"];
            }
            catch (Exception e)
            {
                return "";
            }
        }   
       
         
    
    }
}