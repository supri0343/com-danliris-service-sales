using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Danliris.Service.Sales.Lib.Helpers
{
    public class ProductCategoryHelper
    {
        public List<ProductCategoryMapping> ProductCategoryMappings { get; } = new List<ProductCategoryMapping>
        {
            new ProductCategoryMapping
            {
                ProductCategory = "MEN'S SHIRT",
                Products = new List<string>
                {
                    "UNISEX SHIRT",
                    "MENS SHIRT",
                    "UNIFORM MENS SHIRT",
                    "BOYS SHIRT",
            }
        },
        new ProductCategoryMapping
        {
            ProductCategory = "LADIES BLOUSE",
            Products = new List<string>
            {
                "GIRLS BLOUSE",
                "GIRLS SHIRT",
                "LADIES BLOUSE",
                "LADIES SHIRT",
                "GIRLS BLOUSE",
                "GIRLS SHIRT",
                "UNIFORM WOMENS BLOUSE",
                "UNISEX BLOUSE",
            }
        },
        new ProductCategoryMapping
        {
            ProductCategory = "KIDS WEAR",
            Products = new List<string>
            {
                "INFANT OVERALL",
                "BOYS LAYETTE",
                "BOYS LAYETTE BODY SUIT",
                "BOYS SHIRT AND SHORT",
                "BOYS BLOUSON",
                "BOYS APRON",
                "GIRLS APRON",
            }
        },
        new ProductCategoryMapping
        {
            ProductCategory = "BLAZER/JACKET",
            Products = new List<string>
            {
                "BOYS BLAZER",
                "BOYS VEST",
                "GIRLS BLAZER",
                "LADIES VEST",
                "UNISEX VEST",
                "BOYS JACKET",
                "COOK COAT",
                "DOCTOR COAT",
                "GIRLS JACKET",
                "LADIES JACKET",
                "MENS JACKET",
                "UNISEX COAT",
                "UNISEX JACKET",
                "WORK MENS JACKET",

            }
        },
        new ProductCategoryMapping
        {
            ProductCategory = "SLEEP WEAR",
            Products = new List<string>
            {
                "EASY PANTS",
                "LADIES PAJAMAS",
                "MENS BOXER",
                "MENS PAJAMAS",
                "MENS TOP PAJAMAS",

            }
        },
        new ProductCategoryMapping
        {
            ProductCategory = "OTHERS",
            Products = new List<string>
            {
                "UNISEX ROBE",
                "ACCESSORIES GARMENT LAIN-LAIN",
                "OTHERS COMODITY",
                "KAPPO",
                "LADIES APRON",
                "LADIES T-SHIRT",
                "MENS T-SHIRT",

            }
        },
        new ProductCategoryMapping
        {
            ProductCategory = "PANT",
            Products = new List<string>
            {
                "BOYS SHORTS",
                "BOYS TROUSERS",
                "GIRLS SHORT PANT",
                "LADIES CULLOTES",
                "LADIES PANT/TROUSERS",
                "LADIES SHORTS",
                "LADIES SKORT",
                "MENS SHORTS",
                "MENS TROUSERS",
                "UNIFORM MENS TROUSERS",

            }
        },
        new ProductCategoryMapping
        {
            ProductCategory = "DRESS",
            Products = new List<string>
            {
                "GIRLS DRESS",
                "GIRLS PINAFORE",
                "GIRLS TUNIC",
                "LADIES DRESS",

            }
        },
        new ProductCategoryMapping
        {
            ProductCategory = "SKIRT",
            Products = new List<string>
            {
                "BOYS KILT",
                "GIRLS SKIRT",
                "LADIES SKIRT",
                "UNIFORM SKIRT",

            }
        },
        new ProductCategoryMapping
        {
            ProductCategory = "OVERALL",
            Products = new List<string>
            {
                "COVER ALL",
                "LADIES JUMPSUIT",
                "LADIES OVERALL",

            }
        }
    };

        public string GetProductCategory(string commodity)
        {
            return ProductCategoryMappings
                .Where(p => p.Products.Contains(commodity, StringComparer.OrdinalIgnoreCase))
                .Select(p => p.ProductCategory)
                .FirstOrDefault() ?? "-";
        }
    }

    public class ProductCategoryMapping
    {
        public string ProductCategory { get; set; }
        public List<string> Products { get; set; }
    }


}
