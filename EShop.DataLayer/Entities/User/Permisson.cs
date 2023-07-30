using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Entities.User
{
   public class Permisson : BaseEntity
    {
    
        [MaxLength(50)]
        [Required]
        public string PermissonName { get; set; }
        public long Code { get; set; }
        public string Title { get; set; }
        public long? ParentId { get; set; }

        [ForeignKey("ParentId")]
        public List<Permisson> Permissons { get; set; }
        public List<RolePermisson> RolePermissons { get; set; }
    }
}
