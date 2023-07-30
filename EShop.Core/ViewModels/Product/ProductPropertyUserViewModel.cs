using System.Collections.Generic;
using EShop.DataLayer.Entities.Property;

namespace EShop.Core.ViewModels.Product
{
    public class ProductPropertyUserViewModel
    {
        public string GroupName { get; set; }
        public string PropertyName { get; set; }
        public string PropertValue { get; set; }
        public int OrderGroup { get; set; }
        public int OrderPropertyName { get; set; }
        public bool UseSummary { get; set; }
 
    }

    public class PropertyForSearchViewModel
    {
        public string PropertyName { get; set; }
        public List<PropertyValue> PropertyValues { get; set; }
    }
}
