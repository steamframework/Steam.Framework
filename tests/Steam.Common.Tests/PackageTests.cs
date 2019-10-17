using Xunit;

namespace Steam.Tests
{
    public class PackageTests
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
            var packageValue = (Package)value;

            Assert.Equal(value, (uint)packageValue);
            Assert.Equal(packageValue, (Package)value);
            Assert.Equal(packageValue, new Package(value));
        }

        [Theory]
        [InlineData(1, "1")]
        [InlineData(53, "53")]
        public void ValueToString(uint value, string expected)
        {
            var packageValue = (Package)value;
            Assert.Equal(expected, packageValue.ToString());
        }

        /// <summary>
        /// Check comparison methods to make sure they work properly
        /// </summary>
        [Theory]
        [InlineData(1, 1, true)]
        [InlineData(0, 1, false)]
        public void Comparisons(uint a, uint b, bool areEqual)
        {
            var aPackage = (Package)a;
            var bPackage = (Package)b;

            Assert.Equal(areEqual, aPackage.Equals(bPackage));
            Assert.Equal(areEqual, bPackage.Equals(aPackage));

            Assert.Equal(areEqual, aPackage == bPackage);
            Assert.Equal(areEqual, bPackage == aPackage);

            Assert.Equal(areEqual, !(aPackage != bPackage));
            Assert.Equal(areEqual, !(bPackage != aPackage));
        }

        /// <summary>
        /// Check that comparing Package objects with different objects compare properly.
        /// 
        /// Packages should compare properly with other Packages
        /// Packages should fail to compare with uints
        /// Packages should fail to compare to other types not Packages
        /// </summary>
        [Theory]
        [InlineData(1, 1, true)]
        [InlineData(0, 1, false)]
        public void ObjectComparisons(uint a, uint b, bool areEqual)
        {
            object boxAPackage = (Package)a;
            object boxBPackage = (Package)b;
            Assert.Equal(areEqual, boxAPackage.Equals(boxBPackage));

            object boxA = a;
            object boxB = b;

            Assert.NotEqual(boxA, boxAPackage);
            Assert.NotEqual(boxB, boxBPackage);

            Assert.False(boxAPackage.Equals(null));
            Assert.False(boxBPackage.Equals(null));
        }
    }
}