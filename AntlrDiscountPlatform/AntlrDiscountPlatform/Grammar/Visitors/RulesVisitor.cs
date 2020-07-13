using System.Linq;
using AntlrDiscountPlatform.Grammar.Expressions;

namespace AntlrDiscountPlatform.Grammar.Visitors
{
    public class RulesVisitor : DiscountsParserBaseVisitor<RuleExpression>
    {
        private readonly BasketVisitor _basketVisitor = new BasketVisitor();
        private readonly ApplyVisitor _applyVisitor = new ApplyVisitor();

        public override RuleExpression VisitRules(DiscountsParser.RulesContext context)
        {
            var applyExpressions = context.apply().Select(x => x.Accept(_applyVisitor)).ToList();
            if (context.basket() != null)
            {
                var basketExpression = context.basket().Accept(_basketVisitor);
                return new RuleExpression(basketExpression, applyExpressions);
            }
            return new RuleExpression(applyExpressions);
        }
    }
}