using System.Collections.Generic;

namespace EShop.Core.ViewModels.Product
{
    public class ProductPropertyAddAdminViewModel
    {
        public long NameId { get; set; }
        public string NameTitle { get; set; }
        public int Type { get; set; }
        public List<PropertyValueForAddViewModel> Values { get; set; }
    }

    public class PropertyValueForAddViewModel
    {
        public long ProductProertyId { get; set; }
        public long NameId { get; set; }
        public long ValueId { get; set; }
        public string Value { get; set; }
    }

    
}
