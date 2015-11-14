using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessForms.src
{
    class PlaybackAgent : Agent
    {
        string fileName;
        List<Move> moves;
        int sleepTime;

        public PlaybackAgent(string col, string fileName, int sleepTime) : base(col)
        {
            this.fileName = fileName;
            this.sleepTime = sleepTime;
            moves = ChessForms.file.SaveManager.loadMoves(fileName);
        }

        public override Move getInput(Board B)
        {
            System.Threading.Thread.Sleep(sleepTime);

            uint turn = B.getTurn() - 1;
            if (turn <= moves.Count) {
                Move move = moves.ElementAt((int) turn);
                return move;
            }
            return new Move();
        }

        public int getSleepTime()
        {
            return sleepTime;
        }
    }
}
