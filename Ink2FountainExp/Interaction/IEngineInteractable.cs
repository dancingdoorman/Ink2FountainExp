using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ink.Ink2FountainExp;

namespace Ink.Ink2FountainExp.Interaction
{
    /// <summary>The IEngineInteractable interface defines the interaction with the engine.</summary>
    public interface IEngineInteractable
    {
        Runtime.IStory CreateStoryFromJson(string fileTextContent);
    }
}
