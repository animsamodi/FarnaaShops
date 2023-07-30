using EShop.DataLayer.Entities.Base;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EShop.DataLayer.Entities.Common
{
    public class BaseEntity : IEntity
    {
        [Key]
        [Display(Name = "کد رایانه")]
        public long Id { get; set; }

        public bool IsDelete { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime LastUpdateDate { get; set; }

        public long? ChangeUserId { get; set; }

    }
}
