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
    public class ContactService : BaseService<Contact>, IContactService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public ContactService(ApplicationDbContext context, IUserService userService) : base(context)
        {
            _context = context;
            _userService = userService;
        }


        public List<Contact> GetListForAdmin()
        {
            return _context.Contacts.ToList();
        }

        public List<Contact> GetListForUser()
        {
            return _context.Contacts.ToList();

        }

        public bool Add(Contact Contact)
        {
            try
            {
                Contact.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(Contact);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(Contact Contact)
        {
            try
            {
                Contact.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(Contact);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(Contact Contact)
        {
            try
            {
                Contact.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(Contact);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Contact FindById(long id)
        {
            return _context.Contacts.Find(id);
        }

        public Contact GetActiveRow()
        {
            return _context.Contacts.FirstOrDefault();
        }
    }
}
