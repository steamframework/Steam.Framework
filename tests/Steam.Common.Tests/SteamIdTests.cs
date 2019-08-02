using System;
using Xunit;

namespace Steam.Tests
{
    public class SteamIdTests
    {
        [Theory]
        [InlineData(ulong.MaxValue)]
        [InlineData(0)]
        [InlineData(ulong.MaxValue - 12345678901234567890)]
        public void RoundtripRawValue(ulong raw)
        {
            var id = new SteamId(raw);
            var other = new SteamId(id.AccountId, id.AccountType, id.AccountUniverse, id.AccountInstance);

            Assert.Equal(id.AccountId, other.AccountId);
            Assert.Equal(id.AccountType, other.AccountType);
            Assert.Equal(id.AccountUniverse, other.AccountUniverse);
            Assert.Equal(id.AccountInstance, other.AccountInstance);
        }

        [Theory]
        [InlineData(1, AccountType.Individual, Universe.Public, 1)]
        [InlineData(0, AccountType.Invalid, Universe.Invalid, 0)]
        [InlineData(123, AccountType.Chat, Universe.Dev, SteamId.ClanFlag)]
        public void RoundtripParts(uint accountId, AccountType type, Universe universe, uint instance)
        {
            var id = new SteamId(accountId, type, universe, instance);

            Assert.Equal(accountId, id.AccountId);
            Assert.Equal(type, id.AccountType);
            Assert.Equal(universe, id.AccountUniverse);
            Assert.Equal(instance, id.AccountInstance);
        }

        [Theory]
        [InlineData(0, 0, AccountType.Invalid, Universe.Invalid, 0)]
        [InlineData(76561198092222042, 131956314, AccountType.Individual, Universe.Public, 1)]
        [InlineData(76561198020686397, 60420669, AccountType.Individual, Universe.Public, 1)]
        public void WholeMakesCorrectParts(ulong whole, uint id, AccountType type, Universe universe, uint instance)
        {
            var value = new SteamId(whole);

            Assert.Equal(value, new SteamId(whole));
            Assert.Equal(id, value.AccountId);
            Assert.Equal(type, value.AccountType);
            Assert.Equal(universe, value.AccountUniverse);
            Assert.Equal(instance, value.AccountInstance);
        }

        [Theory]
        [InlineData(0, 0, AccountType.Invalid, Universe.Invalid, 0)]
        [InlineData(76561198092222042, 131956314, AccountType.Individual, Universe.Public, 1)]
        [InlineData(76561198020686397, 60420669, AccountType.Individual, Universe.Public, 1)]
        public void PartsMakeCorrectWhole(ulong whole, uint id, AccountType type, Universe universe, uint instance)
        {
            var value = new SteamId(id, type, universe, instance);

            Assert.Equal(whole, (ulong)value);
        }

        [Theory]
        [InlineData("STEAM_0:0:0", 4503603922337792)]
        [InlineData("STEAM_1:1:30210334", 76561198020686397)]
        [InlineData("STEAM_1:0:65978157", 76561198092222042)]
        public void TestFromSteam2(string id, ulong whole)
        {
            Assert.Equal(whole, (ulong)SteamId.FromSteam2(id));
        }

        [Theory]
        [InlineData("STEAM_0:0:0", 4503603922337792)]
        [InlineData("STEAM_1:1:30210334", 76561198020686397)]
        [InlineData("STEAM_1:0:65978157", 76561198092222042)]
        public void TestToSteam2(string id, ulong whole)
        {
            Assert.Equal(id, SteamId.ToSteam2((SteamId)whole));
        }

        [Theory]
        [InlineData("[I:0:0]", 4294967296)]
        [InlineData("[A:1:123432:412332]", 91842945002627624)]
        public void TestFromSteam3(string id, ulong whole)
        {
            Assert.Equal(whole, (ulong)SteamId.FromSteam3(id));
        }

        [Theory]
        [InlineData("[I:0:0]", 4294967296)]
        [InlineData("[A:1:123432:412332]", 91842945002627624)]
        public void TestToSteam3(string id, ulong whole)
        {
            Assert.Equal(id, SteamId.ToSteam3((SteamId)whole));
        }

        [Theory]
        [InlineData(1, AccountType.Individual, Universe.Public, 1)]
        [InlineData(0, AccountType.AnonUser, Universe.Public, 0)]
        [InlineData(123, AccountType.Chat, Universe.Public, 0)]
        [InlineData(12312, AccountType.GameServer, Universe.Dev, 0)]
        [InlineData(0, AccountType.AnonGameServer, Universe.Internal, 0)]
        public void TestValidCheck(uint id, AccountType type, Universe universe, uint instance)
        {
            var value = new SteamId(id, type, universe, instance);
            Assert.True(SteamId.IsValid(value));
        }

        [Theory]
        [InlineData(1, AccountType.Invalid, Universe.Public, 1)] // invalid type
        [InlineData(1, (AccountType)11, Universe.Public, 1)] // invalid type
        [InlineData(1, AccountType.Individual, Universe.Invalid, 1)] // invalid universe
        [InlineData(1, AccountType.Individual, (Universe)5, 1)] // invalid universe
        [InlineData(0, AccountType.Individual, Universe.Public, 1)] // invalid id
        [InlineData(1, AccountType.Individual, Universe.Public, 5)] // invalid instance for invidividual
        [InlineData(0, AccountType.Clan, Universe.Public, 0)] // invalid id for clan
        [InlineData(1, AccountType.Clan, Universe.Public, 1)] // invalid instance for clan
        [InlineData(0, AccountType.GameServer, Universe.Public, 0)] // invalid id for game server
        public void TestInvalidIdCheck(uint id, AccountType type, Universe universe, uint instance)
        {
            var value = new SteamId(id, type, universe, instance);
            Assert.False(SteamId.IsValid(value));
        }

        [Fact]
        public void ValidClanChatConversions()
        {
            var id = new SteamId(1, AccountType.Clan, Universe.Public, 0);

            Assert.Equal(id, SteamId.ChatToClan(SteamId.ClanToChat(id)));
        }

        [Theory]
        [InlineData(1, AccountType.Individual, Universe.Public, SteamId.ClanFlag)] // wrong type
        [InlineData(1, AccountType.Chat, Universe.Public, 0)] // invalid flag
        [InlineData(1, AccountType.Individual, Universe.Public, 0)]
        public void InvalidChatClanConversions(uint id, AccountType type, Universe universe, uint instance)
        {
            Assert.Throws<ArgumentException>(() => SteamId.ChatToClan(new SteamId(id, type, universe, instance)));
        }

        [Theory]
        [InlineData(1, AccountType.Individual, Universe.Public, 1)]
        public void StaticKeyConversions(uint id, AccountType type, Universe universe, uint instance)
        {
            var dyn = new SteamId(id, type, universe, instance);
            var sta = new SteamId(id, type, universe, 0);

            Assert.True(dyn.StaticEquals(sta));
            Assert.Equal(SteamId.ToStaticAccountKey(dyn), (ulong)sta);
        }
    }
}
