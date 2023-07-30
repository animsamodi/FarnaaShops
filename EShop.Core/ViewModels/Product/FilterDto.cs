using System.Collections.Generic;
using EShop.DataLayer.Enum.Product;

namespace EShop.Core.ViewModels.Product
{
    public class FilterDto
    {
        public int? min_price { get; set; } = 0;
        public int? max_price { get; set; } = 0;
        public EnumSortOnProducts sort { get; set; } =  EnumSortOnProducts.MainPriceDesc;
        public int page { get; set; } = 1;
        public List<long?> brand { get; set; } = null;
        public List<long?> Seri { get; set; } = null;
        public List<long> propvalue { get; set; } = null;
        public bool availablestock { get; set; } = false;
        public bool discounted { get; set; } = false;
    }
}