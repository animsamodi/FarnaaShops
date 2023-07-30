using System;
using System.Collections.Generic;
using System.Text;

namespace EShop.DataLayer.Configurations
{
    public class ElasticConnectionSettings
    {
        public string ElasticSearchHost { get; set; }
        public string ElasticLogingIndex { get; set; }
        public string ElasticAuditIndex { get; set; }
    }
}
