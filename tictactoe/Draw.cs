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

        string trainingTitle = @"         88888888888 8888888b.         d8888 8888888 888b    888 8888888 888b    888  .d8888b.  
                     888     888   Y88b       d88888   888   8888b   888   888   8888b   888 d88P  Y88b 
                     888     888    888      d88P888   888   88888b  888   888   88888b  888 888    888 
                     888     888   d88P     d88P 888   888   888Y88b 888   888   888Y88b 888 888        
                     888     8888888P      d88P  888   888   888 Y88b888   888   888 Y88b888 888  88888 
                     888     888 T88b     d88P   888   888   888  Y88888   888   888  Y88888 888    888 
                     888     888  T88b   d8888888888   888   888   Y8888   888   888   Y8888 Y88b  d88P 
                     888     888   T88b d88P     888 8888888 888    Y888 8888888 888    Y888   Y8888P88 ";
        public void Start()
        {
            string startInfoText = "Välkommen till AlphaToe!";
            Console.WriteLine(String.Format("{0, " + ((Console.WindowWidth / 2) + (startInfoText.Length / 2)) + "}", startInfoText));
            Options();
        }
        public void Options()
        {
            string option1 = "1. Play two Person Game";
            string option2 = "2. Play Easy AI";
            string option3 = "3. Play Hard AI";
            string option4 = "4. Train AI";
            string option5 = "5. Play your AI";
            Console.WriteLine(String.Format("{0, " + ((Console.WindowWidth / 2) + 10) + "}", option1));
            Console.WriteLine(String.Format("{0, " + ((Console.WindowWidth / 2) + 10) + "}", option2));
            Console.WriteLine(String.Format("{0, " + ((Console.WindowWidth / 2) + 10) + "}", option3));
            Console.WriteLine(String.Format("{0, " + ((Console.WindowWidth / 2) + 10) + "}", option4));
            Console.WriteLine(String.Format("{0, " + ((Console.WindowWidth / 2) + 10) + "}", option5));


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
            Console.ReadLine();
        }

        public void MatchDraw()
        {
            Console.WriteLine("It's a Draw!");
            Console.ReadLine();
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
            Console.WriteLine($"Progress: {count} / {total} Games Played");
        }
        public void Wins(int player1Wins, int player2Wins,int drawMatch)
        {
            SetPosition(35, 12);
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
        public void TrainingTitle()
        {
            SetPosition(8, 1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(trainingTitle);
            ResetFGColor();
            Divide();
        }
        public void NoTrainedAgents()
        {
            string text = "No Trained Agents. Press enter to continue";
            Console.WriteLine(String.Format("{0, " + ((Console.WindowWidth / 2) + 10) + "}", text));
        }

        public void Divide()
        {
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
        }
    }
}
