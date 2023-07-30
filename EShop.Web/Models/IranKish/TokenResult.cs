using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace EShop.Web.Models.IranKish
{
    public class TokenResult
    {
        TokenResult()
        {
            result = new Result();
        }
        public string responseCode { get; set; }
        public object description { get; set; }
        public bool status { get; set; }
        public Result result { get; set; }
    }

    public class Result
    {
        public Result()
        {
            billInfo = new Billinfo();
        }
        public string token { get; set; }
        public int initiateTimeStamp { get; set; }
        public int expiryTimeStamp { get; set; }
        public string transactionType { get; set; }
        public Billinfo billInfo { get; set; }
    }

    public class Billinfo
    {
        public object billId { get; set; }
        public object billPaymentId { get; set; }
    }

    public class RequestVerify
    {
        [DataMember]
        public string terminalId { get; set; }

        [DataMember]
        public string retrievalReferenceNumber { get; set; }

        [DataMember]
        public string systemTraceAuditNumber { get; set; }

        [DataMember]
        public string tokenIdentity { get; set; }
    }


    public class VerifyResult
    {
        public string responseCode { get; set; }
        public string description { get; set; }
        public bool status { get; set; }
        public SubResult result { get; set; }
    }

    public class SubResult
    {
        public string responseCode { get; set; }
        public string systemTraceAuditNumber { get; set; }
        public string retrievalReferenceNumber { get; set; }
        public DateTime transactionDateTime { get; set; }
        public int transactionDate { get; set; }
        public int transactionTime { get; set; }
        public string processCode { get; set; }
        public object requestId { get; set; }
        public object additional { get; set; }
        public object billType { get; set; }
        public object billId { get; set; }
        public string paymentId { get; set; }
        public string amount { get; set; }
        public object revertUri { get; set; }
        public object acceptorId { get; set; }
        public object terminalId { get; set; }
        public object tokenIdentity { get; set; }
    }

    public class Inquery
    {
        public int findOption { get; set; }
        public string passPhrase { get; set; }
        public string requestId { get; set; }
        public object retrievalReferenceNumber { get; set; }
        public string terminalId { get; set; }
        public object tokenIdentity { get; set; }
    }
}