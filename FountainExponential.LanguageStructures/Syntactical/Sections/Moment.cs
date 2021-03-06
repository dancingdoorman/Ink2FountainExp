﻿using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical;
using FountainExponential.LanguageStructures.Lexical.Sections;
using FountainExponential.LanguageStructures.Syntactical;

namespace FountainExponential.LanguageStructures.Syntactical.Sections
{
    public class Moment : SectionBase, ISyntacticalElementable, ISubsectionAddable, IStartTokenEnsurable
    {
        public MomentToken MomentStartToken { get; set; }

        //
        // Because a section can only be closed by a equal or bigger section we must order them from small to bigger.
        // A section can only contain sections that are smaller then itself.
        //

        public List<NanoSlice> NanoSlices { get; set; } = new List<NanoSlice>();
        public List<MicroSlice> MicroSlices { get; set; } = new List<MicroSlice>();
        public List<Slice> Slices { get; set; } = new List<Slice>();

        public bool HasSubsection
        {
            get
            {
                return NanoSlices.Count > 0 || MicroSlices.Count > 0 || Slices.Count > 0;
            }
        }

        public ILexicalElementable EnsureStartToken()
        {
            if (MomentStartToken == null)
                MomentStartToken = new MomentToken();

            return MomentStartToken;
        }

        public SectionBase AddSubsection()
        {
            var subsection = new Slice();
            Slices.Add(subsection);
            return subsection;
        }

        public override string ToString()
        {
            return MomentStartToken + SectionName;
        }
    }
}
