﻿using System;
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
        private src.Board board;

        public delegate void startGame(string p1Agent, string p2Agent);
        startGame startGameFunc;

        public GUI(startGame start)
        {
            InitializeComponent();
            startGameFunc = start;
        }

        private void GUI_Load(object sender, EventArgs e)
        {
            whiteAgentDropDown.SelectedIndex = 0;
            blackAgentDropDown.SelectedIndex = 0;
            consoleInput.KeyDown += consoleInputOnKeyDown;
        }

        // Console I/O

        public void putString(string s)
        {
            consoleOutput.AppendText(s + "\r\n");
        }

        public void putPlayerTurn(bool whiteTurn)
        {
            string s1;
            string s2 = " players turn";
            if (whiteTurn)
                s1 = "White";
            else
                s1 = "Black";
            playerTurn.Text = s1 + s2;
        }

        public void putTurn(uint turn)
        {
            string s1 = "Turn: ";
            turnText.Text = s1 + turn.ToString();
        }

        public string readString()
        {
            string tmp = lastInput;
            lastInput = "";
            return tmp;
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

        public void updateBoard(src.Board board)
        {
            this.board = board;

            renderGUI();
        }

        public void renderGUI()
        {
            renderTextGUI();
            renderGraphicsGUI();
        }

        private void renderTextGUI()
        {
            chessTextBox.Clear();

            chessTextBox.Text = "    A   B   C   D   E   F   G   H\r\n";

            src.Piece P;
            for (int i = 7; i >= 0; i--)
            {
                string tmp = (i + 1) + "   ";
                for (uint j = 0; j < 8; j++)
                {

                    P = board.getPieceAt(j, (uint)i);
                    if (P == null)
                        tmp = tmp + ".   ";
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

            chessTextBox.Text += "\r\n\r\n";
            chessTextBox.Text += "    A   B   C   D   E   F   G   H\r\n";


            // Cover
            for (int i = 7; i >= 0; i--)
            {
                string tmp = (i + 1) + "   ";
                for (uint j = 0; j < 8; j++)
                {

                    uint wc = board.getSquareAt(j, (uint)i).getWhiteCover();
                    uint bc = board.getSquareAt(j, (uint)i).getBlackCover();

                    tmp += wc + "," + bc + " ";
                }

                // Print one row
                chessTextBox.Text += tmp + "\r\n";
            }
        }

        private void renderGraphicsGUI()
        {
            // Setup graphics objects
            Graphics g = drawingArea.CreateGraphics();
            
            // Calculate size parameters
            int squareWidth = drawingArea.Width / 9;
            int squareHeight = drawingArea.Height / 9;

            // Draw numbers and letters
            SolidBrush brushText = new SolidBrush(Color.Black);
            for (int i = 1; i <= 8; i++)
            {
                g.DrawString((char) (i + 'A' - 1) + "", new Font("Arial", 20), brushText, new Point((int)((i + 0.25) * squareWidth), (int) (squareHeight * 0.3)));
                g.DrawString((9 - i).ToString(), new Font("Arial", 20), brushText, new Point((int)(squareWidth * 0.3), (int)((i + 0.2) * squareHeight)));
            }

            // Draw squares
            SolidBrush brushWhite = new SolidBrush(Color.LightGray);
            SolidBrush brushBlack = new SolidBrush(Color.Gray);
            for (int y = 0; y <= 7; y++)
            {
                for (int x = 0; x <= 7; x++)
                {
                    int drawX = (x + 1) * squareWidth;
                    int drawY = (y + 1) * squareHeight;
                    Rectangle rect = new Rectangle(drawX, drawY, squareWidth, squareHeight);

                    // Draw square
                    if ((y + x) % 2 == 0)
                    {
                        // White
                        g.FillRectangle(brushWhite, rect);
                    }
                    else
                    {
                        // White
                        g.FillRectangle(brushBlack, rect);
                    }

                    src.Square square = board.getSquareAt((uint) x, (uint) (7 - y));
                    if (square.getPiece() != null)
                    {
                        // Draw image
                        g.DrawString(square.getPiece().getColour().Replace('w', 'W').Replace('b', 'B'),
                                     new Font("Arial", 10),
                                     brushText,
                                     new Point((int)(drawX + squareWidth * 0.2), (int)(drawY + squareHeight * 0.35)));
                        g.DrawString(square.getPiece().GetType().ToString().Substring(15),
                                     new Font("Arial", 10),
                                     brushText,
                                     new Point((int) (drawX + squareWidth * 0.2), (int) (drawY + squareHeight * 0.55)));
                    }
                }
            }
            
            brushWhite.Dispose();
            brushBlack.Dispose();
            g.Dispose();
        }

        // Buttons

        private void startButton_Click(object sender, EventArgs e)
        {
            startButton.Enabled = false;
            whiteAgentDropDown.Enabled = false;
            blackAgentDropDown.Enabled = false;
            consoleOutput.Clear();
            lastInput = "";
            consoleInput.Focus();

            startGameFunc(whiteAgentDropDown.SelectedText, whiteAgentDropDown.SelectedText);
        }

        private void onFormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void onPaint(object sender, PaintEventArgs e)
        {
            renderGUI();
        }
    }
}
