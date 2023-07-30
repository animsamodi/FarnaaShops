using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Core.ViewModels
{
    public class TokenRequestData
    {
        public long merchantConfigurationId { get; set; }
        public long serviceTypeId { get; set; }
        public long localInvoiceId { get; set; }
        public long amountInRials { get; set; }
        public string localDate { get; set; }
        public string additionalData { get; set; }
        public string callbackURL { get; set; }
        public long paymentId { get; set; }
        public List<SettlementPortion> settlementPortions { get; set; }
    }
    public class SettlementPortion
    {
        public string iban { get; set; }
        public long amountInRials { get; set; }
        public long paymentId { get; set; }
    }
    public class TranResultResponse
    {
        public string cardNumber { get; set; }
        public string rrn { get; set; }
        public string refID { get; set; }
        public string amount { get; set; }
        public string payGateTranID { get; set; }
        public string salesOrderID { get; set; }
        public string hash { get; set; }
        public long serviceTypeId { get; set; }
        public string serviceStatusCode { get; set; }
        public string destinationMobile { get; set; }
        public long? productId { get; set; }
        public string productNameFa { get; set; }
        public long? productPrice { get; set; }
        public long? operatorId { get; set; }
        public string operatorNameFa { get; set; }
        public long? simTypeId { get; set; }
        public string simTypeTitleFa { get; set; }
        public string billId { get; set; }
        public string payId { get; set; }
        public string billOrganizationNameFa { get; set; }
        public string payGateTranDate { get; set; }
        public long payGateTranDateEpoch { get; set; }
    }

    public class VerifyRequest
    {
        public long merchantConfigurationId { get; set; }
        public long payGateTranId { get; set; }
    }

    


  
    public class ResponseError
    {
        public ResponseErrorDetail error { get; set; }
    }
    public class ResponseErrorDetail
    {
        public long code { get; set; }
        public string message { get; set; }
        public ResolveEventArgs args { get; set; }
    }
    public class ResponseErrorDetailArgs
    {
        public long merchantConfigurationId { get; set; }
    }





}
