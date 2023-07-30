using System;

namespace EShop.Core.ViewModels
{
    public class CrmTokenResult
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public long expires_in { get; set; }
        public string userName { get; set; } 
    }
    
    public class InitializeFactor
    {
        public string token { get; set; }

        public DateTime pSPDate { get; set; }
        public int pSPReference { get; set; }
        public string pPrjCode { get; set; }
        public string pRCVRName { get; set; }
        public string pRCVRPhone { get; set; }
        public string pRCVRPostalCode { get; set; }
        public string pRCVRAddress { get; set; }
        public string pCustNationalCode { get; set; }
        public string pCustName { get; set; }
        public string pCustPhone { get; set; }
        public string pCustAddress { get; set; }
        public string pCustProvince { get; set; }
        public string pCustCity { get; set; }
        public string pCustPostalCode { get; set; }
        public float pDiscount { get; set; }
         public float pCostAmount1 { get; set; }
        public float pCostAmount2 { get; set; }
        public float pCostAmount3 { get; set; }
        public float pCostAmount4 { get; set; }
        public string pSPDesc { get; set; }
        public string pMerchandiseCode { get; set; }
        public float pAmount { get; set; }
        public float pUnitPrice { get; set; }
        public float pCashValue { get; set; }
        public string pCashDesc { get; set; }
        public string PMerchDesc { get; set; } 
        public float pRemainder { get; set; }
        public long pQId { get; set; }
        //
        public string pCustFamily { get; set; }
        public int pCustType { get; set; }
        public string pCDesc { get; set; }
        public string pSubscriptionNo { get; set; }
        public string pGroupName { get; set; }
        public string pAddress2 { get; set; }
        public string pCEOEmail { get; set; }
        public string pWhatsapp { get; set; }
        public string pTelegram { get; set; }
        public string pLinkdin { get; set; }
        public string pFacebook { get; set; }
        public string pManuFactoryNo { get; set; }
        public string pWebSite { get; set; }
 
    }
     public class InitializeFactorResult
    {
        public string Item { get; set; }
        public string ValidationItem { get; set; }
        public string Description { get; set; }
        public string ErrorCode { get; set; }

 
        
    }
    public class ValidityAndImportInvoce
    {
        public string token { get; set; }
        public int pSPReference { get; set; }
        public long pQId { get; set; }
  

    }
}