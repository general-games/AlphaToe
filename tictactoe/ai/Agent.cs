using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace tictactoe
{
    class Agent
    {
        public int Player { get; set; }
        public float Reward { get;private set; }
        public float Penalty { get;private set; }
        public float DrawVal { get;private set; }

        private Initialize init = new Initialize();
        private List<State> states;
        private State statePrevious;
        Random random;

        public Agent(int player, State startState, Random random, float reward, float penalty, float drawVal)
        {
            this.Player = player;
            this.random = random;
            this.Reward = reward;
            this.Penalty = penalty;
            this.DrawVal = drawVal;
            states = InitStates(init,player, reward, penalty, drawVal);
            statePrevious = GetStateFromExt(startState);
        }
        public Agent(State startState, Random random, float reward, float penalty, float drawVal)
        {
            states = InitStates(init);
            statePrevious = GetStateFromExt(startState);
            this.random = random;
            this.Reward = reward;
            this.Penalty = penalty;
            this.DrawVal = drawVal;
        }
        public Agent(int player, Random random)
        {
            this.Player = player;
            this.random = random;
            states = InitStates(player);
        }

        //Meta functions
        private List<State> InitStates(Initialize init, int player ,float winVal, float loseVal, float drawVal)
        {
            return init.GetValid(player, winVal, loseVal, drawVal);
        }
        private List<State> InitStates(Initialize init)
        {
            return init.GetValid();
        } 
        private List<State> InitStates(int player)
        {
            return init.GetValid(player);
        }
        public List<State> GetPolicy()
        {
            return states;
        }
        //Action functions
        public int StepForwardRnd(State state)
        {
            int randomAction = random.Next(1, 9 + 1);
            if (IsValidAction(randomAction, state))
                return randomAction;
            else
                return StepForwardRnd(state);
        }
        public int StepForwardEpsilon(State state)
        {
            List<State> availableStates = GetNextValidStates(state);
            {
                availableStates = Shuffle(availableStates);
                int action = GetActionFromStates(state, availableStates[0]);
                return action;
            }
        }
        public int StepForward(State state)
        {
            List<State> availableStates = GetNextValidStates(state);
            List<State> maxStates = GetMaxStates(availableStates);
            maxStates = Shuffle(maxStates);

            for (int i = 0; i < maxStates.Count; i++)
            {
                int action = GetActionFromStates(state, maxStates[i]);
                if (IsValidAction(action, state))
                {
                    return action;
                }
                else
                    continue;
            }
            return 0;
        }
        //Training functions
        public void Train(State statePrime, float ALPHA)
        {
           statePrevious.SetValue(TemporalDifference(statePrevious, GetStateFromExt(statePrime), ALPHA));
        }
        public void TrainReward(float ALPHA)
        {
            statePrevious.SetValue(TemporalDifference(statePrevious, ALPHA, Reward));
        }
        public void TrainPenalty(float ALPHA)
        {
            statePrevious.SetValue(TemporalDifference(statePrevious, ALPHA, Penalty));
        }
        private float TemporalDifference(State state, State statePrime, float ALPHA)
        {
            float newValue = state.Value + (ALPHA * (statePrime.Value - state.Value));
            return newValue;
        }
        private float TemporalDifference(State state, float ALPHA, float reward)
        {
            float newValue = state.Value + (ALPHA * (reward - state.Value));
            return newValue;
        }
        //Control functions
        private bool IsValidStateTransition(State state, State statePrime)
        {
            int difference = 0;
            for (int i = 0; i < state.Sequence.Length; i++)
            {
                if (state.Sequence[i] != statePrime.Sequence[i])
                    difference++;
            }
            return !(difference > 1);
        }
        private int GetActionFromStates(State state, State statePrime)
        {
            for (int i = 0; i < state.Sequence.Length; i++)
            {
                if (state.Sequence[i] != statePrime.Sequence[i])
                    return i + 1;
            }
            return 0;
        }
        private bool IsValidAction(int action, State state)
        {
            return GetValidActions(state).Contains(action);
        }
        private List<int> GetValidActions(State state)
        {
            List<int> validMoves = new List<int>();
            for (int i = 0; i < state.Sequence.Length; i++)
            {
                if (state.Sequence[i] == 0)
                    validMoves.Add(i + 1);
            }
            return validMoves;

        }
        private List<State> GetNextValidStates(State state)
        {
            List<State> filteredStates = states.Where(s => s.MoveNum == (state.MoveNum + 1)).OrderBy(s => -s.Value).ToList();
            for (int i = filteredStates.Count - 1; i >= 0; i--)
            {
                int action = GetActionFromStates(state, filteredStates[i]);
                if (!IsValidAction(action, state))
                    filteredStates.RemoveAt(i);
            }
            for (int i = filteredStates.Count - 1; i >= 0; i--)
            {
                if (!IsValidStateTransition(state, filteredStates[i]))
                    filteredStates.RemoveAt(i);
            }
            return filteredStates;

        }
        public List<State> GetMaxStates(List<State> availableStates)
        {
            List<State> maxStates = new List<State>();
            for (int i = 0; i < availableStates.Count; i++)
            {
                if (i == 0)
                    maxStates.Add(availableStates[i]);
                else if (i > 0 && availableStates[i - 1].Value == (availableStates[i].Value))
                    maxStates.Add(availableStates[i]);
                else
                    break;
            }
            return maxStates;
        }
        public List<State> Shuffle(List<State> statesToShuffle)
        {
            return statesToShuffle.OrderBy(a => Guid.NewGuid()).ToList();
        }
        public State GetStateFromExt(State externalState)
        {
            State internalState = states.First(s => s.GetSequence() == externalState.GetSequence());
            return internalState;
        }
        public void SetStatePrevious(State state)
        {
            statePrevious = GetStateFromExt(state);
        }
   
        //Debug functions:
        public void DrawPrevious()
        {
            Console.WriteLine(statePrevious.GetSequence());
        }
        public void DrawMaxValue(State state)
        {
            List<State> availableStates = GetNextValidStates(state);
            List<State> maxStates = GetMaxStates(availableStates);
            foreach (State s in maxStates)
            {
                Console.WriteLine($"Action:{GetActionFromStates(state, s)} Value:{s.Value}");
            }

        }
        public void DrawValue(State state)
        {
            List<State> availableStates = GetNextValidStates(state);
            foreach (State s in availableStates)
            {
                Console.WriteLine(s.Value);
            }
        }
        public void DrawValueExt(State state)
        {
            State myState = GetStateFromExt(state);
            Console.WriteLine($"Prob: {myState.Value}");
        }

    }
}
