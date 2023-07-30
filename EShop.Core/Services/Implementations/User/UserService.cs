using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using EndPoint.Web.Utilities;
using EShop.Core.ExtensionMethods;
using EShop.Core.Security;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.Core.ViewModels.Credit;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities.Address;
using EShop.DataLayer.Entities.User;
using EShop.DataLayer.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace EShop.Core.Services.Implementations.User
{
    public class UserService : BaseService<DataLayer.Entities.User.User>, IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor accessor;

        public UserService(ApplicationDbContext context, IHttpContextAccessor accessor) : base(context)
        {
            _context = context;
            this.accessor = accessor;
        }

        private ClaimsPrincipal GetUser()
        {
            return accessor?.HttpContext?.User as ClaimsPrincipal;
        }

        public long AddUser(DataLayer.Entities.User.User user)
        {
            try
            {
                user.SetCreateDefaultValue(GetUserId());
                _context.Add(user);
                _context.SaveChanges();
                return user.Id;
            }
            catch (Exception e)
            {
                return 0;
            }


        }

        public DataLayer.Entities.User.User GetUserById(long id)
        {
            return _context.Users.Find(id);
        }

        public UserLegal GetUserLegalByUserId(long userId)
        {
            return _context.UserLegals.FirstOrDefault(c => c.UserId == userId);
        }

        public bool EditUser(DataLayer.Entities.User.User user)
        {
            try
            {
                user = user.SetEditDefaultValue(GetUserId());
                _context.Update(user);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CheckExistEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }

        public DataLayer.Entities.User.User CheckExistPhone(string phone, EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa)
        {

            return _context.Users.FirstOrDefault(u => u.Phone == phone && !u.IsDelete && u.TypeSystem == typeSystem);
        }

        public ActiveEmailViewModel GetUserForForgetPassword(string email)
        {
            return _context.Users.Where(u => u.Email == email).Select(u => new ActiveEmailViewModel
            {
                UserId = u.Id,
                ActiveCode = u.EmailActiveCode
            }).SingleOrDefault();
        }

        public DataLayer.Entities.User.User LoginUser(string email_phone)
        {
            return _context.Users.FirstOrDefault(u =>/* u.Email == email_phone || */u.Phone == email_phone);
        }

        #region Permisson

        public List<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }
        public long AddRole(Role role)
        {
            role = role.SetCreateDefaultValue(GetUserId());
            _context.Add(role);
            _context.SaveChanges();
            return role.Id;
        }
        public bool UpdateRole(Role role)
        {
            role = role.SetEditDefaultValue(GetUserId());
            _context.Update(role);
            _context.SaveChanges();
            return true;
        }

        public List<Permisson> GetPermissons()
        {
            return _context.Permissons.ToList();
        }

        public bool AddRolePermissons(List<RolePermisson> rolePermissons)
        {
            foreach (var rolePermisson in rolePermissons)
            {
                rolePermisson.IsDelete = false;
                rolePermisson.CreateDate = DateTime.Now;
                rolePermisson.LastUpdateDate = DateTime.Now;

            }
            _context.AddRange(rolePermissons);
            _context.SaveChanges();
            return true;
        }

        public bool UpdateRolePermissons(List<RolePermisson> rolePermissons)
        {
            foreach (var rolePermisson in rolePermissons)
            {
                rolePermisson.LastUpdateDate = DateTime.Now;

            }
            _context.UpdateRange(rolePermissons);
            _context.SaveChanges();
            return true;
        }

        public bool RemoveRolePermissons(List<RolePermisson> rolePermissons)
        {
            foreach (var rolePermisson in rolePermissons)
            {
                rolePermisson.LastUpdateDate = DateTime.Now;
                rolePermisson.IsDelete = true;
            }
            _context.UpdateRange(rolePermissons);
            _context.SaveChanges();
            return true;
        }

        public EditRoleViewModel GetRoleAndPermissonsForEdit(int roleid)
        {
            return _context.Roles.Where(c => c.Id == roleid).Select(r => new EditRoleViewModel
            {
                RoleId = r.Id,
                Name = r.Name,
                SelctedPermissons = r.RolePermissons.Select(r => r.PermissonId).ToList()
            }).SingleOrDefault();
        }

        public List<RolePermisson> GetRolePermissonsByRoleId(long roleid)
        {
            return _context.RolePermissons.Where(c => c.RoleId == roleid).Include(c => c.Permisson).ToList();
        }

        public List<UserAdminViewModel> GetUserAdmins()
        {
            var res = _context.Users.Where(c => c.TypeUser == EnumTypeUser.Admin /*|| c.TypeUser == EnumTypeUser.Api*/)
                .Include(c => c.Role)
                .Select(c => new UserAdminViewModel
                {
                    Id = c.Id,
                    FullName = c.FullName,
                    IsActive = c.IsActive,
                    Phone = c.Phone,
                    RoleId = c.RoleId,
                    RoleTitle = c.Role.Name,
                    Password = "***"
                });
            return res.ToList();
        }

        public bool AddUserAdmin(UserAdminViewModel adminViewModel)
        {
            try
            {
                DataLayer.Entities.User.User user = new DataLayer.Entities.User.User
                {
                    IsNewsLetter = false,
                    Password = string.Join("-", PasswordHash.HashPasswordV2(adminViewModel.Password)),
                    PhoneActiveCode = 0,
                    TypeUser = EnumTypeUser.Admin,
                    FullName = adminViewModel.FullName,
                    Phone = adminViewModel.Phone,
                    IsActive = adminViewModel.IsActive,
                    RoleId = adminViewModel.RoleId
                };
                user = user.SetCreateDefaultValue(GetUserId());

                _context.Add(user);
                var res = _context.SaveChanges();
                return res > 0;
            }
            catch (Exception e)
            {
                return false;
            }

        }
        public bool EditUserAdmin(UserAdminViewModel adminViewModel)
        {
            try
            {
                var user = _context.Users.Find(adminViewModel.Id);


                user.FullName = adminViewModel.FullName;
                user.Phone = adminViewModel.Phone;
                user.IsActive = adminViewModel.IsActive;
                user.RoleId = adminViewModel.RoleId;
                user.TypeUser = EnumTypeUser.Admin;
                if (adminViewModel.Password != "***")
                    user.Password = string.Join("-", PasswordHash.HashPasswordV2(adminViewModel.Password));


                user.SetEditDefaultValue(GetUserId());

                _context.Update(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public UserEditProfile GetUserForUpdate(long id)
        {
            var user = _context.Users.Where(c => c.Id == id)
                .Select(c => new UserEditProfile
                {
                    NatioalCode = c.NatioalCode,
                    FullName = c.FullName,
                    Name = c.Name,
                    Family = c.Family,
                    Phone = c.Phone,
                    Id = c.Id,
                    Email = c.Email,
                    IsHoghughi = c.IsHoghughi
                }).FirstOrDefault();
            var address = _context.UserAddresses.FirstOrDefault(c => c.UserId == id && c.IsClientAddress);
            if (address != null)
            {
                user.UserAddressId = address.Id;
                user.ProvinceId = address.ProvinceId;
                user.CityId = address.CityId;
                user.PostalAddress = address.PostalAddress;
                user.PostalCode = address.PostalCode;
            }

            if (user != null && user.IsHoghughi)
            {
                var legal = _context.UserLegals.FirstOrDefault(c => c.UserId == id && !c.IsDelete);
                if (legal != null)
                {
                    user.UserLegalId = legal.Id;
                    user.CompanyName = legal.CompanyName;
                    user.CodeEghtesadi = legal.CodeEghtesadi;
                    user.ShomareSabt = legal.ShomareSabt;
                    user.CodeNaghshTajer = legal.CodeNaghshTajer;
                    user.TarikhTasis = legal.TarikhTasis;
                    user.ShenaseMeli = legal.ShenaseMeli;
                    user.NoeSherkat = legal.NoeSherkat;
                    user.NoeMalekiyat = legal.NoeMalekiyat;
                    user.NoeTamalok = legal.NoeTamalok;
                    user.TozihatSherkat = legal.TozihatSherkat;
                    user.NeshaniMahaleKar = legal.NeshaniMahaleKar;
                    user.CodePostiNeshaniMahaleKar = legal.CodePostiNeshaniMahaleKar;
                    user.TelephoneSabet = legal.TelephoneSabet;
                    //user.FileRuznameRasmi = legal.FileRuznameRasmi;
                    user.FileAkharinTaghirat = legal.FileAkharinTaghirat;
                    user.FileSahebanEmza = legal.FileSahebanEmza;
                    user.FileAgahiTasis = legal.FileAgahiTasis;

                }
            }
            return user;
        }

        public bool EditUserProfile(UserEditProfile user)
        {

            try
            {
                var u = _context.Users.Find(user.Id);
                if (user.NewPassword != null)
                    u.Password = string.Join("-", PasswordHash.HashPasswordV2(user.NewPassword));
                u.NatioalCode = user.NatioalCode;
                u.Name = user.Name;
                u.Family = user.Family;
                u.FullName = user.Name + " " + user.Family;
                u.Email = user.Email;
                u.IsHoghughi = user.IsHoghughi;
                u.CartNo = user.CartNo;
                u.ShebaNo = user.ShebaNo;
                u.BDate = user.BDate;
                u.SetEditDefaultValue(GetUserId());

                _context.Update(u);
                _context.SaveChanges();
                if (user.UserAddressId == null)
                {
                    var address = new UserAddress
                    {
                        UserId = user.Id,
                        CityId = user.CityId,
                        Name = user.Name,
                        Family = user.Family,
                        FullName = user.Name + " " + user.Family,
                        IsClientAddress = true,
                        Phone = user.Phone,
                        PostalAddress = user.PostalAddress,
                        PostalCode = user.PostalCode,
                        ProvinceId = user.ProvinceId,

                    };
                    address.SetCreateDefaultValue(GetUserId());
                    _context.Add(address);
                    _context.SaveChanges();
                }
                else
                {
                    var address = _context.UserAddresses.Find(user.UserAddressId);
                    address.CityId = user.CityId;
                    address.FullName = user.Name + " " + user.Family;
                    address.Phone = user.Phone;
                    address.Name = user.Name;
                    address.Family = user.Family;
                    address.PostalAddress = user.PostalAddress;
                    address.PostalCode = user.PostalCode;
                    address.ProvinceId = user.ProvinceId;
                    address.SetEditDefaultValue(GetUserId());
                    _context.Update(address);
                    _context.SaveChanges();
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool IncreaseCredit(long id, long price)
        {

            try
            {
                var user = _context.Users.Find(id);
                if (user.IsDelete == false && user.IsActive && user.IsColleague && !user.IsCredit)
                {
                    user.IsCredit = true;
                    user.AcceptPrice = price;
                    user.SetEditDefaultValue(GetUserId());
                    _context.Update(user);
                    _context.SaveChanges();
                    return true;

                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public bool IncreaseCreditPro(long id, long price)
        {

            try
            {
                var user = _context.Users.Find(id);
                if (user.IsDelete == false && user.IsActive)
                {
                    user.IsCredit = true;
                    user.AcceptPrice = price;
                    user.SetEditDefaultValue(GetUserId());
                    _context.Update(user);
                    _context.SaveChanges();
                    return true;

                }
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<DataLayer.Entities.User.User> GetCustomerForAdmin()
        {
            var quary = _context.Users.Where(c => c.TypeUser == EnumTypeUser.User)
                .Include(c => c.UserAddresses)
                .Include("UserAddresses.Province")
                .Include("UserAddresses.City");

            return quary.ToList();
        }


        public void DeleteUserById(long id)
        {
            var user = _context.Users.Find(id);
            user.SetRemoveDefaultValue(GetUserId());
            _context.Update(user);
            _context.SaveChanges();
        }

        public UserAdminViewModel GetUserAdminById(long id)
        {
            return _context.Users.Where(c => c.Id == id)
                .Include(c => c.Role)
                .Select(c => new UserAdminViewModel
                {
                    Id = c.Id,
                    FullName = c.FullName,
                    IsActive = c.IsActive,
                    Phone = c.Phone,
                    RoleId = c.RoleId,
                    RoleTitle = c.Role.Name,
                    Password = "***"
                }).FirstOrDefault();

        }

        public IEnumerable<UserCustomerAddressViewModel> GetCustomers()
        {
            try
            {
                var quary = _context.Users.Where(c => c.TypeUser == EnumTypeUser.User)
                                .Include(c => c.UserAddresses)
                                .Include("UserAddresses.Province")
                                .Include("UserAddresses.City")
                                .Include(c => c.Orders)
                                .ThenInclude(c => c.OrderDetails)
                                .Select(c => new UserCustomerAddressViewModel
                                {
                                    UserId = c.Id,
                                    AddressId = c.UserAddresses.FirstOrDefault() != null ? c.UserAddresses.FirstOrDefault().Id : 0,
                                    SumPrice = c.Orders.Where(c => c.OrderStatus != null && c.OrderStatus != EnumOrderStatus.NotPay).Any() ? c.Orders.Where(c => c.OrderStatus != null && c.OrderStatus != EnumOrderStatus.NotPay).Sum(o => (Int64)o.AmountPayable) / 10 : 0,
                                    DateLastOrder = c.Orders.Any() ? c.Orders.OrderByDescending(o => o.Id).FirstOrDefault().CreateDate : (DateTime?)null,
                                    CustomerFullName = c.FullName,
                                    CustomerNatioalCode = c.NatioalCode,
                                    CustomerPhone = c.Phone,
                                    RegisterDate = c.RegisterDate,
                                    TransfereeCity = c.UserAddresses.FirstOrDefault() != null ? c.UserAddresses.FirstOrDefault().City.CityName : "",
                                    TransfereeFullName = c.UserAddresses.FirstOrDefault() != null ? c.UserAddresses.FirstOrDefault().FullName : "",
                                    TransfereePhone = c.UserAddresses.FirstOrDefault() != null ? c.UserAddresses.FirstOrDefault().Phone : "",
                                    TransfereePostalAddress = c.UserAddresses.FirstOrDefault() != null ? c.UserAddresses.FirstOrDefault().PostalAddress : "",
                                    TransfereePostalCode = c.UserAddresses.FirstOrDefault() != null ? c.UserAddresses.FirstOrDefault().PostalCode : "",
                                    TransfereeProvince = c.UserAddresses.FirstOrDefault() != null ? c.UserAddresses.FirstOrDefault().Province.ProvinceName : "",
                                });

                return quary;
            }
            catch (Exception e)
            {
                return null;
            }

        }

        public IEnumerable<UserCustomerAddressViewModel> GetCustomerAddress()
        {
            try
            {
                var quary = _context.UserAddresses
                                .Include(c => c.User)

                                .Include(c => c.Province)
                                .Include(c => c.City)
                                .Where(c => c.User.TypeUser == EnumTypeUser.User)
                                .Select(c => new UserCustomerAddressViewModel
                                {
                                    UserId = c.UserId,
                                    AddressId = c.Id,
                                    CustomerFullName = c.User.FullName,
                                    CustomerNatioalCode = c.User.NatioalCode,
                                    CustomerPhone = c.User.Phone,
                                    TransfereeCity = c.City.CityName,
                                    TransfereeFullName = c.FullName,
                                    TransfereePhone = c.Phone,
                                    TransfereePostalAddress = c.PostalAddress,
                                    TransfereePostalCode = c.PostalCode,
                                    TransfereeProvince = c.Province.ProvinceName
                                });

                return quary;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public long? GetUserId()
        {
            var user = GetUser();
            return ClaimUtility.GetUserId(user);
        }

        public bool AddUserLegal(UserLegal userLegal)
        {
            try
            {
                userLegal.SetCreateDefaultValue(GetUserId());
                _context.Add(userLegal);
                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool EditUserLegal(UserLegal userLegal)
        {
            try
            {
                userLegal.SetEditDefaultValue(GetUserId());
                _context.Update(userLegal);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteUserLegalById(long userLegalId)
        {
            try
            {
                var userLegal = _context.UserLegals.Find(userLegalId);
                userLegal.SetRemoveDefaultValue(GetUserId());
                _context.Update(userLegal);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        #endregion

        #region MyRegion

        public void CheckUserFullName()
        {
            try
            {
                var users = _context.Users.ToList();
                var counter = 0;
                foreach (var user in users)
                {
                    try
                    {
                        counter++;
                        var namefamily = user.FullName?.Split(' ', 2);
                        if (namefamily != null)
                        {
                            user.Name = namefamily[0];
                            if (namefamily.Length > 1)
                                user.Family = namefamily[1];
                            user.SetEditDefaultValue(GetUserId());
                            _context.Update(user);
                            _context.SaveChanges();
                        }
                    }
                    catch (Exception e)
                    {
                        //
                    }

                }
            }
            catch (Exception e)
            {
                //
            }
        }

        public bool IsUserColleague()
        {
            var userId = GetUserId();
            if (userId == null) return false;
            var user = GetUserById(userId.Value);
            return user.IsColleague;

        }

        public DataLayer.Entities.User.User CheckExistUsername(string username, EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username && u.IsColleague && u.IsActive && typeSystem == typeSystem);
        }

        public bool DisableAllUserByPhone(string requestShomareTamas)
        {
            try
            {
                var userId = GetUserId();

                var lstUser = _context.Users.Where(c => c.Phone == requestShomareTamas).ToList();
                foreach (var user in lstUser)
                {
                    user.IsActive = false;
                    user.SetEditDefaultValue(userId);
                    _context.Update(user);
                    _context.SaveChanges();

                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public DataLayer.Entities.User.User GetUserByUsername(string username)
        {
            var res = _context.Users.FirstOrDefault(c => c.Username == username && c.IsActive && c.IsColleague);
            return res;
        }

        public bool ResetPassword(long userId, string hashPass)
        {
            try
            {
                var res = GetUserById(userId);
                res.Password = hashPass;
                _context.Update(res);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public Tuple<int, List<DataLayer.Entities.User.User>> GetListCustomer(UserCustomerSerchViewModel search, int count)
        {
            if (search.Pagenumber == 0)
                search.Pagenumber = 1;
            int skip = (search.Pagenumber - 1) * count;

            var quary = _context.Users.Where(c => c.TypeUser == EnumTypeUser.User && c.IsActive && !c.IsColleague && !c.IsDelete).OrderByDescending(c => c.Id).AsQueryable();
            if (!string.IsNullOrEmpty(search.Name))
                quary = quary.Where(c => c.FullName.Contains(search.Name));
            if (!string.IsNullOrEmpty(search.CodeMeli))
                quary = quary.Where(c => c.NatioalCode.Contains(search.CodeMeli));
            if (!string.IsNullOrEmpty(search.Phone))
                quary = quary.Where(c => c.Phone.Contains(search.Phone));

            return Tuple.Create(quary.Count(), quary.Skip(skip).Take(count).ToList());
        }

        public Tuple<int, List<UserAddress>> GetListUserAddress(UserAddressSerchViewModel search, int count)
        {
            if (search.Pagenumber == 0)
                search.Pagenumber = 1;
            int skip = (search.Pagenumber - 1) * count;

            var quary = _context.UserAddresses.Include(c => c.User)
                .Include(c => c.Province)
                .Include(c => c.City)
                .Where(c => c.User.TypeUser == EnumTypeUser.User && c.User.IsActive && !c.User.IsColleague && !c.User.IsDelete && !c.IsDelete);
            if (!string.IsNullOrEmpty(search.Name))
                quary = quary.Where(c => c.FullName.Contains(search.Name) ||
                                         c.Name.Contains(search.Name) ||
                                         c.Family.Contains(search.Name) ||
                                         c.User.FullName.Contains(search.Name));
            if (!string.IsNullOrEmpty(search.CodeMeli))
                quary = quary.Where(c => c.User.NatioalCode.Contains(search.CodeMeli));
            if (!string.IsNullOrEmpty(search.Phone))
                quary = quary.Where(c => c.Phone.Contains(search.Phone) ||
                                         c.User.Phone.Contains(search.Phone));

            return Tuple.Create(quary.Count(), quary.Skip(skip).Take(count).ToList());
        }

        public UserAddress GetUserAddressById(long id)
        {
            var res = _context.UserAddresses.Find(id);
            return res;
        }

        public bool EditUserAddress(UserAddress address)
        {
            try
            {
                var userId = GetUserId();


                address.SetEditDefaultValue(userId);
                _context.Update(address);
                _context.SaveChanges();


                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool ChangeUserStatus(long id, bool status)
        {
            try
            {
                var user = _context.Users.Find(id);
                user.IsActive = status;
                user.SetEditDefaultValue(GetUserId());
                _context.Update(user);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public List<DataLayer.Entities.User.User> GetListColleaugeUser(string filter)
        {
            var res = _context.Users.Where(c => (c.TypeUser == EnumTypeUser.User && c.IsColleague) && (c.Family.Contains(filter)
                                                || c.Name.Contains(filter)
                                                || c.Phone.Contains(filter)
                                                || c.NatioalCode.Contains(filter)))
                .ToList();
            return res;
        }

        public bool IsUserColleagueByUserId(int userid)
        {
              var user = GetUserById(userid);
            return user.IsColleague;
        }


        public Tuple<int, List<DataLayer.Entities.User.User>> GetListColleauge(UserColleagueSerchViewModel search, int count)

        {
            if (search.Pagenumber == 0)
                search.Pagenumber = 1;
            int skip = (search.Pagenumber - 1) * count;

            var quary = _context.Users.Where(c => c.TypeUser == EnumTypeUser.User /*&& c.IsActive*/ && c.IsColleague && !c.IsDelete);
            if (!string.IsNullOrEmpty(search.Name))
                quary = quary.Where(c => c.FullName.Contains(search.Name));
            if (!string.IsNullOrEmpty(search.Username))
                quary = quary.Where(c => c.Username.Contains(search.Username));
            if (!string.IsNullOrEmpty(search.Phone))
                quary = quary.Where(c => c.Phone.Contains(search.Phone));

            return Tuple.Create(quary.Count(), quary.Skip(skip).Take(count).ToList());
        }

        public DataLayer.Entities.User.User LoginApiUser(string username, string password)
        {
            var res = _context.Users.SingleOrDefault(c => c.Username == username && c.TypeUser == EnumTypeUser.Api && !c.IsDelete && c.IsActive);
            return res;
        }
        #endregion
    }
}
