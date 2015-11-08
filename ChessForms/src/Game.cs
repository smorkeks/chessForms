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

        //Methods
        public Game()
        {
            board = new Board();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            gui = new ChessForms.GUI(start);
            gui.updateBoard(board);
            Application.Run(gui);
        }

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

        public void start(string p1, string p2)
        {
            gui.putString(p1);
            gui.putString(p2);

            turnWhite = true;
            // Set white agent
            switch (p1)
            {
                case "Terminal Agent":
                    white = new TerminalAgent("white", gui.readString);
                    break;
                case "Graphics Agent":
                    white = new GraphicsAgent("white", gui.readSelectedMove);
                    break;
                case "AI":
                    white = new AiAgent("white", 2, gui.putAiScore);
                    break;
            }

            // Set black agent
            switch (p2)
            {
                case "Terminal Agent":
                    black = new TerminalAgent("black", gui.readString);
                    break;
                case "Graphics Agent":
                    black = new GraphicsAgent("black", gui.readSelectedMove);
                    break;
                case "AI":
                    black = new AiAgent("black", 4, gui.putAiScore);
                    break;
            }
            //white = new TerminalAgent("white", gui.readString);
            //black = new TerminalAgent("black", gui.readString);
            //white = new GraphicsAgent("white", gui.readSelectedMove);
            //black = new GraphicsAgent("black", gui.readSelectedMove);

            gui.updateBoard(board);
            gui.putPlayerTurn(turnWhite);
            gui.putTurn(board.getTurn());

            run();
        }

        // The main game loop
        public void run()
        {
            Tuple<uint, uint, uint, uint> tmp = null;
            bool oldTurnWhite = turnWhite;
            while (true)
            {
                if (turnWhite)
                {
                    tmp = white.getInput(board);
                    //printMoves(tmp.Item1, tmp.Item2);
                    //printPieceAt(tmp.Item1, tmp.Item2);
                    turnWhite = !board.makeMove("white", tmp.Item1, tmp.Item2, tmp.Item3, tmp.Item4);
                }

                else if (!turnWhite)
                {
                    tmp = black.getInput(board);
                    //printMoves(tmp.Item1, tmp.Item2);
                    //printPieceAt(tmp.Item1, tmp.Item2);
                    turnWhite = board.makeMove("black", tmp.Item1, tmp.Item2, tmp.Item3, tmp.Item4);
                }

                Application.DoEvents();

                if (oldTurnWhite != turnWhite)
                {
                    // Print accepted move
                    gui.putString(tmp.ToString());

                    // Update graphics
                    gui.updateBoard(board);

                    // Check if win
                    if (board.playerLost("black"))
                    {
                        gui.putString("White player won!");
                        return;
                    }
                    if (board.playerLost("white"))
                    {
                        gui.putString("Black player won!");
                        return;
                    } 
                    if (board.remi())
                    {
                        gui.putString("Remi!");
                        return;
                    }

                    // Update turn GUI
                    board.updateTurn();
                    oldTurnWhite = turnWhite;
                    gui.putPlayerTurn(turnWhite);
                    gui.putTurn(board.getTurn());
                }
            }
        }
    }
}