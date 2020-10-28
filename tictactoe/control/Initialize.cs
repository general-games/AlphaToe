using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NUnit.Framework;

namespace tictactoe
{
    //Enumeration logic courtesy of 'Mystical' on StackOverflow. https://stackoverflow.com/a/7466492

    class Initialize
    {
        public enum Players
        {
            Player1,
            Player2
        }
        Rules rules = new Rules();
        public int[,] EnumerateAll()
        {
            int numStates = (int)Math.Pow(3, 9);
            int numSquares = 9;
            int numRuns = 262144;
            int[,] allStates = new int[numStates, numSquares];
            int c = 0;
            int idx = 0;
            while (c < (numRuns))
            {
                bool valid = (c & 3) < 3;
                valid &= ((c >> 2) & 3) < 3;
                valid &= ((c >> 4) & 3) < 3;
                valid &= ((c >> 6) & 3) < 3;
                valid &= ((c >> 8) & 3) < 3;
                valid &= ((c >> 10) & 3) < 3;
                valid &= ((c >> 12) & 3) < 3;
                valid &= ((c >> 14) & 3) < 3;
                valid &= ((c >> 16) & 3) < 3;

                if (valid)
                {
                    int i = c;
                    int j = 0;
                    while (j < 9)
                    {
                        allStates[idx, j] = (i & 3);
                        i >>= 2;
                        j++;
                    }
                    idx++;
                }
                c++;
            }
            return allStates;
        }
        public List<State> GetValid(int player, float reward, float penalty, float drawVal)
        {
            Assert.IsTrue(IsValidPlayer(player));

            int[,] allStates = EnumerateAll();
            List<State> validStates = new List<State>();

            for (int i = 0; i < allStates.GetLength(0); i++)
            {
                int zero = 0;
                int one = 0;
                int two = 0;
                int[] sequence = new int[9];

                for (int j = 0; j < allStates.GetLength(1); j++)
                {
                    if (allStates[i, j] == 0)
                        zero += 1;
                    if (allStates[i, j] == 1)
                        one += 1;
                    if (allStates[i, j] == 2)
                        two += 1;
                    sequence[j] = allStates[i, j];
                }
                if (one <= 5 && two <= 4 && one >= two && !(one-two >= 2))
                {
                    State state = new State(sequence);
                    validStates.Add(state);
                }
            }
            for (int i = validStates.Count - 1; i > -1; i--)
            {
                if (rules.CheckWinner(validStates[i], 1) && rules.CheckWinner(validStates[i], 2))
                    validStates.RemoveAt(i);
            }

            SetBoardProbabilities(player, validStates, reward, penalty, drawVal);

            return validStates;
        }
        public List<State> GetValid(int player)
        {
            Assert.IsTrue(IsValidPlayer(player));

            int[,] allStates = EnumerateAll();
            List<State> validStates = new List<State>();

            for (int i = 0; i < allStates.GetLength(0); i++)
            {
                int zero = 0;
                int one = 0;
                int two = 0;
                int[] sequence = new int[9];

                for (int j = 0; j < allStates.GetLength(1); j++)
                {
                    if (allStates[i, j] == 0)
                        zero += 1;
                    if (allStates[i, j] == 1)
                        one += 1;
                    if (allStates[i, j] == 2)
                        two += 1;
                    sequence[j] = allStates[i, j];
                }
                if (one <= 5 && two <= 4 && one >= two && !(one - two >= 2))
                {
                    State state = new State(sequence);
                    validStates.Add(state);
                }
            }
            for (int i = validStates.Count - 1; i > -1; i--)
            {
                if (rules.CheckWinner(validStates[i], 1) && rules.CheckWinner(validStates[i], 2))
                    validStates.RemoveAt(i);
            }

            SetBoardProbabilities(player, validStates);

            return validStates;
        }
        public List<State> GetValid()
        {
            int[,] allStates = EnumerateAll();
            List<State> validStates = new List<State>();
            for (int i = 0; i < allStates.GetLength(0); i++)
            {
                int zero = 0;
                int one = 0;
                int two = 0;
                int[] sequence = new int[9];

                for (int j = 0; j < allStates.GetLength(1); j++)
                {
                    if (allStates[i, j] == 0)
                        zero += 1;
                    if (allStates[i, j] == 1)
                        one += 1;
                    if (allStates[i, j] == 2)
                        two += 1;
                    sequence[j] = allStates[i, j];
                }
                if (one <= 5 && two <= 4 && one >= two && !(one - two >= 2))
                {
                    State state = new State(sequence);
                    validStates.Add(state);
                }
            }
            for (int i = validStates.Count - 1; i > -1; i--)
            {
                if (rules.CheckWinner(validStates[i], 1) && rules.CheckWinner(validStates[i], 2))
                    validStates.RemoveAt(i);
            }

            SetBoardProbabilitiesUniform(validStates, 0.5f);
            return validStates;
        }
        private void SetBoardProbabilitiesUniform(List<State> states, float stateValue)
        {
            foreach(State s in states)
            {
                s.SetValue(stateValue);
            }
        }
        private void SetBoardProbabilities(int player, List<State> states, float reward, float penalty, float drawVal)
        {
            if (player == 1)
            {
                for (int i = 0; i < states.Count; i++)
                {
                    if (rules.CheckGameOver(states[i]))
                    {
                        if (rules.CheckWinner(states[i], 2))
                            states[i].SetValue(penalty);
                        else if (rules.CheckWinner(states[i], 1))
                            states[i].SetValue(reward);
                        else if (rules.CheckFull(states[i]))
                            states[i].SetValue(drawVal);
                    }
                    else
                    {
                        states[i].SetValue(0.5f);
                    }

                }
            }
            else if (player == 2)
            {
                for (int i = 0; i < states.Count; i++)
                {
                    if (rules.CheckGameOver(states[i]))
                    {
                        if (rules.CheckWinner(states[i], 1))
                            states[i].SetValue(penalty);
                        else if (rules.CheckWinner(states[i], 2))
                            states[i].SetValue(reward);
                        else if (rules.CheckFull(states[i]))
                            states[i].SetValue(drawVal);
                    }
                    else
                    {
                        states[i].SetValue(0.5f);
                    }

                }
            }
        }
        private void SetBoardProbabilities(int player, List<State> states)
        {
            if (player == 1)
            {
                for (int i = 0; i < states.Count; i++)
                {
                    if (rules.CheckGameOver(states[i]))
                    {
                        if (rules.CheckWinner(states[i], 2))
                            states[i].SetValue(0.0f);
                        else if (rules.CheckWinner(states[i], 1))
                            states[i].SetValue(1.0f);
                        else if (rules.CheckFull(states[i]))
                            states[i].SetValue(0.0f);
                    }
                    else
                    {
                        states[i].SetValue(0.5f);
                    }

                }
            }
            else if (player == 2)
            {
                for (int i = 0; i < states.Count; i++)
                {
                    if (rules.CheckGameOver(states[i]))
                    {
                        if (rules.CheckWinner(states[i], 1))
                            states[i].SetValue(0.0f);
                        else if (rules.CheckWinner(states[i], 2))
                            states[i].SetValue(1.0f);
                        else if (rules.CheckFull(states[i]))
                            states[i].SetValue(0.0f);
                    }
                    else
                    {
                        states[i].SetValue(0.5f);
                    }

                }
            }
        }
        private bool IsValidPlayer(int player)
        {
            if (player == 1 || player == 2)
                return true;
            return false;
        }
    }
}
