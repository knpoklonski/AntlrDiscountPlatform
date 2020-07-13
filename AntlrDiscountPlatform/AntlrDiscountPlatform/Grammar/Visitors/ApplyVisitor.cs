using AntlrDiscountPlatform.Grammar.Expressions;

namespace AntlrDiscountPlatform.Grammar.Visitors
{
    public class ApplyVisitor : DiscountsParserBaseVisitor<ApplyExpression>
    {
        private readonly ProductsVisitor _productsVisitor = new ProductsVisitor();
        public override ApplyExpression VisitApply(DiscountsParser.ApplyContext context)
        {
            var percentage = int.Parse(context.INT().GetText());
            if (context.products() == null)
            {
                return new ApplyExpression(percentage);
            }
            return new ApplyExpression(percentage, context.products().Accept(_productsVisitor));
        }
    }
}