using System;
using Xunit;

namespace Steam.Tests
{
    public class AppTests
    {   
        /// <summary>
        /// Check to make sure conversions work back and forth
        /// </summary>
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(0xFFFFFF)]
        public void Conversions(uint value)
        {
            var appValue = (App)value;

            Assert.Equal(value, (uint)appValue);
            Assert.Equal(appValue, (App)value);
        }

        /// <summary>
        /// Check to make sure converting a value out of range throws an exception
        /// </summary>
        [Theory]
        [InlineData(0xFFFFFF + 1)]
        [InlineData(uint.MaxValue)]
        public void OutOfRangeCheck(uint outOfRangeValue)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => (App)outOfRangeValue);
        }

        [Theory]
        [InlineData(1, "1")]
        [InlineData(53, "53")]
        public void ValueToString(uint value, string expected)
        {
            var appValue = (App)value;
            Assert.Equal(expected, appValue.ToString());
        }

        /// <summary>
        /// Check comparison methods to make sure they work properly
        /// </summary>
        [Theory]
        [InlineData(1, 1, true)]
        [InlineData(0, 1, false)]
        public void Comparisons(uint a, uint b, bool areEqual)
        {
            var aApp = (App)a;
            var bApp = (App)b;

            Assert.Equal(areEqual, aApp.Equals(bApp));
            Assert.Equal(areEqual, bApp.Equals(aApp));

            Assert.Equal(areEqual, aApp == bApp);
            Assert.Equal(areEqual, bApp == aApp);

            Assert.Equal(areEqual, !(aApp != bApp));
            Assert.Equal(areEqual, !(bApp != aApp));
        }

        /// <summary>
        /// Check that comparing App objects with different objects compare properly.
        /// 
        /// Apps should compare properly with other Apps
        /// Apps should fail to compare with uints
        /// Apps should fail to compare to other types not Apps
        /// </summary>
        [Theory]
        [InlineData(1, 1, true)]
        [InlineData(0, 1, false)]
        public void ObjectComparisons(uint a, uint b, bool areEqual)
        {
            object boxAApp = (App)a;
            object boxBApp = (App)b;
            Assert.Equal(areEqual, boxAApp.Equals(boxBApp));

            object boxA = a;
            object boxB = b;

            Assert.NotEqual(boxA, boxAApp);
            Assert.NotEqual(boxB, boxBApp);

            Assert.False(boxAApp.Equals(null));
            Assert.False(boxBApp.Equals(null));
        }
    }
}