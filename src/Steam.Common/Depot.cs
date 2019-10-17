using System;

namespace Steam
{
    /// <summary>
    /// An ID value for a logical grouping of files which are all delivered to a customer as a single group, see https://partner.steamgames.com/doc/store/application/depots
    /// </summary>
    public readonly struct Depot : IEquatable<Depot>
    {
        private readonly uint _value;

        /// <summary>
        /// An invalid depot value
        /// </summary>
        public readonly static Depot Invalid = new Depot(0x0);

        /// <summary>
        /// Creates a new instance of <see cref="Depot"/>
        /// </summary>
        /// <param name="value">The ID value</param>
        public Depot(uint value)
        {
            _value = value;
        }

        /// <summary>
        /// Explicit <see cref="uint"/> to <see cref="Depot"/> conversion
        /// </summary>
        /// <param name="value">The depot ID to convert</param>
        /// <returns>The new <see cref="Depot"/></returns>
        public static explicit operator Depot(uint value)
        {
            return new Depot(value);
        }

        /// <summary>
        /// Explicit <see cref="Depot"/> to <see cref="uint"/> conversion
        /// </summary>
        /// <param name="depot">The depot to convert</param>
        /// <returns>The converted depot Id</returns>
        public static explicit operator uint(Depot depot)
        {
            return depot._value;
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
        /// Converts the <see cref="Depot"/> to a <see cref="string"/>
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
            return obj is Depot other && Equals(other);
        }

        /// <summary>
        /// Equality comparison between this instance and <paramref name="other"/>
        /// </summary>
        /// <param name="other">The object to compare to</param>
        /// <returns>Whether the 2 instances are equal</returns>
        public bool Equals(Depot other)
        {
            return _value == other._value;
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