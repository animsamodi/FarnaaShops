using System.ComponentModel.DataAnnotations;
using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Entities
{
    public class Warehouse: BaseEntity
    {
        [Display(Name = "نام")]
        public string Name { get; set; }

        [Display(Name = "محل انبار")]
        public string Mahal { get; set; }
    }
}