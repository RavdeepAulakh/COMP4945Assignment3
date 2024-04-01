using System;
using NuGet.ContentModel;

namespace Assignment3.Tests
{
    public class MyTests
    {
        [Fact]
        public void TestAddition()
        {
            // Arrange
            var calculator = new Calculator();

            // Act
            var result = calculator.Add(2, 3);

            // Assert
            Assert.Equal(5, result);
        }
    }
}

