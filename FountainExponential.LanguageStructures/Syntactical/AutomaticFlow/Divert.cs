using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;
using FountainExponential.LanguageStructures.Lexical.AutomaticFlow;
using FountainExponential.LanguageStructures.Syntactical;
using FountainExponential.LanguageStructures.Syntactical.FountainElement;

namespace FountainExponential.LanguageStructures.Syntactical.AutomaticFlow
{
    public class Divert : ISyntacticalElementable, IIndentable
    {
        public FlowTargetToken FlowTargetToken { get; set; }
        public SpaceToken SpaceToken { get; set; }
        
        public EndLine EndLine { get; set; }
        public IndentLevel IndentLevel { get; set; }

        //public override string ToString()
        //{
        //    if (!string.IsNullOrEmpty(TextContent))
        //    {
        //        return TextContent;
        //    }
        //    return base.ToString();
        //}
    }
}
