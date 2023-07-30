using System.Collections.Generic;

namespace EShop.Core.ViewModels.Product
{
    public class SearchWithFilterDto:FilterDto
    {
        public string q { get; set; }
        public List<long> catId { get; set; }
        public bool JustColleauge { get; set; }
    }
}