using System;

namespace Steam
{
    /// <summary>
    /// Main representation of a product, see https://partner.steamgames.com/doc/store/application
    /// </summary>
    public readonly struct App : IEquatable<App>
    {
        /// <summary>
        /// The value of the app id
        /// </summary>
        public readonly uint Value;

        /// <summary>
        /// Invalid app value
        /// </summary>
        public static readonly App Invalid = (App)0x0;

        /// <summary>
        /// The max value of a uint24 data structure
        /// </summary>
        public const uint UInt24MaxValue = 16777215;

        /// <summary>
        /// Creates a new instance of <see cref="App"/>
        /// </summary>
        /// <param name="value">The id with a max value of <seealso cref="UInt24MaxValue"/>(16777215)</param>
        public App(uint value)
        {
            if (value > UInt24MaxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(value),
                    $"Apps are represented as a uint24 and therefore has a max value of {UInt24MaxValue}");
            }
            
            Value = value;
        }

        /// <summary>
        /// Explicit <see cref="uint"/> to <see cref="App"/> conversion
        /// </summary>
        /// <param name="value">The app id to convert</param>
        /// <returns>The new <see cref="App"/></returns>
        public static explicit operator App(uint value)
        {
            return new App(value);
        }

        /// <summary>
        /// Explicit <see cref="App"/> to <see cref="uint"/> conversion
        /// </summary>
        /// <param name="app">The app to convert</param>
        /// <returns>The converted app Id</returns>
        public static explicit operator uint(App app)
        {
            return app.Value;
        }


        /// <summary>
        /// Calculates a hashcode for this instance
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// Converts the <see cref="App"/> to a <see cref="string"/>
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return Value.ToString();
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
            return Value == other.Value;
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