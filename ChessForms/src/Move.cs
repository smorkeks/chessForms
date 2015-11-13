using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessForms.src
{
    public class Move
    {
        // --- Flags ---
        private bool giveUp;
        private bool illegal;

        public bool GiveUp
        {
            get { return giveUp; }
            set { giveUp = value; }
        }

        public bool Illegal
        {
            get { return illegal; }
            set { illegal = value; }
        }

        // --- Coordinates from and to ---
        private uint fromX, fromY, toX, toY;
        
        public uint FromX
        {
            get { return fromX; }
            set { fromX = value; }
        }

        public uint FromY
        {
            get { return fromY; }
            set { fromY = value; }
        }

        public uint ToX
        {
            get { return toX; }
            set { toX = value; }
        }

        public uint ToY
        {
            get { return toY; }
            set { toY = value; }
        }

        // --- Constructors ---

        public Move()
        {
            fromX = 10000;
            fromY = 10000;
            toX = 10000;
            toY = 10000;
            giveUp = false;
            illegal = true;
        }

        public Move(uint toX, uint toY)
        {
            fromX = 10000;
            fromY = 10000;
            this.toX = toX;
            this.toY = toY;
            giveUp = false;
            illegal = false;
        }

        public Move(uint fromX, uint fromY, uint toX, uint toY)
        {
            this.fromX = fromX;
            this.fromY = fromY;
            this.toX = toX;
            this.toY = toY;
            giveUp = false;
            illegal = false;
        }

        // --- Other functions ---

        public void setTo(uint x, uint y)
        {
            toX = x;
            toY = y;
        }

        public void setFrom(uint x, uint y)
        {
            fromX = x;
            fromY = y;
        }

        public override string ToString()
        {
            if (giveUp)
            {
                return "Give Up";
            }
            else
            {
                return "(" + fromX + ", " + fromY + ", " + toX + ", " + toY + ")";
            }
        }

        public Move Copy()
        {
            Move copy = new Move(fromX, fromY, toX, toY);
            copy.GiveUp = giveUp;
            copy.Illegal = illegal;
            return copy;
        }

        public bool Equals(Move m)
        {
            if (m == null)
            {
                return false;
            }

            return m.FromX == fromX &&
                   m.FromY == fromY &&
                   m.ToX == toX &&
                   m.ToY == toY &&
                   m.GiveUp == giveUp &&
                   m.Illegal == illegal;
        }

        public bool hasFrom()
        {
            return fromX != 10000 && fromY != 10000;
        }

        public bool hasTo()
        {
            return toX != 10000 && toY != 10000;
        }
    }
}
