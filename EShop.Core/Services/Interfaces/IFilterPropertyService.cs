using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.DataLayer.Entities;

namespace EShop.Core.Services.Interfaces
{
    public interface IFilterPropertyService : IBaseService<FilterProperty>
    {
        List<FilterProperty> GetListForAdmin();
        List<FilterProperty> GetListForUser();
        bool Add(FilterProperty PageSeo);
        bool Update(FilterProperty PageSeo);
        bool Delete(FilterProperty PageSeo);
        FilterProperty FindById(long id);
    }
}