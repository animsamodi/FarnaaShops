namespace EShop.Core.ViewModels.Product
{
    public class AnswerViewModel
    {
        public long? AnswerId { get; set; }
        public string AnswerText { get; set; }
        public int? Like { get; set; }
        public int? Dislike { get; set; }
        public string AnswerDate { get; set; }
        public string AnswerUser { get; set; }
    }
}
