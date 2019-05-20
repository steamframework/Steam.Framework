using System;

namespace Steam
{
    /// <summary>
    /// Instance flags for individual user IDs
    /// </summary>
    [Flags]
    public enum Instance
    {
        All = 0,
        Desktop = 1,
        Console = 2,
        Web = 4,
    }
}
