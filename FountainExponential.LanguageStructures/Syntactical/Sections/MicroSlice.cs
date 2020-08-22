using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;
using FountainExponential.LanguageStructures.Lexical.Sections;
using FountainExponential.LanguageStructures.Syntactical;

namespace FountainExponential.LanguageStructures.Syntactical.Sections
{
    public class MicroSlice : SectionBase, ISyntacticalElementable, ISubsectionAddable, IStartTokenEnsurable
    {
        public MicroSliceToken MicroSliceStartToken { get; set; }

        //
        // Because a section can only be closed by a equal or bigger section we must order them from small to bigger.
        // A section can only contain sections that are smaller then itself.
        //

        public List<NanoSlice> NanoSlices { get; set; } = new List<NanoSlice>();

        public bool HasSubsection
        {
            get
            {
                return NanoSlices.Count > 0;
            }
        }

        public ILexicalElementable EnsureStartToken()
        {
            if (MicroSliceStartToken == null)
                MicroSliceStartToken = new MicroSliceToken();

            return MicroSliceStartToken;
        }

        public SectionBase AddSubsection()
        {
            var subsection = new NanoSlice();
            NanoSlices.Add(subsection);
            return subsection;
        }

        public override string ToString()
        {
            return MicroSliceStartToken + SectionName;
        }
    }
}
