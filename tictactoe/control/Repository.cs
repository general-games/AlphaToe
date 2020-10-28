using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace tictactoe.control
{
    class Repository
    {
        Path path = new Path();
        public void CreateNowDataDir(out string outPath)
        {
            outPath = path.NowFolder();
            Directory.CreateDirectory(outPath);
        }
        public void Write(List<int> toWrite, string path)
        {
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                foreach(int i in toWrite)
                {
                    writer.WriteLine(i);
                }
            }
        }
        public void Write(List<State> toWrite, string path)
        {
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                foreach (State state in toWrite)
                {
                    writer.WriteLine(state.Sequence);
                }
            }
        }
        public void Write(Data data)
        {
            string dataPath = path.NowFolder();
            Directory.CreateDirectory(dataPath);
            string winsPath = path.NowWins(dataPath);
            string drawsPath = path.NowDraws(dataPath);
            string velPath1 = path.NowVelocity(dataPath, 1);
            string velPath2 = path.NowVelocity(dataPath, 2);
            string seqPath = path.NowSequence(dataPath);
            string openingPath = path.NowOpening(dataPath);
            string policyPath1 = path.NowPolicy(dataPath, 1);
            string policyPath2 = path.NowPolicy(dataPath, 2);
            string metaPath = path.NowMeta(dataPath); 
            using (StreamWriter sw = new StreamWriter(seqPath, false))
            {
                foreach(string[] array in data.Sequences)
                {
                    for (int i = 0; i < array.Length; i++)
                    {
                        sw.Write(array[i]+",");
                    }
                    sw.Write("\n");
                }
            }
            using (StreamWriter sw = new StreamWriter(winsPath, false))
            {
                foreach(int i in data.Wins)
                {
                    sw.Write(i + ",");
                }
            }
            using (StreamWriter sw = new StreamWriter(policyPath1, false))
            {
                foreach(State s in data.PolicyPlayer1)
                {
                    sw.Write(s.GetSequence() + "|" + s.Value + "¤");
                }
            }
            using (StreamWriter sw = new StreamWriter(policyPath2, false))
            {
                foreach (State s in data.PolicyPlayer2)
                {
                    sw.Write(s.GetSequence() + "|" + s.Value + "¤");
                }
            }
            using (StreamWriter sw = new StreamWriter(metaPath, false))
            {
                sw.Write($"Date:{DateTime.Now} TrainTime: {data.TrainTime} , ALPHA: {data.ALPHA} , EPSILON: {data.EPSILON} , Episodes: {data.Episodes} P1 Init State Values:" +
                    $" {data.P1Policy[0]} , {data.P1Policy[1]} , {data.P1Policy[2]} P2 Init State Values: {data.P2Policy[0]} , {data.P2Policy[1]} , {data.P2Policy[2]} ");
            }
            using (StreamWriter sw = new StreamWriter(velPath1, false))
            {
                int p1wins = 0;
                int counter = 0;
                foreach(int i in data.Wins)
                {
                    if (i == 1)
                        p1wins++;
                    counter++;
                    if(counter == 10)
                    {
                        sw.Write(p1wins + ",");
                        counter = 0;
                        p1wins = 0;
                    }
                }
            }
            using (StreamWriter sw = new StreamWriter(velPath2, false))
            {
                int p2Wins = 0;
                int counter = 0;
                foreach (int i in data.Wins)
                {
                    if (i == 2)
                        p2Wins++;
                    counter++;
                    if (counter == 10)
                    {
                        sw.Write(p2Wins + ",");
                        counter = 0;
                        p2Wins = 0;
                    }
                }
            }
            using (StreamWriter sw = new StreamWriter(drawsPath, false))
            {
                int draws = 0;
                int counter = 0;
                foreach (int i in data.Wins)
                {
                    if (i == 0)
                        draws++;
                    counter++;
                    if (counter == 10)
                    {
                        sw.Write(draws + ",");
                        counter = 0;
                        draws = 0;
                    }
                }
            }
            using (StreamWriter sw = new StreamWriter(openingPath, false))
            {
                foreach(int i in data.Opening)
                {
                    sw.Write(i + ",");
                }
            }
        }
    }
}
