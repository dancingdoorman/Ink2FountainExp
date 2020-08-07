namespace Ink.Parsed
{
    public class BinaryExpression : Expression
	{
		public Expression leftExpression;
		public Expression rightExpression;
		public string opName;

		public BinaryExpression(Expression left, Expression right, string opName)
		{
            leftExpression = AddContent(left);
            rightExpression = AddContent(right);
			this.opName = opName;
		}

        public override void GenerateIntoContainer(Runtime.Container container)
		{
			leftExpression.GenerateIntoContainer (container);
			rightExpression.GenerateIntoContainer (container);

            opName = NativeNameForOp (opName);

            container.AddContent(Runtime.NativeFunctionCall.CallWithName(opName));
		}

        public override void ResolveReferences (IFiction context)
        {
            base.ResolveReferences (context);

            // Check for the following case:
            //
            //    (not A) ? B
            //
            // Since this easy to accidentally do:
            //
            //    not A ? B
            //
            // when you intend:
            //
            //    not (A ? B)
            if (NativeNameForOp (opName) == "?") {
                var leftUnary = leftExpression as UnaryExpression;
                if( leftUnary != null && (leftUnary.op == "not" || leftUnary.op == "!") ) {
                    Error ("Using 'not' or '!' here negates '"+leftUnary.innerExpression+"' rather than the result of the '?' or 'has' operator. You need to add parentheses around the (A ? B) expression.");
                }
            }
        }

        string NativeNameForOp(string opName)
        {
            if (opName == "and")
                return "&&";
            
            if (opName == "or")
                return "||";

            if (opName == "mod")
                return "%";

            if (opName == "has")
                return "?";

            if (opName == "hasnt")
                return "!?";
            
            return opName;
        }

        public override string ToString ()
        {
            return string.Format ("({0} {1} {2})", leftExpression, opName, rightExpression);
        }
	}
        
}

