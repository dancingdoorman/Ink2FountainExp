using System.Collections.Generic;
using System.Linq;

namespace Ink.Parsed
{
    public class MultipleConditionExpression : Expression
    {
        public List<Expression> subExpressions {
            get {
                return this.content.Cast<Expression> ().ToList ();
            }
        }

        public MultipleConditionExpression(List<Expression> conditionExpressions)
        {
            AddContent (conditionExpressions);
        }

        public override void GenerateIntoContainer(Runtime.Container container)
        {
            //    A && B && C && D
            // => (((A B &&) C &&) D &&) etc
            bool isFirst = true;
            foreach (var conditionExpr in subExpressions) {

                conditionExpr.GenerateIntoContainer (container);

                if (!isFirst) {
                    container.AddContent (Runtime.NativeFunctionCall.CallWithName ("&&"));
                }

                isFirst = false;
            }
        }
    }
        
}

