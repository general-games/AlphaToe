using System;
using System.Collections.Generic;
using System.Text;

namespace tictactoe
{
    class Game
    {
        enum GameOptions
        {
            Blank,
            HumanPlay,
            EasyAiPlay,
            HardAiPlay,
            TrainAi,
            PlayAi
        }
        enum Players
        {
            Blank,
            Player1,
            Player2
        }
        Random random;
        public Game()
        {
            this.random = new Random(42);
        }
        public void Run()
        {
            Environment environment = new Environment(random);
            Rules rules = new Rules();
            Draw draw = new Draw();
            Input input = new Input();
            Agent[] lastAgents = new Agent[2];

            while (true)
            {
                draw.Clear();
                draw.GameTitle();
                draw.Start();
                int? startInput = input.ParseStart(Console.ReadLine(), 5);
                if (startInput != null)
                {
                    switch (startInput)
                    {
                        case (int)GameOptions.HumanPlay:
                            draw.Clear();
                            draw.GameTitle();
                            RunHumanPlay(rules, draw, input);
                            break;
                        case (int)GameOptions.EasyAiPlay:
                            RunEasyAiPlay(rules, draw, input);
                            break;
                        case (int)GameOptions.HardAiPlay:
                            draw.NotImplemented();
                            Console.ReadLine();
                            break;
                        case (int)GameOptions.TrainAi:
                            draw.EnterEpisodes();
                            bool isValid = input.IsValidNumber(Console.ReadLine(), out int episodes);
                            if (isValid)
                                lastAgents = TrainAgents(episodes, random);
                            break;
                        case (int)GameOptions.PlayAi:
                            if (input.IsTrained(lastAgents))
                                RunAiPlay(lastAgents, rules, draw, input);
                            else
                            {
                                draw.NoTrainedAgents();
                                Console.ReadLine();
                            }
                            break;

                    }
                }
            }

        }

        private void RunHumanPlay(Rules rules, Draw draw, Input input)
        {
            bool gameOver = false;
            while (true)
            {
                State state = new State(new int[9]);
                while (true && !gameOver)
                {
                    while (true && !gameOver)
                    {

                        DrawBase(draw, state);
                        draw.EnterAction((int)Players.Player1);
                        bool isValidAction = input.IsValidAction(Console.ReadLine(), state, out int playerAction);
                        if (isValidAction)
                        {
                            state.Set(playerAction, 1);
                            draw.Board(state);
                            if (rules.CheckGameOver(state))
                                if (rules.CheckWinner(state, 1))
                                {
                                    draw.PlayerWins(1);
                                    gameOver = true;
                                }
                                else
                                {
                                    draw.MatchDraw();
                                    gameOver = true;
                                }
                            break;
                        }
                    }
                    while (true && !gameOver)
                    {
                        DrawBase(draw, state);
                        draw.EnterAction((int)Players.Player2);
                        bool isValidAction = input.IsValidAction(Console.ReadLine(), state, out int playerAction);
                        if (isValidAction)
                        {
                            state.Set(playerAction, 2);
                            draw.Board(state);
                            if (rules.CheckGameOver(state))
                                if (rules.CheckWinner(state, 2))
                                {
                                    draw.PlayerWins(1);
                                    gameOver = true;
                                }
                                else
                                {
                                    draw.MatchDraw();
                                    gameOver = true;
                                }
                            break;
                        }
                    }
                }
                break;
            }
        }
        private void RunAiPlay(Agent[] agents, Rules rules, Draw draw, Input input)
        {
            bool gameOver = false;
            draw.ChooseOpponent();
            bool isValidPlayer = input.IsValidPlayer(Console.ReadLine(), out int chosenPlayer);
            if (isValidPlayer)
            {
                State state = new State();
                draw.Clear();
                draw.GameTitle();
                if (chosenPlayer == 1)
                {
                    Agent player2 = agents[1];
                    while (true)
                    {
                        while (true && !gameOver)
                        {
                            while (true && !gameOver)
                            {
                                draw.Board(state);
                                draw.EnterAction();
                                bool isValidMove = int.TryParse(Console.ReadLine(), out int playerAction);
                                if (isValidMove)
                                {
                                    state.Set(playerAction, 1);
                                    draw.Board(state);
                                    if (rules.CheckGameOver(state))
                                        if (rules.CheckWinner(state, 1))
                                        {
                                            draw.PlayerWins(1);
                                            gameOver = true;
                                        }
                                        else
                                        {
                                            draw.MatchDraw();
                                            gameOver = true;
                                        }
                                    break;
                                }
                                else
                                {
                                    draw.NotValidInput();
                                }
                            }
                            if (!gameOver)
                            {
                                int player2Action = player2.StepForward(state);
                                state.Set(player2Action, 2);
                                draw.Board(state);
                                if (rules.CheckGameOver(state))
                                    if (rules.CheckWinner(state, 2))
                                    {
                                        draw.PlayerWins(2);
                                        gameOver = true;
                                    }
                                    else
                                    {
                                        draw.MatchDraw();
                                        gameOver = true;
                                    }
                            }
                        }
                        break;
                    }
                }
                else if (chosenPlayer == 2)
                {
                    Agent player1 = agents[0];
                    while (true)
                    {
                        while (true && !gameOver)
                        {
                            if (!gameOver)
                            {
                                int player2Action = player1.StepForward(state);
                                state.Set(player2Action, 1);
                                draw.Board(state);
                                if (rules.CheckGameOver(state))
                                    if (rules.CheckWinner(state, 1))
                                    {
                                        draw.PlayerWins(1);
                                        gameOver = true;
                                    }
                                    else
                                    {
                                        draw.MatchDraw();
                                        gameOver = true;
                                    }
                            }
                            while (true && !gameOver)
                            {
                                draw.EnterAction();
                                bool isValidMove = input.IsValidAction(Console.ReadLine(), state, out int playerAction);
                                if (isValidMove)
                                {
                                    state.Set(playerAction, 2);
                                    draw.Board(state);
                                    if (rules.CheckGameOver(state))
                                        if (rules.CheckWinner(state, 2))
                                        {
                                            draw.PlayerWins(2);
                                            gameOver = true;
                                        }
                                        else
                                        {
                                            draw.MatchDraw();
                                            gameOver = true;
                                        }
                                    break;
                                }
                                else
                                {
                                    draw.NotValidInput();
                                }
                            }

                        }
                        break;
                    }
                }
            }
        }
        private void RunEasyAiPlay(Rules rules, Draw draw, Input input)
        {
            bool gameOver = false;
            draw.ChooseOpponent();
            while (true)
            {
                bool isValidPlayer = input.IsValidPlayer(Console.ReadLine(), out int chosenPlayer);
                if (isValidPlayer)
                {
                    if (chosenPlayer == 1)
                    {
                        State state = new State(new int[9]);
                        Agent easyAgent = new Agent(2, random);
                        while (true && !gameOver)
                        {
                            DrawBase(draw, state);
                            while (true && !gameOver)
                            {
                                draw.EnterAction();
                                bool isValidAction = input.IsValidAction(Console.ReadLine(), state, out int playerAction);
                                if (isValidAction)
                                {
                                    state.Set(playerAction, 1);
                                    draw.Board(state);
                                    if (rules.CheckGameOver(state))
                                        if (rules.CheckWinner(state, 1))
                                        {
                                            draw.PlayerWins(1);
                                            gameOver = true;
                                        }
                                        else
                                        {
                                            draw.MatchDraw();
                                            gameOver = true;
                                        }
                                    break;
                                }
                            }
                            if (!gameOver)
                            {
                                int player2Action = easyAgent.StepForwardRnd(state);
                                state.Set(player2Action, 2);
                                draw.Board(state);
                                if (rules.CheckGameOver(state))
                                    if (rules.CheckWinner(state, 2))
                                    {
                                        draw.PlayerWins(2);
                                        gameOver = true;
                                    }
                                    else
                                    {
                                        draw.MatchDraw();
                                        gameOver = true;
                                    }
                            }

                        }
                        break;
                    }
                    else if (chosenPlayer == 2)
                    {
                        State state = new State(new int[9]);
                        Agent easyAgent = new Agent(1, random);
                        while (true && !gameOver)
                        {
                            DrawBase(draw, state);
                            if (!gameOver)
                            {
                                int player2Action = easyAgent.StepForwardRnd(state);
                                state.Set(player2Action, 1);
                                draw.Board(state);
                                if (rules.CheckGameOver(state))
                                    if (rules.CheckWinner(state, 1))
                                    {
                                        draw.PlayerWins(1);
                                        gameOver = true;
                                    }
                                    else
                                    {
                                        draw.MatchDraw();
                                        gameOver = true;
                                    }

                            }
                            while (true && !gameOver)
                            {
                                draw.EnterAction();
                                bool isValidMove = input.IsValidAction(Console.ReadLine(), state, out int playerAction);
                                if (isValidMove)
                                {
                                    state.Set(playerAction, 2);
                                    draw.Board(state);
                                    if (rules.CheckGameOver(state))
                                        if (rules.CheckWinner(state, 2))
                                        {
                                            draw.PlayerWins(2);
                                            gameOver = true;
                                        }
                                        else
                                        {
                                            draw.MatchDraw();
                                            gameOver = true;
                                        }
                                    break;
                                }
                            }

                        }
                        break;
                    }
                }
            }

        }
        private Agent[] TrainAgents(int episodes, Random random)
        {
            Environment environment = new Environment(random);
            Agent[] agents = environment.Train(episodes);
            return agents;
        }
        public void DrawBase(Draw draw, State state)
        {
            draw.Clear();
            draw.GameTitle();
            draw.Board(state);
        }
    }
}


