using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using tictactoe.control;

namespace tictactoe
{
    class Environment
    {
        private float ALPHA = 0.1f;
        private double EPSILON = 0.05f;
        private float reward = 1.0f;
        private float penalty = -1.0f;
        private float drawReward = 0.3f;
        private Random random;
        public Environment(Random random)
        {
            this.random = random;
        }
        public Agent[] Train(int episodes)
        {
            Stopwatch stopwatch = new Stopwatch();
            Repository repo = new Repository();
            State state = new State(new int[9]);
            Rules rules = new Rules();
            Agent player1 = new Agent(1, state,random, reward, penalty, drawReward);
            Agent player2 = new Agent(2, state,random, reward, penalty, drawReward);
            Draw draw = new Draw();
            Data data = new Data(ALPHA, EPSILON, episodes, new float[] {reward, penalty, drawReward},new float[] { reward, penalty, drawReward});

            bool gameOver = false;
            int counter = 0;

            draw.Clear();
            draw.TrainingTitle();
            stopwatch.Start();
            while (counter < episodes)
            {
                int round = 0;
                draw.SetPosition(0, 11);
                draw.Progress(counter, episodes);
                draw.Wins(data.P1Wins, data.P2Wins, data.Draws);
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
                            if (round == 0)
                                data.AddOpening(playerAction);
                            if (round > 0)
                                player1.Train(state, ALPHA);

                        }
                        if (rules.CheckGameOver(state))
                            if (rules.CheckWinner(state, 1))
                            {
                                player2.TrainPenalty(ALPHA);
                                data.RecordWin(player1.Player);
                                gameOver = true;
                            }
                            else
                            {
                                data.RecordWin(0);
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
                                player1.TrainPenalty(ALPHA);
                                data.RecordWin(player2.Player);
                                gameOver = true;
                            }
                            else
                            {
                                data.RecordWin(0);
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
            stopwatch.Stop();
            data.SetTrainTime(stopwatch.Elapsed);
            data.RecordPlayerPolicy(player1);
            data.RecordPlayerPolicy(player2);
            repo.Write(data);
            return new Agent[] {player1, player2};
        }
    }
}
