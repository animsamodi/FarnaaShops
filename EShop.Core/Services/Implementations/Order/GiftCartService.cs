using System;
using System.Collections.Generic;
using System.Linq;
using EShop.Core.ExtensionMethods;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities.Cart;

namespace EShop.Core.Services.Implementations.Order
{
    public class GiftCartService : BaseService<GiftCard>, IGiftCartService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public GiftCartService(ApplicationDbContext context, IUserService userService):base(context)
        {
            _context = context;
            _userService = userService;
        }

        public List<GiftCard> GetGiftCartsByUserId(int userid)
        {
            return _context.GiftCards.Where(c => c.UserId == userid && c.Balance >0).ToList();
        }

        public GiftCard GetGiftCardByCode(string code)
        {
            return _context.GiftCards.FirstOrDefault(g => g.Code == code && g.Balance > 0);
        }

        public bool UpdateGiftCard(GiftCard giftCard)
        {
            try
            {
                giftCard = giftCard.SetEditDefaultValue(_userService.GetUserId());
                _context.Update(giftCard);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
            
        }

        public void AddGiftCardTransaction(GiftCardTransaction giftCardTransaction)
        {
            giftCardTransaction = giftCardTransaction.SetEditDefaultValue(_userService.GetUserId());
            _context.Update(giftCardTransaction);
            _context.SaveChanges();
        }
    }
}
