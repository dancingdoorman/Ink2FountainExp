using FountainExponential.LanguageStructures.Lexical;
using FountainExponential.LanguageStructures.Syntactical;
using FountainExponential.LanguageStructures.Syntactical.MetaData;
using System;
using System.Collections.Generic;
using System.Text;

namespace FountainExponential.LanguageStructures
{
    public class FountainFile
    {
        /// <summary>Gets or sets the filename.</summary>
        /// <value>The filename.</value>
        public string Filename { get; set; }
        
        /// <summary>Gets or sets the text content of the file.</summary>
        /// <value>The text content of the file.</value>
        public string TextContent { get; set; }

        /// <summary>Gets or sets the file lexical elements. This list is expected to hold all the lexical elements in the file. Also those in the title page or in code, YAML or container blocks and code or attribute spans.
        /// The lexical level is aimed at giving insight in what is contained in the file and should give an accurate 1 to 1 representation of the things in the file.</summary>
        /// <value>The file lexical elements.</value>
        public List<ILexicalElementable> LexicalElements { get; set; } = new List<ILexicalElementable>();

        /// <summary>Gets or sets the title page. The syntactical element is default created because every Fountain file has a title page.
        /// The title page is split off from the other syntactical elements for ease of use.</summary>
        /// <value>The title page.</value>
        public TitlePage TitlePage { get; set; } = new TitlePage();

        /// <summary>Gets or sets the play syntactical elements. This list is expected to hold all the syntactical elements in the play except for those in the title page.
        /// The syntactical level is aimed at usability for other parts of the code instead of a 1 to 1 representation of the things in the file.</summary>
        /// <value>The play syntactical elements.</value>
        public List<ISyntacticalElementable> SyntacticalElements{ get; set; } = new List<ISyntacticalElementable>();


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
