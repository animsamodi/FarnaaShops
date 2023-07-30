using System;
using System.Collections.Generic;
using System.Linq;
using EShop.Core.ExtensionMethods;
using EShop.Core.Helpers;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.Core.ViewModels;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities.Address;
using Microsoft.EntityFrameworkCore;

namespace EShop.Core.Services.Implementations.User
{
    public class AddressService : BaseService<UserAddress>, IAddressService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public AddressService(ApplicationDbContext context, IUserService userService):base(context)
        {
            _context = context;
            _userService = userService;
        }

        public List<CityViewModel> GetCityByProvinceId(int id)
        {
            return _context.Cities.Where(c => c.ProvinceId == id).Select(c => new CityViewModel
            {
                CityId = c.Id,
                CityName = c.CityName
            }).ToList();
        }

        public List<ProvinceViewModel> GetProvince()
        {
            return _context.Provinces.Select(p => new ProvinceViewModel
            {
                ProvinceId = p.Id,
                ProvinceName = p.ProvinceName
            }).ToList();
        }

        public List<AddressViewModel> GetUserAddresses(int id)
        {
            return _context.UserAddresses.Where(a => a.UserId == id).Select(a => new AddressViewModel
            {
                UserAddressId = a.Id,
                FullName = a.FullName,
                PostalCode = a.PostalCode,
                PostalAddress = a.PostalAddress,
                Phone = a.Phone,
                CityId = a.CityId,
                ProvinceId = a.ProvinceId,
                CityName = a.City.CityName,
                ProvinceName = a.Province.ProvinceName,
                Lat = a.Lat,
                Lng = a.Lng,
                IsDefault = a.IsDefault
            }).ToList();
        }

        public bool AddAddress(UserAddress userAddress)
        {
            var lastDefault =
                _context.UserAddresses.FirstOrDefault(c => c.IsDefault && c.UserId == userAddress.UserId);
            if (lastDefault == null)
                userAddress.IsDefault = true;
            userAddress = userAddress.SetCreateDefaultValue(_userService.GetUserId());
            _context.Add(userAddress);
            _context.SaveChanges();
            return true;

        }

        public UserAddress GetUserAddressForEdit(int userid, long addressid)
        {
            return _context.UserAddresses.SingleOrDefault(ua => ua.UserId == userid && ua.Id == addressid);
        }

        public bool EditUserAddress(UserAddress userAddress)
        {

            try
            {
                userAddress = userAddress.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(userAddress);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public AddressForSubmitOrder GetAddressForOrder(long? addressid)
        {

            var query = (from a in _context.UserAddresses.Where(u => u.Id == addressid)
                         join c in _context.Cities on a.CityId equals c.Id
                         join p in _context.Provinces on a.ProvinceId equals p.Id
                         select new AddressForSubmitOrder
                         {
                             CityName = c.CityName,
                             FullName = a.FullName,
                             Phone = a.Phone,
                             PostalAddress = a.PostalAddress,
                             PostalCode = a.PostalCode,
                             ProvinceName = p.ProvinceName
                         }).SingleOrDefault();
            return query;
        }

        public bool ChangeDefaultUserAddress(long userId, long id)
        {
            try
            {
                var userAddress = _context.UserAddresses.FirstOrDefault(c => c.IsDefault && c.UserId == userId);
                if (userAddress != null)
                {
                    userAddress.IsDefault = false;
                    userAddress.SetEditDefaultValue(_userService.GetUserId());
                    _context.Update(userAddress);
                    _context.SaveChanges();
                }

                var newDefault = _context.UserAddresses.Find(id);
                newDefault.IsDefault = true;
                newDefault.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(newDefault);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public List<AddressForSubmitOrder> GetUserAddressesForOrder(int userid)
        {
            var query = (from a in _context.UserAddresses.Where(u => u.UserId == userid)
                         join c in _context.Cities on a.CityId equals c.Id
                         join p in _context.Provinces on a.ProvinceId equals p.Id
                         select new AddressForSubmitOrder
                         {
                             AddressId = a.Id,
                             IsDefault = a.IsDefault,
                             CityName = c.CityName,
                             FullName = a.FullName,
                             Phone = a.Phone,
                             PostalAddress = a.PostalAddress,
                             PostalCode = a.PostalCode,
                             ProvinceName = p.ProvinceName
                         }).ToList();
            return query;
        }


        public AddressForSubmitOrder GetUserClientAddress(int userid)
        {
            var query = (from a in _context.UserAddresses.Where(u => u.UserId == userid && u.IsClientAddress) 
                join c in _context.Cities on a.CityId equals c.Id
                join p in _context.Provinces on a.ProvinceId equals p.Id
                select new AddressForSubmitOrder
                {
                    CityName = c.CityName,
                    FullName = a.FullName,
                    Phone = a.Phone,
                    PostalAddress = a.PostalAddress,
                    PostalCode = a.PostalCode,
                    ProvinceName = p.ProvinceName
                }).FirstOrDefault();
            return query;
        }

        public List<BlockPostalCode> GetBlockPostCode()
        {
            return _context.BlockPostalCodes.OrderByDescending(c => c.Id).ToList();
        }

        public bool CheckBlockPostalCode(string userPostalCode)
        {
             userPostalCode.PersianToEnglish();
             //if (userPostalCode.Contains("0") || userPostalCode.Contains("2"))
             //{
             //    BlockPostalCode blockPostalCode = new BlockPostalCode
             //    {
             //        Code = userPostalCode,
             //        CountTry = 0,
                     
             //    };
             //    blockPostalCode.SetCreateDefaultValue(_userService.GetUserId());
             //    _context.Add(blockPostalCode);
             //    _context.SaveChanges();
             //    return true;
             //}
            var block = _context.BlockPostalCodes.FirstOrDefault(c => c.Code == userPostalCode);
            if (block != null)
            {
                block.CountTry = block.CountTry + 1;
                block.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(block);
                _context.SaveChanges();
                return true;
            }

            return false;
        }

        public bool CreateBlockPostCode(BlockPostalCode postalCode)
        {
            try
            {
                postalCode.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(postalCode);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool DeleteBlockPostCode(long id)
        {
            try
            {
                var data = _context.BlockPostalCodes.Find(id);
                data.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(data);
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public void UpdateTaxCodeProvince(List<Province> list)
        {
            var provinces = _context.Provinces.Where(c => c.TaxCode == null).ToList();
            foreach (var province in provinces)
            {
                var pr = list.FirstOrDefault(c => c.Id == province.Id);
                if (pr == null) continue;
                province.TaxCode = pr.TaxCode;
                province.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(province);
                _context.SaveChanges();
            }
        }

        public void UpdateTaxCodeCity(List<City> list)
        {
            var cities = _context.Cities.Where(c=>c.TaxCode == null).ToList();
            var counter = 0;
            foreach (var city in cities)
            {
                counter++;
                var pr = list.FirstOrDefault(c => c.Id == city.Id);
                if (pr == null) continue;
                city.TaxCode = pr.TaxCode;
                city.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(city);
                _context.SaveChanges();
            }
        }

        public List<CityViewModel> GetCity()
        {
            return _context.Cities.Select(p => new CityViewModel
            {
                CityId = p.Id,
                CityName = p.CityName
            }).ToList();
        }
    }
}
