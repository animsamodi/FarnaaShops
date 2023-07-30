using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.Core.ViewModels;
using EShop.DataLayer.Entities.Address;

namespace EShop.Core.Services.Interfaces
{
   public interface IAddressService : IBaseService<UserAddress>
    {
        List<AddressViewModel> GetUserAddresses(int id);
        List<ProvinceViewModel> GetProvince();
        List<CityViewModel> GetCityByProvinceId(int id);
        bool AddAddress(UserAddress userAddress);
        UserAddress GetUserAddressForEdit(int userid, long addressid);
        bool EditUserAddress(UserAddress userAddress);
        AddressForSubmitOrder GetAddressForOrder(long? addressid);
        bool ChangeDefaultUserAddress(long userId,long id);
        List<AddressForSubmitOrder> GetUserAddressesForOrder( int userid);
         AddressForSubmitOrder GetUserClientAddress(int userid);
         List<BlockPostalCode> GetBlockPostCode();
         bool CheckBlockPostalCode(string userPostalCode);
         bool CreateBlockPostCode(BlockPostalCode postalCode);
         bool DeleteBlockPostCode(long id);
         void UpdateTaxCodeProvince(List<Province> list);
         void UpdateTaxCodeCity(List<City> list);
         List<CityViewModel> GetCity();
    }
}
