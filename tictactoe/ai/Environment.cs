using System;
using System.Collections.Generic;
using System.Text;

namespace tictactoe
{
    class Environment
    {
        float ALPHA = 0.1f;
        double EPSILON = 0.1;
        public Agent[] Train(int episodes)
        {
            Random random = new Random(42);
            State state = new State(new int[9]);
            Rules rules = new Rules();
            Agent player1 = new Agent(1, state);
            Agent player2 = new Agent(2, state);
            Draw draw = new Draw();
            bool gameOver = false;
            int counter = 0;
            int player1Wins = 0;
            int player2Wins = 0;
            int drawMatch = 0;


            draw.Clear();
            draw.TrainingTitle();
            while (counter < episodes)
            {
                int round = 0;
                draw.SetPosition(45, 11);
                draw.Progress(counter, episodes);
                draw.Wins(player1Wins, player2Wins, drawMatch);
                while (!gameOver)
                {
                    if (!gameOver)
                    {
                        player2.SetStatePrevious(state);
                        if (random.NextDouble() <= EPSILON)
                        {
                            int playerAction = player1.StepForwardEpsilon(state);
                            state.Set(playerAction, 1);
                        }
                        else
                        {
                            int playerAction = player1.StepForward(state);
                            state.Set(playerAction, 1);
                            if(round > 0)
                                player1.Train(state, ALPHA);
                            
                        }
                        if (rules.CheckGameOver(state))
                            if (rules.CheckWinner(state, 1))
                            {
                                player1Wins++;
                                gameOver = true;
                            }
                            else
                            {
                                drawMatch++;
                                gameOver = true;
                            }
                    }

                    if (!gameOver)
                    {
                        player1.SetStatePrevious(state);
                        if (random.NextDouble() <= EPSILON)
                        {
                            int playerAction = player2.StepForwardEpsilon(state);
                            state.Set(playerAction, 2);
                        }
                        else
                        {
                            int playerAction = player2.StepForward(state);
                            state.Set(playerAction, 2);
                            player2.Train(state, ALPHA);
                        }
                        if (rules.CheckGameOver(state))
                        {
                            if (rules.CheckWinner(state, player2.Player))
                            {
                                player2Wins++;
                                gameOver = true;
                            }
                            else
                            {
                                drawMatch++;
                                gameOver = true;
                            }
                        }
                    }
                    round++;
                }

                if (gameOver)
                {
                    state = new State(new int[9]);
                    counter++;
                    gameOver = !gameOver;
                }
            }

            return new Agent[] {player1, player2};
        }
    }
}
