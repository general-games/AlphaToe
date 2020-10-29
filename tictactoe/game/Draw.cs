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
            string startInfoText = "Welcome to AlphaToe!";
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
            Console.WriteLine(String.Format("{0, " + ((Console.WindowWidth / 2) + (option1.Length / 2)) + "}", option1));
            Console.WriteLine(String.Format("{0, " + ((Console.WindowWidth / 2) + (option2.Length / 2)) + "}", option2));
            Console.WriteLine(String.Format("{0, " + ((Console.WindowWidth / 2) + (option3.Length / 2)) + "}", option3));
            Console.WriteLine(String.Format("{0, " + ((Console.WindowWidth / 2) + (option4.Length / 2)) + "}", option4));
            Console.WriteLine(String.Format("{0, " + ((Console.WindowWidth / 2) + (option5.Length / 2)) + "}", option5));


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
        public void Board(State state)
        {
            int width = Console.WindowWidth/2-9;
            int height = 14;
            string board = $"      |       |     \n  {Translate(state.Sequence[0])}   |   {Translate(state.Sequence[1])}   |   {Translate(state.Sequence[2])} \n_____ | _____ | _____\n      |       |\n  {Translate(state.Sequence[3])}   |   {Translate(state.Sequence[4])}   |   {Translate(state.Sequence[5])}\n_____ | _____ | _____\n      |       |\n  {Translate(state.Sequence[6])}   |   {Translate(state.Sequence[7])}   |   {Translate(state.Sequence[8])}\n      |       | ";
            string b1 = $"      |       |     ";
            string b2 = $"  {Translate(state.Sequence[0])}   |   {Translate(state.Sequence[1])}   |   {Translate(state.Sequence[2])} ";
            string b3 = $"_____ | _____ | _____";
            string b4 = $"      |       |";
            string b5 = $"  {Translate(state.Sequence[3])}   |   {Translate(state.Sequence[4])}   |   {Translate(state.Sequence[5])}";
            string b6 = $"_____ | _____ | _____";
            string b7 = $"      |       |";
            string b8 = $"  {Translate(state.Sequence[6])}   |   {Translate(state.Sequence[7])}   |   {Translate(state.Sequence[8])}";
            string b9 = $"      |       | ";
            SetPosition(width, height+1);
            Console.WriteLine(b1);
            SetPosition(width, height+2);
            Console.WriteLine(b2);
            SetPosition(width, height+3);
            Console.WriteLine(b3);
            SetPosition(width, height+4);
            Console.WriteLine(b4);
            SetPosition(width, height+5);
            Console.WriteLine(b5);
            SetPosition(width, height+6);
            Console.WriteLine(b6);
            SetPosition(width, height+7);
            Console.WriteLine(b7);
            SetPosition(width, height+8);
            Console.WriteLine(b8);
            SetPosition(width, height+9);
            Console.WriteLine(b9);
        }
        public void EnterAction()
        {
            Console.WriteLine();
            string text = "Enter Move:";
            Console.Write(String.Format("{0, " + ((Console.WindowWidth / 2) + (text.Length / 2)) + "}", text));
        }
        public void EnterAction(int player)
        {
            string text = $"Enter Move Player{player}: ";
            Console.Write(String.Format("{0, " + ((Console.WindowWidth / 2) + (text.Length / 2)) + "}", text));
        }
        public string Translate(int action)
        {
            if (action == 0)
                return "-";
            if (action == 1)
                return "O";
            if (action == 2)
                return "X";
            else return null;
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
                string text = "Player One Has Won!";
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine(String.Format("{0, " + ((Console.WindowWidth / 2) + 11) + "}", text));
                Console.ForegroundColor = ConsoleColor.White;

            }
            if (player == 2)
            {
                string text = "Player Two Has Won!";
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine(String.Format("{0, " + ((Console.WindowWidth / 2) + 11) + "}", text));
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
            string text = "Not A Valid Input!";
            Console.WriteLine(String.Format("{0, " + ((Console.WindowWidth / 2) + (text.Length / 2)) + "}", text));
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
            string text = $"Progress: {count} / {total} Games Played";
            Console.WriteLine(String.Format("{0, " + ((Console.WindowWidth / 2) + (text.Length/2)) + "}", text));

        }
        public void Wins(int player1Wins, int player2Wins,int drawMatch)
        {
            string text = $"Player 1 Wins:{player1Wins}  Draw Games:{drawMatch}  Player 2 Wins:{player2Wins}";
            Console.WriteLine(String.Format("{0, " + ((Console.WindowWidth / 2) + (text.Length/2)) + "}", text));

        }
        public void ChooseOpponent()
        {
            string text = "Play as 1 or 2:";
            Console.Write(String.Format("{0, " + ((Console.WindowWidth / 2) + (text.Length / 2)) + "}", text));
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
        public void NotImplemented()
        {
            Console.WriteLine("Feature not yet implemented! Press return to continue");
        }
    }
}
