using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Entities.Credit
{
    public class CreditPartner : BaseEntity
    {
        [Display(Name = "نام و نام خانوادگی")]
        public string FullName { get; set; }
        [Display(Name = "درصد مالکیت( سهم )")]
        public int Darsad { get; set; }


        public long CreditId { get; set; }

        [ForeignKey("CreditId")]
        public Credit Credit { get; set; }
    }
}