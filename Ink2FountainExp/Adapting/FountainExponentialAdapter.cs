using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Ink;

namespace Ink.Ink2FountainExp.Adapting
{
    public class FountainExponentialAdapter : IFountainExponentialAdaptable
    {
        public string ConvertToFountainExponential(Ink.Parsed.Fiction parsedFiction, string inputFileName)
        {
            // The optional Title Page is always the first thing in a Fountain document. 
            // Information is encoding in the format key: value. 
            // Keys can have spaces (e. g. Draft date), but must end with a colon.
            // Excample:
            // Title:
            //    _** BRICK &STEEL * *_
            //    _** FULL RETIRED** _
            //Credit: Written by
            //Author: Stu Maschwitz
            //Source: Story by KTM
            //Draft date: 1 / 20 / 2012
            //Contact:
            //    Next Level Productions
            //    1588 Mission Dr.
            //    Solvang, CA 93463


            var builder = new StringBuilder();
            builder.Append("Title:\r\n\t");
            builder.Append("\t");
            builder.Append(Path.GetFileNameWithoutExtension(inputFileName));
            builder.Append("\r\n");

            // A page break is implicit after the Title Page. Just drop down two lines and start writing your screenplay.
            builder.Append("\r\n\r\n");

            // We add the Act and Sequence manually because Ink files do not have them.
            builder.Append("# Act I\r\n\r\n");
            builder.Append("## Sequence 1\r\n\r\n");

            //bool choiceStarted = false;
            foreach (var parsedObject in parsedFiction.content)
            {
                if (parsedObject.typeName == "Function")
                {
                    var function = parsedObject as Ink.Parsed.Knot;
                    if (function != null)
                    {
                        builder.Append("```\r\nfunction ");
                        builder.Append(function.name);
                        builder.Append("(");
                        bool firstArgument = true;
                        foreach (var arg in function.arguments)
                        {
                            if (firstArgument == false)
                                builder.Append(", ");

                            builder.Append(arg.name);

                            firstArgument = false;
                        }
                        builder.Append(")");
                        builder.Append(" {\r\n");

                        foreach (var functionContent in function.content)
                        {
                            var functionWeave = functionContent as Ink.Parsed.Weave;
                            if (functionWeave != null)
                            {
                                foreach (var weaveContent in functionWeave.content)
                                {
                                    var variableAssignment = weaveContent as Ink.Parsed.VariableAssignment;
                                    if (variableAssignment != null)
                                    {
                                        builder.Append("\t");
                                        builder.Append(variableAssignment.variableName);
                                        builder.Append(" = ");
                                        builder.Append(variableAssignment.expression.ToString());

                                        //foreach (var variableAssignmentContent in variableAssignment.content)
                                        //{
                                        //    var binaryExpression = variableAssignmentContent as Ink.Parsed.BinaryExpression;
                                        //    if (binaryExpression != null)
                                        //    {
                                        //        builder.Append(binaryExpression.leftExpression);
                                        //        builder.Append(" = ");
                                        //        builder.Append(binaryExpression.rightExpression);
                                        //    }
                                        //}
                                    }
                                }
                            }
                        }
                        builder.Append("\r\n}\r\n```\r\n\r\n");
                    }
                }
                if (parsedObject.typeName == "Knot")
                {

                    var flowBase = parsedObject as Ink.Parsed.FlowBase;
                    if (flowBase != null)
                    {
                        builder.AppendFormat("\r\n### {0}\r\n\r\n", flowBase.name);

                        for (int i = 0; i < flowBase.content.Count; i++)
                        {
                            Ink.Parsed.Object flowBaseContent = flowBase.content[i];

                            var flowBaseStitch = flowBaseContent as Ink.Parsed.Stitch;
                            if (flowBaseStitch != null)
                            {
                                builder.AppendFormat("\r\n#### {0}\r\n\r\n", flowBaseStitch.name);
                            }

                            var flowBaseWeave = flowBaseContent as Ink.Parsed.Weave;
                            if (flowBaseWeave != null)
                            {
                                Ink.Parsed.Object previousWeaveContent = null;
                                Ink.Parsed.Object weaveContent = null;
                                Ink.Parsed.Object nextWeaveContent = null;
                                for (int ii = 0; ii < flowBaseWeave.content.Count; ii++)
                                {
                                    previousWeaveContent = weaveContent;
                                    weaveContent = flowBaseWeave.content[ii];
                                    if (ii == flowBaseWeave.content.Count - 1)
                                        nextWeaveContent = null;
                                    else
                                        nextWeaveContent = flowBaseWeave.content[1 + ii];

                                    var weaveText = weaveContent as Ink.Parsed.Text;
                                    if (weaveText != null)
                                    {
                                        if (weaveText.text == "\r" || weaveText.text == "\n")
                                            builder.Append("\r\n");
                                        else
                                            builder.Append(weaveText.text);
                                    }
                                    var weaveGather = weaveContent as Ink.Parsed.Gather;
                                    if (weaveGather != null)
                                    {
                                        // Gather points bring the branches back together and have currently no real benefit for Fountian Exponential.
                                        //builder.Append("\r\n");
                                    }
                                    var weaveChoice = weaveContent as Ink.Parsed.Choice;
                                    if (weaveChoice != null)
                                    {
                                        if (previousWeaveContent == null || !(previousWeaveContent is Ink.Parsed.Choice))
                                            builder.Append(":::\r\n");

                                        foreach (var choiceContent in weaveChoice.content)
                                        {
                                            builder.Append("* ");

                                            var choiceContentList = choiceContent as Ink.Parsed.ContentList;
                                            if (choiceContentList != null)
                                            {
                                                foreach (var choiceContentListContent in choiceContentList.content)
                                                {
                                                    var choiceContentListText = choiceContentListContent as Ink.Parsed.Text;
                                                    if (choiceContentListText != null)
                                                    {
                                                        if (choiceContentListText.text == "\r" || choiceContentListText.text == "\n")
                                                            builder.Append("\r\n");
                                                        else
                                                            builder.Append(choiceContentListText.text);
                                                    }
                                                }
                                            }
                                        }
                                        if (nextWeaveContent == null || !(nextWeaveContent is Ink.Parsed.Choice))
                                            builder.Append(":::\r\n");
                                    }
                                    var weaveDivert = weaveContent as Ink.Parsed.Divert;
                                    if (weaveDivert != null)
                                    {
                                        builder.Append(weaveDivert.ToString());
                                    }
                                    var weaveContentList = weaveContent as Ink.Parsed.ContentList;
                                    if (weaveContentList != null)
                                    {
                                        //var flowBaseStitch = weaveContentList as Ink.Parsed.Stitch;
                                        //if (flowBaseStitch != null)
                                        //{
                                        //    builder.AppendFormat("## {0}\r\n\r\n", flowBaseStitch.name);
                                        //}
                                        builder.Append(weaveContentList.ToString());
                                    }
                                }
                            }
                        }
                    }
                }
            }


            var fountainContent = builder.ToString();//story.ToJson();
            return fountainContent;
        }


    }
}
