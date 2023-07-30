using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace EShop.Core.ViewModels
{
    public class CreateMenuViewModel
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0}نباید بیشتر از {1} باشد")]
        public string ParentMenuTitle { get; set; }

        [Display(Name = "لینک")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "{0}نباید بیشتر از {1} باشد")]
        public string ParentMenuLink { get; set; }

        [Display(Name = "ترتیب ")]
        [Range(0,255,ErrorMessage = "{0}باید بین {1}و {2} باشد")]
        [RegularExpression("^[0-9]+$",ErrorMessage ="لطفا فقط عدد وارد کنید")]
        public string ParentSort { get; set; }
        public List<CreateSubMenuViewModel> SubMenuList { get; set; }

    }

    public class CreateSubMenuViewModel
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0}نباید بیشتر از {1} باشد")]
        public string SubMenuTitle { get; set; }

        [Display(Name = "لینک")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "{0}نباید بیشتر از {1} باشد")]
        public string SubMenuLink { get; set; }

        [Display(Name = "ترتیب ")]
        public int SubMenuSort { get; set; }
        public IFormFile Image { get; set; }
        public  int Type { get; set; }
        public bool IsHidden { get; set; }

    }

    public class EditMenuViewModel
    {
        public long ParentMenuId { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0}نباید بیشتر از {1} باشد")]
        public string ParentMenuTitle { get; set; }

        [Display(Name = "لینک")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "{0}نباید بیشتر از {1} باشد")]
        public string ParentMenuLink { get; set; }

        [Display(Name = "ترتیب ")]
        [Range(0, 255, ErrorMessage = "{0}باید بین {1}و {2} باشد")]
        public int ParentSort { get; set; }
        public List<EditSubMenuViewModel> SubMenuList { get; set; }

    }

    public class EditSubMenuViewModel
    {
        public long SubMenuId { get; set; }
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(50, ErrorMessage = "{0}نباید بیشتر از {1} باشد")]
        public string SubMenuTitle { get; set; }

        [Display(Name = "لینک")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        [MaxLength(250, ErrorMessage = "{0}نباید بیشتر از {1} باشد")]
        public string SubMenuLink { get; set; }

        [Display(Name = "ترتیب ")]
        public int SubMenuSort { get; set; }

        [Display(Name = "عکس فعلی ")]
        public string CurrentImage { get; set; }
        public IFormFile Image { get; set; }
        public int Type { get; set; }
        public bool IsHidden { get; set; }

    }

    public class MainMenuShowViewModel
    {
        public long MenuId { get; set; }
        public string MenuTitle { get; set; }
        public string Link { get; set; }
        public int Sort { get; set; }
        public byte Type { get; set; }
        public long? ParentId { get; set; }
    }
}
