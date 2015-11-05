using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace Chess.src
{
    public class Game
    {
        // Fields
        Board board;
        Agent white;
        Agent black;
        ChessForms.GUI gui;
        private bool turnWhite;
        private string newInput = "";


        //Methods
        public Game()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ChessForms.GUI());                  //start(dropdown1,dropdown2) ?
        }

        public void start(string p1, string p2)
        {
            turnWhite = true;
            board = new Board();
            gui = new ChessForms.GUI();


            if (p1 == "TA")
            {
                white = new TerminalAgent("white", gui.readString());
            }
            gui.putString(p1);


            if (p2 == "TA")
            {
                black = new TerminalAgent("black", gui.readString());
            }
            gui.putString(p2);
            printBoard();

            run();
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
                gui.putString(tmp);
            }
        }


        public void run()
        {
            Tuple<uint, uint, uint, uint> tmp;

            if (turnWhite)
            {
                if (white is TerminalAgent) // TODO not AI instead
                {
                    tmp = white.getInput(board, newInput);
                    turnWhite = !board.makeMove("white", tmp.Item1, tmp.Item2, tmp.Item3, tmp.Item4);
                    if (board.blackLost())
                    {
                        gui.putString("White player won!");
                        return;
                    }
                    printBoard();
                    newInput = "";

                }
            }

            else if (!turnWhite)
            {
                if (black is TerminalAgent) // TODO not AI instead
                {
                    tmp = black.getInput(board, newInput);
                    turnWhite = board.makeMove("black", tmp.Item1, tmp.Item2, tmp.Item3, tmp.Item4);
                    if (board.whiteLost())
                    {
                        gui.putString("Black player won!");
                        return;
                    }
                    printBoard();
                    newInput = "";

                }
            }

        }


        public void setNewPlayerInput(string input)
        {
            newInput = input;
            run();
        }
    }
}