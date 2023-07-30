using EShop.DataLayer.Entities.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace EShop.DataLayer.Entities.Components
{
    public class UiCircleComponent : BaseEntity
    {
        [Display(Name = "متن کامپوننت"), Required]
        public string Title { get; set; }

        [Display(Name = "آدرس کامپوننت"), Required]
        public string Url { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }

        [Display(Name = "اولویت نمایش")]
        public int Order { get; set; }

        public void SetIsActive(bool isActive) => IsActive = isActive;

        public void SetTitle(string title) => Title = title;

        public void SetUrl(string url) => Url = url;

        public void SetOrder(int order) => Order = order;
    }
}
