using System;

namespace EShop.Web.Helper
{
    public static class IranKishHelper
    {
        public static string GetPaymentUrl()
        {
            try
            {
                return ConfigurationManager.AppSetting["IranKish:PaymentUrl"];
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
                return ConfigurationManager.AppSetting["IranKish:TokenUrl"];
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
                return ConfigurationManager.AppSetting["IranKish:VerifyUrl"];
            }
            catch (Exception e)
            {
                return "";
            }
        }
        public static string GetInqueryUrl()
        {
            try
            {
                return ConfigurationManager.AppSetting["IranKish:InqueryUrl"];
            }
            catch (Exception e)
            {
                return "";
            }
        }
        public static string GetTreminalId()
        {
            try
            {
                return ConfigurationManager.AppSetting["IranKish:TreminalId"];
            }
            catch (Exception e)
            {
                return "";
            }
        }   
        public static string GetAcceptorId()
        {
            try
            {
                return ConfigurationManager.AppSetting["IranKish:AcceptorId"];
            }
            catch (Exception e)
            {
                return "";
            }
        }   
        public static string GetPassPhrase()
        {
            try
            {
                return ConfigurationManager.AppSetting["IranKish:PassPhrase"];
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
                return ConfigurationManager.AppSetting["IranKish:CallBackUrl"];
            }
            catch (Exception e)
            {
                return "";
            }
        }   
        public static string GetRSAPublicKey()
        {
            try
            {
                //return ConfigurationManager.AppSetting["IranKish:RSAPublicKey"];
                return @"-----BEGIN PUBLIC KEY-----
MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDLagCx3lowQh6oeRaWxXSfuVx3
je02pArJClm9jHXQ1FosQLiJt5KNTE25N6l/19gBq6DOZi9K9/QC0z0hHcQT9GY9
k0AkDlYaOOlXVrPixsb8mKjK+56N8ZoyI6xTziq8hhAJu89vyNGNOIFK9W3knWMF
CAEyGlZfNlaVGWKUgQIDAQAB
-----END PUBLIC KEY-----";
            }
            catch (Exception e)
            {
                return "";
            }
        }   
        public static string GetCmsPreservationId()
        {
            try
            {
                return ConfigurationManager.AppSetting["IranKish:CmsPreservationId"];
            }
            catch (Exception e)
            {
                return "";
            }
        }   
         
    
    }
}