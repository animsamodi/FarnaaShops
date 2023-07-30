using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Enum;

namespace EShop.DataLayer.Entities.Ofogh
{
    public class _OfoghHistory : BaseEntity
    {
        [Display(Name = "کد/ شناسه ملی فروشنده")]
        public string PersonNationalID { get; set; }
        [Display(Name = "کد نقش فروشنده")]
        public string UserRoleIDstr { get; set; }

        /// <OfoghUserRoleExtraField>
        [Display(Name = "کد پستی محل فعالیت")]
        public int OfoghUserRoleExtraFieldPostalCode { get; set; }
        [Display(Name = "شماره مجوز")]
        public int OfoghUserRoleExtraFieldLicenseNumber { get; set; }
        [Display(Name = "نوع فعالیت")]
        public EnumActivityType ActivityType { get; set; }
        /// </OfoghUserRoleExtraField>
        [Display(Name = "تاریخ سند")]
        public DateTime OfoghUserRoleExtraFieldDocumentDate { get; set; }
        [Display(Name = "شرح سند")]
        public string OfoghUserRoleExtraFieldDescription { get; set; }
        /// <OfoghBuyerDatile>
        [Display(Name = "نوع خریدار")]
        public EnumBuyerType OfoghBuyerDatileBuyerType { get; set; }
        [Display(Name = "نام خریدار")]
        public string OfoghBuyerDatileBuyerName { get; set; }
        [Display(Name = "کد ملی خریدار")]
        public string OfoghBuyerDatileBuyerNationalCode { get; set; }
        [Display(Name = "شماره همراه خریدار")]
        public string OfoghBuyerDatileBuyerMobile { get; set; }
        [Display(Name = "نام شرکت خریدار")]
        public string OfoghBuyerDatileCompanyName { get; set; }
        [Display(Name = "کد ملی مدیرعامل")]
        public string OfoghBuyerDatileBuyerManagerNationalCode { get; set; }
        [Display(Name = "شناسه ملی شرکت")]
        public string OfoghBuyerDatileCompanyNationalCode { get; set; }
        /// </OfoghBuyerDatile>
        [Display(Name = "کد پستی انبار مبدا")]
        public string OfoghBuyerDatilePostalCode { get; set; }
        [Display(Name = "شماره فاکتور")]
        public string OfoghBuyerDatileDocNumber { get; set; }
        [Display(Name = "گروه کالایی")]
        public EnumStuffGroupType StuffGroupId { get; set; }
        /// <OfoghTrackingCode>
        [Display(Name = "شناسه رهگیری")]
        public string OfoghTrackingCodeTrackingCode { get; set; }
        [Display(Name = "قیمت کالا")]
        public string OfoghTrackingCodePrice { get; set; }
        /// </OfoghTrackingCode>
        /// <OfoghService>
        [Display(Name = "شرح خدمات")]
        public string OfoghServiceDescription { get; set; }
        [Display(Name = "خدمات")]
        public int OfoghServiceServicePrice { get; set; }
        /// </OfoghService>
        [Display(Name = "وضعیت ثبت سند")]
        public EnumStatusAppointment statusAppointment { get; set; }
        //
        /// <OfoghOutput>
        /// <OfoghObjList>
        [Display(Name = "لیست نقش ها")]
        public string OfoghObjListRolesList { get; set; }

        [Display(Name = "شماره سند")]
        public int OfoghObjListFactorID { get; set; }
        /// </OfoghObjList>
        [Display(Name = "کد خطا")]
        public int OfoghOutputResultCode { get; set; }
        [Display(Name = "متن خطا")]
        public string OfoghOutputResultMessage { get; set; }
         
        

    }

    public class OfoghHistory : BaseEntity
    {
        [Display(Name = "نوع")]

        public EnumTypeOfogh TypeOfogh { get; set; }
        [Display(Name = "نوع")]

        public EnumStatusOfogh StatusOfogh { get; set; }
        [Display(Name = "پیام خروجی")]

        public string ResultMessage { get; set; }
        [Display(Name = "کد خروجی")]

        public string CodeMessage { get; set; }
        [Display(Name = "تعداد تلاش")]

        public int CountTry { get; set; }

        [Display(Name = "تاریخ اخرین تلاش")]

        public DateTime LastTryDate { get; set; }

        [Display(Name = "زمان اخرین تلاش")]

        public string LastTryTime { get; set; }

        [Display(Name = "تاریخ سند")]
        public string TarikhSanad { get; set; }
        [Display(Name = "شماره صورتحساب")]
        public string ShomareSuratHesab { get; set; }
        [Display(Name = "کد/شناسه ملی خریدار")]
        public string ShenaseMeli { get; set; }
        [Display(Name = "نام خریدار")]
        public string NameKharidar { get; set; }
        [Display(Name = "تلفن همراه")]
        public string Mobile { get; set; }
        [Display(Name = "کدپستی انبار مبدا")]
        public string CodePostiMabda { get; set; }
        [Display(Name = "شرح سند")]
        public string Sharh { get; set; }
        /// <OfoghBuyerDatile>
        [Display(Name = "شناسه کالا")]
        public string ShenaseKala { get; set; }

        [Display(Name = "مبلغ واحد (ریال)")]
        public string Mablagh { get; set; }
        //Khord


        [Display(Name = "شناسه رهگیری")]
        public string ShenaseRahgiri { get; set; }
        //End Khord

        //Hamkar

        [Display(Name = " کد نقش تجاری خریدار  ")]
        public string CodeNaghs { get; set; }
        [Display(Name = " کدپستی انبار مقصد ")]
        public string CodePostiMaghsad { get; set; }
        [Display(Name = " شماره قرارداد بورس ")]
        public string ShomareGharardadBurs { get; set; }
        [Display(Name = " وضعیت حمل ")]
        public string VaziyatHaml { get; set; }
        [Display(Name = " شماره بارنامه ")]
        public string ShomareBarname { get; set; }
        [Display(Name = " تاریخ بارنامه ")]
        public string TarikhBarname { get; set; }
        [Display(Name = " سریال بارنامه ")]
        public string SerialBarname { get; set; }
        [Display(Name = " تعداد/مقدار ")]
        public string Tedad { get; set; }
        [Display(Name = " مبلغ تخفیف (ریال) ")]
        public string Takhfif { get; set; }
        [Display(Name = " مبلغ مالیات و عوارض (ریال) ")]
        public string Maliyat { get; set; }
        //End Hamkar




    }





}
