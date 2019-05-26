using Xunit;

namespace Steam.Tests
{
    public class DepotTests
    {
        [Theory]
        [InlineData(1)]
        public void ConstructorSetsId(uint id)
        {
            Depot depot = new Depot(id);
            Assert.Equal(id, depot.Value);
        }

        [Theory]
        [InlineData(1)]
        public void GetHashCodeIsEqualToId(uint id)
        {
            Depot depot = new Depot(id);
            Assert.Equal(id.GetHashCode(), depot.GetHashCode());
        }

        [Theory]
        [InlineData(1)]
        public void ToStringIsEqualToId(uint id)
        {
            Depot depot = new Depot(id);
            Assert.Equal(id.ToString(), depot.ToString());
        }

        [Theory]
        [InlineData(1)]
        public void ImplicitConversionFromUint(uint id)
        {
            Depot depot = id;
            Assert.Equal(id, depot.Value);
        }

        [Theory]
        [InlineData(1)]
        public void ImplicitConversionFromDepot(uint id)
        {
            Depot depot = new Depot(id);
            uint uintDepot = depot;
            Assert.Equal(depot.Value, uintDepot);
        }

        [Theory]
        [InlineData(1, 1, false)]
        [InlineData(1, null, false)]
        [InlineData(1, true, false)]
        public void EqualToObject(uint id, object other, bool expectedResult)
        {
            Depot depot = new Depot(id);
            Assert.Equal(depot.Equals(other), expectedResult);
        }

        [Theory]
        [InlineData(1, 1, true)]
        [InlineData(1, 0, false)]
        public void EqualToDepotObject(uint id, uint other, bool expectedResult)
        {
            Depot depot = new Depot(id);
            Depot depot2 = new Depot(other);
            Assert.Equal(depot.Equals((object) depot2), expectedResult);
        }

        [Theory]
        [InlineData(1, 1, true)]
        [InlineData(1, 0, false)]
        public void EqualityOperator(uint id, uint other, bool expectedResult)
        {
            Depot depot = new Depot(id);
            Depot depot2 = new Depot(other);
            Assert.Equal(depot == depot2, expectedResult);
        }

        [Theory]
        [InlineData(1, 1, false)]
        [InlineData(1, 0, true)]
        public void InequalityOperator(uint id, uint other, bool expectedResult)
        {
            Depot depot = new Depot(id);
            Depot depot2 = new Depot(other);
            Assert.Equal(depot != depot2, expectedResult);
        }
    }
}