using System;
using System.Collections.Generic;
using System.Text;
using tictactoe.control;

namespace tictactoe
{
    class Environment
    {
        float ALPHA = 0.1f;
        double EPSILON = 0.1;
        public Agent[] Train(int episodes)
        {
            Random random = new Random(42);
            Repository repo = new Repository();
            State state = new State(new int[9]);
            Rules rules = new Rules();
            Agent player1 = new Agent(1, state);
            Agent player2 = new Agent(2, state);
            Draw draw = new Draw();
            Data data = new Data();

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
                draw.SetPosition(0, 11);
                draw.Progress(counter, episodes);
                draw.Wins(player1Wins, player2Wins, drawMatch);
                string[] gameSequence = new string[9];
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
                                data.RecordVelocity(1);
                                gameOver = true;
                            }
                            else
                            {
                                drawMatch++;
                                data.RecordVelocity(0);
                                gameOver = true;
                            }
                        gameSequence[round] = state.GetSequence();
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
                                data.RecordVelocity(2);
                                gameOver = true;
                            }
                            else
                            {
                                drawMatch++;
                                data.RecordVelocity(0);
                                gameOver = true;
                            }
                        }
                        gameSequence[round] = state.GetSequence();
                    }
                    round++;
                }

                if (gameOver)
                {
                    data.RecordSequence(gameSequence);
                    state = new State(new int[9]);
                    counter++;
                    gameOver = !gameOver;
                }
            }
            data.RecordPlayerPolicy(player1);
            data.RecordPlayerPolicy(player2);
            repo.Write(data);
            return new Agent[] {player1, player2};
        }
    }
}
