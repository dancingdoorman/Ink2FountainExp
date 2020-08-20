using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using FountainExponential.LanguageStructures;
using FountainExponential.LanguageStructures.Lexical;
using FountainExponential.LanguageStructures.Lexical.AutomaticFlow;
using FountainExponential.LanguageStructures.Lexical.InteractiveFlow;
using FountainExponential.LanguageStructures.Lexical.MetaData;
using FountainExponential.LanguageStructures.Lexical.Sections;
using FountainExponential.LanguageStructures.Syntactical;
using FountainExponential.LanguageStructures.Syntactical.AutomaticFlow;
using FountainExponential.LanguageStructures.Syntactical.Code;
using FountainExponential.LanguageStructures.Syntactical.FountainElement;
using FountainExponential.LanguageStructures.Syntactical.InteractiveFlow;
using FountainExponential.LanguageStructures.Syntactical.MetaData;
using FountainExponential.LanguageStructures.Syntactical.Sections;

namespace Ink.Ink2FountainExp.Adapting
{
    public class FountainRenderer
    {
        #region Properties

        /// <summary>Gets or sets the file system interactor.</summary>
        /// <value>The file system interactor.</value>
        public FountainSyntacticalRenderer SyntacticalRenderer { get; set; } = new FountainSyntacticalRenderer();

        #endregion Properties

        public void Write(StringBuilder builder, FountainPlay fountainPlay)
        {
            var mainFile = fountainPlay.MainFile;
            //var act = mainFile.SyntacticalElements[2] as Act;
            //var sequence = act.SyntacticalElements[1] as Sequence;

            Write(builder, mainFile);
        }

        public void Write(StringBuilder builder, FountainFile mainFile)
        {
            SyntacticalRenderer.Write(builder, mainFile.TitlePage);

            // Syntactic elements not belonging to any of the other sections. 
            SyntacticalRenderer.Write(builder, mainFile.SyntacticalElements);

            SyntacticalRenderer.Write(builder, new BlankLine());

            // Write from small to large because equal or bigger sections can not be contained by equal or smaller ones.
            SyntacticalRenderer.Write(builder, mainFile.NanoSlice);
            SyntacticalRenderer.Write(builder, mainFile.MicroSlices);
            SyntacticalRenderer.Write(builder, mainFile.Slices);
            SyntacticalRenderer.Write(builder, mainFile.Moments);
            SyntacticalRenderer.Write(builder, mainFile.Scenes);
            SyntacticalRenderer.Write(builder, mainFile.Sequences);
            SyntacticalRenderer.Write(builder, mainFile.Acts);
        }
    }
}
