using System.Collections.Generic;

namespace EShop.Core.ViewModels
{
   public class CompareViewModel
    {
        public long ProductId { get; set; }
        public string CategoryTitle { get; set; }
        public string FaTitle { get; set; }
        public string EnTitle { get; set; }
        public long CategoryId { get; set; }
        public List<string> Gallery { get; set; }
        public int? Price { get; set; }
        public List<ComparePropertyViewModel> Properties { get; set; }

    }

    public class ComparePropertyViewModel
    {
        public long NameId { get; set; }
        public string Value { get; set; }
    }

    public class ComparePropertyGroupVieWModel
    {
        public long NameId { get; set; }
        public string NameTitle { get; set; }
        public string GroupTitle { get; set; }
    }

    public class CompareProductViewModel
    {
        public long ProductId { get; set; }
        public string FaTitle { get; set; }
        public string EnTitle { get; set; }
        public string ImgName { get; set; }
    }
}
