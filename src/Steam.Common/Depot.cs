using System;

namespace Steam
{
    /// <summary>
    /// A depot is a logical grouping of files which are all delivered to a customer as a single group, see https://partner.steamgames.com/doc/store/application/depots
    /// </summary>
    public struct Depot : IEquatable<Depot>
    {
        /// <summary>
        /// The Id
        /// </summary>
        public readonly uint Id;

        /// <summary>
        /// Invalid depot value
        /// </summary>
        public static readonly uint Invalid = 0x0;

        /// <summary>
        /// Creates a new instance of <see cref="Depot"/>
        /// </summary>
        /// <param name="id">The id</param>
        public Depot(uint id)
        {
            Id = id;
        }

        /// <summary>
        /// Implicit <see cref="uint"/> to <see cref="Depot"/> conversion
        /// </summary>
        /// <param name="id">The depot id to convert</param>
        /// <returns>The new <see cref="Depot"/></returns>
        public static implicit operator Depot(uint id)
        {
            return new Depot(id);
        }

        /// <summary>
        /// Implicit <see cref="Depot"/> to <see cref="uint"/> conversion
        /// </summary>
        /// <param name="depot">The depot to convert</param>
        /// <returns>The converted depot Id</returns>
        public static implicit operator uint(Depot depot)
        {
            return depot.Id;
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
        /// Converts the <see cref="Depot"/> to a <see cref="string"/>
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
            return obj is Depot other && Equals(other);
        }

        /// <summary>
        /// Equality comparison between this instance and <paramref name="other"/>
        /// </summary>
        /// <param name="other">The object to compare to</param>
        /// <returns>Whether the 2 instances are equal</returns>
        public bool Equals(Depot other)
        {
            return Id == other.Id;
        }

        /// <summary>
        /// Equality comparison between <paramref name="a"/> and <paramref name="b"/>
        /// </summary>
        /// <param name="a">The first instance</param>
        /// <param name="b">The second instance</param>
        /// <returns>Whether the 2 instances are equal</returns>
        public static bool operator ==(Depot a, Depot b)
        {
            return a.Equals(b);
        }

        /// <summary>
        /// Inequality comparison between <paramref name="a"/> and <paramref name="b"/>
        /// </summary>
        /// <param name="a">The first instance</param>
        /// <param name="b">The second instance</param>
        /// <returns>Whether the 2 instances are not equal</returns>
        public static bool operator !=(Depot a, Depot b)
        {
            return !(a == b);
        }
    }
}