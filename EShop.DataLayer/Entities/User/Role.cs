using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Entities.User
{
   public class Role : BaseEntity
    {
     
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        public List<RolePermisson> RolePermissons { get; set; }
        public List<User> Users { get; set; }
    }
}
