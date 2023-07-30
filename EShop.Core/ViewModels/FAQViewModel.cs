using System.ComponentModel.DataAnnotations;

namespace EShop.Core.ViewModels
{
    public class AddQuestionViewModel
    {
        [Required]
        public int Productid { get; set; }
        [Required]
        public string Question { get; set; }
        public bool IsNotifiAnswer { get; set; }

        
    }

    public class AddAnswerViewModel
    {
        [Required]
        public string answertxt { get; set; }

        [Required]
        public int questionid { get; set; }
    }
}
