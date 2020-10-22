using System;
using System.Collections.Generic;
using System.Text;

namespace tictactoe
{
    class State
    {
        public int MoveNum { get { return GetMoveNum(); } }
        public int[] Sequence { get; private set; }
        public float Probability { get; private set; }

        public State(int[] sequence)
        {
            this.Sequence = sequence;
        }

        public int[] Get()
        {
            return Sequence;
        }
        public int Get(int pos)
        {
            return Sequence[pos];
        }
        public void SetProb(float prob)
        {
            this.Probability = prob;
        }
        public void Set(int move, int player)
        {
            if(CanSet(move))
              Sequence[move-1] = player;
        }
        public bool CanSet(int move)
        {
            if(move > 0 && move <= 9)
                if (Sequence[move-1] == 0)
                    return true;
            return false;
        }
        public string GetSequence()
        {
            return string.Join("", Sequence);
        }
        private int GetMoveNum()
        {
            int moveNum = 0;
            for (int i = 0; i < Sequence.Length; i++)
            {
                if (Sequence[i] != 0)
                    moveNum++;
            }
            return moveNum;
        }
    }
}
