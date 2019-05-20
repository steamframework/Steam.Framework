namespace Steam
{
    /// <summary>
    /// Represents the various account types storable in a SteamID
    /// </summary>
    public enum AccountType
    {
        /// <summary>
        /// An invalid account type
        /// </summary>
        Invalid,
        /// <summary>
        /// Individual user accounts
        /// </summary>
        Individual,
        /// <summary>
        /// Multiseat cybercafe accounts
        /// </summary>
        Multiseat,
        /// <summary>
        /// Game server accounts
        /// </summary>
        GameServer,
        /// <summary>
        /// Anonymous game server accounts
        /// </summary>
        AnonGameServer,
        /// <summary>
        /// Pending individual user accounts
        /// </summary>
        Pending,
        /// <summary>
        /// Content server accounts
        /// </summary>
        ContentServer,
        /// <summary>
        /// Clan accounts
        /// </summary>
        Clan,
        /// <summary>
        /// Chat room accounts
        /// </summary>
        Chat,
        /// <summary>
        /// Faked console user accounts
        /// </summary>
        ConsoleUser,
        /// <summary>
        /// Anonymous user accounts
        /// </summary>
        AnonUser
    }
}
