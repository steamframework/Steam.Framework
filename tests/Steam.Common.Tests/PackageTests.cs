using Xunit;

namespace Steam.Tests
{
    public class PackageTests
    {
        [Theory]
        [InlineData(1)]
        public void ConstructorSetsId(uint id)
        {
            Package package = new Package(id);
            Assert.Equal(id, package.Value);
        }

        [Theory]
        [InlineData(1)]
        public void GetHashCodeIsEqualToId(uint id)
        {
            Package package = new Package(id);
            Assert.Equal(id.GetHashCode(), package.GetHashCode());
        }

        [Theory]
        [InlineData(1)]
        public void ToStringIsEqualToId(uint id)
        {
            Package package = new Package(id);
            Assert.Equal(id.ToString(), package.ToString());
        }

        [Theory]
        [InlineData(1)]
        public void ImplicitConversionFromUint(uint id)
        {
            Package package = id;
            Assert.Equal(id, package.Value);
        }

        [Theory]
        [InlineData(1)]
        public void ImplicitConversionFromPackage(uint id)
        {
            Package package = new Package(id);
            uint uintPackage = package;
            Assert.Equal(package.Value, uintPackage);
        }

        [Theory]
        [InlineData(1, 1, false)]
        [InlineData(1, null, false)]
        [InlineData(1, true, false)]
        public void EqualToObject(uint id, object other, bool expectedResult)
        {
            Package package = new Package(id);
            Assert.Equal(package.Equals(other), expectedResult);
        }

        [Theory]
        [InlineData(1, 1, true)]
        [InlineData(1, 0, false)]
        public void EqualToPackageObject(uint id, uint other, bool expectedResult)
        {
            Package package = new Package(id);
            Package package2 = new Package(other);
            Assert.Equal(package.Equals((object) package2), expectedResult);
        }

        [Theory]
        [InlineData(1, 1, true)]
        [InlineData(1, 0, false)]
        public void EqualityOperator(uint id, uint other, bool expectedResult)
        {
            Package package = new Package(id);
            Package package2 = new Package(other);
            Assert.Equal(package == package2, expectedResult);
        }

        [Theory]
        [InlineData(1, 1, false)]
        [InlineData(1, 0, true)]
        public void InequalityOperator(uint id, uint other, bool expectedResult)
        {
            Package package = new Package(id);
            Package package2 = new Package(other);
            Assert.Equal(package != package2, expectedResult);
        }
    }
}