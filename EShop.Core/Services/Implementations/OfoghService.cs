using System;
using System.Collections.Generic;
using System.Linq;
using EShop.Core.ExtensionMethods;
using EShop.Core.Helpers;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.Core.ViewModels.Product;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Entities.Banner;
using EShop.DataLayer.Entities.Category;
using EShop.DataLayer.Entities.Ofogh;
using EShop.DataLayer.Entities.Site;
using EShop.DataLayer.Enum;
using Microsoft.EntityFrameworkCore;

namespace EShop.Core.Services.Implementations
{
    public class OfoghService : BaseService<OfoghHistory>, IOfoghService 
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public OfoghService(ApplicationDbContext context, IUserService userService):base(context)
        {
            _context = context;
            _userService = userService;
        }


     


        public List<OfoghHistory> GetListForAdminKamkar()
        {
            return _context.OfoghHistories.Where(c=>c.TypeOfogh == EnumTypeOfogh.Hamkar)
                .OrderByDescending(c => c.Id).ToList();
        }

        public List<OfoghHistory> GetListForAdminKhord()
        {
            return _context.OfoghHistories.Where(c => c.TypeOfogh == EnumTypeOfogh.KhordeForush)
                .OrderByDescending(c => c.Id).ToList();
        }


        public bool Add(OfoghHistory OfoghHistory)
        {
            try
            {
                OfoghHistory.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(OfoghHistory);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(OfoghHistory OfoghHistory)
        {
            try
            {
                OfoghHistory.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(OfoghHistory);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(OfoghHistory OfoghHistory)
        {
            try
            {
                OfoghHistory.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(OfoghHistory);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public OfoghHistory FindById(long id)
        {
            return _context.OfoghHistories.Find(id);
        }

        public bool AddRange(List<OfoghHistory> ofoghHistories)
        {
            try
            {
                foreach (var ofoghHistory in ofoghHistories)
                {
                    ofoghHistory.SetCreateDefaultValue(_userService.GetUserId());

                }
                _context.AddRange(ofoghHistories);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
