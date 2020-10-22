using System;
using System.Collections.Generic;
using System.Text;

namespace tictactoe
{
    class Path
    {
        string allStates = $"..\\..\\..\\states\\allStates.txt";

        public string States()
        {
            return String.Copy(allStates);
        }
    }
}
