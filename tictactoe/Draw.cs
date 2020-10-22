using System;
using System.Collections.Generic;
using System.Text;

namespace tictactoe
{
    class Draw
    {
        public void Start()
        {
            Console.WriteLine("Welcome to AlphaToe, Where you can train a state of the art AI to battle against you in the game of Tic Tac Toe!");
        }

        public void Clear()
        {
            Console.Clear();
        }

        public void GameState(State state)
        {
            for (int i = 0; i < state.Sequence.Length; i++)
            {
                Console.Write(state.Sequence[i]);
            }
            Console.WriteLine();
        }

        public void EnterEpisodes()
        {
            Console.Write("Please enter how many rounds of play your agent will train during: ");
        }

        public void PlayerWins(int player)
        {
            if (player == 1)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Player One Has Won!");
                Console.ForegroundColor = ConsoleColor.White;

            }
            if (player == 2)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine("Player Two Has Won!");
                Console.ForegroundColor = ConsoleColor.White;
            }

        }

        public void MatchDraw()
        {
            Console.WriteLine("It's a Draw!");
        }
        public void NotValidInput()
        {
            Console.WriteLine("Not a Valid Input!");
        }
        public void SetPosition(int left, int top)
        {
            Console.SetCursorPosition(left, top);
        }
        public void ResetPosition()
        {
            Console.SetCursorPosition(0, 0);
        }
        public void Progress(int count, int total)
        {
            SetPosition(45, 11);
            Console.WriteLine($"Progress: {count} / {total}");

        }
        public void Wins(int player1Wins, int player2Wins,int drawMatch)
        {
            SetPosition(30, 12);
            Console.WriteLine($"Player 1 Wins:{player1Wins}  Draw Games:{drawMatch}  Player 2 Wins:{player2Wins}");
        }

        public void ChooseOpponent()
        {
            Console.Write("Do you want to play as player 1, or player 2?:");
        }

    }
}
