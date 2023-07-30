using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace EShop.Core.ViewModels.Credit
{
    public class CreditDocumentViewModel 
    {
        [Display(Name = "کد رایانه")]
        public long Id { get; set; }
        public long CreditId { get; set; }
        public long CreditDocumentTypeId { get; set; }
        public string Title { get; set; }
        public string File { get; set; }
        public IFormFile FormFile { get; set; }


    }
}