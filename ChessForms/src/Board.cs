﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace ChessForms.src
{
    public class Board
    {
        // --- Fields ---
        public const uint BOARD_SIZE_X = 8;
        public const uint BOARD_SIZE_Y = 8;

        // Colours
        private const string COLOUR_WHITE = "white";
        private const string COLOUR_BLACK = "black";
        private uint turn;

        // List of squares
        private Square[,] squares = new Square[BOARD_SIZE_X, BOARD_SIZE_Y];

        // Passed to Pieces to limit access to board.
        public delegate Square QueryFunc(uint x, uint y);

        // --- Constructor ---
        public Board()
        {
            //Set turn
            turn = 1;

            // Create squares
            for (uint y = 0; y < BOARD_SIZE_Y; y++)
            {
                for (uint x = 0; x < BOARD_SIZE_X; x++)
                {
                    squares[x, y] = new Square(x, y);
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
                squares[x, 1].setPiece(new Pawn(x, 1, COLOUR_WHITE));
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

            updateCover();
        }

        public Board(uint turn)
        {
            this.turn = turn;

            // Create squares
            for (uint y = 0; y < BOARD_SIZE_Y; y++)
            {
                for (uint x = 0; x < BOARD_SIZE_X; x++)
                {
                    squares[x, y] = new Square(x, y);
                }
            }
        }

        // --- Methods ---

        // Get the square at x, y
        public Square getSquareAt(uint x, uint y)
        {
            // Check if outside board
            if (x >= BOARD_SIZE_X || y >= BOARD_SIZE_Y)
            {
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
            Piece p = getPieceAt(x, y);
            return (p != null && p.getColour() == COLOUR_WHITE);
        }

        // Check if black piece at x, y
        public bool isBlack(uint x, uint y)
        {
            Piece p = getPieceAt(x, y);
            return (p != null && p.getColour() == COLOUR_BLACK);
        }

        public bool makeMove(string col, Move move)
        {
            uint x1 = move.FromX;
            uint y1 = move.FromY;
            uint x2 = move.ToX;
            uint y2 = move.ToY;

            Piece p = getPieceAt(x1, y1);
            Square s1 = getSquareAt(x1, y1);
            Square s2 = getSquareAt(x2, y2);
            bool castling = false;
            bool enPassant = false;

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
            // Move is legal

            //Check if this is a castling
            if ((p is King) && (((x1 - x2) == 2) || ((x2 - x1) == 2)))
            {
                castling = true;
            }

            // Special handling for pawns
            if ((p is Pawn))
            {
                // Check double step
                if (Math.Abs((int)move.FromY - (int)move.ToY) == 2)
                {
                    ((Pawn)p).setDoubleStepTurn(getTurn());
                }

                //Check if en passant
                if ((getSquareAt(x2, y2).getPiece() == null) && (x1 != x2))
                {
                    enPassant = true;
                }
            }

            // Remove from old position
            s1.removePiece();
            // Add to new position
            if ((p is Pawn && p.getColour() == "white" && y2 == 7) ||
               (p is Pawn && p.getColour() == "black" && y2 == 0))
            {
                p = new Queen(x1, y1, p.getColour());
            }
            s2.setPiece(p);
            p.move(x2, y2);

            // Handles the special castling case
            if (castling)
            {
                uint x2rook;
                uint x1rook;
                if (x2 > x1)// Castling right
                {
                    x1rook = 7;
                    x2rook = x2 - 1;
                }
                else
                {
                    x1rook = 0;
                    x2rook = x2 + 1;
                }
                Square s3 = getSquareAt(x1rook, y1);
                Piece p3 = s3.getPiece();
                Square s4 = getSquareAt(x2rook, y1);
                s3.removePiece();
                s4.setPiece(p3);
                p3.move(x2rook, y2);

            }

            // Handles the special en passant case
            if (enPassant)
            {
                int yMod;
                if (p.getColour() == "white")
                    yMod = -1;
                else
                    yMod = 1;
                Square s3 = getSquareAt(x2, (uint)(y2 + yMod));
                Piece p3 = s3.getPiece();
                s3.removePiece();
            }

            updateCover();
            return true;
        }

        // returns turn number
        public uint getTurn()
        {
            return turn;
        }

        // sets what turn it is
        public void setTurn(uint t)
        {
            turn = t;
        }

        // Increments turn by one
        public void updateTurn()
        {
            turn++;
        }

        // Update cover for all squares.
        public void updateCover()
        {
            Piece p;
            Piece kingWhite = null;
            Piece kingBlack = null;
            List<Tuple<uint, uint>> cover;

            foreach (Square s in squares)
            {
                s.resetCover();
            }

            foreach (Square s in squares)
            {
                p = s.getPiece();
                if (p != null)
                {
                    if (p is King)
                    {
                        // Save kings for last.
                        if (p.getColour() == COLOUR_WHITE)
                        {
                            kingWhite = p;
                        }
                        else
                        {
                            kingBlack = p;
                        }
                    }
                    else
                    {
                        // Update cover
                        cover = ChessForms.rules.Rules.getCover(this, p);
                        if (p.getColour() == COLOUR_WHITE)
                        {
                            foreach (Tuple<uint, uint> t in cover)
                            {
                                getSquareAt(t.Item1, t.Item2).addWhiteCover();
                            }
                        }
                        else
                        {
                            foreach (Tuple<uint, uint> t in cover)
                            {
                                getSquareAt(t.Item1, t.Item2).addBlackCover();
                            }
                        }
                    }
                }
            }

            // Cover from kings
            if (kingWhite != null)
            {
                cover = ChessForms.rules.Rules.getCover(this, kingWhite);
                foreach (Tuple<uint, uint> t in cover)
                {
                    getSquareAt(t.Item1, t.Item2).addWhiteCover();
                }
            }
            if (kingBlack != null)
            {
                cover = ChessForms.rules.Rules.getCover(this, kingBlack);
                foreach (Tuple<uint, uint> t in cover)
                {
                    getSquareAt(t.Item1, t.Item2).addBlackCover();
                }
            }
        }

        // Gets the total score of a gamestate
        public int getScore(string col)
        {
            int score = 0;
            for (uint i = 0; i < BOARD_SIZE_X; i++)
            {
                for (uint j = 0; j < BOARD_SIZE_Y; j++)
                {
                    if (col == "white")
                    {
                        score += ((int)getSquareAt(i, j).getWhiteCover() - (int)getSquareAt(i, j).getBlackCover()) * 10;
                        if (getSquareAt(i, j).getPiece() != null)
                            if (getSquareAt(i, j).getPiece().getColour() == col)
                                score = score + (int)getSquareAt(i, j).getPiece().getValue();
                            else
                                score = score - (int)getSquareAt(i, j).getPiece().getValue();
                    }
                    else
                    {
                        score += ((int)getSquareAt(i, j).getBlackCover() - (int)getSquareAt(i, j).getWhiteCover()) * 10;
                        if (getSquareAt(i, j).getPiece() != null)
                            if (getSquareAt(i, j).getPiece().getColour() == col)
                                score = score + (int)getSquareAt(i, j).getPiece().getValue();
                            else
                                score = score - (int)getSquareAt(i, j).getPiece().getValue();
                    }
                }

            }
            return score;
        }

        // Deep copy of board
        public void Copy(Board oldBoard)
        {
            for (int i = 0; i < BOARD_SIZE_X; i++)
            {
                for (int j = 0; j < BOARD_SIZE_Y; j++)
                {
                    squares[i, j].Copy(oldBoard.squares[i, j]);
                }
            }
            turn = oldBoard.turn;
        }


        // returns the total number of remaining pieces on the board
        public int getNumPieces()
        {
            int num = 0;
            for (uint i = 0; i < BOARD_SIZE_X; i++)
            {
                for (uint j = 0; j < BOARD_SIZE_Y; j++)
                {
                    if (getPieceAt(i, j) != null)
                        num++;
                }
            }
            return num;
        }

        // Checks if coordinates are within the edges of the board
        public bool withinBoard(int x, int y)
        {
            return (0 <= x && x < BOARD_SIZE_X && 0 <= y && y < BOARD_SIZE_Y);
        }

        // returns a list of all squares
        public List<Square> getAllSquares()
        {
            List<Square> list = new List<Square>();
            foreach (Square s in squares)
            {
                list.Add(s);
            }
            return list;
        }

        // Empties the board of pieces. Used in load
        public void clearBoard()
        {
            for (uint y = 0; y < BOARD_SIZE_Y; y++)
            {
                for (uint x = 0; x < BOARD_SIZE_X; x++)
                {
                    squares[x, y] = new Square(x, y);
                }
            }
        }

        // put a piece on square with coordinates x y
        public void setPiece(uint x, uint y, Piece piece)
        {
            squares[x, y].setPiece(piece);
        }
    }
}