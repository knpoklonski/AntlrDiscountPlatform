using System;
using System.Collections.Generic;
using System.Linq;

namespace AntlrDiscountPlatform.Grammar.Expressions
{
    public interface IBasketExpression
    {
        Predicate<Basket> Filter { get; }
    }

    public class BasketProductsQuantityExpression : IBasketExpression
    {
        public BasketProductsQuantityExpression(ProductsExpression productsExpr)
        {
            ProductsExpr = productsExpr;
        }

        public ProductsExpression ProductsExpr { get; }

        public Predicate<Basket> Filter
        {
            get { return basket => basket.Products.Any(x => ProductsExpr.Filter(x)); }
        }
    }

    public class BasketAndOrExpression : IBasketExpression
    {
        public BasketAndOrExpression(bool and, List<IBasketExpression> basketExpressions)
        {
            And = and;
            BasketExpressions = basketExpressions;
        }

        public bool And { get; }

        public List<IBasketExpression> BasketExpressions { get; }

        public Predicate<Basket> Filter
        {
            get
            {
                if (And)
                {
                    return basket => BasketExpressions.TrueForAll(x => x.Filter(basket));
                }
                else //Or
                {
                    return basket => BasketExpressions.Any(x => x.Filter(basket));
                }
            }
        }
    }

}