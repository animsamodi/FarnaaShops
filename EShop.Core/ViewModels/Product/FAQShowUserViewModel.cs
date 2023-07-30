using System;

namespace EShop.Core.ViewModels.Product
{
    public class FAQShowUserViewModel
    {
        public long QuestionID { get; set; }
        public string QuestionText { get; set; }
        public int AnswerCount { get; set; }
        public DateTime QuestionDate { get; set; }
        public string QuestionName { get; set; }
        public AnswerViewModel Answer { get; set; }
    }
}
