using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Entities.Product;

namespace EShop.DataLayer.Entities
{
    public class Supplier : BaseEntity
    {

        [Display(Name = "نام طرف شرکت")]
        public string FullName { get; set; }
        [Display(Name = "نام شرکت")]
        public string Company { get; set; }
        [Display(Name = "موبایل")]
        public string Mobile { get; set; }
        [Display(Name = "تلفن")]
        public string Phone { get; set; }
        [Display(Name = "آدرس")]
        public string Address { get; set; }
        [Display(Name = "کدپستی")]
        public string CodePosti { get; set; }
        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        //
        public List<SupplierFactor> SupplierFactors { get; set; }

    }
}