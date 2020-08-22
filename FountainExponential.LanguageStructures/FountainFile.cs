using FountainExponential.LanguageStructures.Lexical;
using FountainExponential.LanguageStructures.Syntactical;
using FountainExponential.LanguageStructures.Syntactical.MetaData;
using FountainExponential.LanguageStructures.Syntactical.Sections;
using System;
using System.Collections.Generic;
using System.Text;

namespace FountainExponential.LanguageStructures
{
    public class FountainFile : SyntacticalElementContainer, ISubsectionAddable
    {
        /// <summary>Gets or sets the filename.</summary>
        /// <value>The filename.</value>
        public string Filename { get; set; }

        /// <summary>Gets or sets the title page. The syntactical element is default created because every Fountain file has a title page.
        /// The title page is split off from the other syntactical elements for ease of use.</summary>
        /// <value>The title page.</value>
        public TitlePage TitlePage { get; set; } = new TitlePage();


        //
        // Because a section can only be closed by a equal or bigger section we must order them from small to bigger.
        // A section can only contain sections that are smaller then itself.
        //

        public List<NanoSlice> NanoSlices { get; set; } = new List<NanoSlice>();
        public List<MicroSlice> MicroSlices { get; set; } = new List<MicroSlice>();
        public List<Slice> Slices { get; set; } = new List<Slice>();
        public List<Moment> Moments { get; set; } = new List<Moment>();
        public List<Scene> Scenes { get; set; } = new List<Scene>();
        public List<Sequence> Sequences { get; set; } = new List<Sequence>();
        public List<Act> Acts { get; set; } = new List<Act>();

        public bool HasSubsection
        {
            get
            {
                return NanoSlices.Count > 0 || MicroSlices.Count > 0 || Slices.Count > 0 || Moments.Count > 0 || Scenes.Count > 0 || Sequences.Count > 0 || Acts.Count > 0;
            }
        }

        public SectionBase AddSubsection()
        {
            var subsection = new Act();
            Acts.Add(subsection);
            return subsection;
        }

        public override string ToString()
        {
            if (!string.IsNullOrEmpty(Filename))
            {
                return Filename;
            }
            if (TitlePage != null)
            {
                if (TitlePage.KeyInformationList != null)
                {
                    foreach (var keyInfo in TitlePage.KeyInformationList)
                    {
                        var title = keyInfo as Title;
                        if (title != null)
                        {
                            return title.ToString();
                        }
                    }
                }
                return TitlePage.ToString();
            }
            return base.ToString();
        }
    }
}
