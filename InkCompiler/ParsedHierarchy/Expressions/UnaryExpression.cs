namespace Ink.Parsed
{
    public class UnaryExpression : Expression
	{
		public Expression innerExpression;
        public string op;

        // Attempt to flatten inner expression immediately
        // e.g. convert (-(5)) into (-5)
        public static Expression WithInner(Expression inner, string op) {

            var innerNumber = inner as Number;
            if( innerNumber ) {

                if( op == "-" ) {
                    if( innerNumber.value is int ) {
                        return new Number( -((int)innerNumber.value) );
                    } else if( innerNumber.value is float ) {
                        return new Number( -((float)innerNumber.value) );
                    }
                }

                else if( op == "!" || op == "not" ) {
                    if( innerNumber.value is int ) {
                        return new Number( ((int)innerNumber.value == 0) ? 1 : 0 );
                    } else if( innerNumber.value is float ) {
                        return new Number( ((float)innerNumber.value == 0.0f) ? 1 : 0 );
                    }
                }

                throw new System.Exception ("Unexpected operation or number type");
            }

            // Normal fallback
            var unary = new UnaryExpression (inner, op);
            return unary;
        }

        public UnaryExpression(Expression inner, string op)
		{
            this.innerExpression = AddContent(inner);
            this.op = op;
		}

        public override void GenerateIntoContainer(Runtime.Container container)
		{
			innerExpression.GenerateIntoContainer (container);

            container.AddContent(Runtime.NativeFunctionCall.CallWithName(nativeNameForOp));
		}

        public override string ToString ()
        {
            return nativeNameForOp + innerExpression;
        }

        string nativeNameForOp
        {
            get {
                // Replace "-" with "_" to make it unique (compared to subtraction)
                if (op == "-")
                    return "_";
                if (op == "not")
                    return "!";
                return op;
            }
        }
	}
        
}

