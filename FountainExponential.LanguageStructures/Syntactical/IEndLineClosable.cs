using System;
using System.Collections.Generic;
using System.Text;

namespace FountainExponential.LanguageStructures.Syntactical
{
    /// <summary>The IEndLineClosable interface ensures elements can be closed with an end line and do not end in the middle </summary>
    public interface IEndLineClosable
    {
        EndLine EndLine { get; set; }
    }
}
