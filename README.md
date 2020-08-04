# Ink2FountainExp
A tool that converts stories written in Ink by Inkle to Fountain Exponential.

Fountain Exponential and Ink are very similar, but where Ink focuses on programming power, Fountain Exponential focuses on readability. This means that Ink messes things together for briefness. This is specifically so with text generation. Fountain Exponential will separate thing out so it will be easier to see where things start and end.

## Technical approach
Ink2FountainExp is the refactored to unit tests version of the Ink engine. It is a version of the Ink that can function as the Ink engine running an Ink game, complete with loading Ink files and save compiled Ink JSON files, but also load and save Fountain Exponential files. 
The steps needed to achieve this are:
1. Enable writing Fountain Exponential files from loaded Ink files.
2. Add missing and lost information to the writen files based on rules of thumb, like scenes starting when a new location is entered.
3. Enable reading Fountain Exponential files so they are usable as Ink stories.
