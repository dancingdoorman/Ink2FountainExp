namespace Ink.Parsed
{
    [System.Flags]
    public enum SequenceType
    {
        Stopping = 1, // default
        Cycle = 2,
        Shuffle = 4,
        Once = 8
    }
}

