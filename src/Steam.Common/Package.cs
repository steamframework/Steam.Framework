using System;

namespace Steam
{
    /// <summary>
    /// A package is a collection of one or more applications and depots that can be sold via Steam or can be granted to users based on the activation of a Steam key, see https://partner.steamgames.com/doc/store/application/packages
    /// </summary>
    public struct Package : IEquatable<Package>
    {
        /// <summary>
        /// The Id
        /// </summary>
        public readonly uint Id;

        /// <summary>
        /// Invalid package value
        /// </summary>
        public static readonly uint Invalid = 0xFFFFFFFF;

        /// <summary>
        /// FreeSub value
        /// </summary>
        public static readonly uint FreeSub = 0x0;

        /// <summary>
        /// Creates a new instance of <see cref="Package"/>
        /// </summary>
        /// <param name="id">The id</param>
        public Package(uint id)
        {
            Id = id;
        }

        /// <summary>
        /// Implicit <see cref="uint"/> to <see cref="Package"/> conversion
        /// </summary>
        /// <param name="id">The package id to convert</param>
        /// <returns>The new <see cref="Package"/></returns>
        public static implicit operator Package(uint id)
        {
            return new Package(id);
        }

        /// <summary>
        /// Implicit <see cref="uint"/> to <see cref="Package"/> conversion
        /// </summary>
        /// <param name="package">The package to convert</param>
        /// <returns>The new <see cref="Package"/></returns>
        public static implicit operator uint(Package package)
        {
            return package.Id;
        }

        /// <summary>
        /// Calculates a hashcode for this instance
        /// </summary>
        /// <returns>The hashcode</returns>
        public override int GetHashCode()
        {
            return (int) Id;
        }

        /// <summary>
        /// Converts the <see cref="Package"/> to a <see cref="string"/>
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
            return obj is Package other && Equals(other);
        }

        /// <summary>
        /// Equality comparison between this instance and <paramref name="other"/>
        /// </summary>
        /// <param name="other">The object to compare to</param>
        /// <returns>Whether the 2 instances are equal</returns>
        public bool Equals(Package other)
        {
            return Id == other.Id;
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