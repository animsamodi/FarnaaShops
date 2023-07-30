using EShop.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EShop.Core.Services.Base;
using EShop.DataLayer.Entities.Banner;
using EShop.DataLayer.Enum;

namespace EShop.Core.Services.Interfaces
{
    public interface ISliderService : IBaseService<Slider>
    {
        List<Slider> GetAllSlider( EnumTypeSlider type, EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa);

        List<Slider> GetSliderForAdmin();
        Slider FindSliderById(long id);
        bool DeleteSlider(Slider slider);
        bool AddSlider(Slider slider);
        bool UpdateSlider(Slider slider);
        BannerImage FindBannerImageById(long id);
        bool UpdateBannerImage(BannerImage bannerImage);
    }
}