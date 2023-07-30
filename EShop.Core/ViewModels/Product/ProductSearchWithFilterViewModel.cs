using EShop.Core.ViewModels.Category;
using System;
using System.Collections.Generic;
using EShop.Core.ViewModels.Seo;
using Newtonsoft.Json;

namespace EShop.Core.ViewModels.Product
{
    public class ProductSearchWithFilterViewModel:BasePageWithFilterViewModel
    {
        public ProductSearchWithFilterViewModel(int page, int? minPrice,
            int? maxPrice, int count,
            List<ProductSearchWithFilterItem> products)
        {
            PaggingData = new PaggingViewModel(page, count);
            SideBarData = new SideBarViewModel(minPrice, maxPrice);
            Products = products;
        }

        public ProductSearchWithFilterViewModel()
        {
        }

     
        public List<ProductSearchWithFilterItem> Products { get; set; }
        public PageStructureViewModel pageStructure { get; set; }
    }

    public class BasePageWithFilterViewModel
    {
        public  SideBarViewModel SideBarData { get; set; }
        public PaggingViewModel PaggingData { get; set; }
        public CategoryViewModel Category { get; set; }
        public string FAQSchema { get; set; }
        public string SelectedParentCategory { get; set; }
        public string SelectedCategory { get; set; }
        public string SelectedBrand { get; set; }
        public string SelectedBrandFa { get; set; }
        public string SelectedSeri { get; set; }
        public string SelectedSeriFa { get; set; }
    }
    public class CategoryViewModel
    {
        public CategoryViewModel()
        {
 
        }
        public CategoryViewModel(long id, string persianTitle,
            string englishTitle, CategoryViewModel parentCategory)
        {
            Id = id;
            PersianTitle = persianTitle;
            EnglishTitle = englishTitle;
            ParentCategory = parentCategory;
        }
        public CategoryViewModel(long id, string persianTitle, 
            string englishTitle)
        {
            Id = id;
            PersianTitle = persianTitle;
            EnglishTitle = englishTitle;
        }

        public long Id { get; set; }
        public string PersianTitle { get; set; }
        public string EnglishTitle { get; set; }
        public CategoryViewModel ParentCategory { get; set; }
    }

    public class CategoryFilterItem
    {
        public CategoryFilterItem()
        {
            
        }
        public CategoryFilterItem(long id, string persianTitle,
            string englishTitle)
        {
            Id = id;
            PersianTitle = persianTitle;
            EnglishTitle = englishTitle;
        }

        public long Id { get; set; }
        public string PersianTitle { get; set; }
        public string EnglishTitle { get; set; }
    }
    public class MainCategoryFilterItem : CategoryFilterItem
    {
        public MainCategoryFilterItem()
        {
            
        }
        public MainCategoryFilterItem(long id, string englishTitle, string persianTitle)
            : base(id, persianTitle, englishTitle)
        {

        }
        public MainCategoryFilterItem(long id, string englishTitle, string persianTitle,
            List<CategoryFilterItem> subCategory) : base(id, persianTitle, englishTitle)
        {
            SubCategory = subCategory;
        }

        public List<CategoryFilterItem> SubCategory { get; set; }
    }
    public class BrandFilterItem
    {
        public BrandFilterItem(long id, string englishTitle,
            string persianTitle)
        {
            Id = id;
            EnglishTitle = englishTitle;
            PersianTitle = persianTitle;
        }

        public long Id { get; set; }
        public string EnglishTitle { get; set; }
        public string PersianTitle { get; set; }
    }
    public class SeriFilterItem
    {
        public SeriFilterItem(long id, string englishTitle,
            string persianTitle)
        {
            Id = id;
            EnglishTitle = englishTitle;
            PersianTitle = persianTitle;
        }

        public long Id { get; set; }
        public string EnglishTitle { get; set; }
        public string PersianTitle { get; set; }
    }
    public class ProductSearchWithFilterItem
    {
        public long ProductId { get; set; }
        public int SellCount { get; set; }
        public int ViewCount { get; set; }
        public long CategoryId { get; set; }
        public string CategoryProduct { get; set; }
        public string FaTitle { get; set; }
        public string EnTitle { get; set; }
        public string GuranateeTitle { get; set; }
        public int? MainPrice { get; set; }
        public int? PromotionPrice { get; set; }
        public DateTime? PromotionEndDate { get; set; }
        public string Img { get; set; }
        public byte? PromotionType { get; set; }
        public List<VariantColorViewModel> VariantColor { get; set; }


    }

    public class SideBarViewModel
    {
        public SideBarViewModel()
        {

        }
        public SideBarViewModel(int? minPrice, int? maxPrice)
        {
            MinPrice = minPrice;
            MaxPrice = maxPrice;
        }
        public SideBarViewModel(int? minPrice, int? maxPrice,
            List<BrandFilterItem> brands, List<SeriFilterItem> series, List<MainCategoryFilterItem> categories)
        {
            MinPrice = minPrice;
            MaxPrice = maxPrice;
            Brands = brands;
            Series = series;
            Categories = categories;
        }

        public List<BrandFilterItem> Brands { get; set; }
        public List<SeriFilterItem> Series { get; set; }
        public List<MainCategoryFilterItem> Categories { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
    }

    public class PaggingViewModel
    {
        public PaggingViewModel(int page, int count)
        {
            Page = page;
            Count = count;
        }

        public PaggingViewModel()
        {
            
        }
        public int Page { get; set; }
        public int Count { get; set; }
    }
}