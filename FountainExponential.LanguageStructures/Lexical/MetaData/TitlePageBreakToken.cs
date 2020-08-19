using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;
using FountainExponential.LanguageStructures.Syntactical;

namespace FountainExponential.LanguageStructures.Lexical.MetaData
{
    public class TitlePageBreakToken : ILexicalElementable
    {
        /// <summary>The pattern of dropping 2 lines implies that writing start on the 3de line</summary>
        public static List<BlankLine> Pattern = new List<BlankLine>() { new BlankLine(), new BlankLine() };
    }
}
