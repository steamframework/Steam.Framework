using System;
using System.Text.RegularExpressions;

namespace Steam
{
    /// <summary>
    /// A globally unique identifier for Steam accounts, groups, lobbies, and chat rooms
    /// </summary>
    public readonly struct SteamId : IEquatable<SteamId>
    {
        private const uint AccountIdMask = 0xFFFFFFFF;

        private const uint AccountInstanceMask = 0xFFFFF;
        private const int AccountInstanceShift = 32;

        private const uint AccountTypeMask = 0xF;
        private const int AccountTypeShift = 52;

        private const int AccountUniverseShift = 56;

        private const int ChatAccountInstanceMask = 0xFFF;

        private readonly static Regex Steam2Regex = new Regex(@"STEAM_([0-4]):(0|1):(\d+)");
        private static readonly Regex Steam3Regex = new Regex(@"^\[([iIUMGAPCgTLca]):([0-4]):(\d+)(?::(\d+))?\]");

        /// <summary>
        /// An instance flag used for clan chat rooms
        /// </summary>
        public const int ClanFlag = (ChatAccountInstanceMask + 1) >> 1;
        /// <summary>
        /// An instance flag used for lobby chats
        /// </summary>
        public const int LobbyFlag = (ChatAccountInstanceMask + 1) >> 2;

        /// <summary>
        /// The default SteamID value with all values set to 0
        /// </summary>
        public static readonly SteamId Zero = default;

        /// <summary>
        /// The SteamID from a user game connection with an out of date
        /// game server that hasn't implemented the protocol to provide its SteamID
        /// </summary>
        public static readonly SteamId OutOfDateGameServer = default;
        /// <summary>
        /// The SteamID from a user game connection to a sv_lan game server
        /// </summary>
        public static readonly SteamId LanModeGameServer = default;
        /// <summary>
        /// The SteamID that can come from a user game connection to a game server
        /// that has just booted but hasn't yet initialized its Steam3 component
        /// and started logging on.
        /// </summary>
        public static readonly SteamId NetYetInitializedGameServer = new SteamId(1, AccountType.Invalid, Universe.Invalid, 0);
        /// <summary>
        /// The SteamID that can come from a user game connection to a game server
        /// that isn't using the Steam authentication system but still wants to support
        /// the "Join Game" option in the friends list
        /// </summary>
        public static readonly SteamId NonSteamGameServer = new SteamId(2, AccountType.Invalid, Universe.Invalid, 0);

        /// <summary>
        /// The underlying value that represents this SteamID
        /// </summary>
        public readonly ulong Value;

        /// <summary>
        /// The universe this ID is in
        /// </summary>
        public Universe AccountUniverse
        {
            get
            {
                return (Universe)(Value >> AccountUniverseShift);
            }
        }

        /// <summary>
        /// The account type represented by this ID
        /// </summary>
        public AccountType AccountType
        {
            get
            {
                return (AccountType)((Value >> AccountTypeShift) & AccountTypeMask);
            }
        }

        /// <summary>
        /// The account ID of this SteamID
        /// </summary>
        public uint AccountId
        {
            get
            {
                return (uint)(Value & AccountIdMask);
            }
        }

        /// <summary>
        /// The dynamic instance bits of the ID
        /// </summary>
        public uint AccountInstance
        {
            get
            {
                return (uint)((Value >> AccountInstanceShift) & AccountInstanceMask);
            }
        }

        /// <summary>
        /// Creates a new SteamID from the specified value
        /// </summary>
        public SteamId(ulong value)
        {
            Value = value;
        }

        /// <summary>
        /// Creates a new SteamID with the specified parts
        /// </summary>
        /// <param name="id">The account ID of the SteamID. This part is 32 bits.</param>
        /// <param name="type">The account type of the SteamID. This part is 4 bits.</param>
        /// <param name="universe">The universe of the SteamID. This part is 8 bits.</param>
        /// <param name="instance">The dynamic instance part of the SteamID. This part is 20 bits.</param>
        public SteamId(uint id, AccountType type, Universe universe, uint instance)
        {
            Value = id 
                + ((ulong)instance << AccountInstanceShift) 
                + ((ulong)type << AccountTypeShift) 
                + ((ulong)universe << AccountUniverseShift);
        }

        /// <summary>
        /// Creates an anonymous user SteamID in the specified universe
        /// </summary>
        /// <param name="universe">The universe to set</param>
        public static SteamId CreateAnonymousUser(Universe universe)
        {
            return new SteamId(0, AccountType.AnonUser, universe, 0);
        }

        /// <summary>
        /// Creates an anonymous game server SteamID in the specified universe
        /// </summary>
        /// <param name="universe">The universe to set</param>
        public static SteamId CreateAnonymousGameServer(Universe universe)
        {
            return new SteamId(0, AccountType.AnonGameServer, universe, 0);
        }

        /// <summary>
        /// Creates an anonymous individual account SteamID with the specified ID, universe, and instance flags
        /// </summary>
        /// <param name="id">The account ID</param>
        /// <param name="universe">The universe</param>
        /// <param name="instance">The instance flags</param>
        public static SteamId CreateIndividualAccount(uint id, Universe universe, Instance instance)
        {
            return new SteamId(id, AccountType.Individual, universe, (uint)instance);
        }

        /// <summary>
        /// Creates a new ID from the specified ID string in the format STEAM_X:Y:Z
        /// </summary>
        /// <param name="id">A string containing the Steam2 ID to parse</param>
        public static SteamId FromSteam2(string id)
        {
            var match = Steam2Regex.Match(id);
            if (!match.Success)
                throw new ArgumentException("The input string does not match the Steam2 pattern");

            var universe = (Universe)int.Parse(match.Groups[1].Value);
            var magic = uint.Parse(match.Groups[2].Value);
            var account = uint.Parse(match.Groups[3].Value);
            return new SteamId((account * 2) + magic, AccountType.Individual, universe, 1);
        }

        /// <summary>
        /// Creates a new ID from the specified ID string in the format [W:X:Y:Z]
        /// </summary>
        /// <param name="id">A string containing the Steam3 ID to parse</param>
        /// <remarks>
        /// The instance for returned IDs depends on the input.
        /// 
        /// For 'c' and 'L' types, the instance is the ClanFlag or LobbyFlag respectively.
        /// For 'U' and 'i'/'I' the instance is 1.
        /// For 'g' and 'T' the instance is 0.
        /// 
        /// For all other account types, the instance is Z or 0 if Z wasn't found.
        /// </remarks>
        public static SteamId FromSteam3(string id)
        {
            var match = Steam3Regex.Match(id);
            if (!match.Success)
                throw new ArgumentException("The input string does not match the Steam3 pattern");

            var typeChar = match.Groups[1].Value[0];
            var universe = (Universe)int.Parse(match.Groups[2].Value);
            var account = uint.Parse(match.Groups[3].Value);
            uint instance = match.Groups[4].Success ? uint.Parse(match.Groups[4].Value) : 0;
            AccountType type;
            switch (typeChar)
            {
                case 'A':
                    type = AccountType.AnonGameServer;
                    break;
                case 'G':
                    type = AccountType.GameServer;
                    break;
                case 'C':
                    type = AccountType.ContentServer;
                    break;
                case 'g':
                    type = AccountType.Clan;
                    instance = 0;
                    break;
                case 'c':
                    type = AccountType.Chat;
                    instance = ClanFlag;
                    break;
                case 'L':
                    type = AccountType.Chat;
                    instance = LobbyFlag;
                    break;
                case 'T':
                    type = AccountType.Chat;
                    instance = 0;
                    break;
                case 'U':
                    type = AccountType.Individual;
                    instance = 1;
                    break;
                case 'I':
                case 'i':
                    type = AccountType.Invalid;
                    instance = 1;
                    break;
                default:
                    throw new InvalidOperationException("unreachable");
            }

            return new SteamId(account, type, universe, instance);
        }

        /// <summary>
        /// Returns whether a given SteamID is considered valid
        /// </summary>
        /// <param name="id">The ID to check</param>
        public static bool IsValid(SteamId id)
        {
            if (id.AccountType <= AccountType.Invalid || id.AccountType > AccountType.AnonUser)
                return false;

            if (id.AccountUniverse <= Universe.Invalid || id.AccountUniverse > Universe.Dev)
                return false;

            if (id.AccountType == AccountType.Individual)
            {
                if (id.AccountId == 0 || id.AccountInstance > (uint)Instance.Web)
                    return false;
            }

            if (id.AccountType == AccountType.Clan)
            {
                if (id.AccountId == 0 || id.AccountInstance != 0)
                    return false;
            }

            if (id.AccountType == AccountType.GameServer)
            {
                if (id.AccountId == 0)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Returns whether this is a login ID to be filled in
        /// </summary>
        public bool IsBlankAnonymousAccount => AccountId == 0 && IsAnonymousAccount && AccountInstance == 0;
        /// <summary>
        /// Returns whether this is a persistent game server ID
        /// </summary>
        public bool IsPersistentGameServer => AccountType == AccountType.GameServer;
        /// <summary>
        /// Returns whether this is a game server ID, either anonymous or persistent
        /// </summary>
        public bool IsGameServer => IsPersistentGameServer || IsAnonymousGameServer;
        /// <summary>
        /// Returns whether this is a content server ID
        /// </summary>
        public bool IsContentServer => AccountType == AccountType.ContentServer;
        /// <summary>
        /// Returns whether this is a clan ID
        /// </summary>
        public bool IsClan => AccountType == AccountType.Clan;
        /// <summary>
        /// Returns whether this is a chat ID
        /// </summary>
        public bool IsChat => AccountType == AccountType.Chat;
        /// <summary>
        /// Returns whether this is an individual user ID or a faked console user ID
        /// </summary>
        public bool IsIndividualAccount => AccountType == AccountType.Individual || IsConsoleUser;
        /// <summary>
        /// Returns whether this is a faked SteamID for a PSN friend account
        /// </summary>
        public bool IsConsoleUser => AccountType == AccountType.ConsoleUser;
        /// <summary>
        /// Returns whether this is an anonymous game server ID
        /// </summary>
        public bool IsAnonymousGameServer => AccountType == AccountType.AnonGameServer;
        /// <summary>
        /// Returns whether this is an anonymous user ID
        /// </summary>
        public bool IsAnonymousUser => AccountType == AccountType.AnonUser;
        /// <summary>
        /// Returns whether this is an anonymous account, either a user or game server
        /// </summary>
        public bool IsAnonymousAccount => IsAnonymousUser || IsAnonymousGameServer;
        /// <summary>
        /// Returns whether this is a lobby chat ID
        /// </summary>
        public bool IsLobby => IsChat && (AccountInstance & LobbyFlag) != 0;

        /// <summary>
        /// Returns a matching chat SteamID for a clan. If it is already a clat ID, it returns the ID
        /// </summary>
        /// <param name="id">The ID to convert</param>
        /// <exception cref="ArgumentException">The ID isn't a chat or clan ID</exception>
        public static SteamId ToChat(SteamId id)
        {
            if (id.AccountType == AccountType.Chat)
                return id;

            return ClanToChat(id);
        }
        /// <summary>
        /// Returns a matching clan SteamID given a clan chat ID. If it is already a clan ID, it returns the ID
        /// </summary>
        /// <param name="id">The chat ID to convert</param>
        /// <exception cref="ArgumentException">The ID isn't a clan or chat ID</exception>
        public static SteamId ToClan(SteamId id)
        {
            if (id.AccountType == AccountType.Clan)
                return id;

            return ChatToClan(id);
        }

        /// <summary>
        /// Returns a matching clan chat ID for a clan ID. If it is already a clan
        /// </summary>
        /// <param name="id">The clan ID to convert</param>
        /// <exception cref="ArgumentException">The ID isn't a clan ID</exception>
        public static SteamId ClanToChat(SteamId id)
        {
            if (id.AccountType != AccountType.Clan)
                throw new ArgumentException("The provided ID must have an account type of Clan");

            return new SteamId(id.AccountId, AccountType.Chat, id.AccountUniverse, ClanFlag);
        }
        /// <summary>
        /// Returns a matching clan ID for a clan chat ID
        /// </summary>
        /// <param name="id">The chat ID to convert</param>
        /// <exception cref="ArgumentException">The ID isn't a chat ID with a clan flag</exception>
        public static SteamId ChatToClan(SteamId id)
        {
            if (id.AccountType != AccountType.Chat || (id.AccountInstance & ClanFlag) == 0)
                throw new ArgumentException("The provided ID must have an account type of Chat and have a clan instance flag");

            return new SteamId(id.AccountId, AccountType.Clan, id.AccountUniverse, 0);
        }

        /// <summary>
        /// Converts the ID to a static account key. That is, the ID without dynamic instance information
        /// </summary>
        /// <param name="id">The ID to convert</param>
        public static ulong ToStaticAccountKey(SteamId id)
        {
            return id.AccountId
                + ((ulong)id.AccountType << AccountTypeShift)
                + ((ulong)id.AccountUniverse << AccountUniverseShift);
        }

        /// <summary>
        /// Renders the individual user SteamID in Steam2 format (STEAM_X:Y:Z)
        /// </summary>
        /// <param name="id">The ID to render</param>
        public static string ToSteam2(SteamId id)
        {
            if (id.AccountType != AccountType.Individual)
                throw new ArgumentException("Cannot render non-individual account SteamID to Steam2 format", nameof(id));

            return $"STEAM_{(int)id.AccountUniverse}:{id.AccountId & 1}:{Math.Floor(id.AccountId / 2d)}";
        }

        /// <summary>
        /// Renders the ID in Steam3 format ([W:X:Y:Z])
        /// </summary>
        /// <param name="id">The ID to render</param>
        public static string ToSteam3(SteamId id)
        {
            switch (id.AccountType)
            {
                case AccountType.AnonGameServer:
                    return $"[A:{(int)id.AccountUniverse}:{id.AccountId}:{id.AccountInstance}]";
                case AccountType.GameServer:
                    return $"[G:{(int)id.AccountUniverse}:{id.AccountId}]";
                case AccountType.Multiseat:
                    return $"[M:{(int)id.AccountUniverse}:{id.AccountId}:{id.AccountInstance}]";
                case AccountType.Pending:
                    return $"[P:{(int)id.AccountUniverse}:{id.AccountId}]";
                case AccountType.ContentServer:
                    return $"[C:{(int)id.AccountUniverse}:{id.AccountId}]";
                case AccountType.Clan:
                    return $"[g:{(int)id.AccountUniverse}:{id.AccountId}]";
                case AccountType.Chat:
                    if ((id.AccountInstance & ClanFlag) != 0)
                        return $"[c:{(int)id.AccountUniverse}:{id.AccountId}]";
                    else if ((id.AccountInstance & LobbyFlag) != 0)
                        return $"[L:{(int)id.AccountUniverse}:{id.AccountId}]";
                    else
                        return $"[T:{(int)id.AccountUniverse}:{id.AccountId}]";
                case AccountType.Invalid:
                    return $"[I:{(int)id.AccountUniverse}:{id.AccountId}]";
                case AccountType.Individual:
                    if (id.AccountInstance != (int)Instance.Desktop)
                        return $"[U:{(int)id.AccountUniverse}:{id.AccountId}:{id.AccountInstance}]";
                    else
                        return $"[U:{(int)id.AccountUniverse}:{id.AccountId}]";
                case AccountType.AnonUser:
                    return $"[a:{(int)id.AccountUniverse}:{id.AccountId}]";
                default:
                    return $"[i:{(int)id.AccountUniverse}:{id.AccountId}]";
            }
        }

        /// <summary>
        /// Returns if this <see cref="SteamId"/> is equal to the other <see cref="SteamId"/> by comparing the static properties of each ID
        /// </summary>
        /// <param name="other">The other ID to compare with</param>
        /// <returns>True if their account IDs, types, and universes are equal. Otherwise false</returns>
        public bool StaticEquals(SteamId other)
        {
            return AccountId == other.AccountId && AccountType == other.AccountType && AccountUniverse == other.AccountUniverse;
        }

        public bool Equals(SteamId other)
        {
            return Value == other.Value;
        }

        public override bool Equals(object obj)
        {
            return obj is SteamId id ? Equals(id) : false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return ToSteam3(this);
        }

        public static implicit operator ulong(SteamId id)
        {
            return id.Value;
        }

        public static implicit operator SteamId(ulong id)
        {
            return new SteamId(id);
        }

        public static bool operator ==(SteamId a, SteamId b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(SteamId a, SteamId b)
        {
            return !a.Equals(b);
        }
    }
}
