using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShapesApp
{
    public partial class Form1 : Form
    {
        private CommandParser commandParser;
        public Form1()
        {
            InitializeComponent();
            commandParser = new CommandParser(panel1.CreateGraphics(), new Point(0, 0));

        }

        private void btnRun_Click(object sender, EventArgs e)
        {

            var command = txtCommandLine.Text;

            commandParser.Execute(command);

            txtCommandLine.Text = "";
        }
