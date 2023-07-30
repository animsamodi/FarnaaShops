using System;
using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Entities.Order
{
    public class CrmAccount : BaseEntity
    {
        public string Key { get; set; }
        public string Token { get; set; }
        public DateTime? TokenExpireDate { get; set; }
        public string Usename { get; set; }
        public string Password { get; set; }
        public string AccountId { get; set; }
        public string AccCostTitle1 { get; set; }
        public string AccCostTitle2 { get; set; }
        public string AccCostTitle3 { get; set; }
        public string AccCostTitle4 { get; set; }
        public string AccCost1 { get; set; }
        public string AccCost2 { get; set; }
        public string AccCost3 { get; set; }
        public string AccCost4 { get; set; }
        public int UserId { get; set; }
        public string StockCode { get; set; }
        public string CabinetCode { get; set; }
        public string CashAcc { get; set; }
        public string CashFAccCode { get; set; }
         public string CashCCCode { get; set; }
        public string CashPrjCode { get; set; }
        public int InsertType { get; set; }
 
    }
}