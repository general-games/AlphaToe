using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using tictactoe.control;

namespace tictactoe
{
    class Environment
    {
        private float ALPHA;
        private double EPSILON;
        private float reward = 1.0f;
        private float penalty = -1.0f;
        private float drawReward = 0.0f;
        private bool isWriteToDisk;
        private Random random;
        public Environment(Random random, bool isWriteToDisk, float ALPHA, float EPSILON)
        {
            this.ALPHA = ALPHA;
            this.EPSILON = EPSILON;
            this.random = random;
            this.isWriteToDisk = isWriteToDisk;
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
            Data data = new Data(ALPHA, EPSILON, episodes, new float[] {reward, penalty, drawReward},new float[] {reward, penalty, drawReward});

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
                        if (IsGameOver(rules, state, data, player1, out bool isWin))
                        {
                            gameOver = true;
                            if (isWin)
                                player2.TrainPenalty(ALPHA);
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
                        if (IsGameOver(rules, state, data, player2, out bool isWin))
                        {
                            gameOver = true;
                            if (isWin)
                                player1.TrainPenalty(ALPHA);
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
            if(isWriteToDisk)
                repo.Write(data);
            return new Agent[] {player1, player2};
        }
        private bool IsGameOver(Rules rules, State state, Data data, Agent player, out bool isWin)
        {
            if (rules.CheckGameOver(state))
            {
                if (rules.CheckWinner(state, player.Player))
                {
                    data.RecordWin(player.Player);
                    isWin = true;
                    return true;
                }
                else
                {
                    data.RecordWin(0);
                    isWin = false;
                    return true;
                }
            }
            isWin = false;
            return false;
        }
    }
}
