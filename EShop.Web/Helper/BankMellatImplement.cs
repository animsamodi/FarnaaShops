using System;
using System.Threading.Tasks;

namespace EShop.Web.Helper
{
    public class BankMellatImplement
    {
     
        #region Base Variable Definition

        public static readonly string PgwSite = "https://bpm.shaparak.ir/pgwchannel/startpay.mellat";


        static readonly long TerminalId = Convert.ToInt64(ConfigurationManager.AppSetting["BankMellat:TerminalId"]);
        static readonly string UserName = ConfigurationManager.AppSetting["BankMellat:UserName"];
        static readonly string Password = ConfigurationManager.AppSetting["BankMellat:Password"];
        static readonly string CallBackUrl = ConfigurationManager.AppSetting["BankMellat:CallBackUrl"];

        readonly string _localDate = string.Empty;
        readonly string _localTime = string.Empty;
        #endregion

        public BankMellatImplement()
        {
            try
            {
                _localDate = DateTime.Now.ToString("yyyyMMdd");
                _localTime = DateTime.Now.ToString("HHMMSS");
            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }


        public string bpPayRequest(long orderId, long priceAmount, string additionalText, long payerId)
        {
            try
            {


                IPGMellatService.PaymentGatewayClient WebService = new IPGMellatService.PaymentGatewayClient();
                return WebService.bpPayRequest(TerminalId,
                    UserName,
                    Password,
                    orderId,
                    priceAmount,
                    _localDate,
                    _localTime,
                    additionalText, CallBackUrl, payerId.ToString(), "", "", "", "", "");

            }
            catch (Exception error)
            {
                throw new Exception(error.Message);
            }
        }

        public string VerifyRequest(long orderId, long saleOrderId, long saleReferenceId)
        {
            try
            {
                IPGMellatService.PaymentGatewayClient WebService = new IPGMellatService.PaymentGatewayClient();
                return WebService.bpVerifyRequest(TerminalId, UserName, Password, orderId, saleOrderId, saleReferenceId);


            }
            catch (Exception Error)
            {
                throw new Exception(Error.Message);
            }
        }

        public string InquiryRequest(long orderId, long saleOrderId, long saleReferenceId)
        {
            try
            {
                IPGMellatService.PaymentGatewayClient WebService = new IPGMellatService.PaymentGatewayClient();
                return WebService.bpInquiryRequest(TerminalId, UserName, Password, orderId, saleOrderId, saleReferenceId);

            }
            catch (Exception Error)
            {
                throw new Exception(Error.Message);
            }
        }


        public string SettleRequest(long orderId, long saleOrderId, long saleReferenceId)
        {
            try
            {
                IPGMellatService.PaymentGatewayClient WebService = new IPGMellatService.PaymentGatewayClient();
                return WebService.bpSettleRequest(TerminalId, UserName, Password, orderId, saleOrderId, saleReferenceId);
            }
            catch (Exception Error)
            {
                throw new Exception(Error.Message);
            }
        }

        public string bpReversalRequest(long orderId, long saleOrderId, long saleReferenceId)
        {
            try
            {

                 IPGMellatService.PaymentGatewayClient WebService = new IPGMellatService.PaymentGatewayClient();
                 return WebService.bpReversalRequest(TerminalId, UserName, Password, orderId, saleOrderId, saleReferenceId);
 
            }
            catch (Exception error)
            {
                throw new Exception(error.Message); ;
            }
        }


        public String DesribtionStatusCode(int statusCode)
        {
            switch (statusCode)
            {
                case 0:
                    return "ﺗﺮاﻛﻨﺶ_ﺑﺎ_ﻣﻮﻓﻘﻴﺖ_اﻧﺠﺎم_ﺷﺪ";
                case 11:
                    return "ﺷﻤﺎره_ﻛﺎرت_ﻧﺎﻣﻌﺘﺒﺮ_اﺳﺖ";
                case 12:
                    return "ﻣﻮﺟﻮدي_ﻛﺎﻓﻲ_ﻧﻴﺴﺖ";
                case 13:
                    return "رﻣﺰ_ﻧﺎدرﺳﺖ_اﺳﺖ";
                case 14:
                    return "ﺗﻌﺪاد_دﻓﻌﺎت_وارد_ﻛﺮدن_رﻣﺰ_ﺑﻴﺶ_از_ﺣﺪ_ﻣﺠﺎز_اﺳﺖ";
                case 15:
                    return "ﻛﺎرت_ﻧﺎﻣﻌﺘﺒﺮ_اﺳﺖ";
                case 16:
                    return "دﻓﻌﺎت_ﺑﺮداﺷﺖ_وﺟﻪ_ﺑﻴﺶ_از_ﺣﺪ_ﻣﺠﺎز_اﺳﺖ";
                case 17:
                    return "ﻛﺎرﺑﺮ_از_اﻧﺠﺎم_ﺗﺮاﻛﻨﺶ_ﻣﻨﺼﺮف_ﺷﺪه_اﺳﺖ";
                case 18:
                    return "ﺗﺎرﻳﺦ_اﻧﻘﻀﺎي_ﻛﺎرت_ﮔﺬﺷﺘﻪ_اﺳﺖ";
                case 19:
                    return "ﻣﺒﻠﻎ_ﺑﺮداﺷﺖ_وﺟﻪ_ﺑﻴﺶ_از_ﺣﺪ_ﻣﺠﺎز_اﺳﺖ";
                case 111:
                    return "ﺻﺎدر_ﻛﻨﻨﺪه_ﻛﺎرت_ﻧﺎﻣﻌﺘﺒﺮ_اﺳﺖ";
                case 112:
                    return "ﺧﻄﺎي_ﺳﻮﻳﻴﭻ_ﺻﺎدر_ﻛﻨﻨﺪه_ﻛﺎرت";
                case 113:
                    return "ﭘﺎﺳﺨﻲ_از_ﺻﺎدر_ﻛﻨﻨﺪه_ﻛﺎرت_درﻳﺎﻓﺖ_ﻧﺸﺪ";
                case 114:
                    return "دارﻧﺪه_ﻛﺎرت_ﻣﺠﺎز_ﺑﻪ_اﻧﺠﺎم_اﻳﻦ_ﺗﺮاﻛﻨﺶ_ﻧﻴﺴﺖ";
                case 21:
                    return "ﭘﺬﻳﺮﻧﺪه_ﻧﺎﻣﻌﺘﺒﺮ_اﺳﺖ";
                case 23:
                    return "ﺧﻄﺎي_اﻣﻨﻴﺘﻲ_رخ_داده_اﺳﺖ";
                case 24:
                    return "اﻃﻼﻋﺎت_ﻛﺎرﺑﺮي_ﭘﺬﻳﺮﻧﺪه_ﻧﺎﻣﻌﺘﺒﺮ_اﺳﺖ";
                case 25:
                    return "ﻣﺒﻠﻎ_ﻧﺎﻣﻌﺘﺒﺮ_اﺳﺖ";
                case 31:
                    return "ﭘﺎﺳﺦ_ﻧﺎﻣﻌﺘﺒﺮ_اﺳﺖ";
                case 32:
                    return "ﻓﺮﻣﺖ_اﻃﻼﻋﺎت_وارد_ﺷﺪه_ﺻﺤﻴﺢ_ﻧﻤﻲ_ﺑﺎﺷﺪ";
                case 33:
                    return "ﺣﺴﺎب_ﻧﺎﻣﻌﺘﺒﺮ_اﺳﺖ";
                case 34:
                    return "ﺧﻄﺎي_ﺳﻴﺴﺘﻤﻲ";
                case 35:
                    return "ﺗﺎرﻳﺦ_ﻧﺎﻣﻌﺘﺒﺮ_اﺳﺖ";
                case 41:
                    return "ﺷﻤﺎره_درﺧﻮاﺳﺖ_ﺗﻜﺮاري_اﺳﺖ";
                case 42:
                    return "ﺗﺮاﻛﻨﺶ_Sale_یافت_نشد_";
                case 43:
                    return "ﻗﺒﻼ_Verify_درﺧﻮاﺳﺖ_داده_ﺷﺪه_اﺳﺖ";



                case 44:
                    return "درخواست_verify_یافت_نشد";
                case 45:
                    return "ﺗﺮاﻛﻨﺶ_Settle_ﺷﺪه_اﺳﺖ";
                case 46:
                    return "ﺗﺮاﻛﻨﺶ_Settle_نشده_اﺳﺖ";

                case 47:
                    return "ﺗﺮاﻛﻨﺶ_Settle_یافت_نشد";
                case 48:
                    return "تراکنش_Reverse_شده_است";
                case 49:
                    return "تراکنش_Refund_یافت_نشد";
                case 412:
                    return "شناسه_قبض_نادرست_است";
                case 413:
                    return "ﺷﻨﺎﺳﻪ_ﭘﺮداﺧﺖ_ﻧﺎدرﺳﺖ_اﺳﺖ";
                case 414:
                    return "سازﻣﺎن_ﺻﺎدر_ﻛﻨﻨﺪه_ﻗﺒﺾ_ﻧﺎﻣﻌﺘﺒﺮ_اﺳﺖ";
                case 415:
                    return "زﻣﺎن_ﺟﻠﺴﻪ_ﻛﺎري_ﺑﻪ_ﭘﺎﻳﺎن_رسیده_است";
                case 416:
                    return "ﺧﻄﺎ_در_ﺛﺒﺖ_اﻃﻼﻋﺎت";
                case 417:
                    return "ﺷﻨﺎﺳﻪ_ﭘﺮداﺧﺖ_ﻛﻨﻨﺪه_ﻧﺎﻣﻌﺘﺒﺮ_اﺳﺖ";
                case 418:
                    return "اﺷﻜﺎل_در_ﺗﻌﺮﻳﻒ_اﻃﻼﻋﺎت_ﻣﺸﺘﺮي";
                case 419:
                    return "ﺗﻌﺪاد_دﻓﻌﺎت_ورود_اﻃﻼﻋﺎت_از_ﺣﺪ_ﻣﺠﺎز_ﮔﺬﺷﺘﻪ_اﺳﺖ";
                case 421:
                    return "IP_نامعتبر_است";

                case 51:
                    return "ﺗﺮاﻛﻨﺶ_ﺗﻜﺮاري_اﺳﺖ";
                case 54:
                    return "ﺗﺮاﻛﻨﺶ_ﻣﺮﺟﻊ_ﻣﻮﺟﻮد_ﻧﻴﺴﺖ";
                case 55:
                    return "ﺗﺮاﻛﻨﺶ_ﻧﺎﻣﻌﺘﺒﺮ_اﺳﺖ";
                case 61:
                    return "ﺧﻄﺎ_در_واریز";

            }
            return "";
        }
    }
}