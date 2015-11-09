using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ChessForms.src
{
    public class Game
    {
        // Fields
        Board board;
        Agent white;
        Agent black;
        ChessForms.GUI gui;
        private bool turnWhite;

        private bool running = false;
        private bool paused = false;
        
        public Game()
        {
            board = new Board();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            gui = new ChessForms.GUI(start, pauseUnpause, reset);
            gui.updateBoard(board);
            Application.Run(gui);
        }

        // -- Debug ---

        //D3bUG
        public void printMoves(uint x, uint y)
        {
            Piece P;
            List<Tuple<uint, uint>> tmp;
            P = board.getPieceAt(x, y);
            if (P != null)
            {
                tmp = P.getPossibleMoves(board.getSquareAt, board.getTurn());
                gui.putString("Prints all possible moves");
                foreach (Tuple<uint, uint> item in tmp)
                {
                    string s1 = item.Item1.ToString();
                    string s2 = item.Item2.ToString();
                    gui.putString(s1 + " " + s2);
                }
                gui.putString("End of possible moves output.");
            }
        }

        //D3bUG
        public void printPieceAt(uint x, uint y)
        {
            Piece P;
            P = board.getPieceAt(x, y);
            if (P != null)
            {
                gui.putString("Piece at " + x.ToString() + y.ToString());
                gui.putString(P.GetType().ToString());
            }
        }

        // --- Control functions ---

        // Start a new game
        public void start()
        {
            //gui.putString(p1);
            //gui.putString(p2);

            // Reset the game before starting
            reset();

            // Set white agent
            switch (gui.getWhiteAgentType())
            {
                case "Terminal Agent":
                    white = new TerminalAgent("white", gui.readString);
                    break;
                case "Graphics Agent":
                    white = new GraphicsAgent("white", gui.readSelectedMove);
                    break;
                case "AI Easy":
                    white = new AiAgent("white", 2, gui.putAiScore);
                    break;
                case "AI Medium":
                    white = new AiAgent("white", 3, gui.putAiScore);
                    break;
                case "AI Hard":
                    white = new AiAgent("white", 6, gui.putAiScore);
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
                case "AI Easy":
                    black = new AiAgent("black", 2, gui.putAiScore);
                    break;
                case "AI Medium":
                    black = new AiAgent("black", 3, gui.putAiScore);
                    break;
                case "AI Hard":
                    black = new AiAgent("black", 6, gui.putAiScore);
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
                    case "AI Easy":
                        if (!(white is AiAgent && ((AiAgent)white).getDifficulty() == 2))
                            white = new AiAgent("white", 2, gui.putAiScore);
                        break;
                    case "AI Medium":
                        if (!(white is AiAgent && ((AiAgent)white).getDifficulty() == 4))
                            white = new AiAgent("white", 4, gui.putAiScore);
                        break;
                    case "AI Hard":
                        if (!(white is AiAgent && ((AiAgent)white).getDifficulty() == 6))
                            white = new AiAgent("white", 6, gui.putAiScore);
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
                    case "AI Easy":
                        if (!(black is AiAgent && ((AiAgent)black).getDifficulty() == 2))
                            black = new AiAgent("black", 2, gui.putAiScore);
                        break;
                    case "AI Medium":
                        if (!(black is AiAgent && ((AiAgent)black).getDifficulty() == 4))
                            black = new AiAgent("black", 4, gui.putAiScore);
                        break;
                    case "AI Hard":
                        if (!(black is AiAgent && ((AiAgent)black).getDifficulty() == 6))
                            black = new AiAgent("black", 6, gui.putAiScore);
                        break;
                }
            }
        }

        // --- The main game loop ---

        public void run()
        {
            Tuple<uint, uint, uint, uint> tmp = null;
            bool oldTurnWhite = turnWhite;

            while (running)
            {
                if (paused)
                {
                    gui.putString("Pause");
                    Thread.Sleep(100);
                }
                else
                {
                    if (turnWhite)
                    {
                        tmp = white.getInput(board);
                        //printMoves(tmp.Item1, tmp.Item2);
                        //printPieceAt(tmp.Item1, tmp.Item2);
                        turnWhite = !board.makeMove("white", tmp.Item1, tmp.Item2, tmp.Item3, tmp.Item4);
                    }
                    else
                    {
                        tmp = black.getInput(board);
                        //printMoves(tmp.Item1, tmp.Item2);
                        //printPieceAt(tmp.Item1, tmp.Item2);
                        turnWhite = board.makeMove("black", tmp.Item1, tmp.Item2, tmp.Item3, tmp.Item4);
                    }

                    // New move accepted, update GUI and check for winner.
                    if (oldTurnWhite != turnWhite)
                    {
                        // Print accepted move
                        gui.putString(tmp.ToString());

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
                    }
                }

                Application.DoEvents();
            }
        }

        private bool checkGameOver()
        {
            // Check if win
            if (board.playerLost("black"))
            {
                gui.gameOver(true, false);
                return true;
            }
            if (board.playerLost("white"))
            {
                gui.gameOver(false, false);
                return true;
            }
            if (board.remi())
            {
                gui.gameOver(false, true);
                return true;
            }

            return false;
        }
    }
}