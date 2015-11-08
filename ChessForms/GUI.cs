using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChessForms.graphics;

namespace ChessForms
{
    public partial class GUI : Form
    {
        private string lastInput = "";
        private src.Board board;
        private uint turn = 0;

        int selectedSquareX = -1;
        int selectedSquareY = -1;
        int selectedMoveX = -1;
        int selectedMoveY = -1;

        public delegate void gameInterfaceFunc();
        gameInterfaceFunc startGameFunc;
        gameInterfaceFunc pauseGameFunc;
        gameInterfaceFunc resetGameFunc;

        ImageHandler imageHandler;

        public GUI(gameInterfaceFunc start, gameInterfaceFunc pause, gameInterfaceFunc reset)
        {
            InitializeComponent();
            startGameFunc = start;
            pauseGameFunc = pause;
            resetGameFunc = reset;
            imageHandler = new ImageHandler(AppDomain.CurrentDomain.BaseDirectory + "../../resources/", "png");
        }

        private void GUI_Load(object sender, EventArgs e)
        {
            whiteAgentDropDown.SelectedIndex = 0;
            blackAgentDropDown.SelectedIndex = 0;
            consoleInput.KeyDown += consoleInputOnKeyDown;
        }

        // GUI I/O

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
            this.turn = turn;
            string s1 = "Turn: ";
            turnText.Text = s1 + turn.ToString();
        }

        public void putScore(int score)
        {
            string s1 = "White leading by: ";
            scoreBoard.Text = s1 + score.ToString();
        }

        public string getWhiteAgentType()
        {
            return whiteAgentDropDown.SelectedItem.ToString();
        }

        public string getBlackAgentType()
        {
            return blackAgentDropDown.SelectedItem.ToString();
        }

        public bool getPlaybackEnabled()
        {
            return playbackCheckBox.Checked;
        }

        public void gameOver(bool whiteWon, bool remi)
        {
            if (remi)
            {
                putString("Remi!");
            }
            else if (whiteWon)
            {
                putString("White player won!");
            }
            else
            {
                putString("Black player won!");
            }

            pauseButton.Enabled = false;
        }

        // Agent interface

        public string readString()
        {
            string tmp = lastInput;
            lastInput = "";
            return tmp;
        }

        public Tuple<int, int, int, int> readSelectedMove()
        {
            Tuple<int, int, int, int> tmp = new Tuple<int, int, int, int>(selectedSquareX,
                                                                        selectedSquareY,
                                                                        selectedMoveX,
                                                                        selectedMoveY);
            // If correct move, reset selection
            if (selectedMoveX != -1 && selectedMoveY != -1)
            {
                selectedMoveX = -1;
                selectedMoveY = -1;
                selectedSquareX = -1;
                selectedSquareY = -1;
            }

            return tmp;
        }

        public void putAiScore(int score)
        {
            AiScoreTextBox.Text = "" + score;
        }

        // Graphics interface

        public void updateBoard(src.Board board)
        {
            this.board = board;

            selectedSquareX = -1;
            selectedSquareY = -1;

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
                g.DrawString((char)(i + 'A' - 1) + "", new Font("Arial", 20), brushText, new Point((int)((i + 0.25) * squareWidth), (int)(squareHeight * 0.3)));
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

                    src.Square square = board.getSquareAt((uint)x, (uint)(7 - y));
                    if (square.getPiece() != null)
                    {
                        // Draw piece
                        /*g.DrawString(square.getPiece().getColour().Replace('w', 'W').Replace('b', 'B'),
                                     new Font("Arial", 10),
                                     brushText,
                                     new Point((int)(drawX + squareWidth * 0.2), (int)(drawY + squareHeight * 0.35)));
                        g.DrawString(square.getPiece().GetType().ToString().Substring(15),
                                     new Font("Arial", 10),
                                     brushText,
                                     new Point((int)(drawX + squareWidth * 0.2), (int)(drawY + squareHeight * 0.55)));*/
                        src.Piece p = square.getPiece();
                        string type = p.GetType().Name;
                        string colour = p.getColour().Replace('w', 'W').Replace('b', 'B');
                        string filename = type + "_" + colour;
                        g.DrawImage(imageHandler.getImage(filename),
                                    new Point[] {new Point(drawX + 8, drawY + 8),
                                                 new Point(drawX + squareWidth - 8, drawY + 8),
                                                 new Point(drawX + 8, drawY + squareHeight - 8)});
                    }
                }
            }

            // Selection, moves and cover
            SolidBrush brushSelect = new SolidBrush(Color.Green);
            SolidBrush brushMoves = new SolidBrush(Color.Red);
            if (selectedSquareX != -1 && selectedSquareY != -1)
            {
                src.Square s = board.getSquareAt((uint)selectedSquareX, (uint)selectedSquareY);
                if (s.getPiece() != null)
                {
                    int drawX = (selectedSquareX + 1) * squareWidth;
                    int drawY = (7 - selectedSquareY + 1) * squareHeight;

                    // Mark selected square
                    drawBorder(g, brushSelect, drawX, drawY, squareWidth, squareHeight, 5);

                    foreach (Tuple<uint, uint> t in s.getPiece().getPossibleMoves(board.getSquareAt, turn))
                    {
                        drawX = (int)(t.Item1 + 1) * squareWidth;
                        drawY = (int)(7 - t.Item2 + 1) * squareHeight;
                        drawBorder(g, brushMoves, drawX, drawY, squareWidth, squareHeight, 5);
                    }
                }
            }

            brushWhite.Dispose();
            brushBlack.Dispose();
            g.Dispose();
        }

        private void drawBorder(Graphics g, SolidBrush b, int x, int y, int w, int h, int t)
        {
            g.FillRectangle(b, x, y, w, t);
            g.FillRectangle(b, x, y, t, h);
            g.FillRectangle(b, x, y + h - t, w, t);
            g.FillRectangle(b, x + w - t, y, t, h);
        }
        
        // Events

        private void startButton_Click(object sender, EventArgs e)
        {
            if (startButton.Text == "Start Game")
            {
                // Enable and disable controls
                pauseButton.Enabled = true;
                loadButton.Enabled = false;
                saveButton.Enabled = false;
                playbackCheckBox.Enabled = false;
                whiteAgentDropDown.Enabled = false;
                blackAgentDropDown.Enabled = false;

                // Clear console
                consoleOutput.Clear();
                lastInput = "";

                if (getWhiteAgentType() == "Terminal Agent" ||
                    getBlackAgentType() == "Terminal Agent")
                {
                    consoleInput.Focus();
                    consoleInput.Enabled = true;
                }

                // Change text of this button
                startButton.Text = "Reset Game";

                // Start new game
                startGameFunc();
            }
            else
            {
                // Enable and disable controls
                pauseButton.Enabled = false;
                loadButton.Enabled = true;
                saveButton.Enabled = true;
                playbackCheckBox.Enabled = true;
                whiteAgentDropDown.Enabled = true;
                blackAgentDropDown.Enabled = true;
                
                // Clear console
                consoleOutput.Clear();
                lastInput = "";
                consoleInput.Enabled = false;
                
                // Change text of this button
                startButton.Text = "Start Game";

                // Reset game
                resetGameFunc();
            }
        }

        private void onFormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void onPaint(object sender, PaintEventArgs e)
        {
            renderGraphicsGUI();
        }

        private void onDrawingClick(object sender, MouseEventArgs e)
        {
            if (selectedMoveX != -1 && selectedMoveY != -1)
            {
                // A move has already been selected.
                return;
            }

            int newClickX = e.X / (drawingArea.Width / 9) - 1;
            int newClickY = 7 - (e.Y / (drawingArea.Height / 9) - 1);

            // Clicked outside of the board
            if (newClickX == -1 || newClickY == 8)
            {
                return;
            }

            if (newClickX == selectedSquareX && newClickY == selectedSquareY)
            {
                selectedSquareX = -1;
                selectedSquareY = -1;
            }
            else
            {
                List<Tuple<uint, uint>> moves;
                if (selectedSquareX != -1 && selectedSquareY != -1)
                {
                    moves = board.getSquareAt((uint)selectedSquareX, (uint)selectedSquareY).getPiece().getPossibleMoves(board.getSquareAt, turn);
                }
                else
                {
                    moves = new List<Tuple<uint, uint>>();
                }

                // Check if part of possible moves.
                if (moves.Contains(new Tuple<uint, uint>((uint)newClickX, (uint)newClickY)))
                {
                    // Move selected
                    selectedMoveX = newClickX;
                    selectedMoveY = newClickY;
                }
                else
                {
                    // Move not selected, check if new piece clicked
                    src.Piece p = board.getSquareAt((uint)newClickX, (uint)newClickY).getPiece();
                    if (p != null)
                    {
                        selectedSquareX = newClickX;
                        selectedSquareY = newClickY;
                    }
                    else
                    {
                        selectedSquareX = -1;
                        selectedSquareY = -1;
                    }
                }
            }
            renderGraphicsGUI();

            //putString(selectedSquareX + ", " + selectedSquareY);
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

        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (pauseButton.Text == "Pause Game")
            {
                // Enable and disable controls
                saveButton.Enabled = true;
                loadButton.Enabled = true;
                whiteAgentDropDown.Enabled = true;
                blackAgentDropDown.Enabled = true;

                // Set button text
                pauseButton.Text = "Unpause Game";

                // Pause game
                pauseGameFunc();
            }
            else
            {
                // Enable and disable controls
                saveButton.Enabled = false;
                loadButton.Enabled = false;
                whiteAgentDropDown.Enabled = false;
                blackAgentDropDown.Enabled = false;

                // Set button text
                pauseButton.Text = "Pause Game";

                // Unpause game
                pauseGameFunc();
            }
        }
    }
}
