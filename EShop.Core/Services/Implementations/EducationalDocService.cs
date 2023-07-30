using System;
using System.Collections.Generic;
using System.Linq;
using EShop.Core.ExtensionMethods;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.Core.ViewModels.Product;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Entities.Banner;
using EShop.DataLayer.Entities.Site;
using EShop.DataLayer.Enum;
using Microsoft.EntityFrameworkCore;

namespace EShop.Core.Services.Implementations
{
    public class EducationalDocService : BaseService<EducationalDoc>, IEducationalDocService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public EducationalDocService(ApplicationDbContext context, IUserService userService):base(context)
        {
            _context = context;
            _userService = userService;
        }


        public List<EducationalDoc> GetListForAdmin()
        {
            return _context.EducationalDocs.ToList();
        }

        public List<EducationalDoc> GetListForUser()
        {
            return _context.EducationalDocs.Where(c=>c.IsActive).ToList();

        }

        public bool Add(EducationalDoc educationalDoc)
        {
            try
            {
                educationalDoc.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(educationalDoc);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(EducationalDoc educationalDoc)
        {
            try
            {
                educationalDoc.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(educationalDoc);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(EducationalDoc educationalDoc)
        {
            try
            {
                educationalDoc.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(educationalDoc);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public EducationalDoc FindById(long id)
        {
            return _context.EducationalDocs.Find(id);
        }
    }
}
