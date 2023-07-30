using System.Collections.Generic;
using EShop.Core.Services.Base;
using EShop.DataLayer.Entities.Cart;

namespace EShop.Core.Services.Interfaces
{
   public interface IGiftCartService : IBaseService<GiftCard>
    {
        List<GiftCard> GetGiftCartsByUserId(int userid);
        GiftCard GetGiftCardByCode(string code);
        bool UpdateGiftCard(GiftCard giftCard);
        void AddGiftCardTransaction(GiftCardTransaction giftCardTransaction);
    }
}
