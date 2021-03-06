﻿using System;
using System.Collections.Generic;
using Ink.InkCompiler.StringParsing;

namespace Ink.InkParser
{

    public delegate void ParserErrorEventHandler(object sender, ParserErrorEventArgs e);
    public class ParserErrorEventArgs : EventArgs
    {
        public ParserErrorType ErrorType { get; set; }
        public string Message { get; set; }
    }
}

