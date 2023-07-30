using System.Collections.Generic;

namespace EShop.Core.ViewModels
{
    public class VariantUpdateMessageViewModel 
    {
        public long Id { get; set; }
        public string Message { get; set; }

        public bool IsOk { get; set; }
        public long ProductId { get; set; }



    }
}