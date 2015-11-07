using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessForms.src
{
    public abstract class Piece
    {
        // fields
        protected uint xCoord;
        protected uint yCoord;
        protected uint score;         //Value of the piece
        protected bool hasMoved;      //Determines if the piece has moved from initial pos
        protected string colour;      //White or black


        // methods
        public Piece(uint x, uint y, uint val, string col)
        {
            this.xCoord = x;
            this.yCoord = y;
            this.score = val;
            this.colour = col;
            this.hasMoved = false;
        }


        public void move(uint x, uint y)
        {
            this.xCoord = x;
            this.yCoord = y;
            if (!hasMoved)
                hasMoved = true;
        }

        // Returns a list of all possible moves
        public abstract List<Tuple<uint, uint>> getPossibleMoves(Board.QueryFunc QF, uint turn);

        // Returns a list of all covered squares
        public abstract List<Tuple<uint, uint>> getCover(Board.QueryFunc QF);

        //Checks if move is possible
        public bool movePossible(uint x, uint y, Board.QueryFunc QF, uint turn)
        {
            List<Tuple<uint, uint>> tmp;
            tmp = this.getPossibleMoves(QF, turn);
            foreach (Tuple<uint, uint> item in tmp)
            {
                if ((item.Item1 == x) && (item.Item2 == y))
                {
                    return true;
                }
            }
            return false;
        }

        // Filter if check
        protected virtual void checkFilter(ref List<Tuple<uint, uint>> moves, Board.QueryFunc QF)
        {
            // Find king square
            Square kingSquare = null;
            for (uint j = 0; j < 8; j++)
            {
                for (uint i = 0; i < 8; i++)
                {
                    kingSquare = QF(i, j);
                    if (kingSquare.getPiece() is King &&
                        kingSquare.getPiece().getColour() == colour)
                    {
                        break;
                    }
                }
                if (kingSquare.getPiece() is King &&
                        kingSquare.getPiece().getColour() == colour)
                {
                    break;
                }
            }

            // Get king position
            uint xKing = kingSquare.getX();
            uint yKing = kingSquare.getY();

            // Check if king has more than one threat
            if ((colour == "white" && kingSquare.getBlackCover() > 1) ||
                (colour == "black" && kingSquare.getWhiteCover() > 1))
            {
                moves.Clear();
            }
            else if ((colour == "white" && kingSquare.getBlackCover() == 1) ||
                     (colour == "black" && kingSquare.getWhiteCover() == 1))
            {
                Square threat = null;

                // Get all squares with cover of king
                Square s;
                for (uint j = 0; j < 8; j++)
                {
                    for (uint i = 0; i < 8; i++)
                    {
                        s = QF(j, i);
                        if (s.getPiece() != null && s.getPiece().getColour() != colour)
                        {
                            // Enemy piece, check if threat
                            List<Tuple<uint, uint>> cover = s.getPiece().getCover(QF);
                            foreach (Tuple<uint, uint> t in cover)
                            {
                                if (t.Item1 == xKing && t.Item2 == yKing)
                                {
                                    // Threat
                                    threat = s;
                                    break;
                                }
                            }
                        }
                        if (threat != null)
                            break;
                    }
                    if (threat != null)
                        break;
                }

                // Remove all moves that do not resolve the check
                List<Tuple<uint, uint>> tmpMoves = new List<Tuple<uint, uint>>(moves);
                Piece p = threat.getPiece();
                if (p is Knight || p is Pawn)
                {
                    // Must take threat
                    foreach (Tuple<uint, uint> move in tmpMoves)
                    {
                        if (move.Item1 != threat.getX() ||
                            move.Item2 != threat.getY())
                        {
                            // Will not take, remove
                            moves.Remove(move);
                        }
                    }
                }

                // Seems like a bro
                tmpMoves = new List<Tuple<uint, uint>>(moves);
                if (p is Rook || p is Queen)
                {
                    // Must take or block threat
                    foreach (Tuple<uint, uint> move in tmpMoves)
                    {
                        if (xKing == p.getX())
                        {
                            // Same x
                            if (!(move.Item1 == xKing &&                                   // Move will move to same x
                                  ((yKing > move.Item2 && move.Item2 >= p.getY()) ||       // Move will block or take when King to the right of Rook
                                   (yKing < move.Item2 && move.Item2 <= p.getY()))))       // Move will block or take when King to the left of Rook
                            {
                                // Will not block or take
                                moves.Remove(move);
                            }
                        }
                        else if (yKing == p.getY())
                        {
                            // Same y
                            if (!(move.Item2 == yKing &&                                   // Move will move to same y
                                  ((xKing > move.Item1 && move.Item1 >= p.getX()) ||       // Move will block or take when King above Rook
                                   (xKing < move.Item1 && move.Item1 <= p.getX()))))       // Move will block or take when King below Rook
                            {
                                // Will not block or take
                                moves.Remove(move);
                            }
                        }
                    }
                }

                tmpMoves = new List<Tuple<uint, uint>>(moves);
                if ((p is Bishop || p is Queen) && !(xKing == p.getX() || yKing == p.getY()))
                {
                    // Must take or block threat
                    int xMod = (xKing > p.getX() ? -1 : 1);
                    int yMod = (yKing > p.getY() ? -1 : 1);

                    // Step from King to Threat
                    int steps = (int)Math.Abs(xKing - p.getX());
                    foreach (Tuple<uint, uint> move in tmpMoves)
                    {
                        // Search for any blocking or taking moves
                        int x = (int)xKing;
                        int y = (int)yKing;
                        bool foundGoodMove = false;
                        for (int i = 0; i < steps; i++)
                        {
                            x += xMod;
                            y += yMod;

                            if (move.Item1 == x && move.Item2 == y)
                            {
                                foundGoodMove = true;
                                break;
                            }
                        }

                        // Remove if no good move
                        if (!foundGoodMove)
                        {
                            moves.Remove(move);
                        }
                    }
                }
            }

            // Make sure that no new check is created by a move
            if (QF(getX(), getY()).getEnemyCover(colour))
            {
                // Empty list of squares with cover of this square
                List<Square> threats = new List<Square>();

                // Get all squares with cover of king
                Square s;
                for (uint j = 0; j < 8; j++)
                {
                    for (uint i = 0; i < 8; i++)
                    {
                        s = QF(j, i);
                        if (s.getPiece() != null && s.getPiece().getColour() != colour)
                        {
                            // Remove impossible cases
                            if (s.getPiece() is King || s.getPiece() is Knight || s.getPiece() is Pawn)
                            {
                                continue;
                            }
                            
                            // Enemy piece, check if threat
                            List<Tuple<uint, uint>> cover = s.getPiece().getCover(QF);
                            foreach (Tuple<uint, uint> t in cover)
                            {
                                if (t.Item1 == getX() && t.Item2 == getY())
                                {
                                    // Threat
                                    threats.Add(s);
                                }
                            }
                        }
                    }
                }

                // Check each threat for possible new check
                foreach (Square square in threats)
                {
                    Piece p = square.getPiece();

                    int xMod = 0, yMod = 0;

                    if (p.getX() == xKing || p.getY() == yKing)
                    {
                        // Not diagonal
                        if ((int)p.getX() - (int)xKing > 0)
                            xMod = 1;
                        else if ((int)p.getX() - (int)xKing < 0)
                            xMod = -1;

                        if ((int)p.getY() - ((int)yKing) > 0)
                            yMod = 1;
                        else if ((int)p.getY() - ((int)yKing) < 0)
                            yMod = -1;
                    }
                    else
                    {
                        // Diagonal
                        xMod = (xKing > p.getX() ? -1 : 1);
                        yMod = (yKing > p.getY() ? -1 : 1);
                    }

                    int steps = (int)(xMod != 0 ? (Math.Abs(p.getX() - xKing)) : (Math.Abs(p.getY() - yKing)));
                    int x = (int)xKing;
                    int y = (int)yKing;
                    int numBlocks = 0;
                    bool foundSelf = false;
                    for (int i = 0; i < steps-1; i++)
                    {
                        x += xMod;
                        y += yMod;

                        if (x == getX() && y == getY())
                        {
                            foundSelf = true;
                            numBlocks++;
                        }
                        else if (QF((uint)x, (uint)y).getPiece() != null)
                        {
                            numBlocks++;
                        }
                    }

                    // Must have at least two blocks to be a possible move
                    if (foundSelf && numBlocks < 2)
                    {
                        moves.Clear();
                    }
                }
            }
        }

        // Used to determine if the piece has moved from initial position.
        // Nessecary for En Passant and Castling
        public bool movedFromInit()
        {
            return hasMoved;
        }

        //Checks if square is inside the bounds of the board
        public bool withinBoard(int x, int y)
        {
            if ((x <= 7) & (x >= 0) & (y <= 7) & (y >= 0))
                return true;
            else
                return false;
        }

        //checks if same colour
        public bool isSameColour(Piece P)
        {
            if (P.getColour() == this.colour)
                return true;
            else
                return false;
        }

        public string getColour()
        {
            return this.colour;
        }

        public uint getX()
        {
            return xCoord;
        }

        public uint getY()
        {
            return yCoord;
        }
    }
}