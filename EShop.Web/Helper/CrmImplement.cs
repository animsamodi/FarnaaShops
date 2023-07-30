using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using EShop.Core.Helpers;
using EShop.Core.ServiceApi;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities.Order;
using Newtonsoft.Json;

namespace EShop.Web.Helper
{
    public class CrmImplement
    {

        #region Base Variable Definition



        static readonly string TokenUrl = ConfigurationManager.AppSetting["Crm:token"];
        static readonly string InitializeFactorInfoUrl = ConfigurationManager.AppSetting["Crm:initializeFactorInfo"];
        static readonly string ValidityAndImportInvoceUrl = ConfigurationManager.AppSetting["Crm:ValidityANDImportInvoce"];
        static readonly string FactorDetailUrl = ConfigurationManager.AppSetting["Crm:FactorDetail"];
        private ICrmService _crmService;
        #endregion

        public CrmImplement(ICrmService crmService)
        {
            _crmService = crmService;
        }

        public string GetToken(long? userId = null, long? orderId = null)
        {
            string token = "";
            var account = GetAccount();
            if (string.IsNullOrEmpty(account.Token) || account.TokenExpireDate < DateTime.Now)
            {
                var apiCaller = new ApiCaller(TokenUrl);
                var loginData = new
                {
                    username = account.Usename,
                    password = account.Password,
                    grant_type = "password"
                };

                var data = apiCaller.Call(HttpMethod.Post,
                    "login",
                    account.Usename, account.Password, "password");
                CrmLog log = new CrmLog
                {
                    CreateDate = DateTime.Now,
                    DateCall = DateTime.Now,
                    IsDelete = false,
                    LastUpdateDate = DateTime.Now,
                    Params = JsonConvert.SerializeObject(loginData),
                    Result = data.ToString(),
                    Url = TokenUrl,
                    UserId = userId,
                    OrderId = orderId
                };
                var logId = AddCrmLog(log);
                try
                {
                    var res = JsonConvert.DeserializeObject<CrmTokenResult>(data.ToString());
                    var crmLog = GetCrmLog(logId);
                    crmLog.Token = res.access_token;
                    crmLog.Status = true;
                    UpdateToken(res.access_token);
                    UpdateCrmLog(crmLog);
                    token = res.access_token;
                }
                catch (Exception e)
                {
                }
            }
            else
            {
                token = account.Token;
            }


            return token;
        }



        public bool InitializeFactorInfo(InitializeFactor factor, long? userId = null, long? orderId = null, long? CrmSendCode = null)
        {
            try
            {
                try
                {
                    var t = factor.pSPDate.ToString("yyyy-MM-dd");
                }
                catch (Exception e)
                {

                }
                var account = GetAccount();
                var sParams =
                    $"&pSPDate={factor.pSPDate.ToString("yyyy-MM-dd")}" +
                              $"&pSPReference={factor.pSPReference}" +
                              $"&pAccountId={account.AccountId}" +
                              $"&pPrjCode={factor.pPrjCode}" +
                              $"&pRCVRName={factor.pRCVRName}" +
                              $"&pRCVRPhone={factor.pRCVRPhone}" +
                              $"&pRCVRPostalCode={factor.pRCVRPostalCode}" +
                              $"&pRCVRAddress={factor.pRCVRAddress.ReplaceSpecialChars(" ")}" +
                              $"&pCustNationalCode={factor.pCustNationalCode}" +
                              $"&pCustName={factor.pCustName}" +
                              $"&pCustPhone={factor.pCustPhone}" +
                              $"&pCustAddress={factor.pCustAddress.ReplaceSpecialChars(" ")}" +
                              $"&pCustProvince={factor.pCustProvince}" +
                              $"&pCustCity={factor.pCustCity}" +
                              $"&pCustPostalCode={factor.pCustPostalCode}" +
                              $"&pDiscount={factor.pDiscount}" +
                              $"&pACCCost1={account.AccCost1}" +
                              $"&pCostAmount1={factor.pCostAmount1}" +
                              $"&pACCCost2={account.AccCost2}" +
                              $"&pCostAmount2={factor.pCostAmount2}" +
                              $"&pSPDesc={factor.pSPDesc}" +
                              $"&pUserId={account.UserId}" +
                              $"&pMerchandiseCode={factor.pMerchandiseCode}" +
                              $"&pAmount={factor.pAmount}" +
                              $"&pStockCode={account.StockCode}" +
                              $"&pCabinetCode={account.CabinetCode}" +
                              $"&pUnitPrice={factor.pUnitPrice}" +
                              $"&pCashAcc={account.CashAcc}" +
                              $"&pCashFAccCode={account.CashFAccCode}" +
                              $"&pCashCCCode={account.CashCCCode}" +
                              $"&pCashPrjCode={account.CashPrjCode}" +
                              $"&pCashValue={factor.pCashValue}" +
                              $"&pCashDesc={factor.pCashDesc}" +
                              $"&PMerchDesc={factor.PMerchDesc}" +
                              $"&pRemainder={factor.pRemainder}" +
                              $"&pQId={factor.pQId}" +
                              //
                              $"&pCustFamily={factor.pCustFamily}" +
                              $"&pCustType={factor.pCustType}" +
                              $"&pCDesc={factor.pCDesc}" +
                              $"&pSubscriptionNo={factor.pSubscriptionNo}" +
                              $"&pGroupName={factor.pGroupName}" +
                              $"&pAddress2={factor.pAddress2}" +
                              $"&pCEOEmail={factor.pCEOEmail}" +
                              $"&pWhatsapp={factor.pWhatsapp}" +
                              $"&pTelegram={factor.pTelegram}" +
                              $"&pLinkdin={factor.pLinkdin}" +
                              $"&pFacebook={factor.pFacebook}" +
                              $"&pManuFactoryNo={factor.pManuFactoryNo}" +
                              $"&pWebSite={factor.pWebSite}"
                    ;




                FormUrlEncodedContent content = new FormUrlEncodedContent(new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>("pSPDate", factor.pSPDate.ToString("yyyy-MM-dd")),
                    new KeyValuePair<string, string>("pSPReference",  factor.pSPReference.ToString()),
                    new KeyValuePair<string, string>("pAccountId", account.AccountId.ToString()),
                    new KeyValuePair<string, string>("pPrjCode", factor.pPrjCode ?? ""),
                    new KeyValuePair<string, string>("pRCVRName", factor.pRCVRName.ToString()),
                    new KeyValuePair<string, string>("pRCVRPhone", factor.pRCVRPhone.ToString()),
                    new KeyValuePair<string, string>("pRCVRPostalCode", factor.pRCVRPostalCode.ToString()),
                    new KeyValuePair<string, string>("pRCVRAddress", factor.pRCVRAddress.ReplaceSpecialChars(" ")),
                    new KeyValuePair<string, string>("pCustNationalCode", factor.pCustNationalCode.ToString()),
                    new KeyValuePair<string, string>("pCustName", factor.pCustName.ToString()),
                    new KeyValuePair<string, string>("pCustPhone", factor.pCustPhone.ToString()),
                    new KeyValuePair<string, string>("pCustAddress", factor.pCustAddress.ReplaceSpecialChars(" ") ?? ""),
                    new KeyValuePair<string, string>("pCustProvince", factor.pCustProvince),
                    new KeyValuePair<string, string>("pCustCity", factor.pCustCity.ToString()),
                    new KeyValuePair<string, string>("pCustPostalCode", factor.pCustPostalCode ?? ""),
                    new KeyValuePair<string, string>("pDiscount", factor.pDiscount.ToString()),
                    new KeyValuePair<string, string>("pACCCost1", account.AccCost1.ToString()),
                    new KeyValuePair<string, string>("pCostAmount1",factor.pCostAmount1.ToString()),
                    new KeyValuePair<string, string>("pACCCost2", account.AccCost2.ToString()),
                    new KeyValuePair<string, string>("pCostAmount2", factor.pCostAmount2.ToString()),
                    new KeyValuePair<string, string>("pSPDesc", factor.pSPDesc.ToString()),
                    new KeyValuePair<string, string>("pUserId", account.UserId.ToString()),
                    new KeyValuePair<string, string>("pMerchandiseCode", factor.pMerchandiseCode.ToString()),
                    new KeyValuePair<string, string>("pAmount", factor.pAmount.ToString()),
                    new KeyValuePair<string, string>("pStockCode", account.StockCode.ToString()),
                    new KeyValuePair<string, string>("pCabinetCode", account.CabinetCode.ToString()),
                    new KeyValuePair<string, string>("pUnitPrice", factor.pUnitPrice.ToString()),
                    new KeyValuePair<string, string>("pCashAcc", account.CashAcc.ToString()),
                    new KeyValuePair<string, string>("pCashFAccCode", account.CashFAccCode.ToString()),
                    new KeyValuePair<string, string>("pCashCCCode", account.CashCCCode.ToString()),
                    new KeyValuePair<string, string>("pCashPrjCode", account.CashPrjCode.ToString()),
                    new KeyValuePair<string, string>("pCashValue", factor.pCashValue.ToString()),
                    new KeyValuePair<string, string>("pCashDesc", factor.pCashDesc.ToString()),
                    new KeyValuePair<string, string>("PMerchDesc", factor.PMerchDesc.ToString()),
                    new KeyValuePair<string, string>("pRemainder", factor.pRemainder.ToString()),
                    new KeyValuePair<string, string>("pQId", factor.pQId.ToString()),
                    //
                    new KeyValuePair<string, string>("pCustFamily", factor.pCustFamily.ToString()),
                    new KeyValuePair<string, string>("pCustType", factor.pCustType.ToString()),
                    new KeyValuePair<string, string>("pCDesc", factor.pCDesc.ToString()),
                    new KeyValuePair<string, string>("pSubscriptionNo", factor.pSubscriptionNo.ToString()),
                    new KeyValuePair<string, string>("pGroupName", factor.pGroupName.ToString()),
                    new KeyValuePair<string, string>("pAddress2", factor.pAddress2.ToString()),
                    new KeyValuePair<string, string>("pCEOEmail", factor.pCEOEmail.ToString()),
                    new KeyValuePair<string, string>("pWhatsapp", factor.pWhatsapp.ToString()),
                    new KeyValuePair<string, string>("pTelegram", factor.pTelegram.ToString()),
                    new KeyValuePair<string, string>("pLinkdin", factor.pLinkdin.ToString()),
                    new KeyValuePair<string, string>("pFacebook", factor.pFacebook.ToString()),
                    new KeyValuePair<string, string>("pManuFactoryNo", factor.pManuFactoryNo.ToString()),
                    new KeyValuePair<string, string>("pWebSite", factor.pWebSite.ToString())

                });
                var tt = factor.pCashValue.ToString();
                var apiCaller = new ApiCaller(InitializeFactorInfoUrl);
                var data = apiCaller.Call(HttpMethod.Post,
                    "token",
                    factor.token, content);








                CrmLog log = new CrmLog
                {
                    CreateDate = DateTime.Now,
                    DateCall = DateTime.Now,
                    IsDelete = false,
                    LastUpdateDate = DateTime.Now,
                    Params = sParams,
                    Result = data.ToString(),
                    Url = InitializeFactorInfoUrl,
                    Token = factor.token,
                    UserId = userId,
                    OrderId = orderId,
                    OrderDetailId = CrmSendCode
                };
                var logId = AddCrmLog(log);
                try
                {
                    var res = JsonConvert.DeserializeObject<List<InitializeFactorResult>>(data.ToString() ?? string.Empty);
                    if (res.FirstOrDefault()?.ErrorCode == "DONE")
                    {
                        var crmLog = GetCrmLog(logId);
                        crmLog.Status = true;
                        crmLog.ResultMessage = res.FirstOrDefault()?.Description;
                        crmLog.ResultCode = res.FirstOrDefault()?.ErrorCode;
                        UpdateCrmLog(crmLog);
                        return true;

                    }
                    else
                    {
                        try
                        {
                            var crmLog = GetCrmLog(logId);
                            crmLog.Status = false;
                            crmLog.ResultMessage = res.FirstOrDefault()?.Description;
                            crmLog.ResultCode = res.FirstOrDefault()?.ErrorCode;
                            UpdateCrmLog(crmLog);
                        }
                        catch (Exception e)
                        {

                        }
                    }

                    return false; ;
                }
                catch (Exception e)
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                return false;
            }
        }
        public int ValidityAndImportInvoce(ValidityAndImportInvoce validityAndImportInvoce, long? userId = null, long? orderId = null, long? CrmSendCode = null)
        {
            try
            {
                var account = GetAccount();
                var sParams = $"&pSPReference={validityAndImportInvoce.pSPReference}" +
                              $"&pUserId={account.UserId}" +
                              $"&pInsertType={account.InsertType}" +
                              $"&pQId={validityAndImportInvoce.pQId}";
                FormUrlEncodedContent content = new FormUrlEncodedContent(new KeyValuePair<string, string>[]
           {
                    new KeyValuePair<string, string>("pSPReference", validityAndImportInvoce.pSPReference.ToString()),
                    new KeyValuePair<string, string>("pUserId",  account.UserId.ToString()),
                    new KeyValuePair<string, string>("pInsertType", account.InsertType.ToString()),
                    new KeyValuePair<string, string>("pQId", validityAndImportInvoce.pQId.ToString()),

           });
                var apiCaller = new ApiCaller(ValidityAndImportInvoceUrl);


                var data = apiCaller.Call(HttpMethod.Post,
                    "token",
                    validityAndImportInvoce.token
                    , content);
                CrmLog log = new CrmLog
                {
                    CreateDate = DateTime.Now,
                    DateCall = DateTime.Now,
                    IsDelete = false,
                    LastUpdateDate = DateTime.Now,
                    Params = sParams,
                    Result = data?.ToString(),
                    Url = ValidityAndImportInvoceUrl,
                    Token = validityAndImportInvoce.token,
                    UserId = userId,
                    OrderId = orderId,
                    OrderDetailId = CrmSendCode
                };
                var logId = AddCrmLog(log);
                try
                {
                    var res = JsonConvert.DeserializeObject<List<InitializeFactorResult>>(data?.ToString() ?? string.Empty);
                    if (res != null && res.FirstOrDefault()?.ErrorCode == "DONE")
                    {
                        var crmLog = GetCrmLog(logId);
                        crmLog.Status = true;
                        crmLog.ResultMessage = res.FirstOrDefault()?.Description;
                        crmLog.ResultCode = res.FirstOrDefault()?.ErrorCode;
                        UpdateCrmLog(crmLog);
                        return Convert.ToInt32(res.FirstOrDefault()?.ValidationItem);

                    }
                    else
                    {
                        try
                        {
                            var crmLog = GetCrmLog(logId);
                            crmLog.Status = false;
                            crmLog.ResultMessage = res.FirstOrDefault()?.Description;
                            crmLog.ResultCode = res.FirstOrDefault()?.ErrorCode;
                            UpdateCrmLog(crmLog);
                        }
                        catch (Exception e)
                        {

                        }
                    }

                    return 0; ;

                }
                catch (Exception e)
                {
                    return 0;
                }

            }
            catch (Exception e)
            {
                return 0;
            }
        }
        public string FactorDetail(string token,long orderId,long? userId)
        {
            try
            {
                var account = GetAccount();
                var sParams = $"&pQId={orderId}";
                FormUrlEncodedContent content = new FormUrlEncodedContent(new KeyValuePair<string, string>[]
           {

                    new KeyValuePair<string, string>("pQId", orderId.ToString()),

           });
                var apiCaller = new ApiCaller(FactorDetailUrl);


                var data = apiCaller.Call(HttpMethod.Post,
                    "token",
                    token
                    , content);
                CrmLog log = new CrmLog
                {
                    CreateDate = DateTime.Now,
                    DateCall = DateTime.Now,
                    IsDelete = false,
                    LastUpdateDate = DateTime.Now,
                    Params = sParams,
                    Result = data.ToString(),
                    Url = ValidityAndImportInvoceUrl,
                    Token = token,
                    UserId = userId,
                    OrderId = orderId
                };
                var logId = AddCrmLog(log);
                return data.ToString();
                //try
                //{
                //    var res = JsonConvert.DeserializeObject<List<InitializeFactorResult>>(data.ToString() ?? string.Empty);
                //    if (res.FirstOrDefault()?.ErrorCode == "DONE")
                //    {
                //        var crmLog = GetCrmLog(logId);
                //        crmLog.Status = true;
                //        crmLog.ResultMessage = res.FirstOrDefault()?.Description;
                //        crmLog.ResultCode = res.FirstOrDefault()?.ErrorCode;
                //        UpdateCrmLog(crmLog);
                //        return Convert.ToInt32(res.FirstOrDefault()?.ValidationItem);

                //    }
                //    else
                //    {
                //        try
                //        {
                //            var crmLog = GetCrmLog(logId);
                //            crmLog.Status = false;
                //            crmLog.ResultMessage = res.FirstOrDefault()?.Description;
                //            crmLog.ResultCode = res.FirstOrDefault()?.ErrorCode;
                //            UpdateCrmLog(crmLog);
                //        }
                //        catch (Exception e)
                //        {

                //        }
                //    }

                //    return 0; ;

                //}
                //catch (Exception e)
                //{
                //    return 0;
                //}

            }
            catch (Exception e)
            {
                return "";
            }
        }

        private void UpdateToken(string token)
        {
            _crmService.UpdateToken(token);
        }
        private CrmAccount GetAccount()
        {
            return _crmService.GetCrmAccount();
        }

        private long AddCrmLog(CrmLog crmLog)
        {
            return _crmService.AddCrmLog(crmLog);
        }
        private CrmLog GetCrmLog(long id)
        {
            return _crmService.GetCrmLogById(id);
        }
        private void UpdateCrmLog(CrmLog crmLog)
        {
            _crmService.UpdateCrmLog(crmLog);
        }

    }
}
