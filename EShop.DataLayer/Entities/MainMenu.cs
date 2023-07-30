using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Entities
{
   public class MainMenu:BaseEntity
    {
        

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0}نباید بیشتر از {1} باشد")]
        public string MenuTitle { get; set; }

        [Display(Name = "لینک")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "{0}نباید بیشتر از {1} باشد")]
        public string Link { get; set; }

        [Display(Name = "ترتیب ")]
        public int Sort{ get; set; }

        [Display(Name = "اسم عکس")]
        [MaxLength(150, ErrorMessage = "{0}نباید بیشتر از {1} باشد")]
        public string ImageName { get; set; }
        public string OrginalImage { get; set; }


        [Display(Name = "نوع منو ")]
        public byte Type { get; set; }
        public long? ParentId { get; set; }

        [ForeignKey("ParentId")]
        [Display(Name = "والد ")]

        public List<MainMenu> MainMenus { get; set; }
    }
}
