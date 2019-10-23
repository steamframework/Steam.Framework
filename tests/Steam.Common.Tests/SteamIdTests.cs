using System;
using Xunit;

namespace Steam.Tests
{
    public class SteamIdTests
    {
        /// <summary>
        /// Check that the properties of the Steam ID return the correct values for the given ulong ID value and that raw value conversions work properly.
        /// </summary>
        [Theory]
        [InlineData(ulong.MaxValue, uint.MaxValue, (AccountType)0xF, (Universe)0xFF, 0xFFFFF)]
        [InlineData(0, 0, AccountType.Invalid, Universe.Invalid, 0)]
        public void ConstructorsAndProperties(ulong raw, uint accountId, AccountType type, Universe universe, uint instance)
        {
            var rawId = new SteamId(raw);
            var partsId = new SteamId(accountId, type, universe, instance);

            Assert.Equal(rawId, partsId);
            Assert.Equal(rawId, (SteamId)raw);

            Assert.Equal(accountId, rawId.AccountId);
            Assert.Equal(accountId, partsId.AccountId);

            Assert.Equal(type, rawId.AccountType);
            Assert.Equal(type, partsId.AccountType);

            Assert.Equal(universe, rawId.AccountUniverse);
            Assert.Equal(universe, partsId.AccountUniverse);

            Assert.Equal(instance, rawId.AccountInstance);
            Assert.Equal(instance, partsId.AccountInstance);

            Assert.Equal(raw, (ulong)rawId);
            Assert.Equal(raw, (ulong)partsId);
        }

        [Theory]
        [InlineData("STEAM_0:0:0", 0, Universe.Invalid)]
        [InlineData("STEAM_1:1:30210334", 60420669, Universe.Public)]
        [InlineData("STEAM_1:0:65978157",  131956314, Universe.Public)]
        public void Steam2String(string id, uint accountId, Universe universe)
        {
            var expected = new SteamId(accountId, AccountType.Individual, universe, 1);

            Assert.Equal(expected, SteamId.FromSteam2(id));
            Assert.Equal(id, SteamId.ToSteam2(expected));
        }

        [Theory]
        [InlineData("")]
        [InlineData("asdfghjkl")]
        [InlineData("STEAM_::")]
        [InlineData("[A:1:123432:412332]")]
        public void InvalidStringSteam2(string value)
        {
            Assert.False(SteamId.TryFromSteam2(value, out _));
            Assert.Throws<ArgumentException>(() => SteamId.FromSteam2(value));
        }

        [Theory]
        [InlineData(0, AccountType.GameServer, Universe.Public, 0)]
        [InlineData(0, AccountType.Invalid, Universe.Public, 0)]
        public void InvalidSteam2Id(uint id, AccountType type, Universe universe, uint instance)
        {
            var value = new SteamId(id, type, universe, instance);

            Assert.Throws<ArgumentException>(() => SteamId.ToSteam2(value));
        }

        [Theory]
        [InlineData("[I:0:0]", 0, AccountType.Invalid, Universe.Invalid, 1)]
        [InlineData("[A:1:123432:412332]", 123432, AccountType.AnonGameServer, Universe.Public, 412332)]
        public void Steam3String(string id, uint accountId, AccountType type, Universe universe, uint instance)
        {
            var expected = new SteamId(accountId, type, universe, instance);

            Assert.Equal(expected, SteamId.FromSteam3(id));
            Assert.Equal(id, SteamId.ToSteam3(expected));
        }

        [Theory]
        [InlineData("")]
        [InlineData("asdfghjkl")]
        [InlineData("STEAM_::")]
        [InlineData("STEAM_1:0:65978157")]
        public void InvalidStringSteam3(string value)
        {
            Assert.False(SteamId.TryFromSteam3(value, out _));
            Assert.Throws<ArgumentException>(() => SteamId.FromSteam3(value));
        }

        /// <summary>
        /// Test Steam3 outputs that don't create their input value when processed roundtrip
        /// </summary>
        [Theory]
        [InlineData("[i:4:556]", 556, AccountType.ConsoleUser, Universe.Dev, 0)]
        [InlineData("[i:2:123]", 123, (AccountType)11, Universe.Beta, 0)]
        public void NoRoundtripSteam3String(string id, uint accountId, AccountType type, Universe universe, uint instance)
        {
            var expected = new SteamId(accountId, type, universe, instance);

            Assert.NotEqual(expected, SteamId.FromSteam3(id));
            Assert.Equal(id, SteamId.ToSteam3(expected));
        }

        [Theory]
        [InlineData(1, AccountType.Individual, Universe.Public, 1, true)]
        [InlineData(0, AccountType.AnonUser, Universe.Public, 0, true)]
        [InlineData(123, AccountType.Chat, Universe.Public, 0, true)]
        [InlineData(12312, AccountType.GameServer, Universe.Dev, 0, true)]
        [InlineData(0, AccountType.AnonGameServer, Universe.Internal, 0, true)]
        [InlineData(1, AccountType.Invalid, Universe.Public, 1, false)] // invalid type
        [InlineData(1, (AccountType)11, Universe.Public, 1, false)] // invalid type
        [InlineData(1, AccountType.Individual, Universe.Invalid, 1, false)] // invalid universe
        [InlineData(1, AccountType.Individual, (Universe)5, 1, false)] // invalid universe
        [InlineData(0, AccountType.Individual, Universe.Public, 1, false)] // invalid id
        [InlineData(1, AccountType.Individual, Universe.Public, 5, false)] // invalid instance for invidividual
        [InlineData(0, AccountType.Clan, Universe.Public, 0, false)] // invalid id for clan
        [InlineData(1, AccountType.Clan, Universe.Public, 1, false)] // invalid instance for clan
        [InlineData(0, AccountType.GameServer, Universe.Public, 0, false)] // invalid id for game server
        public void IsValid(uint id, AccountType type, Universe universe, uint instance, bool isValid)
        {
            var value = new SteamId(id, type, universe, instance);
            Assert.Equal(isValid, SteamId.IsValid(value));
        }

        [Fact]
        public void Factories()
        {
            Assert.Equal(new SteamId(0, AccountType.AnonUser, Universe.Invalid, 0), SteamId.CreateAnonymousUser(Universe.Invalid));
            Assert.Equal(new SteamId(0, AccountType.AnonUser, Universe.Dev, 0), SteamId.CreateAnonymousUser(Universe.Dev));

            Assert.Equal(new SteamId(0, AccountType.AnonGameServer, Universe.Invalid, 0), SteamId.CreateAnonymousGameServer(Universe.Invalid));
            Assert.Equal(new SteamId(0, AccountType.AnonGameServer, Universe.Dev, 0), SteamId.CreateAnonymousGameServer(Universe.Dev));

            Assert.Equal(new SteamId(0, AccountType.Individual, Universe.Invalid, 0), SteamId.CreateIndividualAccount(0, Universe.Invalid, Instance.All));
            Assert.Equal(new SteamId(uint.MaxValue, AccountType.Individual, Universe.Dev, 7), SteamId.CreateIndividualAccount(uint.MaxValue, Universe.Dev, Instance.Desktop | Instance.Console | Instance.Web));
        }

        [Theory]
        [InlineData(1, AccountType.Individual, Universe.Public, 1)]
        [InlineData(0, AccountType.Invalid, Universe.Invalid, 0)]
        public void StaticKeyConversions(uint id, AccountType type, Universe universe, uint instance)
        {
            var dyn = new SteamId(id, type, universe, instance);
            var sta = new SteamId(id, type, universe, 0);

            Assert.True(dyn.StaticEquals(sta));
            Assert.Equal(SteamId.ToStaticAccountKey(dyn), (ulong)sta);
        }
    }
}
