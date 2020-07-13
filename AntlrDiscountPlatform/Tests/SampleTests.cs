using System;
using System.Collections.Generic;
using AntlrDiscountPlatform;
using Xunit;

namespace Tests
{
    public class SampleTests
    {
        [Fact]
        public void ApplyDiscountToAnyBasket()
        {
            var discount = "APPLY 10% DISCOUNT";
            var basket = new Basket(new List<Product>
            {
                new Product("tshirt", 50),
                new Product("jeans", 100),
                new Product("jacket", 200)
            });

            var calculator = new Calculator();
            calculator.ApplyDiscount(basket, discount);
            
            Assert.Equal(350, basket.OriginalPrice);
            Assert.Equal(35, basket.Discount);
            Assert.Equal(315, basket.TotalPrice);
        }

        [Fact]
        public void ApplyDiscountToJeansOnly()
        {
            var discount = "APPLY 10% DISCOUNT TO jeans";
            var basket = new Basket(new List<Product>
            {
                new Product("tshirt", 50),
                new Product("jeans", 100),
                new Product("jacket", 200)
            });

            var calculator = new Calculator();
            calculator.ApplyDiscount(basket, discount);

            Assert.Equal(350, basket.OriginalPrice);
            Assert.Equal(10, basket.Discount);
            Assert.Equal(340, basket.TotalPrice);
        }

        [Fact]
        public void ApplyComplexDiscount()
        {
            var discount = @"APPLY 10% DISCOUNT TO jeans
                             APPLY 50% DISCOUNT TO jacket";
            var basket = new Basket(new List<Product>
            {
                new Product("tshirt", 50),
                new Product("jeans", 100),
                new Product("jacket", 200)
            });

            var calculator = new Calculator();
            calculator.ApplyDiscount(basket, discount);

            Assert.Equal(350, basket.OriginalPrice);
            Assert.Equal(10 + 100, basket.Discount);
            Assert.Equal(240, basket.TotalPrice);
        }

        [Fact]
        public void ApplyComplexDiscountSuccessfullyWithPrecondition()
        {
            var discount = @"FOR BASKET WITH tshirt
                             APPLY 10% DISCOUNT TO jeans
                             APPLY 50% DISCOUNT TO jacket";
            var basket = new Basket(new List<Product>
            {
                new Product("tshirt", 50),
                new Product("jeans", 100),
                new Product("jacket", 200)
            });

            var calculator = new Calculator();
            calculator.ApplyDiscount(basket, discount);

            Assert.Equal(350, basket.OriginalPrice);
            Assert.Equal(10 + 100, basket.Discount);
            Assert.Equal(240, basket.TotalPrice);
        }

        [Fact]
        public void FailOnPrecondition()
        {
            var discount = @"FOR BASKET WITH nonExistingProduct
                             APPLY 10% DISCOUNT TO jeans
                             APPLY 50% DISCOUNT TO jacket";

            var basket = new Basket(new List<Product>
            {
                new Product("tshirt", 50),
                new Product("jeans", 100),
                new Product("jacket", 200)
            });

            var calculator = new Calculator();
            Assert.Throws<InvalidOperationException>(() => calculator.ApplyDiscount(basket, discount));
        }
    }
}
