using EShop.Core.ExtensionMethods;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities.Site;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EShop.Core.Services.Implementations
{
    public class FaqService : BaseService<Faq>, IFaqService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public FaqService(ApplicationDbContext context, IUserService userService) : base(context)
        {
            _context = context;
            _userService = userService;
        }


        public List<Faq> GetListForAdmin()
        {
            return _context.Faqs.ToList();
        }

        public List<Faq> GetListForUser()
        {
            return _context.Faqs.ToList();

        }

        public bool Add(Faq Faq)
        {
            try
            {
                Faq.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(Faq);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(Faq Faq)
        {
            try
            {
                Faq.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(Faq);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(Faq Faq)
        {
            try
            {
                Faq.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(Faq);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Faq FindById(long id)
        {
            return _context.Faqs.Find(id);
        }
    }
}
