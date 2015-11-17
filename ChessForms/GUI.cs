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

        src.Move selectedMove;
        /*int selectedSquareX = -1;
        int selectedSquareY = -1;
        int selectedMoveX = -1;
        int selectedMoveY = -1;*/

        public delegate void gameInterfaceFunc();
        gameInterfaceFunc startGameFunc;
        gameInterfaceFunc pauseGameFunc;
        gameInterfaceFunc resetGameFunc;
        gameInterfaceFunc saveGameFunc;
        gameInterfaceFunc loadGameFunc;

        ImageHandler imageHandler;

        int AIsearchDepth = 0;
        int AIsearchDepthMax = 0;

        public GUI(gameInterfaceFunc start, gameInterfaceFunc pause, gameInterfaceFunc reset, gameInterfaceFunc save, gameInterfaceFunc load)
        {
            InitializeComponent();
            startGameFunc = start;
            pauseGameFunc = pause;
            resetGameFunc = reset;
            saveGameFunc = save;
            loadGameFunc = load;
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

        public uint getWhiteAIDiff()
        {
            return (uint) whiteAiDiffTrackBar.Value;
        }

        public uint getBlackAIDiff()
        {
            return (uint)blackAiDiffTrackBar.Value;
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

        public string getFileName()
        {
            return fileNameTextBox.Text;
        }

        public string getWhitePlaybackFileName()
        {
            return whitePlaybackFilenameTextBox.Text;
        }

        public string getBlackPlaybackFileName()
        {
            return blackPlaybackFilenameTextBox.Text;
        }

        public int getWhitePlaybackSleepTime()
        {
            return whitePlaybackSleepTime.Value;
        }

        public int getBlackPlaybackSleepTime()
        {
            return blackPlaybackSleepTime.Value;
        }

        // Agent interface

        public string readString()
        {
            string tmp = lastInput;
            lastInput = "";
            return tmp;
        }

        public src.Move readSelectedMove()
        {
            src.Move tmp = selectedMove.Copy();

            // If correct move, reset selection
            if (!selectedMove.Illegal)
            {
                selectedMove = new src.Move();
            }

            return tmp;
        }
        
        public void putAiScore(int score)
        {
            if (score == 0)
            {
                AIsearchDepth = 0;
            } else
            {
                AIsearchDepth++;
            }
            AIsearchDepthMax = Math.Max(AIsearchDepthMax, AIsearchDepth);
            AiScoreTextBox.Text = "Max: " + AIsearchDepthMax + ", now: " + AIsearchDepth;
        }

        // Graphics interface

        public void updateBoard(src.Board board)
        {
            this.board = board;

            selectedMove = new src.Move();

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
            if (selectedMove.hasFrom())
            {
                src.Piece p = board.getPieceAt(selectedMove.FromX, selectedMove.FromY);
                if (p != null)
                {
                    int drawX = ((int) selectedMove.FromX + 1) * squareWidth;
                    int drawY = (7 - (int) selectedMove.FromY + 1) * squareHeight;

                    // Mark selected square
                    drawBorder(g, brushSelect, drawX, drawY, squareWidth, squareHeight, 5);

                    foreach (Tuple<uint, uint> t in ChessForms.rules.Rules.getPossibleMoves(board, p))
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
                fileNameTextBox.Enabled = false;
                whiteAgentDropDown.Enabled = false;
                blackAgentDropDown.Enabled = false;
                whiteAiDiffTrackBar.Enabled = false;
                blackAiDiffTrackBar.Enabled = false;
                whitePlaybackSleepTime.Enabled = false;
                blackPlaybackSleepTime.Enabled = false;

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
                fileNameTextBox.Enabled = true;
                whiteAgentDropDown.Enabled = true;
                blackAgentDropDown.Enabled = true;
                whiteAiDiffTrackBar.Enabled = true;
                blackAiDiffTrackBar.Enabled = true;
                whitePlaybackSleepTime.Enabled = true;
                blackPlaybackSleepTime.Enabled = true;

                // Reset search depth print
                AIsearchDepth = 0;
                AIsearchDepthMax = 0;
                putAiScore(0);

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
            if (!selectedMove.Illegal)
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

            if (newClickX == selectedMove.FromX && newClickY == selectedMove.FromY)
            {
                selectedMove = new src.Move();
            }
            else
            {
                // New square clicked
                List<Tuple<uint, uint>> moves;
                if (selectedMove.hasFrom())
                {
                    moves = ChessForms.rules.Rules.getPossibleMoves(board, board.getPieceAt(selectedMove.FromX, selectedMove.FromY));
                    //moves = board.getSquareAt((uint)selectedSquareX, (uint)selectedSquareY).getPiece().getPossibleMoves(board.getSquareAt, turn);
                }
                else
                {
                    moves = new List<Tuple<uint, uint>>();
                }

                // Check if part of possible moves.
                if (moves.Contains(new Tuple<uint, uint>((uint)newClickX, (uint)newClickY)))
                {
                    // Move selected
                    selectedMove.ToX = (uint) newClickX;
                    selectedMove.ToY = (uint) newClickY;
                    selectedMove.Illegal = false;
                }
                else
                {
                    // Move not selected, check if new piece clicked
                    src.Piece p = board.getPieceAt((uint)newClickX, (uint)newClickY);
                    if (p != null)
                    {
                        selectedMove.FromX = (uint) newClickX;
                        selectedMove.FromY = (uint) newClickY;
                    }
                    else
                    {
                        selectedMove = new src.Move();
                    }
                }
            }
            renderGraphicsGUI();

            //putString(selectedMove.FromX + ", " + selectedMove.FromY + ", " + selectedMove.ToX + ", " + selectedMove.ToY + ", " + selectedMove.Illegal);
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
                whiteAiDiffTrackBar.Enabled = true;
                blackAiDiffTrackBar.Enabled = true;
                whitePlaybackSleepTime.Enabled = true;
                blackPlaybackSleepTime.Enabled = true;

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
                whiteAiDiffTrackBar.Enabled = false;
                blackAiDiffTrackBar.Enabled = false;
                whitePlaybackSleepTime.Enabled = false;
                blackPlaybackSleepTime.Enabled = false;

                // Set button text
                pauseButton.Text = "Pause Game";

                // Unpause game
                pauseGameFunc();
            }
        }

        private void onBlackAgentChange(object sender, EventArgs e)
        {
            if (getBlackAgentType() == "AI")
            {
                blackAiDiffLabel.Visible = true;
                blackAiDiffTrackBar.Visible = true;
            }
            else
            {
                blackAiDiffLabel.Visible = false;
                blackAiDiffTrackBar.Visible = false;
            }

            if (getBlackAgentType() == "Playback Agent")
            {
                blackPlaybackFilenameTextBox.Visible = true;
                blackPlaybackSleepTime.Visible = true;
                blackSleepTimeLabel.Visible = true;
            }
            else
            {
                blackPlaybackFilenameTextBox.Visible = false;
                blackPlaybackSleepTime.Visible = false;
                blackSleepTimeLabel.Visible = false;
            }
        }

        private void onWhiteAgentChange(object sender, EventArgs e)
        {
            if (getWhiteAgentType() == "AI")
            {
                whiteAiDiffLabel.Visible = true;
                whiteAiDiffTrackBar.Visible = true;
            }
            else
            {
                whiteAiDiffLabel.Visible = false;
                whiteAiDiffTrackBar.Visible = false;
            }

            if (getWhiteAgentType() == "Playback Agent")
            {
                whitePlaybackFilenameTextBox.Visible = true;
                whitePlaybackSleepTime.Visible = true;
                whiteSleepTimeLabel.Visible = true;
            }
            else
            {
                whitePlaybackFilenameTextBox.Visible = false;
                whitePlaybackSleepTime.Visible = false;
                whiteSleepTimeLabel.Visible = false;
            }
        }

        private void onBlackAiDiffChange(object sender, EventArgs e)
        {
            string s = blackAiDiffLabel.Text;
            blackAiDiffLabel.Text = s.Substring(0, s.Length - 1) + getBlackAIDiff();
        }

        private void onWhiteAiDiffChange(object sender, EventArgs e)
        {
            string s = whiteAiDiffLabel.Text;
            whiteAiDiffLabel.Text = s.Substring(0, s.Length - 1) + getWhiteAIDiff();
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            saveGameFunc();
        }

        private void onWhiteSleepTimeChange(object sender, EventArgs e)
        {
            string s = whiteSleepTimeLabel.Text;
            whiteSleepTimeLabel.Text = s.Substring(0, s.IndexOf(':') + 1) + " " + getWhitePlaybackSleepTime();
        }

        private void onBlackSleepTimeChange(object sender, EventArgs e)
        {
            string s = blackSleepTimeLabel.Text;
            blackSleepTimeLabel.Text = s.Substring(0, s.IndexOf(':') + 1) + " " + getBlackPlaybackSleepTime();
        }

        private void loadClick(object sender, EventArgs e)
        {
            loadGameFunc();
        }
    }
}
