﻿using System;
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

        public void Run()
        {
            Environment environment = new Environment();
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
                            RunHumanPlay();
                            break;
                        case (int)GameOptions.EasyAiPlay:
                            RunEasyAiPlay(rules, draw, input);
                            break;
                        case (int)GameOptions.HardAiPlay:
                            Console.WriteLine("Case 2");
                            break;
                        case (int)GameOptions.TrainAi:
                            draw.EnterEpisodes();
                            bool isValid = input.IsValidNumber(Console.ReadLine(), out int episodes);
                            if (isValid)
                                lastAgents = TrainAgents(episodes);
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

        private void RunHumanPlay()
        {

        }
        private void RunAiPlay(Agent[] agents, Rules rules, Draw draw, Input input)
        {
            bool gameOver = false;
            draw.ChooseOpponent();
            while (true)
            {
                bool isValidPlayer = input.IsValidPlayer(Console.ReadLine(), out int chosenPlayer);
                if (isValidPlayer)
                {
                    State state = new State(new int[9]);
                    if (chosenPlayer == 1)
                    {
                        Agent player2 = agents[1];
                        while (true)
                        {
                            while (true && !gameOver)
                            {
                                draw.GameState(state);
                                while (true && !gameOver)
                                {
                                    bool isValidMove = int.TryParse(Console.ReadLine(), out int playerAction);
                                    if (isValidMove)
                                    {
                                        state.Set(playerAction, 1);
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
                                    player2.DrawMaxProbs(state);
                                    int player2Action = player2.StepForward(state);
                                    state.Set(player2Action, 2);
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
                            Console.ReadLine();
                            gameOver = !gameOver;
                            state = new State(new int[9]);
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
                                    player1.DrawProbs(player1.GetStateFromExt(state));
                                    int player2Action = player1.StepForward(state);
                                    state.Set(player2Action, 1);
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
                                draw.GameState(state);
                                while (true && !gameOver)
                                {
                                    bool isValidMove = input.IsValidAction(Console.ReadLine(), state, out int playerAction);
                                    if (isValidMove)
                                    {
                                        state.Set(playerAction, 2);
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
                            Console.ReadLine();
                            gameOver = !gameOver;
                            state = new State(new int[9]);
                        }
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
                        Agent easyAgent = new Agent(2);
                        while (true && !gameOver)
                        {
                            draw.GameState(state);
                            while (true && !gameOver)
                            {
                                bool isValidAction = input.IsValidAction(Console.ReadLine(), state, out int playerAction);
                                if (isValidAction)
                                {
                                    state.Set(playerAction, 1);
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
                        Agent easyAgent = new Agent(1);
                        while (true && !gameOver)
                        {
                            if (!gameOver)
                            {
                                int player2Action = easyAgent.StepForwardRnd(state);
                                state.Set(player2Action, 1);
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
                            draw.GameState(state);
                            while (true && !gameOver)
                            {
                                bool isValidMove = input.IsValidAction(Console.ReadLine(), state, out int playerAction);
                                if (isValidMove)
                                {
                                    state.Set(playerAction, 2);
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
        private Agent[] TrainAgents(int episodes)
        {
            Environment environment = new Environment();
            Agent[] agents = environment.Train(episodes);
            return agents;
        }
    }
}


