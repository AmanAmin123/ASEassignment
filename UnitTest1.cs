using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ShapesApp;
using System.Drawing;
namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
       
        [TestMethod]
        public void IsValidCommand_ValidMoveToCommand_ReturnsTrue()
        {
            // Arrange
            
             var graphics = Graphics.FromImage(new Bitmap(640, 480)); // Create a Graphics object
            var initialPosition = new Point(0, 0); // Create a Point object
            var parser = new CommandParser(graphics, initialPosition);
            var command = "moveTo 100 200";
          

            // Act
            var result = parser.IsValidCommand(command);

            // Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void IsValidCommand_InvalidRectangleCommand_ReturnsFalse()
        {

            // Arrange

            var graphics = Graphics.FromImage(new Bitmap(640, 480)); // Create a Graphics object
            var initialPosition = new Point(0, 0); // Create a Point object
            var parser = new CommandParser(graphics, initialPosition);
            var command = "rectangle abc 123";
          

            // Act
            var result = parser.IsValidCommand(command);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsValidCommand_InvalidCommand_ReturnsFalse()
        {
            // Arrange

            var graphics = Graphics.FromImage(new Bitmap(640, 480)); // Create a Graphics object
            var initialPosition = new Point(0, 0); // Create a Point object
            var parser = new CommandParser(graphics, initialPosition);
            var command = "doSomething 100";
            // Act
            var result = parser.IsValidCommand(command);
            // Assert
            Assert.IsFalse(result);
        }

    }
}
