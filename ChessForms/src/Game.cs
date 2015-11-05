using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;

namespace Chess.src
{
    public class Game
    {
        // Fields
        Board board;
        Agent white;
        Agent black;
        private bool turnWhite;
        private string newInput = "";

        // Terminal deligates
        public delegate void putString(string s);
        //public delegate string readString();

        putString put;
        //readString read;

        //Methods
        public Game(putString put)//, readString read)
        {
            this.put = put;
            //this.read = read;
        }

        public void start(string p1, string p2)
        {
            turnWhite = true;
            board = new Board();


            if (p1 == "TA")
            {
                white = new TerminalAgent("white");
            }
            put(p1);


            if (p2 == "TA")
            {
                black = new TerminalAgent("black");
            }
            put(p2);
            printBoard();

            //run();
            //Thread runThread = new Thread(new ThreadStart(run));
            //runThread.IsBackground = false;
            //runThread.Start();

        }

        void printBoard()
        {

            Piece P;
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

                        if (P is Pawn)
                            tmp = tmp + "p  ";
                        else if (P is Rook)
                            tmp = tmp + "r  ";
                        else if (P is Knight)
                            tmp = tmp + "kn ";
                        else if (P is Bishop)
                            tmp = tmp + "b  ";
                        else if (P is Queen)
                            tmp = tmp + "q  ";
                        else if (P is King)
                            tmp = tmp + "k  ";
                        else
                            tmp = tmp + "ERORR ERROR ERROR";

                    }
                }
                put(tmp);
            }
        }

        void run()
        {
            Tuple<uint, uint, uint, uint> tmp;
            //while (true)
            //{
                //while (turnWhite)
                //{
                    if (white is TerminalAgent) // TODO not AI instead
                    {
                        if (newInput != "")
                        {
                            tmp = white.getInput(board, newInput);
                            turnWhite = !board.makeMove("white", tmp.Item1, tmp.Item2, tmp.Item3, tmp.Item4);

                            if (turnWhite)
                            {
                                // Move was not legal
                                put("Move is not legal!");
                            }
                            else
                            {
                                // Move was legal
                                if (board.blackLost())
                                {
                                    put("White player won!");
                                    return;
                                }

                                printBoard();
                            }

                            // Reset player input
                            newInput = "";
                        }
                    }
                //}

                //Thread.Sleep(100);

                //while (!turnWhite)
                //{
                    if (black is TerminalAgent) // TODO not AI instead
                    {
                        if (newInput != "")
                        {
                            tmp = black.getInput(board, newInput);
                            turnWhite = board.makeMove("black", tmp.Item1, tmp.Item2, tmp.Item3, tmp.Item4);

                            if (!turnWhite)
                            {
                                // Move was not legal
                                put("Move is not legal!");
                            }
                            else
                            {
                                // Move was legal
                                if (board.whiteLost())
                                {
                                    put("Black player won!");
                                    return;
                                }
                                printBoard();
                            }

                            // Reset player input
                            newInput = "";
                        }
                    }
                //}

                //Thread.Sleep(100);
            //}
        }

        public void setNewPlayerInput(string input)
        {
            newInput = input;
            run();
        }
    }
}