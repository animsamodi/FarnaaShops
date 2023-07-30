using EShop.DataLayer.Entities.Common;
using EShop.DataLayer.Enum;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace EShop.DataLayer.Entities.Site
{
    public class SiteSetting : BaseEntity
    {
        public string FavIcon { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string SiteName { get; set; }
        public string SiteUrl { get; set; }
        public string LinkedIn { get; set; }
        public string Instagram { get; set; }
        public string Telegram { get; set; }
        public string Enamad { get; set; }
        public string FotterText { get; set; }
        public string FotterAbout { get; set; }
        public string Logo { get; set; }
        public string TopImageBannerWeb { get; set; }
        public string TopImageBannerWebUrl { get; set; }
        public string TopImageBannerMobileUrl { get; set; }
        public string TopImageBannerWebTitle { get; set; }
        public string TopImageBannerMobile { get; set; }
        public string TopImageBannerMobileTitle { get; set; }
        public bool ShowTopImageBannerWeb { get; set; }
        public bool ShowTopImageBannerMobile { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }
        public string Canonical { get; set; }
        public string HeaderTag { get; set; }
        public string Schema { get; set; }
        public string BaseSchema { get; set; }
        public string BlogMetaTitle { get; set; }
        public string BlogMetaDescription { get; set; }
        public string BlogMetaKeywords { get; set; }
        public string BlogCanonical { get; set; }
        public string BlogHeaderTag { get; set; }
        public string BlogSchema { get; set; }
        public string BlogBaseSchema { get; set; }
        public string Robots { get; set; }
        //
        public string HomeText { get; set; }
        public string FoterText { get; set; }
        public string FoterRight { get; set; }
        public string FoterLeft { get; set; }
        //
        public EnumDefaultIpg DefaultIpg{ get; set; }
        public bool ShowMelatIpg { get; set; }
        public bool ShowApIpg { get; set; }
        public bool ShowKishIpg { get; set; }
        public bool ShowZarinPalIpg { get; set; }
        public bool ShowMeliIpg { get; set; }
        public string SearchBg { get; set; }

        [Display(Name = "نوع سیستم")]
        [Required(ErrorMessage = "لطفا {0} را وارد کنید")]
        public EnumTypeSystem TypeSystem { get; set; }
    }


}