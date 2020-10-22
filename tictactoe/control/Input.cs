using System;
using System.Collections.Generic;
using System.Text;

namespace tictactoe
{
    class Input
    {
        public int? ParseStart(string input, int choices)
        {
            bool isValid = int.TryParse(input, out int choice);
            if (isValid)
                if(choice > 0 && choice <= choices)
                return choice;
            return null;
        }
        public bool IsValidPlayer(string playerInput, out int player)
        {
            bool isValidPlayer = int.TryParse(playerInput, out player);
            if (isValidPlayer)
                if (player == 1 || player == 2)
                    return true;
            return false;
        }
        public bool IsValidNumber(string numberToCheck, out int result)
        {
            bool isValid = int.TryParse(numberToCheck, out result);
            if (isValid)
                return true;
            return false;

        }
        public bool IsTrained(Agent[] agents)
        {
            if (agents[0] != null)
                return true;
            return false;
        }
        public bool IsValidAction(string input,State state, out int action)
        {
            bool isValid = int.TryParse(input, out action);
            if (isValid)
                if (state.Sequence[action - 1] == 0)
                    return true;
            return false;
        }
    }
}
