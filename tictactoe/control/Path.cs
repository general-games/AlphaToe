using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace tictactoe
{
    class Path
    {

        public string NowFolder()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"..\\..\\..\\repository\\data\\{DateTime.Now.ToFileTime()}");
            return String.Copy(sb.ToString());
        }

        public string NowVelocity(string path, int player)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(path);
            sb.Append($"\\velocityP{player}.txt");
            return sb.ToString();
        }
        public string NowOpening(string path)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(path);
            sb.Append($"\\openings.txt");
            return sb.ToString();
        }

        public string NowWins(string path)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(path);
            sb.Append("\\wins.txt");
            return sb.ToString();
        }
        public string NowDraws(string path)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(path);
            sb.Append("\\velocityDraws.txt");
            return sb.ToString();
        }
        public string NowSequence(string path)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(path);
            sb.Append("\\sequences.txt");
            return sb.ToString();
        }
        public string NowPolicy(string path, int player)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(path);
            sb.Append($"\\policy{player}.txt");
            return sb.ToString();
        }
        public string NowMeta(string path)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(path);
            sb.Append($"\\meta.txt");
            return sb.ToString();
        }

    }
}
