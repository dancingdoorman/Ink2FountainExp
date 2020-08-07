namespace Ink.Parsed
{
    public abstract class Expression : Parsed.Object
	{
        public bool outputWhenComplete { get; set; }

		public override Runtime.Object GenerateRuntimeObject ()
		{
            var container = new Runtime.Container ();

            // Tell Runtime to start evaluating the following content as an expression
            container.AddContent (Runtime.ControlCommand.EvalStart());

            GenerateIntoContainer (container);

            // Tell Runtime to output the result of the expression evaluation to the output stream
            if (outputWhenComplete) {
                container.AddContent (Runtime.ControlCommand.EvalOutput());
            }

            // Tell Runtime to stop evaluating the content as an expression
            container.AddContent (Runtime.ControlCommand.EvalEnd());

            return container;
		}

        // When generating the value of a constant expression,
        // we can't just keep generating the same constant expression into
        // different places where the constant value is referenced, since then
        // the same runtime objects would be used in multiple places, which
        // is impossible since each runtime object should have one parent.
        // Instead, we generate a prototype of the runtime object(s), then
        // copy them each time they're used.
        public void GenerateConstantIntoContainer(Runtime.Container container)
        {
            if( _prototypeRuntimeConstantExpression == null ) {
                _prototypeRuntimeConstantExpression = new Runtime.Container ();
                GenerateIntoContainer (_prototypeRuntimeConstantExpression);
            }

            foreach (var runtimeObj in _prototypeRuntimeConstantExpression.content) {
                container.AddContent (runtimeObj.Copy());
            }
        }

        public abstract void GenerateIntoContainer (Runtime.Container container);

        Runtime.Container _prototypeRuntimeConstantExpression;
	}
        
}

