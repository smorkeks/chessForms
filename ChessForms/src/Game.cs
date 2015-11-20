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
            //gui.putString(p1);
            //gui.putString(p2);

            // Reset the game before starting
            //if (!loaded)
            //    reset();

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

        public void run()
        {
            Move selectedMove = null;
            bool oldTurnWhite = turnWhite;

            while (running)
            {
                if (paused)
                {
                    Thread.Sleep(100);
                }
                else
                {
                    if (turnWhite)
                    {
                        selectedMove = white.getInput(board);
                    }
                    else
                    {
                        selectedMove = black.getInput(board);
                    }

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
                    if (fileMonitor.changeDetected())
                    {
                        bool ok = SaveManager.loadCurrent(ref board);
                        gui.putString("Load");
                        if (ok)
                        {
                            // Set GUI and other stuff
                            updateOnLoad();
                        }
                    }
                }

                Application.DoEvents();
            }
        }

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

        private void OnChange()
        {
            bool ok = SaveManager.loadCurrent(ref board);
            if (ok)
            {
                // Set GUI and other stuff
                updateOnLoad();
            }
        }

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