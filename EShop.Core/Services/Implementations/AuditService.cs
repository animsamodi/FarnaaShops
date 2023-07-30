using System;
using System.Collections.Generic;
using System.Linq;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities;

namespace EShop.Core.Services.Implementations
{
    public class AuditService : BaseService<Audit>, IAuditService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public AuditService(ApplicationDbContext context, IUserService userService):base(context)
        {
            _context = context;
            _userService = userService;
        }


        public IQueryable<Audit> GetListAudits(int skip, int take)
        {
            return _context.Audits.AsQueryable();
        }
    }
}