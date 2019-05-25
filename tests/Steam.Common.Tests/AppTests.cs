using Xunit;

namespace Steam.Tests
{
    public class AppTests
    {
        [Theory]
        [InlineData(1)]
        public void ConstructorSetsId(uint id)
        {
            App app = new App(id);
            Assert.Equal(id, app.Id);
        }

        [Theory]
        [InlineData(1)]
        public void GetHashCodeIsEqualToId(uint id)
        {
            App app = new App(id);
            Assert.Equal(id.GetHashCode(), app.GetHashCode());
        }

        [Theory]
        [InlineData(1)]
        public void ToStringIsEqualToId(uint id)
        {
            App app = new App(id);
            Assert.Equal(id.ToString(), app.ToString());
        }

        [Theory]
        [InlineData(1)]
        public void ImplicitConversionFromUint(uint id)
        {
            App app = id;
            Assert.Equal(id, app.Id);
        }

        [Theory]
        [InlineData(1)]
        public void ImplicitConversionFromApp(uint id)
        {
            App app = new App(id);
            uint uintApp = app;
            Assert.Equal(app.Id, uintApp);
        }

        [Theory]
        [InlineData(1, 1, false)]
        [InlineData(1, null, false)]
        [InlineData(1, true, false)]
        public void EqualToObject(uint id, object other, bool expectedResult)
        {
            App app = new App(id);
            Assert.Equal(app.Equals(other), expectedResult);
        }

        [Theory]
        [InlineData(1, 1, true)]
        [InlineData(1, 0, false)]
        public void EqualToAppObject(uint id, uint other, bool expectedResult)
        {
            App app = new App(id);
            App app2 = new App(other);
            Assert.Equal(app.Equals((object) app2), expectedResult);
        }

        [Theory]
        [InlineData(1, 1, true)]
        [InlineData(1, 0, false)]
        public void EqualityOperator(uint id, uint other, bool expectedResult)
        {
            App app = new App(id);
            App app2 = new App(other);
            Assert.Equal(app == app2, expectedResult);
        }

        [Theory]
        [InlineData(1, 1, false)]
        [InlineData(1, 0, true)]
        public void InequalityOperator(uint id, uint other, bool expectedResult)
        {
            App app = new App(id);
            App app2 = new App(other);
            Assert.Equal(app != app2, expectedResult);
        }
    }
}