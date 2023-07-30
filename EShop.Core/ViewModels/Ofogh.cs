using EShop.DataLayer.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.Core.ViewModels
{
    public class OfoghEntry
    {

        public string PersonNationalID { get; set; }
        public string UserRoleIDstr { get; set; }
        public OfoghUserRoleExtraField UserRoleExtraFields { get; set; }
        public DateTime DocumentDate { get; set; } 
        public string Description { get; set; }
        public OfoghBuyerDatile BuyerDatiles { get; set; }
        public string PostalCode { get; set; }
        public string DocNumber { get; set; }
        public EnumStuffGroupType StuffGroupId { get; set; }
        public OfoghTrackingCode TrackingCodesList { get; set; }
        public OfoghServiceViewModel ServiceList { get; set; }
        public EnumStatusAppointment statusAppointment { get; set; }


    }
    public class OfoghUserRoleExtraField
    {

        public int PostalCode { get; set; }
        public int LicenseNumber { get; set; }
        public EnumActivityType ActivityType { get; set; }



    }
    public class OfoghBuyerDatile
    {

        public EnumBuyerType BuyerType { get; set; }
        public string BuyerName { get; set; }
        public string BuyerNationalCode { get; set; }
        public string BuyerMobile { get; set; }
        public string CompanyName { get; set; }
        public string BuyerManagerNationalCode { get; set; }
        public string CompanyNationalCode { get; set; }



    }

    public class OfoghTrackingCode
    {

        public string TrackingCode { get; set; }
        public string Price { get; set; }



    }
    public class OfoghServiceViewModel
    {

        public string Description { get; set; }
        public int ServicePrice { get; set; }



    }
    public class OfoghOutput
    {
        public OfoghObjList ObjList { get; set; }
        public int ResultCode { get; set; }
        public string ResultMessage { get; set; }
    }
    public class OfoghObjList
    {
        public string RolesList { get; set; }
        public int FactorID { get; set; }
    }
}
