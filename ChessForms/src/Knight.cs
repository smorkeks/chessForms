﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessForms.src
{
    public class Knight : Piece
    {
        public Knight(uint x, uint y, string col) : base(x, y, 3, col) { }

        public Knight() : base() { }

        public override List<Tuple<uint, uint>> getPossibleMoves(Board.QueryFunc QF, uint turn)
        {

            List<Tuple<uint, uint>> tmpList = new List<Tuple<uint, uint>>();
            List<Tuple<int, int>> jmpList = new List<Tuple<int, int>>(); //Contains all possible jump locations
            jmpList.Add(new Tuple<int, int>(1, 2));
            jmpList.Add(new Tuple<int, int>(1, -2));
            jmpList.Add(new Tuple<int, int>(2, 1));
            jmpList.Add(new Tuple<int, int>(2, -1));
            jmpList.Add(new Tuple<int, int>(-1, 2));
            jmpList.Add(new Tuple<int, int>(-1, -2));
            jmpList.Add(new Tuple<int, int>(-2, 1));
            jmpList.Add(new Tuple<int, int>(-2, -1));

            // Loops over jump locations
            foreach (Tuple<int, int> item in jmpList)
            {
                int x = item.Item1;
                int y = item.Item2;
                if (withinBoard((int)getX() + x, (int)getY() + y))
                {
                    Square S = QF((uint)(getX() + x), (uint)(getY() + y));
                    Piece P = S.getPiece();
                    if (P != null)
                    {
                        if (!isSameColour(P))
                        {
                            tmpList.Add(new Tuple<uint, uint>((uint)(getX() + x), (uint)(getY() + y)));
                        }
                    }
                    else
                    {
                        tmpList.Add(new Tuple<uint, uint>((uint)(getX() + x), (uint)(getY() + y)));
                    }
                }
            }

            // Filter for check situations
            checkFilter(ref tmpList, QF);

            return tmpList;
        }

        public override List<Tuple<uint, uint>> getCover(Board.QueryFunc QF)
        {
            List<Tuple<uint, uint>> cover = new List<Tuple<uint, uint>>();
            List<Tuple<int, int>> jmpList = new List<Tuple<int, int>>(); //Contains all possible jump locations
            jmpList.Add(new Tuple<int, int>(1, 2));
            jmpList.Add(new Tuple<int, int>(1, -2));
            jmpList.Add(new Tuple<int, int>(2, 1));
            jmpList.Add(new Tuple<int, int>(2, -1));
            jmpList.Add(new Tuple<int, int>(-1, 2));
            jmpList.Add(new Tuple<int, int>(-1, -2));
            jmpList.Add(new Tuple<int, int>(-2, 1));
            jmpList.Add(new Tuple<int, int>(-2, -1));

            // Loops over jump locations
            foreach (Tuple<int, int> item in jmpList)
            {
                int x = item.Item1;
                int y = item.Item2;
                if (withinBoard((int)getX() + x, (int)getY() + y))
                {

                    cover.Add(new Tuple<uint, uint>((uint)(getX() + x), (uint)(getY() + y)));
                }
            }

            return cover;
        }

        public override Piece getCopyPiece()
        {
            return new Knight();
        }
    }
}