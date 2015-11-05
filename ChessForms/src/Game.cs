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
        private string newInput = "";


        //Methods
        public Game()
        {
            board = new Board();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            gui = new ChessForms.GUI(start, ref board);
            Application.Run(gui);
            
        }

        public void start(string p1, string p2)
        {
            turnWhite = true;
            
            
            //if (p1 == "Terminal Agent")
            //{
                white = new TerminalAgent("white", gui.readString);
            //}
            gui.putString(p1);


            //if (p2 == "Terminal Agent")
            //{
                black = new TerminalAgent("black", gui.readString);
            //}
            gui.putString(p2);
            //printBoard();

            // TODO: fix this plz
            Thread runThread = new Thread(new ThreadStart(() => run(gui)));
            runThread.Start();
            //run();
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


        public void run(ChessForms.GUI gui)
        {
            Tuple<uint, uint, uint, uint> tmp;

            while (true)
            {
                if (turnWhite)
                {
                    if (white is TerminalAgent) // TODO not AI instead
                    {
                        tmp = white.getInput(board);
                        turnWhite = !board.makeMove("white", tmp.Item1, tmp.Item2, tmp.Item3, tmp.Item4);
                        if (board.blackLost())
                        {
                            gui.putString("White player won!");
                            return;
                        }
                        //printBoard();
                        //gui.updateBoard(board);
                    }
                }

                else if (!turnWhite)
                {
                    if (black is TerminalAgent) // TODO not AI instead
                    {
                        tmp = black.getInput(board);
                        turnWhite = board.makeMove("black", tmp.Item1, tmp.Item2, tmp.Item3, tmp.Item4);
                        if (board.whiteLost())
                        {
                            gui.putString("Black player won!");
                            return;
                        }
                        //printBoard();
                        //gui.updateBoard(board);
                    }
                }

                //gui.updateBoard(board);
                gui.Refresh();
            }
        }


    }
}