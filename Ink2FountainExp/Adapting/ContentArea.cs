using FountainExponential.LanguageStructures;
using FountainExponential.LanguageStructures.Lexical;
using FountainExponential.LanguageStructures.Lexical.AutomaticFlow;
using FountainExponential.LanguageStructures.Lexical.Sections;
using FountainExponential.LanguageStructures.Syntactical;
using FountainExponential.LanguageStructures.Syntactical.AutomaticFlow;
using FountainExponential.LanguageStructures.Syntactical.FountainElement;
using FountainExponential.LanguageStructures.Syntactical.InteractiveFlow;
using FountainExponential.LanguageStructures.Syntactical.Sections;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ink.Ink2FountainExp.Adapting
{
    /// <summary>The ContentArea class encapsulates a number of helper variables dealing where the content belongs to.</summary>
    public class ContentArea
    {
        public MenuChoice MenuChoice { get; set; }

        public Act Act { get; set; }
        public Sequence Sequence { get; set; }
        public Scene Scene { get; set; }
        public Moment Moment { get; set; }
        public Slice Slice { get; set; }
        public MicroSlice MicroSlice { get; set; }
        public NanoSlice NanoSlice { get; set; }

        public FountainFile File { get; set; }

        
        //public bool LabelDetoured { get; set; } = false;

        public int IndentLevel { get; set; } = 0;

        /// <summary>Initializes a new instance of the <see cref="ContentArea" /> class.</summary>
        public ContentArea()
        {

        }

        /// <summary>Initializes a new instance of the <see cref="ContentArea" /> class.</summary>
        /// <param name="contentArea">The content area.</param>
        public ContentArea(ContentArea contentArea)
        {
            if (contentArea == null)
                return;

            this.MenuChoice = contentArea.MenuChoice;

            this.Act = contentArea.Act;
            this.Sequence = contentArea.Sequence;
            this.Scene = contentArea.Scene;
            this.Moment = contentArea.Moment;
            this.Slice = contentArea.Slice;
            this.MicroSlice = contentArea.MicroSlice;
            this.NanoSlice = contentArea.NanoSlice;

            this.File = contentArea.File;
            

            //this.LabelDetoured = contentArea.LabelDetoured;

            this.IndentLevel = contentArea.IndentLevel;
        }

        public List<ISyntacticalElementable> SyntacticalElements
        { 
            get
            {
                if (MenuChoice != null)
                    return MenuChoice.SyntacticalElements;

                if (Act != null)
                    return Act.SyntacticalElements;
                if (Sequence != null)
                    return Sequence.SyntacticalElements;
                if (Scene != null)
                    return Scene.SyntacticalElements;
                if (Moment != null)
                    return Moment.SyntacticalElements;
                if (Slice != null)
                    return Slice.SyntacticalElements;
                if (MicroSlice != null)
                    return MicroSlice.SyntacticalElements;
                if (NanoSlice != null)
                    return NanoSlice.SyntacticalElements;

                if (File != null)
                    return File.SyntacticalElements;
                
                return null;
            }
        }

        public SectionBase GetCurrentSection()
        {
            if (Act != null)
                return Act;
            if (Sequence != null)
                return Sequence;
            if (Scene != null)
                return Scene;
            if (Moment != null)
                return Moment;
            if (Slice != null)
                return Slice;
            if (MicroSlice != null)
                return MicroSlice;
            if (NanoSlice != null)
                return NanoSlice;

            return null;
        }

        public string GetCurrentSectionName()
        {
            var subsection = GetCurrentSection();

            if (subsection != null)
                return subsection.SectionName;

            return null;
        }

        public void AddSyntacticalElement(ISyntacticalElementable element)
        {
            if (element == null)
                return;

            var syntacticalElements = SyntacticalElements;
            if (syntacticalElements == null)
                return;

            syntacticalElements.Add(element);
            var indentable = element as IIndentable;
            if (indentable != null)
            {
                if (indentable.IndentLevel != null)
                    indentable.IndentLevel.Level = IndentLevel;
                else
                    indentable.IndentLevel = new IndentLevel() { Level = IndentLevel };
            }
        }

        private void AddCurrentSection(SectionBase section)
        {
            var act = section as Act;
            if (act != null)
            {
                Act = act;
                return;
            }
            var sequence = section as Sequence;
            if (sequence != null)
            {
                Sequence = sequence;
                return;
            }
            var scene = section as Scene;
            if (scene != null)
            {
                Scene = scene;
                return;
            }
            var moment = section as Moment;
            if (moment != null)
            {
                Moment = moment;
                return;
            }
            var slice = section as Slice;
            if (slice != null)
            {
                Slice = slice;
                return;
            }
            var microSlice = section as MicroSlice;
            if (microSlice != null)
            {
                MicroSlice = microSlice;
                return;
            }
            var nanoSlice = section as NanoSlice;
            if (nanoSlice != null)
            {
                NanoSlice = nanoSlice;
                return;
            }
        }

        public ContentArea CreateSubsectionContentArea(string labelName)
        {
            if (string.IsNullOrEmpty(labelName))
                return null;

            // if the weave has a name, it is labeled and we want to make it a separate section.

            var subsectionContentArea = new ContentArea() { IndentLevel = 0 };

            var currentSection = GetCurrentSection();
            var currentSectionSubsectionAddable = currentSection as ISubsectionAddable;
            if (currentSectionSubsectionAddable != null)
            {
                var theSubsection = currentSectionSubsectionAddable.AddSubsection();
                // A top level section like Sequence should not be perpended with the act
                if(theSubsection is Sequence || theSubsection is Scene)
                    theSubsection.SectionName = labelName;
                else
                    theSubsection.SectionName = currentSection.SectionName + "__" + labelName;

                theSubsection.SpaceToken = new SpaceToken();
                theSubsection.EndLine = new EndLine();
                theSubsection.SyntacticalElements.Add(new BlankLine());
                subsectionContentArea.AddCurrentSection(theSubsection);

                var startTokenEnsurable = theSubsection as IStartTokenEnsurable;
                if (startTokenEnsurable != null)
                {
                    startTokenEnsurable.EnsureStartToken();
                }

                if (currentSection.SubsectionsSeparatorToken == null)
                    currentSection.SubsectionsSeparatorToken = new SubsectionsSeparatorToken();
            }

            return subsectionContentArea;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();

            if (MenuChoice != null)
                builder.Append("MenuChoice");

            if (Act != null)
                builder.Append("Act");
            if (Sequence != null)
                builder.Append("Sequence");
            if (Scene != null)
                builder.Append("Scene");
            if (Moment != null)
                builder.Append("Moment");
            if (Slice != null)
                builder.Append("Slice");
            if (MicroSlice != null)
                builder.Append("MicroSlice");
            if (NanoSlice != null)
                builder.Append("NanoSlice");


            builder.Append(" ");
            builder.Append(IndentLevel);

            return builder.ToString();
        }
    }
}
