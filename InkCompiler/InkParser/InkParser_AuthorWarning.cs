﻿using Ink.Parsed;
using Ink.InkCompiler.StringParsing;

namespace Ink.InkParser
{
    public partial class InkParser
    {
        protected AuthorWarning AuthorWarning()
        {
            Whitespace ();

            if (Parse (Identifier) != "TODO")
                return null;

            Whitespace ();

            ParseString (":");

            Whitespace ();

            var message = ParseUntilCharactersFromString ("\n\r");

            return new AuthorWarning (message);
        }

    }
}

