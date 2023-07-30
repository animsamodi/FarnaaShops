using System;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EShop.Core.ExtensionMethods;
using EShop.Core.Services.Base;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities.Banner;
using EShop.DataLayer.Enum;

namespace EShop.Core.Services.Implementations
{
    public class SliderService : BaseService<Slider>, ISliderService
    {
        #region constructor

        private ApplicationDbContext _context;
        private readonly IUserService _userService;

        public SliderService(ApplicationDbContext context, IUserService userService):base(context)
        {
            _context = context;
            _userService = userService;
        }

        #endregion

        #region User Section



        public List<Slider> GetAllSlider(EnumTypeSlider type, EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa)
        {
            return _context.Sliders.Where( c=>(c.TypeSystem == typeSystem) &&(c.Type == EnumTypeSlider.Both || c.Type == type)).AsQueryable().OrderBy(o => o.sort).Take(6).ToList();

        }

        public List<Slider> GetSliderForAdmin()
        {
            return _context.Sliders.AsQueryable().ToList();
        }

        public Slider FindSliderById(long id)
        {
            return _context.Sliders.Find(id);
        }

        public bool DeleteSlider(Slider slider)
        {
            try
            {
                slider = slider.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(slider);
                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }


        }

        public bool AddSlider(Slider slider)
        {
            try
            {
                slider = slider.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(slider);
                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;

            }

        }

        public bool UpdateSlider(Slider slider)
        {
            try
            {
                slider = slider.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(slider);
                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;


            }

        }

        public BannerImage FindBannerImageById(long id)
        {
            return _context.BannerImages.Find(id);
        }

        public bool UpdateBannerImage(BannerImage bannerImage)
        {
            try
            {
                var model = FindBannerImageById(bannerImage.Id);
                model = model.SetEditDefaultValue(_userService.GetUserId());
                model.ImageName = bannerImage.ImageName;
                model.Title = bannerImage.Title;
                model.Link = bannerImage.Link;
                model.IsActive = bannerImage.IsActive;
                _context.Update(model);
                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;


            }
        }

        #endregion


    }
}
