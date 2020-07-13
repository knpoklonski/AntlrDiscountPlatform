using System.Linq;
using AntlrDiscountPlatform.Grammar.Expressions;

namespace AntlrDiscountPlatform.Grammar.Visitors
{
    public class BasketVisitor : DiscountsParserBaseVisitor<IBasketExpression>
    {
        private readonly ProductsVisitor _productsVisitor = new ProductsVisitor();

        public override IBasketExpression VisitBasket_products(DiscountsParser.Basket_productsContext context)
        {
            var productsExpression = context.products().Accept(_productsVisitor);

            return new BasketProductsQuantityExpression(productsExpression);
        }

        public override IBasketExpression VisitBasket_and_or(DiscountsParser.Basket_and_orContext context)
        {
            var basketExpressions = context.basket()
                .Select(x => x.Accept(this))
                .ToList();
            var and = context.AND() != null;

            return new BasketAndOrExpression(and, basketExpressions);
        }
    }
}