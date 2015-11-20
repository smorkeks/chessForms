using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessForms.src
{
    public class Move
    {
        // --- Constant ---
        // Use 10000 as an illegal value for the coordinates.
        private const uint NO_COORDINATE = 10000;

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
            fromX = NO_COORDINATE;
            fromY = NO_COORDINATE;
            toX = NO_COORDINATE;
            toY = NO_COORDINATE;
            giveUp = false;
            illegal = true;
        }

        public Move(uint toX, uint toY)
        {
            fromX = NO_COORDINATE;
            fromY = NO_COORDINATE;
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

        // Set the destination coordinates of the move.
        public void setTo(uint x, uint y)
        {
            toX = x;
            toY = y;
        }

        // Set the source coordinates of the move.
        public void setFrom(uint x, uint y)
        {
            fromX = x;
            fromY = y;
        }

        // Convert the move coordinates to a string with a good format.
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

        // Return an exact copy of the move.
        public Move Copy()
        {
            Move copy = new Move(fromX, fromY, toX, toY);
            copy.GiveUp = giveUp;
            copy.Illegal = illegal;
            return copy;
        }

        // Compare this move to another.
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

        // Return true if the move has source coordinates.
        public bool hasFrom()
        {
            return fromX != NO_COORDINATE && fromY != NO_COORDINATE;
        }

        // Return true if the move has desitnation coordinates.
        public bool hasTo()
        {
            return toX != NO_COORDINATE && toY != NO_COORDINATE;
        }
    }
}
