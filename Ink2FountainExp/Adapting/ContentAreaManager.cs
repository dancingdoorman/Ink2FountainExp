using System;
using System.Collections.Generic;
using System.Text;

namespace Ink.Ink2FountainExp.Adapting
{
    public class ContentAreaManager
    {
        public ContentArea BaseContentArea { get; set; }
        public Stack<ContentArea> ContentAreaStack { get; set; } = new Stack<ContentArea>();

        public ContentArea CurrentContentArea
        {
            get
            {
                if (ContentAreaStack != null && ContentAreaStack.Count > 0)
                {
                    return ContentAreaStack.Peek();
                }
                return BaseContentArea;
            }
        }

        public int CurrentContentAreaIndent
        {
            get
            {
                var current = CurrentContentArea;
                if (current != null)
                {
                    return current.IndentLevel;
                }
                return 0;
            }
        }

        public ContentArea CreateSubsectionContentArea(string labelName)
        {
            var current = CurrentContentArea;
            if (current == null)
                return null;
            
            var subsectionContentArea = CurrentContentArea.CreateSubsectionContentArea(labelName);
            if (ContentAreaStack != null)
                ContentAreaStack.Push(subsectionContentArea);

            return subsectionContentArea;
        }

        public ContentArea CreateIndentedContentArea()
        {
            var current = CurrentContentArea;
            if (current == null)
                return null;

            var indentedContentArea = new ContentArea() { IndentLevel = 1 + CurrentContentArea.IndentLevel };

            if (ContentAreaStack != null)
                ContentAreaStack.Push(indentedContentArea);

            return indentedContentArea;
        }

        public ContentAreaManager(ContentArea contentArea)
        {
            BaseContentArea = contentArea;

            var copiedContentArea = new ContentArea(BaseContentArea);
            ContentAreaStack.Push(copiedContentArea);
        }

        public ContentArea GetBelowCurrentContentArea()
        {
            if (ContentAreaStack == null)
                return BaseContentArea;

            if (ContentAreaStack.Count == 1)
                return ContentAreaStack.Peek();

            if (ContentAreaStack.Count > 1)
            {
                var top = ContentAreaStack.Pop();
                var second = ContentAreaStack.Peek();
                ContentAreaStack.Push(top);

                return second;
            }

            return BaseContentArea;
        }

        public ContentArea PopCurrentContentArea()
        {
            if (ContentAreaStack != null && ContentAreaStack.Count > 0)
            {
                return ContentAreaStack.Pop();
            }
            return BaseContentArea;
        }

        public ContentArea ResetContentAreaStack()
        {
            ContentAreaStack.Clear();

            var copiedContentArea = new ContentArea(BaseContentArea);
            ContentAreaStack.Push(copiedContentArea);

            return copiedContentArea;
        }
    }
}
