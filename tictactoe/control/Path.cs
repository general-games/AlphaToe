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

        public string NowVelocity(string path)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(path);
            sb.Append("\\velocity.txt");
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

    }
}
