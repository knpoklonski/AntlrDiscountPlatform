using System;
using System.Linq;

namespace AntlrDiscountPlatform
{
    public class Calculator
    {
        private readonly GrammarParser _grammarParser = new GrammarParser();
        public void ApplyDiscount(Basket basket, string discount)
        {
            var rules = _grammarParser.Parse(discount);

            //validate basket
            if (rules.BasketExpression != null)
            {
                if (!rules.BasketExpression.Filter(basket))
                {
                    throw new InvalidOperationException("Can not apply discount to the basket");
                }
            }

            //apply percentage
            foreach (var applyExpression in rules.ApplyExpressions)
            {
                var productFilter = applyExpression.ProductsExpression == null
                    ? _ => true // all products are suitable
                    : applyExpression.ProductsExpression.Filter;

                //apply discount to filtered products
                foreach (var product in basket.Products.Where(x => productFilter(x)))
                    product.ApplyDiscount(applyExpression.Percentage);
            }
        }
    }
}