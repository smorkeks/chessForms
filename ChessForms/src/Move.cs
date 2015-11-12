using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessForms.src
{
    class Move
    {
        // --- Flag for giving up ---
        private bool giveUp;
        
        public bool GiveUp
        {
            get { return giveUp; }
            set { giveUp = value; }
        }

        // --- Coordinates from and to ---
        private int fromX, fromY, toX, toY;
        
        public int FromX
        {
            get { return fromX; }
            set { fromX = value; }
        }

        public int FromY
        {
            get { return fromY; }
            set { fromY = value; }
        }

        public int ToX
        {
            get { return toX; }
            set { toX = value; }
        }

        public int ToY
        {
            get { return toY; }
            set { toY = value; }
        }

        // --- Constructors ---

        public Move()
        {
            fromX = 0;
            fromY = 0;
            toX = 0;
            toY = 0;
            giveUp = false;
        }

        public Move(int toX, int toY)
        {
            fromX = 0;
            fromY = 0;
            this.toX = toX;
            this.toY = toY;
            giveUp = false;
        }

        public Move(int fromX, int fromY, int toX, int toY)
        {
            this.fromX = fromX;
            this.fromY = fromY;
            this.toX = toX;
            this.toY = toY;
            giveUp = false;
        }

        // --- Other functions ---

        public void setTo(int x, int y)
        {
            toX = x;
            toY = y;
        }

        public void setFrom(int x, int y)
        {
            fromX = x;
            fromY = y;
        }

    }
}
