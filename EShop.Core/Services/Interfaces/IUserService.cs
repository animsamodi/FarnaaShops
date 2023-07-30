using System;
using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.Core.ViewModels.Credit;
using EShop.DataLayer.Entities.Address;
using EShop.DataLayer.Entities.User;
using EShop.DataLayer.Enum;

namespace EShop.Core.Services.Interfaces
{
   public interface IUserService : IBaseService<User>
    {
        #region Account
        long AddUser(User user);
        bool EditUser(User user);
        User GetUserById(long id);
        UserLegal GetUserLegalByUserId(long userId);
        bool CheckExistEmail(string email);
        User CheckExistPhone(string phone, EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa);
        ActiveEmailViewModel GetUserForForgetPassword(string email);
        DataLayer.Entities.User.User LoginUser(string email_phone);

        #endregion


        #region Permisson
        List<Role> GetRoles();
        long AddRole(Role role);
        bool UpdateRole(Role role);
        bool AddRolePermissons(List<RolePermisson> rolePermissons);
        bool UpdateRolePermissons(List<RolePermisson> rolePermissons);
        bool RemoveRolePermissons(List<RolePermisson> rolePermissons);
        List<Permisson> GetPermissons();
        EditRoleViewModel GetRoleAndPermissonsForEdit(int roleid);
        List<RolePermisson> GetRolePermissonsByRoleId(long roleid);
        #endregion

        List<UserAdminViewModel> GetUserAdmins();
        bool AddUserAdmin(UserAdminViewModel adminViewModel);
        bool EditUserAdmin(UserAdminViewModel adminViewModel);
        UserEditProfile GetUserForUpdate(long id);
        bool IncreaseCredit(long id, long price);
        bool EditUserProfile(UserEditProfile user);
        List<User> GetCustomerForAdmin();
        public Tuple<int, List<User>> GetListColleauge(UserColleagueSerchViewModel search, int count);
        void DeleteUserById(long id);
        UserAdminViewModel GetUserAdminById(long id);
        IEnumerable<UserCustomerAddressViewModel> GetCustomers();
        IEnumerable<UserCustomerAddressViewModel> GetCustomerAddress();

        public long? GetUserId();
        bool AddUserLegal(UserLegal userLegal);
        bool EditUserLegal(UserLegal userLegal);
        bool DeleteUserLegalById(long userLegalId);
        public void CheckUserFullName();

        public bool IsUserColleague();

          User CheckExistUsername(string username, EnumTypeSystem typeSystem = EnumTypeSystem.Farnaa);
          bool DisableAllUserByPhone(string requestShomareTamas);
        bool IncreaseCreditPro(long id, long price);
        User GetUserByUsername(string username);
          bool ResetPassword(long userId, string hashPass);
        public Tuple<int, List<User>> GetListCustomer(UserCustomerSerchViewModel search, int count);
        public Tuple<int, List<UserAddress>> GetListUserAddress(UserAddressSerchViewModel search, int count);
        UserAddress GetUserAddressById(long id);
        bool EditUserAddress(UserAddress address);
        bool ChangeUserStatus(long id, bool status);
        List<User> GetListColleaugeUser(string filter);
        bool IsUserColleagueByUserId(int userid);
        User LoginApiUser(string username, string password);
    }
}
