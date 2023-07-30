using System;

namespace EShop.Core.ViewModels.Product
{
    public class CommentAnswerForUserViewModel
    {
        public long CommentId { get; set; }
        public long AnswerId { get; set; }
        public string AnswerTitle { get; set; }
        public string AnswerText { get; set; }
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }

    }
}