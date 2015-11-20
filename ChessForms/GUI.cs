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
        private src.Move selectedMove;

        // Functions that the GUI can call in Game to notify it of events.
        public delegate void gameInterfaceFunc();
        gameInterfaceFunc startGameFunc;
        gameInterfaceFunc pauseGameFunc;
        gameInterfaceFunc resetGameFunc;
        gameInterfaceFunc saveGameFunc;
        gameInterfaceFunc loadGameFunc;

        // Used to show the number of MinMax calls done in each search.
        int AIsearchDepth = 0;
        int AIsearchDepthMax = 0;

        // Custom control with double buffering used for drawing the board.
        DrawControl drawControl;

        public GUI(gameInterfaceFunc start, gameInterfaceFunc pause, gameInterfaceFunc reset, gameInterfaceFunc save, gameInterfaceFunc load)
        {
            InitializeComponent();

            // Delegate functions from Game, called when different buttons are pressed.
            startGameFunc = start;
            pauseGameFunc = pause;
            resetGameFunc = reset;
            saveGameFunc = save;
            loadGameFunc = load;
            
            // Custom control for drawing the chess board
            // Use a lambda expression that allows access to selectedMove.
            drawControl = new DrawControl( () => { return selectedMove; });
        }

        // Event raised when the window is loaded
        private void GUI_Load(object sender, EventArgs e)
        {
            // Select Graphics agents in the player selections,
            // becuase they are usualy what we want.
            whiteAgentDropDown.SelectedIndex = 1;
            blackAgentDropDown.SelectedIndex = 1;

            // Enable entering text commands with the "Enter" key.
            consoleInput.KeyDown += consoleInputOnKeyDown;

            // Initialize the custom draw control,
            // enable mouse click events, and add it to tab page 2.
            this.drawControl.Location = new System.Drawing.Point(7, 7);
            this.drawControl.Name = "drawControl";
            this.drawControl.Size = new System.Drawing.Size(520, 520);
            this.drawControl.TabIndex = 0;
            this.drawControl.TabStop = false;
            this.drawControl.MouseClick += new System.Windows.Forms.MouseEventHandler(this.onDrawingClick);
            this.drawControl.Visible = true;
            this.tabPage2.Controls.Add(drawControl);
        }

        // --- GUI I/O ---
        // Functions that different parts of the program can call to get or set information
        // in the different components in the GUI.

        // Print a string in the console output window, end with new line.
        public void putString(string s)
        {
            consoleOutput.AppendText(s + "\r\n");
        }

        // Set the text in the text-box indicating whose turn it is.
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

        // Set the text in the text-box indicating which turn it is.
        public void putTurn(uint turn)
        {
            this.turn = turn;
            string s1 = "Turn: ";
            turnText.Text = s1 + turn.ToString();
        }

        // Set the text in the text-box indicating which player is in the lead and by how much.
        public void putScore(int score)
        {
            if (score >= 0)
            {
                string s1 = "White leading by: ";
                scoreBoard.Text = s1 + score.ToString();
            } else
            {
                string s1 = "Black leading by: ";
                scoreBoard.Text = s1 + (-score).ToString();
            }

        }

        // Get player type for white.
        public string getWhiteAgentType()
        {
            return whiteAgentDropDown.SelectedItem.ToString();
        }

        // Get player type for black.
        public string getBlackAgentType()
        {
            return blackAgentDropDown.SelectedItem.ToString();
        }

        // Get AI difficulty for white.
        public uint getWhiteAIDiff()
        {
            return (uint) whiteAiDiffTrackBar.Value;
        }

        // Get AI difficulty for black.
        public uint getBlackAIDiff()
        {
            return (uint)blackAiDiffTrackBar.Value;
        }

        // Returns true if all moves should be saved to a playback file that the Playback Agent can read.
        public bool getPlaybackEnabled()
        {
            return playbackCheckBox.Checked;
        }

        // Dissable the pause button and print the relevant Game Over message.
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

        // Get the file name typed in the file name text box.
        public string getFileName()
        {
            return fileNameTextBox.Text;
        }

        // Get the file name for the white player Playback Agent.
        public string getWhitePlaybackFileName()
        {
            return whitePlaybackFilenameTextBox.Text;
        }

        // Get the file name for the black player Playback Agent.
        public string getBlackPlaybackFileName()
        {
            return blackPlaybackFilenameTextBox.Text;
        }

        // Get the sleep time for the white player Playback Agent.
        public int getWhitePlaybackSleepTime()
        {
            return whitePlaybackSleepTime.Value;
        }

        // Get the sleep time for the white player Playback Agent.
        public int getBlackPlaybackSleepTime()
        {
            return blackPlaybackSleepTime.Value;
        }

        // --- Agent interface ---
        // Called by different agents to get relevant information about selected moves.
        // The selected moves are stored after the relevant Events happen, and will
        // be reset when read. This is to prevent the agents from executing a move several times.

        // Reads and resets the last string entered in the console input box.
        public string readString()
        {
            string tmp = lastInput;
            lastInput = "";
            return tmp;
        }

        // Returns and resets the last move selected via click input.
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
        
        // Updates the text in the AI score text box.
        // This function has mostly been used for debug purposes, but the current
        // use is to print the nuber of searched moves in the MinMax algorithm,
        // which is very interesting to look at, so we decided to keep this.
        public void putAiScore(int score)
        {
            // If the input is 0, reset the current search number.
            // If the input is 1, increment the current search number.
            // Never reset the maximum search from this function, only from reset event.
            // We put the increment and max calculations here instead of the MinMax to avoid
            // sending unnecessary information in the search, to speed it up.
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

        // --- Graphics interface ---
        // Functions for printing the text and image versions of the board.

        // Update the board and render the GUI. Also reset the selected move.
        public void updateBoard(src.Board board)
        {
            this.board = board;
            drawControl.setBoard(board);

            selectedMove = new src.Move();

            renderGUI();
        }

        // Render both the text and image GUI.
        public void renderGUI()
        {
            renderTextGUI();
            renderGraphicsGUI();
        }

        // Print a text version of the board state in tab 1 of the tab control.
        private void renderTextGUI()
        {
            // Clear all text
            chessTextBox.Clear();

            // Start by printing the column names
            chessTextBox.Text = "    A   B   C   D   E   F   G   H\r\n";

            // Print the board.
            src.Piece P;
            for (int i = 7; i >= 0; i--)
            {
                // Create string for a single row of the board, and the row number.
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

            // Print column names again
            chessTextBox.Text += "\r\n\r\n";
            chessTextBox.Text += "    A   B   C   D   E   F   G   H\r\n";

            // Print cover
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

        // Render the image based GUI.
        private void renderGraphicsGUI()
        {
            // Simply invalidate the custom drawing control to force repaint.
            drawControl.Invalidate();
        }

        // --- Events ---
        // Event functions, called by the OS (?) when the user interacts with the program.

        // Called when the "start game" button is clicked.
        // Toggles between "start" and "reset" game.
        // Sets the state of the GUI by enabling and disabling different components.
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

                // Auto focus console input box if there is a Terminal Agent in use.
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

                // Change text of pause button
                pauseButton.Text = "Pause Game";

                // Reset game
                resetGameFunc();
            }
        }

        // Called when the window is closed.
        private void onFormClosing(object sender, FormClosingEventArgs e)
        {
            // Close the application when the window is closed.
            Application.Exit();
        }

        // Called when the window needs to be repainted.
        private void onPaint(object sender, PaintEventArgs e)
        {
            // Render the image based GUI.
            renderGraphicsGUI();
        }

        // Called when the custom Drawing Control is clicked.
        // Will calculate the square clicked and save this move for Graphics Agents to use in the future.
        private void onDrawingClick(object sender, MouseEventArgs e)
        {
            if (!selectedMove.Illegal)
            {
                // A move has already been selected.
                // Wait for a Graphics Agent to read the selected move before accepting new clicks in the window.
                return;
            }

            // Calculate the square coordinates of the click.
            // TODO: use Board size.
            int newClickX = e.X / (drawControl.Width / 9) - 1;
            int newClickY = 7 - (e.Y / (drawControl.Height / 9) - 1);

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
        }

        // Called when a key is pressed and the console input has focus.
        // Filters for Enter key presses.
        private void consoleInputOnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                lastInput = consoleInput.Text;
                consoleInput.Text = "";
                putString(lastInput);
            }
        }

        // Called when the pause/unpause button is clicked.
        // Toggles the state of the GUI and notifies Game that the game
        // should be paused when possible.
        private void pauseButton_Click(object sender, EventArgs e)
        {
            if (pauseButton.Text == "Pause Game")
            {
                // Enable and disable controls
                saveButton.Enabled = true;
                loadButton.Enabled = true;
                fileNameTextBox.Enabled = true;
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
                fileNameTextBox.Enabled = false;
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

        // Called when the drop down menu for the black player Agent selection is changed.
        // Updates the GUI to only have the controls that are relevant for the selected
        // agent type.
        private void onBlackAgentChange(object sender, EventArgs e)
        {
            // AI, add (or remove) difficulty slider.
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

            // Playback Agent, add (or remove) file name box and speed slider.
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

        // Called when the drop down menu for the white player Agent selection is changed.
        // Updates the GUI to only have the controls that are relevant for the selected
        // agent type.
        private void onWhiteAgentChange(object sender, EventArgs e)
        {
            // AI, add (or remove) difficulty slider.
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

            // Playback Agent, add (or remove) file name box and speed slider.
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

        // Called when the Difficulty slider for the black AI is changed.
        // Updates the Difficulty label to show selected difficulty.
        private void onBlackAiDiffChange(object sender, EventArgs e)
        {
            string s = blackAiDiffLabel.Text;
            blackAiDiffLabel.Text = s.Substring(0, s.Length - 1) + getBlackAIDiff();
        }

        // Called when the Difficulty slider for the white AI is changed.
        // Updates the Difficulty label to show selected difficulty.
        private void onWhiteAiDiffChange(object sender, EventArgs e)
        {
            string s = whiteAiDiffLabel.Text;
            whiteAiDiffLabel.Text = s.Substring(0, s.Length - 1) + getWhiteAIDiff();
        }

        // Called when the "Save Game" button is clicked.
        private void saveButton_Click(object sender, EventArgs e)
        {
            // Notify Game
            saveGameFunc();
        }

        // Called when the sleep time slider for the white Playback Agent is changed.
        // Updates the associated label with the new sleep time.
        private void onWhiteSleepTimeChange(object sender, EventArgs e)
        {
            string s = whiteSleepTimeLabel.Text;
            whiteSleepTimeLabel.Text = s.Substring(0, s.IndexOf(':') + 1) + " " + getWhitePlaybackSleepTime();
        }

        // Called when the sleep time slider for the black Playback Agent is changed.
        // Updates the associated label with the new sleep time.
        private void onBlackSleepTimeChange(object sender, EventArgs e)
        {
            string s = blackSleepTimeLabel.Text;
            blackSleepTimeLabel.Text = s.Substring(0, s.IndexOf(':') + 1) + " " + getBlackPlaybackSleepTime();
        }

        // Called when the "Load Game" button is clicked.
        private void loadClick(object sender, EventArgs e)
        {
            // Notify Game
            loadGameFunc();
        }
    }
}
