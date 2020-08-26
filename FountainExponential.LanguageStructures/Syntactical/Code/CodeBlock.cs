using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical.Code;
using FountainExponential.LanguageStructures.Syntactical;

namespace FountainExponential.LanguageStructures.Syntactical.Code
{
    public class CodeBlock : CodeContainerBase, ISyntacticalElementable, ICodeBlockEnclosable
    {
        public CodeBlockStartToken CodeBlockStartToken { get; set; }

        public CodeBlockEndToken CodeBlockEndToken { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.Append(CodeBlockStartToken);
            if (SyntacticalElements != null)
            {
                foreach (var element in SyntacticalElements)
                {
                    builder.Append(element);
                }
            }
            builder.Append(CodeBlockEndToken);
            return builder.ToString();
        }
    }
}
