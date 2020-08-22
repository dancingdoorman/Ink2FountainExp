using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;
using FountainExponential.LanguageStructures.Lexical.Sections;
using FountainExponential.LanguageStructures.Syntactical;

namespace FountainExponential.LanguageStructures.Syntactical.Sections
{
    public class Sequence : SectionBase, ISyntacticalElementable, ISubsectionAddable, IStartTokenEnsurable
    {
        public SequenceToken SequenceStartToken { get; set; }

        //
        // Because a section can only be closed by a equal or bigger section we must order them from small to bigger.
        // A section can only contain sections that are smaller then itself.
        //

        public List<NanoSlice> NanoSlices { get; set; } = new List<NanoSlice>();
        public List<MicroSlice> MicroSlices { get; set; } = new List<MicroSlice>();
        public List<Slice> Slices { get; set; } = new List<Slice>();
        public List<Moment> Moments { get; set; } = new List<Moment>();
        public List<Scene> Scenes { get; set; } = new List<Scene>();

        public bool HasSubsection
        {
            get
            {
                return NanoSlices.Count > 0 || MicroSlices.Count > 0 || Slices.Count > 0 || Moments.Count > 0 || Scenes.Count > 0;
            }
        }

        public ILexicalElementable EnsureStartToken()
        {
            if (SequenceStartToken == null)
                SequenceStartToken = new SequenceToken();

            return SequenceStartToken;
        }

        public SectionBase AddSubsection()
        {
            var subsection = new Scene();
            Scenes.Add(subsection);
            return subsection;
        }

        public override string ToString()
        {
            return SequenceStartToken + SectionName;
        }
    }
}
