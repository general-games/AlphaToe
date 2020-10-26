using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using tictactoe.control;

namespace tictactoe
{
    class Environment
    {
        private float ALPHA = 0.05f;
        private double EPSILON = 0.05f;
        private float WINVAL1 = 1.0f;
        private float LOSEVAL1 = -1.0f;
        private float DRAWVAL1 = 0.0f;

        private float WINVAL2 = 1.0f;
        private float LOSEVAL2 = -1.0f;
        private float DRAWVAL2 = 0.0f;

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
            Agent player1 = new Agent(1, state,random, WINVAL1, LOSEVAL1, DRAWVAL1);
            Agent player2 = new Agent(2, state,random, WINVAL2, LOSEVAL2, DRAWVAL2);
            Draw draw = new Draw();
            Data data = new Data(ALPHA, EPSILON, episodes, new float[] {WINVAL1, LOSEVAL1, DRAWVAL1},new float[] { WINVAL2, LOSEVAL2, DRAWVAL2});

            bool gameOver = false;
            int counter = 0;
            int player1Wins = 0;
            int player2Wins = 0;
            int drawMatch = 0;

            draw.Clear();
            draw.TrainingTitle();
            stopwatch.Start();
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
                            if (round == 0)
                                data.AddOpening(playerAction);
                            if (round > 0)
                                player1.Train(state, ALPHA);
                            
                        }
                        if (rules.CheckGameOver(state))
                            if (rules.CheckWinner(state, 1))
                            {
                                player1Wins++;
                                player2.TrainEnd(ALPHA, LOSEVAL2);
                                data.RecordWins(player1.Player);
                                gameOver = true;
                            }
                            else
                            {
                                drawMatch++;
                                data.RecordWins(0);
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
                                player1.TrainEnd(ALPHA, LOSEVAL2);
                                data.RecordWins(player2.Player);
                                gameOver = true;
                            }
                            else
                            {
                                drawMatch++;
                                data.RecordWins(0);
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
