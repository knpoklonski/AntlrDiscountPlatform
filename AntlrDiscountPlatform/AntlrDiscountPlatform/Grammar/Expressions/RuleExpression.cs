using System.Collections.Generic;

namespace AntlrDiscountPlatform.Grammar.Expressions
{
    public class RuleExpression
    {
        public RuleExpression(List<ApplyExpression> applyExpressions)
        {
            ApplyExpressions = applyExpressions;
        }

        public RuleExpression(IBasketExpression basketExpression, List<ApplyExpression> applyExpressions)
        {
            BasketExpression = basketExpression;
            ApplyExpressions = applyExpressions;
        }

        public IBasketExpression BasketExpression { get; }
        public List<ApplyExpression> ApplyExpressions { get; }
    }
}