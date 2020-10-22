using System;
using System.Collections.Generic;

namespace tictactoe
{
    //Sort away last bad game board states
    class Program
    {
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Run();
        }
    }
}
