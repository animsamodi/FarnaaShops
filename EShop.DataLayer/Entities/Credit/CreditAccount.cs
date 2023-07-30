using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Entities.Credit
{
    public class CreditAccount:BaseEntity
    {
        public long CreditId { get; set; }
        //مشخصات حساب‌های بانکی جاری فعال شرکت / صاحب جواز کسب
        [Display(Name = "بانک")]
        public string Bank { get; set; }
        [Display(Name = "شعبه")]
        public string Shobe { get; set; }
        [Display(Name = "جاری")]
        public string Jari { get; set; }
        [Display(Name = "شماره کارت")] 
        public string ShomareKart { get; set; }
        [Display(Name = "شهرستان")]
        public string Shahrestan { get; set; }


        [ForeignKey("CreditId")]
        public Credit Credit { get; set; }
    }
}