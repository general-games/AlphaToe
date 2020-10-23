using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using NUnit.Framework;

namespace tictactoe.control
{
    class Data
    {
        public List<int> Velocity { get;private set; }
        public List<string[]> Sequences { get; private set; }
        public List<State> PolicyPlayer1 { get; private set; }
        public List<State> PolicyPlayer2 { get; private set; }

        Path path = new Path();
        public Data()
        {
            Velocity = new List<int>();
            Sequences = new List<string[]>();
        }

        public void RecordVelocity(int playerWins)
        {
            Assert.IsTrue(playerWins == 0 || playerWins == 1 || playerWins == 2);
            Velocity.Add(playerWins);
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
    }
}
