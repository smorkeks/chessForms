using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chess.src
{
    public abstract class Agent
    {
        // Fields
        protected string colour;
        protected bool myTurn;

        // Methods
        public Agent(string col)
        {
            colour = col;
            if (col == "white")
                myTurn = true;
            else
                myTurn = false;
        }

        //Promts the agent to make a move
        public abstract Tuple<uint, uint, uint, uint> getInput(Board B, string inp);

        public string getColour()
        {
            return colour;
        }
    }
}