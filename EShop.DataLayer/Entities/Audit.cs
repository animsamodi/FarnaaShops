using System;
using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Entities
{
    public class Audit : BaseEntity
    {
        public string TableName { get; set; }
        public DateTime DateTime { get; set; }
        public string KeyValues { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
    }

    public class EducationalDoc : BaseEntity
    {
        public string Title { get; set; }
        public string Video { get; set; }
         public string Text { get; set; }
        public bool IsActive { get; set; }
 
    }
}