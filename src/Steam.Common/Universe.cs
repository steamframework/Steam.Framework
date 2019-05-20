namespace Steam
{
    /// <summary>
    /// Universes: entirely different instances of Steam used by Valve for various purposes. 
    /// Most users use on the public universe.
    /// </summary>
    public enum Universe
    {
        /// <summary>
        /// The invalid universe, seen in some rare cases
        /// </summary>
        Invalid,
        /// <summary>
        /// The public universe where most of Steam operates
        /// </summary>
        Public,
        /// <summary>
        /// The Valve beta universe
        /// </summary>
        Beta,
        /// <summary>
        /// The Valve internal universe
        /// </summary>
        Internal,
        /// <summary>
        /// The Valve dev universe
        /// </summary>
        Dev
    }
}
