using System;
using System.Collections.Generic;

namespace EShop.Core.ViewModels.Product
{
    public class CommentForUserViewModel
    {
        public long CommentId { get; set; }
        public string CommentTitle { get; set; }
        public string CommentText { get; set; }
        public string Positive { get; set; }
        public string Negative { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public int Like { get; set; }
        public int DisLike { get; set; }
        public DateTime CreateDate { get; set; }
        public List<CommentAnswerForUserViewModel> Answers { get; set; }

    }
}
