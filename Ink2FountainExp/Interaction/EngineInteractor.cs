﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ink.Ink2FountainExp;

namespace Ink.Ink2FountainExp.Interaction
{
    public class EngineInteractor : IEngineInteractable
    {
        public Runtime.IStory CreateStoryFromJson(string fileTextContent)
        {
            return new Runtime.Story(fileTextContent);
        }
    }
}
