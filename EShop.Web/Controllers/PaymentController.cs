using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Entities.Order;
using EShop.DataLayer.Enum;
using EShop.Web.Helper;
using Newtonsoft.Json;

namespace EShop.Web.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IOrderService _orderService;

        public PaymentController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public ActionResult ShowError()
        {
            return View();
        }
        public ActionResult Confirmation(long id = 0)
        {

            if (id == 0)
                throw new Exception("Id Is Null");

            var payment = GetPayment(id);

            if (payment == null)
                throw new Exception("Payment Row Not Found");


            try
            {
                 long orderID =(long)payment.Id;
                long priceAmount =  (long)payment.AmountPayable;

                long payerId = 0;
                string additionalText = "توضیحات";
                BankMellatImplement bankMellatImplement = new BankMellatImplement();
                string resultRequest = bankMellatImplement.bpPayRequest(orderID, priceAmount, additionalText, payerId);
                string[] StatusSendRequest = resultRequest.Split(',');
                if (int.Parse(StatusSendRequest[0]) == 0/*(int)BankMellatImplement.MellatBankReturnCode.ﺗﺮاﻛﻨﺶ_ﺑﺎ_ﻣﻮﻓﻘﻴﺖ_اﻧﺠﺎم_ﺷﺪ*/)
                {
                    if (StatusSendRequest[1] == null)
                    {
                        TempData["Message"] = "";

                        return RedirectToAction("ShowError");
                    }
                    else
                    {
                        ViewBag.Mablagh = priceAmount;
                        ViewBag.id = StatusSendRequest[1];
                        return View();
                    }

                }

                TempData["Message"] = bankMellatImplement.DesribtionStatusCode(int.Parse(StatusSendRequest[0].Replace("_", " ")));

                return RedirectToAction("ShowError");
            }
            catch (Exception Error)
            {
                TempData["Message"] =
                    "متاسفانه خطایی رخ داده است، لطفا مجددا عملیات خود را انجام دهید در صورت تکرار این مشکل را به بخش پشتیبانی اطلاع دهید";//Exceptions.TarakoneshMojadad;
                return RedirectToAction("ShowError");
            }
        }



        [HttpPost]
        public ActionResult BankCallback()
        {
            bool runBpReversalRequest = false;
            long saleReferenceId = -999;
            long saleOrderId = -999;
            string resultCodeBpPayRequest;

            BankMellatImplement bankMellatImplement = new BankMellatImplement();

            try
            {
                var saleReference = Request.Query["SaleReferenceId"];
                saleOrderId = long.Parse(Request.Query["SaleOrderId"].ToString());
                resultCodeBpPayRequest = Request.Query["ResCode"].ToString();
                string resultCodeBpinquiryRequest = "-9999";
                string resultCodeBpSettleRequest = "-9999";
                string resultCodeBpVerifyRequest = "-9999";
                EnumVaziyatPardakht vaziyatPardakht = EnumVaziyatPardakht.PardakhtNashode;
                if (int.Parse(resultCodeBpPayRequest) == 0)
                {
                    #region Success
                    saleReferenceId = Convert.ToInt64(saleReference);
                    resultCodeBpVerifyRequest = bankMellatImplement.VerifyRequest(saleOrderId, saleOrderId, saleReferenceId);

                    if (string.IsNullOrEmpty(resultCodeBpVerifyRequest))
                    {
                        #region Inquiry Request

                        resultCodeBpinquiryRequest = bankMellatImplement.InquiryRequest(saleOrderId, saleOrderId, saleReferenceId);
                        if (int.Parse(resultCodeBpinquiryRequest) != 0)
                        {
                            var message = bankMellatImplement.DesribtionStatusCode(int.Parse(resultCodeBpinquiryRequest.Replace("_", " ")));
                            TempData["Message"] = message;
                            runBpReversalRequest = true;

                        }

                        #endregion
                    }

                    if ((int.Parse(resultCodeBpVerifyRequest) == 0)
                        ||
                        (int.Parse(resultCodeBpinquiryRequest) == 0))
                    {

                        #region SettleRequest

                        resultCodeBpSettleRequest = bankMellatImplement.SettleRequest(saleOrderId, saleOrderId, saleReferenceId);
                        if ((int.Parse(resultCodeBpSettleRequest) == 0)
                            || (int.Parse(resultCodeBpSettleRequest) == 45/*(int)BankMellatImplement.MellatBankReturnCode.ﺗﺮاﻛﻨﺶ_Settle_ﺷﺪه_اﺳﺖ)*/))
                        {
                            vaziyatPardakht = EnumVaziyatPardakht.PardakhtShode;
                            TempData["id"] = 0;//Manager.GetBimeNameByOrderId(saleOrderId).ID;
                            var message = "تراکنش شما با موفقیت انجام شد";//Exceptions.TarakoneshMovafagh;
                            message += "لطفا شماره پیگیری را یادداشت نمایید" + saleReferenceId;
                            TempData["Message"] = message;

                        }
                        else
                        {
                            var message = bankMellatImplement.DesribtionStatusCode(int.Parse(resultCodeBpSettleRequest.Replace("_", " ")));
                            TempData["Message"] = message;
                            runBpReversalRequest = true;

                        }

                        #endregion
                    }
                    else
                    {
                        var message = bankMellatImplement.DesribtionStatusCode(int.Parse(resultCodeBpVerifyRequest.Replace("_", " ")));
                        TempData["Message"] = message;
                        runBpReversalRequest = true;

                    }

                    #endregion
                }
                else
                {
                    var message = bankMellatImplement.DesribtionStatusCode(int.Parse(resultCodeBpPayRequest)).Replace("_", " ");

                    TempData["Message"] = message;
                    runBpReversalRequest = true;
                }

                return RedirectToAction("ShowError");
            }
            catch (Exception Error)
            {
                var message =
                    "متاسفانه خطایی رخ داده است، لطفا مجددا عملیات خود را انجام دهید در صورت تکرار این مشکل را به بخش پشتیبانی اطلاع دهید";//Exceptions.TarakoneshMojadad;
                TempData["Message"] = message;
                runBpReversalRequest = true;

                
                return RedirectToAction("ShowError");
            }
            finally
            {
                if (runBpReversalRequest)
                {
                    if (saleOrderId != -999 && saleReferenceId != -999)
                        bankMellatImplement.bpReversalRequest(saleOrderId, saleOrderId, saleReferenceId);

                }
            }

        }

        public Order GetPayment(long id)
        {
           return _orderService.GetOrderById(id);
        }



    }
}
