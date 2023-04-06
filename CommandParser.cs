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
        // stores graphics and pen position information
        private Graphics graphics;
        private Point currentPosition;
        private Point initialPosition;

        // initializes graphics and pen position
        public CommandParser(Graphics graphics, Point initialPosition)
        {
            this.graphics = graphics;
            this.initialPosition = initialPosition;
            Reset();
        }

        public void Execute(string command)
        {
            try
            {
                var parts = command.Split(' ');
                var action = parts[0].ToLower();

                // switch statement to handle different command types
                switch (action)
                {
                    // move pen to a given position
                    case "moveto":
                        MoveTo(int.Parse(parts[1]), int.Parse(parts[2]));
                        break;
                    // draw a line from current position to a given position
                    case "drawto":
                        DrawTo(int.Parse(parts[1]), int.Parse(parts[2]));
                        break;
                    // clear the drawing area and reset pen position
                    case "clear":
                        Clear();
                        break;
                    // reset pen position to initial position
                    case "reset":
                        Reset();
                        break;
                    // draw a rectangle at current position with given dimensions
                    case "rectangle":
                        DrawRectangle(int.Parse(parts[1]), int.Parse(parts[2]));
                        break;
                    // draw a circle at current position with given radius
                    case "circle":

                        DrawCircle(int.Parse(parts[1]));
                        break;
                }
            }
            // catch block to handle invalid command format
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

        private void DrawRectangle(int width, int height)
        {
            // Draw a rectangle with the given width and height, starting from the current position
            graphics.DrawRectangle(Pens.Black, currentPosition.X, currentPosition.Y, width, height);
        }

        private void DrawCircle(int radius)
        {
            // Draw an ellipse with the given radius, centered at the current position
            graphics.DrawEllipse(Pens.Black, currentPosition.X - radius, currentPosition.Y - radius, radius * 2, radius * 2);
        }

    }

}
