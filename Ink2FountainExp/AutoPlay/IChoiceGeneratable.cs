using System;
using System.Collections.Generic;
using System.Text;

namespace Ink.Ink2FountainExp.AutoPlay
{
    public interface IChoiceGeneratable
    {
        int GetRandomChoice(int choiseCount);
    }
}
