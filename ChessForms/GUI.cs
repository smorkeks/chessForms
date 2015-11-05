using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessForms
{
    public partial class GUI : Form
    {
        private string lastInput = "";

        public GUI()
        {
            InitializeComponent();

        }

        private void GUI_Load(object sender, EventArgs e)
        {
            whiteAgentDropDown.SelectedIndex = 0;

        }

        public void putString(string s)
        {
            consoleOutput.Text += consoleInput.Text + "\n";
        }

        public string readString()
        {
            return lastInput;
        }
    }
}
