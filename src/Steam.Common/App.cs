using System;

namespace Steam
{
    /// <summary>
    /// Main representation of a product, see https://partner.steamgames.com/doc/store/application
    /// </summary>
    public readonly struct App : IEquatable<App>
    {
        /// <summary>
        /// The Id
        /// </summary>
        public readonly uint Id;

        /// <summary>
        /// Invalid app value
        /// </summary>
        public static readonly App Invalid = 0x0;

        /// <summary>
        /// Creates a new instance of <see cref="App"/>
        /// </summary>
        /// <param name="id">The id</param>
        public App(uint id)
        {
            Id = id;
        }

        /// <summary>
        /// Implicit <see cref="uint"/> to <see cref="App"/> conversion
        /// </summary>
        /// <param name="appId">The app id to convert</param>
        /// <returns>The new <see cref="App"/></returns>
        public static implicit operator App(uint appId)
        {
            return new App(appId);
        }

        /// <summary>
        /// Implicit <see cref="App"/> to <see cref="uint"/> conversion
        /// </summary>
        /// <param name="app">The app to convert</param>
        /// <returns>The converted app Id</returns>
        public static implicit operator uint(App app)
        {
            return app.Id;
        }


        /// <summary>
        /// Calculates a hashcode for this instance
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        /// <summary>
        /// Converts the <see cref="App"/> to a <see cref="string"/>
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return Id.ToString();
        }

        /// <summary>
        /// Equality comparison between this instance and <paramref name="obj"/>
        /// </summary>
        /// <param name="obj">The object to compare to</param>
        /// <returns>Whether the 2 instances are equal</returns>
        public override bool Equals(object obj)
        {
            return obj is App other && Equals(other);
        }

        /// <summary>
        /// Equality comparison between this instance and <paramref name="other"/>
        /// </summary>
        /// <param name="other">The object to compare to</param>
        /// <returns>Whether the 2 instances are equal</returns>
        public bool Equals(App other)
        {
            return Id == other.Id;
        }

        /// <summary>
        /// Equality comparison between <paramref name="a"/> and <paramref name="b"/>
        /// </summary>
        /// <param name="a">The first instance</param>
        /// <param name="b">The second instance</param>
        /// <returns>Whether the 2 instances are equal</returns>
        public static bool operator ==(App a, App b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Inequality comparison between <paramref name="a"/> and <paramref name="b"/>
        /// </summary>
        /// <param name="a">The first instance</param>
        /// <param name="b">The second instance</param>
        /// <returns>Whether the 2 instances are not equal</returns>
        public static bool operator !=(App a, App b)
        {
            return !(a == b);
        }
    }
}