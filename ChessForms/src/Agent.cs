using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChessForms.src
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
        public abstract Move getInput(Board B);

        // Returns agent colour
        public string getColour()
        {
            return colour;
        }
    }
}