using Xunit;

namespace Vextech.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            // Arrange
            var expected = "Tim Sommerstedt";
            var firstname = "Tim";
            var secondname = "Sommerstedt";

            // Act
            var actual = firstname + " " + secondname;

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}