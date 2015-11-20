using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using ChessForms.rules;
using ChessForms.file;
using System.IO;

namespace ChessForms.src
{
    // Contains the gameloop
    public class Game
    {
        // Fields
        FileMonitor fileMonitor;
        Board board;
        Agent white;
        Agent black;
        GUI gui;
        private bool turnWhite;

        private bool running = false;
        private bool paused = false;


        // Constructor for Game
        public Game()
        {
            board = new Board();
            fileMonitor = new FileMonitor();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            gui = new ChessForms.GUI(start, pauseUnpause, reset, saveGame, loadGame);

            // Load last game
            bool ok = SaveManager.loadCurrent(ref board);
            if (ok)
            {
                // Set GUI and other stuff
                gui.putString("Loading last game");
                updateOnLoad();
            }
            else
            {
                reset();
            }

            gui.updateBoard(board);
            Application.Run(gui);
        }

        // --- Control functions ---

        // Start a new game
        public void start()
        {
            // Set white agent
            switch (gui.getWhiteAgentType())
            {
                case "Terminal Agent":
                    white = new TerminalAgent("white", gui.readString);
                    break;
                case "Graphics Agent":
                    white = new GraphicsAgent("white", gui.readSelectedMove);
                    break;
                case "AI":
                    white = new AiAgent("white", gui.getWhiteAIDiff(), gui.putAiScore);
                    break;
                case "Playback Agent":
                    white = new PlaybackAgent("white", gui.getWhitePlaybackFileName(), gui.getWhitePlaybackSleepTime());
                    break;
            }

            // Set black agent
            switch (gui.getBlackAgentType())
            {
                case "Terminal Agent":
                    black = new TerminalAgent("black", gui.readString);
                    break;
                case "Graphics Agent":
                    black = new GraphicsAgent("black", gui.readSelectedMove);
                    break;
                case "AI":
                    black = new AiAgent("black", gui.getBlackAIDiff(), gui.putAiScore);
                    break;
                case "Playback Agent":
                    black = new PlaybackAgent("black", gui.getBlackPlaybackFileName(), gui.getBlackPlaybackSleepTime());
                    break;
            }

            // Start main loop
            running = true;
            run();
        }

        // Reset the game
        public void reset()
        {
            // Stop the game
            running = false;
            paused = false;

            // Create a new board and update the GUI
            board = new Board();
            gui.updateBoard(board);

            // White allways starts
            turnWhite = true;

            // Update GUI fields
            gui.putPlayerTurn(turnWhite);
            gui.putTurn(board.getTurn());
            gui.putScore(0);
        }

        // Pause or unpause the game
        public void pauseUnpause()
        {
            paused = !paused;
            if (!paused)
            {
                // Change white agent if necessary
                switch (gui.getWhiteAgentType())
                {
                    case "Terminal Agent":
                        if (!(white is TerminalAgent))
                            white = new TerminalAgent("white", gui.readString);
                        break;
                    case "Graphics Agent":
                        if (!(white is GraphicsAgent))
                            white = new GraphicsAgent("white", gui.readSelectedMove);
                        break;
                    case "AI":
                        if (!(white is AiAgent && ((AiAgent)white).getDifficulty() == gui.getWhiteAIDiff()))
                            white = new AiAgent("white", gui.getWhiteAIDiff(), gui.putAiScore);
                        break;
                    case "PlaybackAgent":
                        int time = gui.getWhitePlaybackSleepTime();
                        if (!(white is PlaybackAgent && ((PlaybackAgent)white).getSleepTime() == time))
                            white = new PlaybackAgent("white", gui.getWhitePlaybackFileName(), time);
                        break;
                }

                // Set black agent
                switch (gui.getBlackAgentType())
                {
                    case "Terminal Agent":
                        if (!(black is TerminalAgent))
                            black = new TerminalAgent("black", gui.readString);
                        break;
                    case "Graphics Agent":
                        if (!(black is TerminalAgent))
                            black = new GraphicsAgent("black", gui.readSelectedMove);
                        break;
                    case "AI":
                        if (!(black is AiAgent && ((AiAgent)black).getDifficulty() == gui.getBlackAIDiff()))
                            black = new AiAgent("black", gui.getBlackAIDiff(), gui.putAiScore);
                        break;
                    case "Playback Agent":
                        int time = gui.getBlackPlaybackSleepTime();
                        if (!(black is PlaybackAgent && ((PlaybackAgent)black).getSleepTime() == time))
                            black = new PlaybackAgent("black", gui.getBlackPlaybackFileName(), time);
                        break;
                }
            }
        }

        // Save the game state to a file
        public void saveGame()
        {
            string name = gui.getFileName();
            if (!name.Equals(""))
            {
                SaveManager.saveState(board, name);
            }
        }

        // Load the game state from a file
        public void loadGame()
        {
            string name = gui.getFileName();
            if (!name.Equals(""))
            {
                bool ok = SaveManager.loadState(ref board, name);
                if (ok)
                {
                    updateOnLoad();
                }
            }
        }

        // --- The main game loop ---

        // The game is powered by an infinite loop that works as this:
        // 1) Get move from active player.
        // 2) Check validity of move, and make move if valid.
        // 3) Allow the GUI to run all events as to not freeze the program.
        // 4) If the move was legal:
        //    * Save move if playback saving is enabled.
        //    * Update the GUI with the new board state.
        //    * Check if the game is over, either by a win or remi.
        //    * Update the GUI with new turn, score, etc.
        //    * Save the current state to a file.
        // 5) Check if the current state file has been manualy changed.
        // 6) Allow the GUI to run all events (again) as to not freeze the program.
        //
        // The reason we use a loop to control the game instead of a compleatly event
        // driven design is the we do not want the game to know the differens between
        // a human player and an AI (which is a requirement of the program).
        // In an event driven design the AI would either have to be activated be a
        // human player each turn, or be activated by a very fast timer event, or the
        // AI would have to run in a different thread. The first two options are not
        // very nice, and the last would prevent the AI from interacting with the GUI,
        // because WinForms controls can only be accessed from the thread that created them.
        // With our loop solutions, none of these issues exist, which is why we feel it is
        // the best option.
        public void run()
        {
            Move selectedMove = null;
            bool oldTurnWhite = turnWhite;

            while (running)
            {
                if (paused)
                {
                    // Run the program at 10 fps if it is paused.
                    Thread.Sleep(100);
                }
                else
                {
                    // gets a new move from the active player
                    if (turnWhite)
                    {
                        selectedMove = white.getInput(board);
                    }
                    else
                    {
                        selectedMove = black.getInput(board);
                    }

                    // Makes the move and switches player if it was a legal move
                    if (Rules.movePossible(board, selectedMove, (turnWhite ? "white" : "black")))
                    {
                        board.makeMove((turnWhite ? "white" : "black"), selectedMove);
                        turnWhite = !turnWhite;
                    }

                    // Run everything else
                    Application.DoEvents();

                    // New move accepted, update GUI and check for winner.
                    if (oldTurnWhite != turnWhite)
                    {
                        // Print accepted move
                        gui.putString(selectedMove.ToString());

                        // Save move to file
                        if (gui.getPlaybackEnabled())
                        {
                            string name = gui.getFileName();
                            if (!name.Equals(""))
                            {
                                SaveManager.saveMove(selectedMove, name);
                            }
                        }

                        // Update graphics
                        gui.updateBoard(board);

                        // Check if game over
                        if (checkGameOver())
                        {
                            return;
                        }

                        // Update turn GUI
                        board.updateTurn();
                        oldTurnWhite = turnWhite;
                        gui.putPlayerTurn(turnWhite);
                        gui.putTurn(board.getTurn());
                        gui.putScore(board.getScore("white"));

                        // Save current state
                        fileMonitor.ignoreFor(500);
                        SaveManager.saveCurrent(board);
                    }

                    // Check file monitor
                    checkFileChanged();
                }
                Application.DoEvents();
            }
        }

        // Checks if any player has won or remi
        private bool checkGameOver()
        {
            // Check if win
            if (Rules.playerLost(board, "black"))
            {
                gui.gameOver(true, false);
                return true;
            }
            if (Rules.playerLost(board, "white"))
            {
                gui.gameOver(false, false);
                return true;
            }
            if (Rules.remi(board))
            {
                gui.gameOver(false, true);
                return true;
            }

            return false;
        }

        // Check if there has been a manual change in the current state file.
        // If so, load the new change.
        private void checkFileChanged()
        {
            if (fileMonitor.changeDetected())
            {
                bool ok = SaveManager.loadCurrent(ref board);
                if (ok)
                {
                    gui.putString("Load");
                    // Set GUI and other stuff
                    updateOnLoad();
                }
            }
        }

        // Sets the player turn and calculates score at load.
        private void updateOnLoad()
        {
            gui.putTurn(board.getTurn());
            if (board.getTurn() % 2 == 1)
            {
                turnWhite = true;
            }
            else
            {
                turnWhite = false;
            }
            gui.putPlayerTurn(turnWhite);
            gui.putScore(board.getScore("white"));
        }
    }
}