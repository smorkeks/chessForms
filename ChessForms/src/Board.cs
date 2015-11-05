using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace Chess.src
{
    public class Board
    {
        // --- Fields ---

        // Board [0,0] is at down, left.
        private const uint BOARD_SIZE_X = 8;
        private const uint BOARD_SIZE_Y = 8;

        // Colours
        private const string COLOUR_WHITE = "white";
        private const string COLOUR_BLACK = "black";

        // List of squares
        private Square[,] squares = new Square[BOARD_SIZE_X, BOARD_SIZE_Y];

        // Reward function for board position.
        private int[,] reward = new int[,] { { 0, 0, 0, 0, 0, 0, 0, 0 },
                                             { 0, 1, 1, 1, 1, 1, 1, 0 },
                                             { 0, 1, 2, 2, 2, 2, 1, 0 },
                                             { 0, 1, 2, 3, 3, 2, 1, 0 },
                                             { 0, 1, 2, 3, 3, 2, 1, 0 },
                                             { 0, 1, 2, 2, 2, 2, 1, 0 },
                                             { 0, 1, 1, 1, 1, 1, 1, 0 },
                                             { 0, 0, 0, 0, 0, 0, 0, 0 } };

        // Passed to Pieces to limit access to board.
        public delegate Square QueryFunc(uint x, uint y);

        // --- Constructor ---
        public Board()
        {
            // Create squares
            for (uint y = 0; y < BOARD_SIZE_Y; y++)
            {
                for (uint x = 0; x < BOARD_SIZE_X; x++)
                {
                    squares[x, y] = new Square(x, y, reward[x,y]);
                }
            }

            // Create white pieces
            squares[0, 0].setPiece(new Rook(0, 0, COLOUR_WHITE));
            squares[1, 0].setPiece(new Knight(1, 0, COLOUR_WHITE));
            squares[2, 0].setPiece(new Bishop(2, 0, COLOUR_WHITE));
            squares[3, 0].setPiece(new Queen(3, 0, COLOUR_WHITE));
            squares[4, 0].setPiece(new King(4, 0, COLOUR_WHITE));
            squares[5, 0].setPiece(new Bishop(5, 0, COLOUR_WHITE));
            squares[6, 0].setPiece(new Knight(6, 0, COLOUR_WHITE));
            squares[7, 0].setPiece(new Rook(7, 0, COLOUR_WHITE));
            for (uint x = 0; x < BOARD_SIZE_X; x++)
            {
                squares[x, 1].setPiece(new Pawn(x, 0, COLOUR_WHITE));
            }

            // Create black pieces
            squares[0, BOARD_SIZE_Y - 1].setPiece(new Rook(0, BOARD_SIZE_Y - 1, COLOUR_BLACK));
            squares[1, BOARD_SIZE_Y - 1].setPiece(new Knight(1, BOARD_SIZE_Y - 1, COLOUR_BLACK));
            squares[2, BOARD_SIZE_Y - 1].setPiece(new Bishop(2, BOARD_SIZE_Y - 1, COLOUR_BLACK));
            squares[3, BOARD_SIZE_Y - 1].setPiece(new Queen(3, BOARD_SIZE_Y - 1, COLOUR_BLACK));
            squares[4, BOARD_SIZE_Y - 1].setPiece(new King(4, BOARD_SIZE_Y - 1, COLOUR_BLACK));
            squares[5, BOARD_SIZE_Y - 1].setPiece(new Bishop(5, BOARD_SIZE_Y - 1, COLOUR_BLACK));
            squares[6, BOARD_SIZE_Y - 1].setPiece(new Knight(6, BOARD_SIZE_Y - 1, COLOUR_BLACK));
            squares[7, BOARD_SIZE_Y - 1].setPiece(new Rook(7, BOARD_SIZE_Y - 1, COLOUR_BLACK));
            for (uint x = 0; x < BOARD_SIZE_X; x++)
            {
                squares[x, BOARD_SIZE_Y - 2].setPiece(new Pawn(x, BOARD_SIZE_Y - 2, COLOUR_BLACK));
            }

            // Add cover
            QueryFunc qf = getSquareAt;
            foreach (Square s in squares)
            {
                Piece p = s.getPiece();

                if (p != null)
                {
                    List<Tuple<uint, uint>> cover = p.getPossibleMoves(qf);
                    foreach (Tuple<uint, uint> t in cover)
                    {
                        if (p.getColour() == COLOUR_WHITE)
                        {
                            squares[t.Item1, t.Item2].addWhiteCover();
                        }
                        else
                        {
                            squares[t.Item1, t.Item2].addBlackCover();
                        }
                        
                    }
                }
            }
        }

        // --- Methods ---

        // Get the square at x, y
        public Square getSquareAt(uint x, uint y)
        {
            // Check if outside board
            if (x >= BOARD_SIZE_X || y >= BOARD_SIZE_Y){
                return null;
            }

            // Get square or null
            return squares[x, y];
        }

        // Get the piece at x, y
        public Piece getPieceAt(uint x, uint y)
        {
            // Check if outside board
            if (x >= BOARD_SIZE_X || y >= BOARD_SIZE_Y)
            {
                return null;
            }

            // Get square or null
            return squares[x, y].getPiece();
        }

        // Check if white piece at x, y
        public bool isWhite(uint x, uint y)
        {
            Piece p = getPieceAt(x,y);
            return (p != null && p.getColour() == COLOUR_WHITE);
        }

        // Check if black piece at x, y
        public bool isBlack(uint x, uint y)
        {
            Piece p = getPieceAt(x, y);
            return (p != null && p.getColour() == COLOUR_BLACK);
        }

        // Get all moves of a specific colour.
        private List<Tuple<uint, uint, uint, uint>> getMoves(string col)
        {
            List<Tuple<uint, uint, uint, uint>> moves = new List<Tuple<uint, uint, uint, uint>>();

            for (uint y = 0; y < BOARD_SIZE_Y; y++)
            {
                for (uint x = 0; x < BOARD_SIZE_X; x++)
                {
                    Piece p = getPieceAt(x, y);
                    if (p != null && p.getColour() == col)
                    {
                        List<Tuple<uint, uint>> newMoves = p.getPossibleMoves(getSquareAt);

                        foreach (Tuple<uint, uint> t in newMoves)
                        {
                            moves.Add(new Tuple<uint, uint, uint, uint>(x, y, t.Item1, t.Item2));
                        }
                    }
                }
            }

            return moves;
        }

        // Get all possible white moves, in the format x1,y1, x2,y2.
        public List<Tuple<uint, uint, uint, uint>> getWhiteMoves()
        {
            return getMoves(COLOUR_WHITE);
        }

        // Get all possible black moves, in the format x1,y1, x2,y2.
        public List<Tuple<uint, uint, uint, uint>> getBlackMoves()
        {
            return getMoves(COLOUR_BLACK);
        }

        public bool makeMove(string col, uint x1, uint y1, uint x2, uint y2)
        {
            Piece p = getPieceAt(x1, y1);
            Square s1 = getSquareAt(x1, y1);
            Square s2 = getSquareAt(x2, y2);

            // Check if squares are ok.
            if (s1 == null || s2 == null)
            {
                return false;
            }

            // Check if piece is ok.
            if (p == null)
            {
                return false;
            }
            if (p.getColour() != col)
            {
                return false;
            }
            if (!p.movePossible(x2, y2, getSquareAt))
            {
                return false;
            }

            // Move is legal

            // If there is a piece at x2,y2 then remove its cover.
            Piece p2 = s2.getPiece();
            if (p2 != null)
            {
                foreach (Tuple<uint, uint> t in p2.getPossibleMoves(getSquareAt))
                {
                    // If moving piece is white, remove black cover, and vc.v.
                    if (col == COLOUR_WHITE)
                    {
                        getSquareAt(t.Item1, t.Item2).removeBlackCover();
                    }
                    else
                    {
                        getSquareAt(t.Item1, t.Item2).removeWhiteCover();
                    }
                }
            }

            // Remove from old position
            s1.removePiece();
            foreach (Tuple<uint, uint> t in p.getPossibleMoves(getSquareAt))
            {
                // If moving piece is white, remove white cover, and vc.v.
                if (col == COLOUR_WHITE)
                {
                    getSquareAt(t.Item1, t.Item2).removeWhiteCover();
                }
                else
                {
                    getSquareAt(t.Item1, t.Item2).removeBlackCover();
                }
            }

            
            // Add to new position
            getSquareAt(x2, y2).setPiece(p);
            p.move(x2, y2);
            foreach (Tuple<uint, uint> t in p.getPossibleMoves(getSquareAt))
            {
                // If moving piece is white, add white cover, and vc.v.
                if (col == COLOUR_WHITE)
                {
                    getSquareAt(t.Item1, t.Item2).addWhiteCover();
                }
                else
                {
                    getSquareAt(t.Item1, t.Item2).addBlackCover();
                }
            }

            return true;
        }

        // TODO
        public bool blackLost()
        {
            return false;
        }

        //TODO
        public bool whiteLost()
        {
            return false;
        }
    }
}