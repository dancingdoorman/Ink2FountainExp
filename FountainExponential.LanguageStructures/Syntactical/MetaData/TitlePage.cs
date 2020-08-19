using System;
using System.Collections.Generic;
using System.Text;
using FountainExponential.LanguageStructures.Lexical.MetaData;
using FountainExponential.LanguageStructures.Syntactical;

namespace FountainExponential.LanguageStructures.Syntactical.MetaData
{
    public class TitlePage : ISyntacticalElementable
    {
        /// <summary>Gets or sets the key information list.</summary>
        /// <value>The key information list.</value>
        public List<IKeyInformable> KeyInformationList { get; set; } = new List<IKeyInformable>();

        /// <summary>Gets or sets the title page break token.</summary>
        /// <value>The title page break token.</value>
        public TitlePageBreakToken TitlePageBreakToken { get; set; }

        public override string ToString()
        {
            if (KeyInformationList != null)
            {
                foreach (var keyInfo in KeyInformationList)
                {
                    // Maybe show all the key info here
                    var title = keyInfo as Title;
                    if (title != null)
                    {
                        return title.ToString();
                    }
                }
            }
            return base.ToString();
        }
    }
}
