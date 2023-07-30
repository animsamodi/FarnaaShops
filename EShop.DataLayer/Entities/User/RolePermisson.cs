using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Entities.User
{
    public class RolePermisson : BaseEntity
    {
 
        public long RoleId { get; set; }
        public long PermissonId { get; set; }

        [ForeignKey("RoleId")]

        public Role Role { get; set; }
        [ForeignKey("PermissonId")]

        public Permisson Permisson { get; set; }
    }
}
