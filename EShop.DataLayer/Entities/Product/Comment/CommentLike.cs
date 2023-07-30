using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Entities.Product.Comment
{
    public class CommentLike:BaseEntity
    {
       
        public long CommentId { get; set; }
        public long UserId { get; set; }
        public bool Type { get; set; }
        [ForeignKey("UserId")]

        public User.User User { get; set; }
        [ForeignKey("CommentId")]

        public Comment Comment { get; set; }
    }
}
