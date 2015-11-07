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
            List<Tuple<uint,uint>> tmp;
            P = board.getPieceAt(x, y);
            if (P != null)
            {
                tmp = P.getPossibleMoves(board.getSquareAt,board.getTurn());
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
            if(P != null)
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
            //white = new TerminalAgent("white", gui.readString);
            //black = new TerminalAgent("black", gui.readString);
            white = new GraphicsAgent("white", gui.readSelectedMove);
            black = new GraphicsAgent("black", gui.readSelectedMove);

            gui.updateBoard(board);
            gui.putPlayerTurn(turnWhite);
            gui.putTurn(board.getTurn());

            run();
        }

        // The main game loop
        public void run()
        {
            Tuple<uint, uint, uint, uint> tmp;
            bool oldTurnWhite = turnWhite;
            while (true)
            {
                if (turnWhite)
                {
                    //if (white is TerminalAgent) // TODO not AI instead
                    //{
                        tmp = white.getInput(board);
                        if (tmp.Item1 != 10) gui.putString(tmp.ToString());
                        printMoves(tmp.Item1, tmp.Item2);
                        printPieceAt(tmp.Item1, tmp.Item2);
                        turnWhite = !board.makeMove("white", tmp.Item1, tmp.Item2, tmp.Item3, tmp.Item4);
                        
                    
                }

                else if (!turnWhite)
                {
                    //if (black is TerminalAgent) // TODO not AI instead
                    //{
                        tmp = black.getInput(board);
                        printMoves(tmp.Item1, tmp.Item2);
                        printPieceAt(tmp.Item1, tmp.Item2);
                        turnWhite = board.makeMove("black", tmp.Item1, tmp.Item2, tmp.Item3, tmp.Item4);
                        if (board.playerLost("white"))
                        {
                            gui.putString("Black player won!");
                            return;
                        }
                    //}
                }

                Application.DoEvents();

                if (oldTurnWhite != turnWhite)
                {
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
                    // Move accepted, new player.
                    // Update board and player textboxes.
                    string print = "";
                    List<Tuple<uint,uint,uint,uint>> moves = board.getBlackMoves();
                    foreach (Tuple<uint,uint,uint,uint> move in moves)
                    {
                        print += move.Item1.ToString() + move.Item2.ToString() + move.Item3.ToString() + move.Item4.ToString()+"\r\n";
                    }
                    gui.putString(print);
                    board.updateTurn();
                    oldTurnWhite = turnWhite;
                    gui.updateBoard(board);
                    gui.putPlayerTurn(turnWhite);
                    gui.putTurn(board.getTurn());
                }
            }
        }
    }
}