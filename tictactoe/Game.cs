using System;
using System.Collections.Generic;
using System.Text;

namespace tictactoe
{
    class Game
    {


        public void Run()
        {
            bool gameOver = false;
            Environment environment = new Environment();
            Rules rules = new Rules();
            Draw draw = new Draw();

            while (true)
            {
                draw.Clear();
                draw.Start();
                draw.EnterEpisodes();
                bool isValidNum = int.TryParse(Console.ReadLine(), out int episodes);
                if (isValidNum)
                {
                    Agent[] agents = environment.Train(episodes);
                    State state = new State(new int[9]);
                    draw.ChooseOpponent();
                    bool isValidPlayer = IsValidPlayer(Console.ReadLine(), out int chosenPlayer);
                    if (isValidPlayer)
                    {
                        if(chosenPlayer == 1)
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
                        else if(chosenPlayer == 2)
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
                                        bool isValidMove = int.TryParse(Console.ReadLine(), out int playerAction);
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
        }
        private bool IsValidPlayer(string playerInput, out int player)
        {
            bool isValidPlayer = int.TryParse(playerInput, out player);
            if (isValidPlayer)
                if(player == 1 || player == 2)
                    return true;
            return false;
        }
    }
}


