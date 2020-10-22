using System;
using System.Collections.Generic;
using System.Text;

namespace tictactoe
{
    class Draw
    {
        string gameTitle = @"                     d8888 888          888           88888888888                
                                   d88888 888          888               888                    
                                  d88P888 888          888               888                    
                                 d88P 888 888 88888b.  88888b.   8888b.  888   .d88b.   .d88b.  
                                d88P  888 888 888  88b 888  88b      88b 888  d88''88b d8P  Y8b
                               d88P   888 888 888  888 888  888 .d888888 888  888  888 88888888 
                              d8888888888 888 888 d88P 888  888 888  888 888  Y88..88P Y8b
                             d88P     888 888 88888P   888  888  Y888888 888    Y88P     Y8888  
                                              888                                               
                                              888                                               
                                              888                                               ";
        public void Start()
        {
            string startInfoText = "Välkommen till AlphaToe!";
            Console.WriteLine(String.Format("{0, " + ((Console.WindowWidth / 2) + (startInfoText.Length / 2)) + "}", startInfoText));
            Options();
        }
        public void Options()
        {
            string option1 = "1. Spela mot Människa";
            string option2 = "2. Spela mot lätt AI";
            string option3 = "3. Spela mot svår AI";
            string option4 = "4. Träna en egen AI";
            Console.WriteLine(String.Format("{0, " + ((Console.WindowWidth / 2) + 10) + "}", option1));
            Console.WriteLine(String.Format("{0, " + ((Console.WindowWidth / 2) + 10) + "}", option2));
            Console.WriteLine(String.Format("{0, " + ((Console.WindowWidth / 2) + 10) + "}", option3));
            Console.WriteLine(String.Format("{0, " + ((Console.WindowWidth / 2) + 10) + "}", option4));

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
            string text = "Amount of Episodes:";
            Console.Write(String.Format("{0, " + ((Console.WindowWidth / 2) + 10) + "}", text));
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
            Console.WriteLine($"Progress: {count} / {total} Games Played");

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
        public void ResetFGColor()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }
        public void GameTitle()
        {
            SetPosition(15, 1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(gameTitle);
            ResetFGColor();
            Divide();
        }
        public void Divide()
        {
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
        }
    }
}
