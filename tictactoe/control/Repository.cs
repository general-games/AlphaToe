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
            string velPath = path.NowVelocity(dataPath);
            string seqPath = path.NowSequence(dataPath);
            string policyPath1 = path.NowPolicy(dataPath, 1);
            string policyPath2 = path.NowPolicy(dataPath, 2);
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
            using (StreamWriter sw = new StreamWriter(velPath, false))
            {
                foreach(int i in data.Velocity)
                {
                    sw.Write(i + ",");
                }
            }
            using (StreamWriter sw = new StreamWriter(policyPath1, false))
            {
                foreach(State s in data.PolicyPlayer1)
                {
                    sw.Write(s.GetSequence() + "|" + s.Probability + ",");
                }
            }
            using (StreamWriter sw = new StreamWriter(policyPath2, false))
            {
                foreach (State s in data.PolicyPlayer1)
                {
                    sw.Write(s.GetSequence() + "|" + s.Probability + ",");
                }
            }
        }
    }
}
