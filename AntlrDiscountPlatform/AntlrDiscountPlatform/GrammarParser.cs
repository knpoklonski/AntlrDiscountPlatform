using Antlr4.Runtime;
using AntlrDiscountPlatform.Grammar.Expressions;
using AntlrDiscountPlatform.Grammar.Visitors;

namespace AntlrDiscountPlatform
{
    public class GrammarParser
    {
        private readonly RulesVisitor _rulesVisitor = new RulesVisitor();
        public RuleExpression Parse(string discount)
        {
            var charStream = new AntlrInputStream(discount);
            var lexer = new DiscountsLexer(charStream);
            var tokenStream = new CommonTokenStream(lexer);
            var parser = new DiscountsParser(tokenStream);
            var tree = parser.rules();
            var rules = tree.Accept(_rulesVisitor);
            
            return rules;
        }
    }
}