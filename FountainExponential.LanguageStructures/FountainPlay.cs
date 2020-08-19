using System;
using System.Collections.Generic;
using System.Text;

namespace FountainExponential.LanguageStructures
{
    public class FountainPlay
    {
        /// <summary>Gets or sets the main file that describes the play.
        /// This supports the idea of having a main screenplay like story that has sidetracks in other files.</summary>
        /// <value>The main file that describes the play.</value>
        public FountainFile MainFile { get; set; } = new FountainFile();

        /// <summary>Gets or sets the support files that are part of the play.</summary>
        /// <value>The support files that are part of the play.</value>
        public List<FountainFile> SupportFiles { get; set; } = new List<FountainFile>();

        public override string ToString()
        {
            if(MainFile != null)
            {
                if (MainFile.TitlePage != null)
                {
                    return MainFile.TitlePage.ToString();
                }
                return MainFile.ToString();
            }
            return base.ToString();
        }
    }
}
