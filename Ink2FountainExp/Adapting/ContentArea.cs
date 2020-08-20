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


        public bool LabelDetoured { get; set; } = false;

        public int IndentLevel { get; set; } = 0;
        public ContentArea()
        {

        }

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

            this.LabelDetoured = contentArea.LabelDetoured;

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

                return null;
            }
        }

        public string GetSectionName()
        {

            if (Act != null)
                return Act.SectionName;
            if (Sequence != null)
                return Sequence.SectionName;
            if (Scene != null)
                return Scene.SectionName;
            if (Moment != null)
                return Moment.SectionName;
            if (Slice != null)
                return Slice.SectionName;
            if (MicroSlice != null)
                return MicroSlice.SectionName;
            if (NanoSlice != null)
                return NanoSlice.SectionName;

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
            var indentable = element as IIdentable;
            if (indentable != null)
            {
                if (indentable.IndentLevel != null)
                    indentable.IndentLevel.Level = IndentLevel;
                else
                    indentable.IndentLevel = new IndentLevel() { Level = IndentLevel };
            }
        }

        public ContentArea CreateSubContentArea(string labelName)
        {
            if (string.IsNullOrEmpty(labelName))
                return null;

            // if the weave has a name, it is labeled and we want to make it a seperate section.

            string subSectionName = string.Empty;
            ContentArea subContentArea = new ContentArea();
            SectionBase subSectionBase = null;
            if (Act != null)
            {
                subSectionName = Act.SectionName + "__" + labelName;
                var subSequence = new Sequence() { SectionName = subSectionName, SequenceStartToken = new SequenceToken() };
                Act.Sequences.Add(subSequence);
                subSectionBase = subSequence;

                subContentArea.Sequence = subSequence;
            }
            if (Sequence != null)
            {
                subSectionName = Sequence.SectionName + "__" + labelName;
                var subScene = new Scene() { SectionName = subSectionName, SceneStartToken = new SceneToken() };
                Sequence.Scenes.Add(subScene);
                subSectionBase = subScene;

                subContentArea.Scene = subScene;
            }
            if (Scene != null)
            {
                subSectionName = Scene.SectionName + "__" + labelName;
                var subMoment = new Moment() { SectionName = subSectionName, MomentStartToken = new MomentToken() };
                Scene.Moments.Add(subMoment);
                subSectionBase = subMoment;

                subContentArea.Moment = subMoment;
            }
            if (Moment != null)
            {
                subSectionName = Moment.SectionName + "__" + labelName;
                var subSlice = new Slice() { SectionName = subSectionName, SliceStartToken = new SliceToken() };
                Moment.Slices.Add(subSlice);
                subSectionBase = subSlice;

                subContentArea.Slice = subSlice;
            }
            if (Slice != null)
            {
                subSectionName = Slice.SectionName + "__" + labelName;
                var subMicroSlice = new MicroSlice() { SectionName = subSectionName, MicroSliceStartToken = new MicroSliceToken() };
                Slice.MicroSlices.Add(subMicroSlice);
                subSectionBase = subMicroSlice;

                subContentArea.MicroSlice = subMicroSlice;
            }
            if (MicroSlice != null)
            {
                subSectionName = MicroSlice.SectionName + "__" + labelName;
                var subNanoSlice = new NanoSlice() { SectionName = subSectionName, NanoSliceStartToken = new NanoSliceToken() };
                MicroSlice.NanoSlice.Add(subNanoSlice);
                subSectionBase = subNanoSlice;

                subContentArea.NanoSlice = subNanoSlice;
            }
            if (subSectionBase != null)
            {
                subSectionBase.SpaceToken = new SpaceToken();
                subSectionBase.EndLine = new EndLine();
                subSectionBase.SyntacticalElements.Add(new BlankLine());
            }

            //if (MenuChoice != null)
            //{
            //    MenuChoice.SyntacticalElements.Add(new SeparatedDetour()
            //    {
            //        FlowTargetToken = new FlowTargetToken()
            //        {
            //            Label = subSectionName
            //        },
            //        IndentLevel = new IndentLevel(),
            //        SpaceToken = new SpaceToken(),
            //        SeparatedDetourToken = new SeparatedDetourToken(),
            //        EndLine = new EndLine()
            //    });
            //}

            return subContentArea;
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
