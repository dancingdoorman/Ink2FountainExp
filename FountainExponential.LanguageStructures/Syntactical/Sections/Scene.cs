using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical.Sections;
using FountainExponential.LanguageStructures.Syntactical;

namespace FountainExponential.LanguageStructures.Syntactical.Sections
{
    public class Scene : SectionBase, ISyntacticalElementable
    {
        public SceneToken SceneStartToken { get; set; }
        public override string ToString()
        {
            return SceneStartToken + SectionName;
        }
    }
}
