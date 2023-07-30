using System;
using System.Collections.Generic;
using System.Text;
using EShop.Core.ViewModels.Product;
using EShop.DataLayer.Migrations;

namespace EShop.Core.ExtensionMethods
{
    public static class SchemaHelper
    {
        public const string WebSiteSchema =
            "<script type =\"application/ld+json\">\n    {\n      \"@context\": \"https://schema.org\",\n      \"@type\": \"WebSite\",\n      \"url\": \"https://farnaa.com/\",\n      \"potentialAction\": {\n        \"@type\": \"SearchAction\",\n        \"target\": {\n          \"@type\": \"EntryPoint\",\n          \"urlTemplate\": \"https://farnaa.com/search?q={search_term_string}\"\n        },\n        \"query-input\": \"required name=search_term_string\"\n      }\n    }\n    </script>";
      
        public const string WebSiteSchemaPlus =
            "<script type =\"application/ld+json\">\n    {\n      \"@context\": \"https://schema.org\",\n      \"@type\": \"WebSite\",\n      \"url\": \"https://farnaa.com/\",\n      \"potentialAction\": {\n        \"@type\": \"SearchAction\",\n        \"target\": {\n          \"@type\": \"EntryPoint\",\n          \"urlTemplate\": \"https://farnaaplus.com/search?q={search_term_string}\"\n        },\n        \"query-input\": \"required name=search_term_string\"\n      }\n    }\n    </script>";
      

        public static string GenrateproductPageSchema(ProductDetailUserViewModel product,string category)
        {
            // product
            string  ProductSchema = "<script type =\"application/ld+json\">{  \"@context\": \"https://schema.org/\",   \"@type\": \"Product\",\"aggregateRating\": {    \"@type\": \"AggregateRating\",    \"ratingValue\": \"4.4\",    \"reviewCount\": \"11\"  },   \"name\": \"" + product.FaTitle + " " + product.EnTitle + "\",  \"image\": \"https://farnaa.com/uploads/" + product.ImgName + "\",  \"brand\": {    \"@type\": \"Brand\",    \"name\": \"" + product.BrandName + "\"  }}</script>";
            // product breadcrump
            ProductSchema = ProductSchema + $" <script type=\"application/ld+json\">{{ \"@context\": \"https://schema.org\", \"@type\": \"BreadcrumbList\", \"itemListElement\": [  {{   \"@type\": \"ListItem\",   \"position\": 1,   \"item\":   {{    \"@id\": \"https://farnaa.com/\",    \"name\": \"خانه\"    }}  }},  {{   \"@type\": \"ListItem\",  \"position\": 2,  \"item\":   {{     \"@id\": \"https://farnaa.com/category/{category}\",     \"name\": \"{product.CategoryName}\"   }}  }}, {{   \"@type\": \"ListItem\",  \"position\": 3,  \"item\":   {{     \"@id\": \"https://farnaa.com/product/{product.ProductId}/{category}/{product.EnTitle}\",     \"name\": \"{product.FaTitle}\"   }}  }} ]}}</script>";
            return ProductSchema;
        }
     
    }
}
