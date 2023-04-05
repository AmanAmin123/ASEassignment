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
