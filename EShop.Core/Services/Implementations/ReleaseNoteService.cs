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
    public class ReleaseNoteService : BaseService<ReleaseNote>, IReleaseNoteService
    {
        private readonly ApplicationDbContext _context; private readonly IUserService _userService;


        public ReleaseNoteService(ApplicationDbContext context, IUserService userService):base(context)
        {
            _context = context;
            _userService = userService;
        }


        public List<ReleaseNote> GetListForAdmin()
        {
            return _context.ReleaseNotes.ToList();
        }

        public List<ReleaseNote> GetListForUser()
        {
            return _context.ReleaseNotes.ToList();

        }

        public bool Add(ReleaseNote releaseNote)
        {
            try
            {
                releaseNote.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(releaseNote);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(ReleaseNote releaseNote)
        {
            try
            {
                releaseNote.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(releaseNote);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(ReleaseNote releaseNote)
        {
            try
            {
                releaseNote.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(releaseNote);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public ReleaseNote FindById(long id)
        {
            return _context.ReleaseNotes.Find(id);
        }
    }
}
