using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Banner;
using EShop.DataLayer.Enum;

namespace EShop.Core.ViewModels
{
   public class BannerImageViewModel
    {
        
        public string Title { get; set; }
         public string ImageName { get; set; }
         public string Link { get; set; }
 
         public int? Sort { get; set; }
         
         public EnumBannerType BannerType { get; set; }
    }
}
