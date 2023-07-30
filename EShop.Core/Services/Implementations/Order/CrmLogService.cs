using System;
using System.Collections.Generic;
using System.Linq;
using EShop.Core.ExtensionMethods;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities.Order;

namespace EShop.Core.Services.Implementations.Order
{
    public class CrmService : BaseService<CrmLog>, ICrmService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public CrmService(ApplicationDbContext context, IUserService userService):base(context)
        {
            _context = context;
            _userService = userService;
        }


        public long AddCrmLog(CrmLog crmLog)
        {
            try
            {
                crmLog.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(crmLog);
                _context.SaveChanges();
                return crmLog.Id;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public CrmLog GetCrmLogById(long id)
        {
            try
            {
                return _context.CrmLogs.Find(id);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public bool UpdateCrmLog(CrmLog crmLog)
        {
            try
            {

                crmLog.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(crmLog);
                _context.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        public CrmAccount GetCrmAccount()
        {
            return _context.CrmAccounts.FirstOrDefault();
        }

        public void UpdateToken(string token)
        {
            var account = GetCrmAccount();
            account.Token = token;
            account.TokenExpireDate = DateTime.Now.AddDays(10);
            account.SetEditDefaultValue(_userService.GetUserId());
            _context.Update(account);

        }

        public List<CrmLog> GetCrmLogByOrderId(long id)
        {
            var res = _context.CrmLogs.Where(c => c.OrderId == id).OrderByDescending(c=>c.Id).ToList();
            return res;
                }
    }
}