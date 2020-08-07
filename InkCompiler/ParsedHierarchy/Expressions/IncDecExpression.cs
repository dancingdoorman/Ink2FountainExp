namespace Ink.Parsed
{
    public class IncDecExpression : Expression
    {
        public string varName;
        public bool isInc;
        public Expression expression;

        public IncDecExpression(string varName, bool isInc)
        {
            this.varName = varName;
            this.isInc = isInc;
        }

        public IncDecExpression (string varName, Expression expression, bool isInc) : this(varName, isInc)
        {
            this.expression = expression;
            AddContent (expression);
        }

        public override void GenerateIntoContainer(Runtime.Container container)
        {
            // x = x + y
            // ^^^ ^ ^ ^
            //  4  1 3 2
            // Reverse polish notation: (x 1 +) (assign to x)

            // 1.
            container.AddContent (new Runtime.VariableReference (varName));

            // 2.
            // - Expression used in the form ~ x += y
            // - Simple version: ~ x++
            if (expression)
                expression.GenerateIntoContainer (container);
            else
                container.AddContent (new Runtime.IntValue (1));

            // 3.
            container.AddContent (Runtime.NativeFunctionCall.CallWithName (isInc ? "+" : "-"));

            // 4.
            _runtimeAssignment = new Runtime.VariableAssignment(varName, false);
            container.AddContent (_runtimeAssignment);
        }

        public override void ResolveReferences (IFiction context)
        {
            base.ResolveReferences (context);

            var varResolveResult = context.ResolveVariableWithName(varName, fromNode: this);
            if (!varResolveResult.found) {
                Error ("variable for "+incrementDecrementWord+" could not be found: '"+varName+"' after searching: "+this.descriptionOfScope);
            }

            _runtimeAssignment.isGlobal = varResolveResult.isGlobal;

            if (!(parent is Weave) && !(parent is FlowBase) && !(parent is ContentList)) {
                Error ("Can't use " + incrementDecrementWord + " as sub-expression");
            }
        }

        string incrementDecrementWord {
            get {
                if (isInc)
                    return "increment";
                else
                    return "decrement";
            }
        }

        public override string ToString ()
        {
            if (expression)
                return varName + (isInc ? " += " : " -= ") + expression.ToString ();
            else
                return varName + (isInc ? "++" : "--");
        }

        Runtime.VariableAssignment _runtimeAssignment;
    }
        
}

