using FountainExponential.LanguageStructures.Syntactical.Sections;
using System;
using System.Collections.Generic;
using System.Text;

namespace FountainExponential.LanguageStructures.Syntactical.Sections
{
    public interface ISubsectionAddable
    {
        SectionBase AddSubsection();
    }
}
