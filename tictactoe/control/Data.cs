using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using NUnit.Framework;

namespace tictactoe.control
{
    class Data
    {
        public List<int> Opening { get; private set; }
        public List<int> Wins { get;private set; }
        public List<string[]> Sequences { get; private set; }
        public List<State> PolicyPlayer1 { get; private set; }
        public List<State> PolicyPlayer2 { get; private set; }
        public TimeSpan TrainTime { get;  private set; }
        public float ALPHA { get; private set; }
        public double EPSILON { get; private set; }
        public int Episodes { get; private set; }
        public float[] P1StateValues { get; set; }
        public float[] P2StateValues { get; set; }

        Path path = new Path();
        public Data()
        {
            Wins = new List<int>();
            Sequences = new List<string[]>();
            this.Opening = new List<int>();
        }
        public Data(float alpha, double epsilon, int episodes, float[] p1Vals, float[] p2Vals)
        {
            Wins = new List<int>();
            Sequences = new List<string[]>();
            this.Opening = new List<int>();
            this.ALPHA = alpha;
            this.EPSILON = epsilon;
            this.Episodes = episodes;
            this.P1StateValues = p1Vals;
            this.P2StateValues = p2Vals;
        }
        public void RecordWins(int playerWins)
        {
            Assert.IsTrue(playerWins == 0 || playerWins == 1 || playerWins == 2);
            Wins.Add(playerWins);
        }
        public void RecordSequence(string[] sequence)
        {
            Sequences.Add(sequence);
        }
        public void RecordPlayerPolicy(Agent player)
        {
            if(player.Player == 1)
                PolicyPlayer1 = player.GetPolicy();
            if (player.Player == 2)
                PolicyPlayer2 = player.GetPolicy();
        }
        public void SetTrainTime(TimeSpan timespan)
        {
            this.TrainTime = timespan;
        }
        public void AddOpening(int value)
        {
            Opening.Add(value);
        }
    }
}
