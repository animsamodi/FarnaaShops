using System;
using System.Collections.Generic;
using System.Linq;
using EShop.Core.ExtensionMethods;
using EShop.Core.Services.Base;
using EShop.Core.Services.Interfaces;
using EShop.DataLayer.Context;
using EShop.DataLayer.Entities.Product;
using EShop.DataLayer.Migrations;
using Microsoft.EntityFrameworkCore;

namespace EShop.Core.Services.Implementations
{
    public class GalleryService : BaseService<ProductImage>, IGalleryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IUserService _userService;

        public GalleryService(ApplicationDbContext context, IUserService userService):base(context)
        {
            _context = context;
            _userService = userService;
        }

        public List<ProductImage> GetProductImagesForAdmin(long id)
        {
            return _context.ProductImages.Include(i=>i.ProductOption).Where(pi => pi.ProductId == id).OrderByDescending(c=>c.Id).ToList();
        }

        public bool AddProductImage(ProductImage productImage)
        {
            try
            {
                productImage = productImage.SetCreateDefaultValue(_userService.GetUserId());
                _context.Add(productImage);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public ProductImage FindImageById(long id)
        {
            return _context.ProductImages.Find(id);
        }

        public bool DeleteImage(ProductImage image)
        {
            try
            {
                image = image.SetRemoveDefaultValue(_userService.GetUserId());
                _context.Update(image);
                _context.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                return false;

            }
        }
    }
}
