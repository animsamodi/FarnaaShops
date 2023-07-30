using System;
using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Entities
{
    public class ReleaseNote : BaseEntity
    {
        public string Title { get; set; }
        public string Version { get; set; }
        public DateTime Date { get; set; }
        public string ChangesDescription { get; set; }
        public bool IsPublished { get; set; }
 
    }
}