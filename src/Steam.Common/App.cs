using System;

namespace Steam
{
    /// <summary>
    /// An ID for the main representation of a product, see https://partner.steamgames.com/doc/store/application
    /// </summary>
    public readonly struct App : IEquatable<App>
    {
        private readonly uint _value;

        private const uint Max = 16777215;

        /// <summary>
        /// A value used to represent an invalid app value
        /// </summary>
        public static readonly App Invalid = (App)0x0;

        /// <summary>
        /// The max value of an app ID
        /// </summary>
        public static readonly App MaxValue = (App)Max;

        /// <summary>
        /// Creates a new instance of <see cref="App"/>
        /// </summary>
        /// <param name="value">The ID value. Cannot be more than <seealso cref="MaxValue"/></param>
        public App(uint value)
        {
            if (value > Max)
            {
                throw new ArgumentOutOfRangeException(nameof(value),
                    $"App ID cannot be more than {Max}");
            }

            _value = value;
        }

        /// <summary>
        /// Explicit <see cref="uint"/> to <see cref="App"/> conversion
        /// </summary>
        /// <param name="value">The app ID to convert</param>
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
            return app._value;
        }

        /// <summary>
        /// Calculates a hashcode for this instance
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return _value.GetHashCode();
        }

        /// <summary>
        /// Converts the <see cref="App"/> to a <see cref="string"/>
        /// </summary>
        /// <returns>The string</returns>
        public override string ToString()
        {
            return _value.ToString();
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
            return _value == other._value;
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