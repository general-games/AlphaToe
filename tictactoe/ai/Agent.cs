using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace tictactoe
{
    class Agent
    {
        public int Player { get; set; }

        private Initialize init = new Initialize();
        private List<State> states;
        private State statePrevious;
        Random random;

        public Agent(int player, State startState)
        {
            this.Player = player;
            random = new Random(42);
            states = InitStates();
            statePrevious = GetStateFromExt(startState);
        }
        public Agent(int player)
        {
            this.Player = player;
            random = new Random(42);
            states = InitStates();
        }

        //Init functions
        public List<State> InitStates()
        {
            return init.GetValid(Player);
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
           statePrevious.SetProb(TemporalDifference(statePrevious, GetStateFromExt(statePrime), ALPHA));
        }
        private float TemporalDifference(State state, State statePrime, float ALPHA)
        {
            float newValue = state.Probability + (ALPHA * (statePrime.Probability - state.Probability));
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
        private float GetStateValue(State state)
        {
            return state.Probability;
        }
        private List<State> GetNextValidStates(State state)
        {
            List<State> filteredStates = states.Where(s => s.MoveNum == (state.MoveNum + 1)).OrderBy(s => -s.Probability).ToList();
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
                else if (i > 0 && availableStates[i - 1].Probability == (availableStates[i].Probability))
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
        public State GetStateFromExt(State state)
        {
            State internalState = states.First(s => s.GetSequence() == state.GetSequence());
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
        public void DrawMaxProbs(State state)
        {
            List<State> availableStates = GetNextValidStates(state);
            List<State> maxStates = GetMaxStates(availableStates);
            foreach (State s in maxStates)
            {
                Console.WriteLine($"Action:{GetActionFromStates(state, s)} Value:{s.Probability}");
            }

        }
        public void DrawProbs(State state)
        {
            List<State> availableStates = GetNextValidStates(state);
            foreach (State s in availableStates)
            {
                Console.WriteLine(s.Probability);
            }
        }
    }
}
