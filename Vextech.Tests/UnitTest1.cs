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

            // Act
            var firstname = "Tim";
            var secondname = "Sommerstedt";
            var actual = firstname + " " + secondname;

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}