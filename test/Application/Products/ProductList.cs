﻿using Domain.Shared.ValueObjects;
using Domain.Shops;
using Domain.Shops.Entities.Products;
using UnitTest.Domain.Shops;

namespace UnitTest.Application.Products
{
    public class ProductList
    {
        private readonly List<Product> _productList;
        public List<Product> Products
        {
            get { return _productList; }
        }

        public ProductList(Shop shop)
        {
            var product = shop.AddProduct("productName",
                                          "productDescription",
                                          MoneyValue.Of(10, "USD"),
                                          "pieces",
                                          shop.Id);

            var product2 = shop.AddProduct("productName2",
                                          "productDescription2",
                                          MoneyValue.Of(20, "USD"),
                                          "pieces",
                                          shop.Id);

            _productList = new List<Product>
            {
                product,
                product2
            };
        }

        public ProductList()
        {
            var shop = ShopFactory.Create();

            var product = shop.AddProduct("productName",
                                          "productDescription",
                                          MoneyValue.Of(10, "USD"),
                                          "pieces",
                                          shop.Id);

            var product2 = shop.AddProduct("productName2",
                                          "productDescription2",
                                          MoneyValue.Of(20, "USD"),
                                          "pieces",
                                          shop.Id);

            _productList = new List<Product>
            {
                product,
                product2
            };
        }
    }
}
