using System.ComponentModel.DataAnnotations.Schema;
using EShop.DataLayer.Entities.Common;

namespace EShop.DataLayer.Entities.Credit
{
    public class CreditDocument : BaseEntity
    {
        public long CreditId { get; set; }
        public long CreditDocumentTypeId { get; set; }
        public string File { get; set; }
        public string Title { get; set; }

        [ForeignKey("CreditId")]
        public Credit Credit { get; set; }

        [ForeignKey("CreditDocumentTypeId")]
        public CreditDocumentType CreditDocumentType { get; set; }
    }
}