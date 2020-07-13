using System;
using System.Collections.Generic;
using System.Linq;

namespace AntlrDiscountPlatform
{
    public class Basket
    {
        public Basket(List<Product> products)
        {
            Products = products;
        }

        public List<Product> Products { get; }
        public decimal Discount => Products.Sum(x => x.Discount);
        public decimal OriginalPrice => Products.Sum(x => x.OriginalPrice);
        public decimal TotalPrice => Products.Sum(x => x.TotalPrice);
    }

    public class Product
    {

        public Product(string name, decimal originalPrice)
        {
            Name = name;
            OriginalPrice = originalPrice;
        }

        public string Name { get; }
        public decimal OriginalPrice { get; }
        public decimal Discount { get; set; }
        public decimal TotalPrice => OriginalPrice - Discount;
        public void ApplyDiscount(int percentage)
        {
            if (percentage > 100 || percentage < 0)
                throw new ArgumentOutOfRangeException(nameof(percentage));

            Discount = OriginalPrice * percentage / 100;
        }
    }
}