using EShop.Core.ExtensionMethods;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities.Variety;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using EShop.DataLayer.Entities.Order;
using EShop.DataLayer.Enum;

namespace EShop.Core.Services.Implementations
{
    public class PackingService : BaseService<Packing>, IPackingService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public PackingService(ApplicationDbContext context, IUserService userService) : base(context)
        {
            _context = context;
            _userService = userService;
        }


        public List<Packing> GetListForAdmin()
        {
            return _context.Packings
                .OrderByDescending(c => c.Id).ToList();
        }

        public List<Packing> GetListForUser(EnumTypeSystem typeSystem)
        {
            return _context.Packings.Where(c=>c.TypeSystem == typeSystem)
                .OrderBy(c => c.Order).ToList();
        }

        public bool Add(Packing entity)
        {
            try
            {
                entity.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(Packing entity)
        {
            try
            {
                entity.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(Packing entity)
        {
            try
            {
                entity.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Packing FindById(long id)
        {
            return _context.Packings.Find(id);
        }

       
    }
}