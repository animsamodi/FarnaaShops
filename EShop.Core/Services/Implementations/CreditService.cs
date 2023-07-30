using System;
using System.Collections.Generic;
using System.Linq;
using EShop.Core.ExtensionMethods;
using EShop.Core.Helpers;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.Core.ViewModels.Credit;
using EShop.Core.ViewModels.Product;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities;
using EShop.DataLayer.Entities.Banner;
using EShop.DataLayer.Entities.Credit;
using EShop.DataLayer.Entities.Site;
using EShop.DataLayer.Enum;
using Microsoft.EntityFrameworkCore;

namespace EShop.Core.Services.Implementations
{
    public class CreditService : BaseService<CreditBill>, ICreditService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public CreditService(ApplicationDbContext context, IUserService userService):base(context)
        {
            _context = context;
            _userService = userService;
        }


        public Tuple<int, List<CreditUserListViewModel>> GetListForAdmin(CreditSearchViewModel search, int count)
        {
            int skip = (search.Pagenumber - 1) * count;

            var res = _context.Credits.AsQueryable().Include(c=>c.User)
                .Select(c => new CreditUserListViewModel
            {
                UserId = c.UserId,
                CreditStatus = c.CreditStatus,
                AcceptPrice = c.AcceptPrice,
                AdminMessage = c.AdminMessage,
                Id = c.Id,
                UserMessage = c.UserMessage,
                CreateDate = c.CreateDate.ConvertMiladiToShamsi(),
                LastUpdateDate = c.LastUpdateDate.ConvertMiladiToShamsi(),
                RealLegal = c.RealLegal,
                NameUser = c.User.FullName,
                UserCodeMeli = c.User.NatioalCode,
                UserPhone = c.User.Phone
            }).OrderByDescending(c => c.Id);
             return Tuple.Create(res.Count(), res.Skip(skip).Take(count).ToList());

        }

        public List<CreditUserListViewModel> GetListForUser(long userId)
        {
            return _context.Credits.Where(c=>c.UserId == userId).Select(c=> new CreditUserListViewModel
            {
                UserId = c.UserId,
                TrakingCode = c.TrakingCode,
                CreditStatus = c.CreditStatus,
                AcceptPrice = c.AcceptPrice,
                AdminMessage = c.AdminMessage,
                Id = c.Id,
                UserMessage = c.UserMessage,
                CreateDate = c.CreateDate.ConvertMiladiToShamsi(),
                LastUpdateDate = c.LastUpdateDate.ConvertMiladiToShamsi(),
                RealLegal = c.RealLegal,
                CreditExpDate = c.CreditExpDate != null ? c.CreditExpDate.Value.ConvertMiladiToShamsi() : "--"
            }).OrderByDescending(c=>c.Id).ToList();

        }

        public bool Add(Credit model)
        {
            try
            {
                model.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(Credit model)
        {
            try
            {
                model.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(Credit model)
        {
            try
            {
                model.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Credit FindById(long id)
        {
            return _context.Credits
                .Include(c=>c.CreditAccounts)
                .Include(c=>c.CreditPartners)
                .Include(c=>c.CreditDocument)
                .FirstOrDefault(c=>c.Id == id);
        }

        public List<CreditDocumentType> GetTypeFiles(EnumRealLegal type)
        {
            return _context.CreditDocumentTypes.Where(c => c.RealLegal == type).ToList();
        }

        public bool CheckOpenRequest(long userId)
        {
            return _context.Credits.Any(c => c.UserId == userId && c.CreditStatus == EnumCreditStatus.Wating);
        }

        public bool AddWhitRelated(Credit credit)
        {
            try
            {
                credit.SetCreateDefaultValue(_userService.GetUserId());
                if (credit.CreditAccounts != null)
                    foreach (var account in credit.CreditAccounts)
                    {
                        account.SetCreateDefaultValue(_userService.GetUserId());
                    }

                if (credit.CreditPartners != null)
                    foreach (var partner in credit.CreditPartners)
                    {
                        partner.SetCreateDefaultValue(_userService.GetUserId());
                    }

                if (credit.CreditDocument != null)
                    foreach (var document in credit.CreditDocument)
                    {
                        document.SetCreateDefaultValue(_userService.GetUserId());
                    }

                _context.Add(credit);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool ChangeAdminStatus(long id, string adminMessage, EnumCreditStatus creditStatus, string creditExpDate, int acceptPrice)
        {
            try
            {
                var model = FindById(id);
                model.AdminMessage = adminMessage;
                model.CreditStatus = creditStatus;
                model.AcceptPrice = acceptPrice;

                if (!string.IsNullOrWhiteSpace(creditExpDate))
                {
                    creditExpDate = creditExpDate.PersianToEnglish();
                    model.CreditExpDate = creditExpDate.ConvertShamsiToMiladi().Date;
                }

                model.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool AddCreditBill(CreditBill creditBill)
        {
            try
            {
                creditBill.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(creditBill);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool EditCreditBill(CreditBill creditBill)
        {
            try
            {
                creditBill.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(creditBill);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteCreditBill(CreditBill creditBill)
        {
            try
            {
                creditBill.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(creditBill);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public CreditBill FindCreditBillById(long id)
        {
            return _context.CreditBills.Find(id);
        }

        public Tuple<int, List<CreditBillViewModel>> GetAllCreditBill(CreditBillSearchViewModel search, int count)
        {
            int skip = (search.Pagenumber - 1) * count;

            var res = _context.CreditBills.AsQueryable().Include(c => c.User)
                .Select(c => new CreditBillViewModel()
                {
                    UserId = c.UserId,
             
                    AdminMessage = c.AdminMessage,
                    Id = c.Id,
                     CreateDate = c.CreateDate,
                     DatePay = c.DatePay,
                     Bank = c.Bank,
                     Code = c.Code,
                     ConfirmPrice = c.ConfirmPrice,
                     CreditId = c.CreditId,
                     Description = c.Description,
                     Price = c.Price,
                     Image = c.Image,
                     Shobe = c.Shobe,
                     ShomareHesab = c.ShomareHesab,
                     ShomareKart = c.ShomareKart,
                      Status = c.Status,
                      FullName = c.User.FullName,
                      NatioalCode = c.User.NatioalCode,
                      Phone = c.User.Phone
                   
                }).OrderByDescending(c => c.Id);
            return Tuple.Create(res.Count(), res.Skip(skip).Take(count).ToList());
        }

        public bool DeleteCreditBillById(long id)
        {
            try
            {
                var bill = FindCreditBillById(id);
                return DeleteCreditBill(bill);
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<CreditBill> GetAllUserCreditBillByCreditId(long creditId)
        {
            return _context.CreditBills.Where(c => !c.IsDelete && c.CreditId == creditId).ToList();
        }
        public List<CreditBill> GetAllUserCreditBillByUserId(long userId)
        {

            
            return _context.CreditBills.Where(c => !c.IsDelete && c.UserId == userId).OrderByDescending(c=>c.Id).ToList();

        }

        public bool ChangeBillAdminStatus(long id, string adminMessage, EnumCreditBillStatus status, int confirmPrice)
        {
            try
            {
                var model = FindCreditBillById(id);
                model.AdminMessage = adminMessage;
                model.Status = status;
                model.ConfirmPrice = confirmPrice;
 

                model.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public CreditBillViewModel FindCreditBillForAdminById(long id)
        {
            var res = _context.CreditBills.AsQueryable()
                .Include(c => c.User).Select(c => new CreditBillViewModel()
                {
                    UserId = c.UserId,

                    AdminMessage = c.AdminMessage,
                    Id = c.Id,
                    CreateDate = c.CreateDate,
                    DatePay = c.DatePay,
                    Bank = c.Bank,
                    Code = c.Code,
                    ConfirmPrice = c.ConfirmPrice,
                    CreditId = c.CreditId,
                    Description = c.Description,
                    Price = c.Price,
                    Image = c.Image,
                    Shobe = c.Shobe,
                    ShomareHesab = c.ShomareHesab,
                    ShomareKart = c.ShomareKart,
                    Status = c.Status,
                    FullName = c.User.FullName,
                    NatioalCode = c.User.NatioalCode,
                    Phone = c.User.Phone,
 
                }).FirstOrDefault(c => c.Id == id);
            res.PrDatePay = res.DatePay?.ConvertMiladiToShamsi();
            return res;
        }
    }
}
