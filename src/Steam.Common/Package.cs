using System;

namespace Steam
{
    /// <summary>
    /// An ID for a collection of one or more applications and depots that can be sold via Steam or can be granted to users based on the activation of a Steam key, see https://partner.steamgames.com/doc/store/application/packages
    /// </summary>
    public readonly struct Package : IEquatable<Package>
    {
        private readonly uint _value;

        /// <summary>
        /// An invalid package value
        /// </summary>
        public static readonly Package Invalid = (Package)0xFFFFFFFF;

        /// <summary>
        /// The value for a free package subscription given to all Steam accounts
        /// </summary>
        public static readonly Package FreeSub = (Package)0x0;

        /// <summary>
        /// Creates a new instance of <see cref="Package"/>
        /// </summary>
        /// <param name="value">The ID value</param>
        public Package(uint value)
        {
            _value = value;
        }

        /// <summary>
        /// Explicit <see cref="uint"/> to <see cref="Package"/> conversion
        /// </summary>
        /// <param name="value">The package ID to convert</param>
        /// <returns>The new <see cref="Package"/></returns>
        public static explicit operator Package(uint value)
        {
            return new Package(value);
        }

        /// <summary>
        /// Explicit <see cref="uint"/> to <see cref="Package"/> conversion
        /// </summary>
        /// <param name="package">The package to convert</param>
        /// <returns>The new <see cref="Package"/></returns>
        public static explicit operator uint(Package package)
        {
            return package._value;
        }

        /// <summary>
        /// Calculates a hashcode for this instance
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return (int) _value;
        }

        /// <summary>
        /// Converts the <see cref="Package"/> to a <see cref="string"/>
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
            return obj is Package other && Equals(other);
        }

        /// <summary>
        /// Equality comparison between this instance and <paramref name="other"/>
        /// </summary>
        /// <param name="other">The object to compare to</param>
        /// <returns>Whether the 2 instances are equal</returns>
        public bool Equals(Package other)
        {
            return _value == other._value;
        }

        /// <summary>
        /// Equality comparison between <paramref name="a"/> and <paramref name="b"/>
        /// </summary>
        /// <param name="a">The first instance</param>
        /// <param name="b">The second instance</param>
        /// <returns>Whether the 2 instances are equal</returns>
        public static bool operator ==(Package a, Package b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Inequality comparison between <paramref name="a"/> and <paramref name="b"/>
        /// </summary>
        /// <param name="a">The first instance</param>
        /// <param name="b">The second instance</param>
        /// <returns>Whether the 2 instances are not equal</returns>
        public static bool operator !=(Package a, Package b)
        {
            return !(a == b);
        }
    }
}