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
        
        public delegate void startGame(string p1Agent, string p2Agent);
        startGame startGameFunc;

        src.Board board;

        public GUI(startGame start, ref src.Board b)
        {
            InitializeComponent();
            startGameFunc = start;
            board = b;
        }

        private void GUI_Load(object sender, EventArgs e)
        {
            whiteAgentDropDown.SelectedIndex = 0;
            consoleInput.KeyDown += consoleInputOnKeyDown;

            updateBoard();
        }

        public override void Refresh()
        {
            base.Refresh();
            updateBoard();
        }

        // Console I/O

        public void putString(string s)
        {
            consoleOutput.AppendText(s + "\r\n");
        }

        public string readString()
        {
           return lastInput;
        }

        private void consoleInputOnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lastInput = consoleInput.Text;
                consoleInput.Text = "";
                putString(lastInput);
            }
        }


        // Graphics interface

        public void updateBoard()
        {
            chessTextBox.Clear();

            src.Piece P;
            for (int i = 7; i >= 0; i--)
            {
                string tmp = "";
                for (uint j = 0; j < 8; j++)
                {

                    P = board.getPieceAt(j, (uint)i);
                    if (P == null)
                        tmp = tmp + "0   ";
                    else
                    {
                        if (P.getColour() == "white")
                            tmp = tmp + "W";
                        else
                            tmp = tmp + "B";

                        if (P is src.Pawn)
                            tmp = tmp + "p  ";
                        else if (P is src.Rook)
                            tmp = tmp + "r  ";
                        else if (P is src.Knight)
                            tmp = tmp + "kn ";
                        else if (P is src.Bishop)
                            tmp = tmp + "b  ";
                        else if (P is src.Queen)
                            tmp = tmp + "q  ";
                        else if (P is src.King)
                            tmp = tmp + "k  ";
                        else
                            tmp = tmp + "ERORR ERROR ERROR";

                    }
                }

                // Print one row
                chessTextBox.Text += tmp + "\r\n";
            }
        }

        // Buttons

        private void startButton_Click(object sender, EventArgs e)
        {
            startGameFunc(whiteAgentDropDown.SelectedText, whiteAgentDropDown.SelectedText);
        }
    }
}
