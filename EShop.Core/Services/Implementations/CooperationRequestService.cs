using System;
using System.Collections.Generic;
using System.Linq;
using EShop.Core.ExtensionMethods;
using EShop.Core.Helpers;
using EShop.Core.Security;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels.Credit;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities.Address;
using EShop.DataLayer.Enum;

namespace EShop.Core.Services.Implementations
{
    public class CooperationRequestService : BaseService, ICooperationRequestService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;
        private IAddressService _addressService;

        public CooperationRequestService(ApplicationDbContext context, IUserService userService, IAddressService addressService)
        {
            _context = context;
            _userService = userService;
            _addressService = addressService;
        }


     
        public Tuple<int, List<CooperationRequest>> GetListForAdmin(ColleagueSearchViewModel search, int count)
        {
            

            int skip = (search.Pagenumber - 1) * count;

            var res = _context.CooperationRequests.Where(c=>c.Status == search.Status).OrderByDescending(c => c.Id).AsQueryable();

            if (!String.IsNullOrEmpty(search.Name))
                res = res.Where(c => c.Name.Contains(search.Name));
            if (!String.IsNullOrEmpty(search.Code))
                res = res.Where(c => c.CodeMeli.Contains(search.Code));

            return Tuple.Create(res.Count(), res.Skip(skip).Take(count).ToList());

        }



        public bool Add(CooperationRequest model)
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

        public bool Update(CooperationRequest model)
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

        public bool Delete(CooperationRequest model)
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

        public CooperationRequest FindById(long id)
        {
            return _context.CooperationRequests.Find(id);
        }

        public bool ChangeStatus(long id, string description, EnumCooperationRequestStatus status)
        {
            try
            {
                var model = FindById(id);
                model.Description = description;
                model.Status = status;
                model.PrDateCheck = DateTime.Now.ConvertMiladiToShamsi();
                //
                model.ShomareTamas = model.ShomareTamas.PersianToEnglish();
                model.CodeMeli = model.CodeMeli.PersianToEnglish();
                model.CodeNaghsh = model.CodeNaghsh.PersianToEnglish();
                model.CodePosti = model.CodePosti.PersianToEnglish();
                //
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

        public bool ConvertRequestToUser(long id,string pass)
        {
            var userId = _userService.GetUserId();
            bool res = true;
            try
            {
                var request = FindById(id);

                res = _userService.DisableAllUserByPhone(request.ShomareTamas);
                if (res)
                {

                    var user = new DataLayer.Entities.User.User
                    {
                        IsColleague = true,
                        Password = pass,
                        IsHoghughi = request.Type ==EnumRealLegal.Legal,
                        Username = request.CodeMeli,
                        Email = request.Email,
                        Family = request.Family,
                        FullName = request.Name + " " +request.Family,
                        IsActive = true,
                        Name = request.Name,
                        Phone = request.ShomareTamas,
                        NatioalCode = request.CodeMeli,
                        TypeUser = EnumTypeUser.User,
                        RoleId = 1
                    };
                    user.SetCreateDefaultValue(userId);
                    var uId = _userService.AddUser(user);
                    res = uId != 0;
                    if (request.Type == EnumRealLegal.Legal)
                    {
                        var legal = new UserLegal
                        {
                            CodeEghtesadi = request.CodeMeli,
                            CodeNaghshTajer = request.CodeNaghsh,
                            CompanyName = request.Name,
                            FileAkharinTaghirat = request.FileParvaneKasb,
                            NeshaniMahaleKar = request.Address,
                            TozihatSherkat = request.Tozihat,
                            UserId = uId
                        };
                        legal.SetCreateDefaultValue(userId);
                       res =  _userService.AddUserLegal(legal);
                    }

                    var userAddress = new UserAddress
                    {
                        UserId = uId,
                        CityId = request.CityId,
                        Name = request.Name,
                        Phone = request.ShomareTamas,
                        Family = request.Type == EnumRealLegal.Real? request.Family : request.Name,
                        FullName = request.Name + " " + request.Family,
                        IsClientAddress = true,
                        PostalAddress = request.Address,
                        PostalCode = request.CodePosti,
                        ProvinceId = request.ProvinceId
                    };
                    res = _addressService.AddAddress(userAddress);

                }
            }
            catch (Exception e)
            {
                res = false;
            }

            return res;
        }

        public CooperationRequest CheckExistPhoneInRequest(string phone, EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa)
        {
            return _context.CooperationRequests.FirstOrDefault(c =>c.TypeSystem == typeSystem && c.ShomareTamas == phone && c.Status != EnumCooperationRequestStatus.Reject);
        }

        public bool EditRequest(CooperationRequest model)
        {
            try
            {
                var userId = _userService.GetUserId();

                model.SetEditDefaultValue(userId);
                _context.Update(model);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public long AddRequest(CooperationRequest request)
        {
            try
            {
                request.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(request);
                _context.SaveChanges();
                return request.Id;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}