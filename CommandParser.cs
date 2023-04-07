using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShapesApp
{
    public class CommandParser
    {
        private Graphics graphics;
        private Point currentPosition;
        private Point initialPosition;
        private List<string> commandHistory = new List<string>(); // Define commandHistory as a class-level variable
     

        // Factory class for creating shapes
        private ShapeFactory shapeFactory;

        public CommandParser(Graphics graphics, Point initialPosition)
        {
            this.graphics = graphics;
            this.initialPosition = initialPosition;
            Reset();

            // Initialize the shape factory
            shapeFactory = new ShapeFactory();
        }

        public void Execute(string command)
        {
            try
            {
                var parts = command.Split(' ');
                var action = parts[0].ToLower();

               switch (action)
                {
                    case "moveto":
                        MoveTo(int.Parse(parts[1]), int.Parse(parts[2]));
                        break;
                    case "drawto":
                        DrawTo(int.Parse(parts[1]), int.Parse(parts[2]));
                        break;
                    case "clear":
                        Clear();
                        break;
                    case "reset":
                        Reset();
                        break;
                    case "rectangle":
                        // Create a rectangle shape using the factory
                        Shape rectangle = shapeFactory.CreateShape("rectangle", int.Parse(parts[1]), int.Parse(parts[2]));
                        rectangle.Draw(graphics, currentPosition);
                        break;
                    case "circle":
                        // Create a circle shape using the factory
                        Shape circle = shapeFactory.CreateShape("circle", int.Parse(parts[1]));
                        circle.Draw(graphics, currentPosition);
                        break;
                    case "loop":               
                    case "end":
                      
                        break;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Not a valid command");

            }
        }

        
        public bool IsValidCommand(string command)
        {
            var parts = command.Split(' ');

            // check if the command has at least one part
            if (parts.Length == 0) return false;

            var action = parts[0].ToLower();

            // switch statement to validate different command types
            switch (action)
            {
                // validate format of moveto and drawto commands
                case "moveto":
                case "drawto":
                    if (parts.Length != 3) return false;
                    if (!int.TryParse(parts[1], out int x) || !int.TryParse(parts[2], out int y)) return false;
                    break;

                // validate format of rectangle command
                case "rectangle":
                    if (parts.Length != 3) return false;
                    if (!int.TryParse(parts[1], out int width) || !int.TryParse(parts[2], out int height)) return false;
                    break;

                // validate format of circle command
                case "circle":
                    if (parts.Length != 2) return false;
                    if (!int.TryParse(parts[1], out int radius)) return false;
                    break;

                // validate format of clear and reset commands
                case "clear":
                case "reset":
                    if (parts.Length != 1) return false;
                    break;

                // invalid command type
                default:
                    return false;
            }

            return true;
        }
        // method to draw a line from current position to a given position
        private void DrawTo(int x, int y)
        {
            graphics.DrawLine(Pens.Black, currentPosition, new Point(x, y));
            currentPosition = new Point(x, y);
        }
        // method to move pen to a given position
        private void MoveTo(int x, int y)
        {
            currentPosition = new Point(x, y);
        }

        // method to draw a line from current position to a given position

        private void Clear()
        {
            // Clear the graphics context with a white color
            graphics.Clear(Color.White);

            // Reset the current position
            Reset();
        }

        private void Reset()
        {
            // Set the current position to the initial position
            currentPosition = initialPosition;
        }

        / Shape factory class
    public class ShapeFactory
    {
        public Shape CreateShape(string shapeType, params int[] dimensions)
        {
            switch (shapeType.ToLower())
            {
                case "rectangle":
                    return new Rectangle(dimensions[0], dimensions[1]);
                case "circle":
                    return new Circle(dimensions[0]);
                default:
                    throw new ArgumentException($"Invalid shape type: {shapeType}");
            }
        }
    }

    // Base class for all shapes
    public abstract class Shape
    {
        public abstract void Draw(Graphics graphics, Point position);
    }

    // Rectangle shape
    public class Rectangle : Shape
    {
        private int width;
        private int height;

        public Rectangle(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public override void Draw(Graphics graphics, Point position)
        {
            graphics.DrawRectangle(Pens.Black, position.X, position.Y, width, height);
        }
    }

    // Circle shape
    public class Circle : Shape
    {
        private int radius;

        public Circle(int radius)
        {
            this.radius = radius;
        }

        public override void Draw(Graphics graphics, Point position)
        {
            graphics.DrawEllipse(Pens.Black, position.X - radius, position.Y - radius, radius * 2, radius * 2);
        }
    }
