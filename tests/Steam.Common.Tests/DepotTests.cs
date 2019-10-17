using Xunit;

namespace Steam.Tests
{
    public class DepotTests
    {
        /// <summary>
        /// Check to make sure conversions work back and forth
        /// </summary>
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(uint.MaxValue)]
        public void Conversions(uint value)
        {
            var depotValue = (Depot)value;

            Assert.Equal(value, (uint)depotValue);
            Assert.Equal(depotValue, (Depot)value);
        }

        [Theory]
        [InlineData(1, "1")]
        [InlineData(53, "53")]
        public void ValueToString(uint value, string expected)
        {
            var depotValue = (Depot)value;
            Assert.Equal(expected, depotValue.ToString());
        }

        /// <summary>
        /// Check comparison methods to make sure they work properly
        /// </summary>
        [Theory]
        [InlineData(1, 1, true)]
        [InlineData(0, 1, false)]
        public void Comparisons(uint a, uint b, bool areEqual)
        {
            var aDepot = (Depot)a;
            var bDepot = (Depot)b;

            Assert.Equal(areEqual, aDepot.Equals(bDepot));
            Assert.Equal(areEqual, bDepot.Equals(aDepot));

            Assert.Equal(areEqual, aDepot == bDepot);
            Assert.Equal(areEqual, bDepot == aDepot);

            Assert.Equal(areEqual, !(aDepot != bDepot));
            Assert.Equal(areEqual, !(bDepot != aDepot));
        }

        /// <summary>
        /// Check that comparing Depot objects with different objects compare properly.
        /// 
        /// Depots should compare properly with other Depots
        /// Depots should fail to compare with uints
        /// Depots should fail to compare to other types not Depots
        /// </summary>
        [Theory]
        [InlineData(1, 1, true)]
        [InlineData(0, 1, false)]
        public void ObjectComparisons(uint a, uint b, bool areEqual)
        {
            object boxADepot = (Depot)a;
            object boxBDepot = (Depot)b;
            Assert.Equal(areEqual, boxADepot.Equals(boxBDepot));

            object boxA = a;
            object boxB = b;

            Assert.NotEqual(boxA, boxADepot);
            Assert.NotEqual(boxB, boxBDepot);

            Assert.False(boxADepot.Equals(null));
            Assert.False(boxBDepot.Equals(null));
        }
    }
}