﻿using Nithin2003.Interfaces;
using Nithin2003.Models;

namespace Nithin2003.Services
{
    public class ProductService : IProductService
    {
        public Product GetProduct(string sku)
        {
            return GetProducts().Where(w => w.Sku == sku).FirstOrDefault();
        }

        public IEnumerable<Product> GetProducts()
        {
            return new List<Product>
        {
            new Product{Sku = "sku1", Name="iphone 10 Max", Price=1,ImageUrl="Iphone10.jpg"},
            new Product{Sku = "sku2", Name="Samsung Galaxy S24 Ultra", Price=2,ImageUrl="Sasumsung.png"},
            new Product{Sku = "sku3", Name="Redmi 14", Price=3, ImageUrl = "redmi.png"},
            new Product{Sku = "sku4", Name="iphone 16 Max PRO", Price=4,ImageUrl="iphone16.png"},
            new Product{Sku = "sku4", Name="Vivo V17", Price=1,ImageUrl="vivo.png"},
        };  
        }
    }
}
