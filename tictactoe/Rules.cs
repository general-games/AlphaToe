using System;
using System.Collections.Generic;
using System.Text;

namespace tictactoe
{
    class Rules
    {
        public bool CheckGameOver(State board)
        {
            if (CheckWinner(board, 1) || CheckWinner(board, 2))
                return true;
            if (CheckFull(board))
                return true;
            return false;
        }
        public bool CheckWinner(State board, int playerToCheck)
        {
            if (CheckColumns(board, playerToCheck))
                return true;
            if (CheckRows(board, playerToCheck))
                return true;
            if (CheckDiagonal(board, playerToCheck))
                return true;
            return false;
        }
        public bool CheckColumns(State board, int playerToCheck)
        {
            int checker = (1 - 1);
            if (board.Get()[checker] == playerToCheck && board.Get()[checker + 3] == playerToCheck && board.Get()[checker + 6] == playerToCheck)
            {
                return true;
            }
            checker = (2 - 1);
            if (board.Get()[checker] == playerToCheck && board.Get()[checker + 3] == playerToCheck && board.Get()[checker + 6] == playerToCheck)
            {
                return true;
            }
            checker = (3 - 1);
            if (board.Get()[checker] == playerToCheck && board.Get()[checker + 3] == playerToCheck && board.Get()[checker + 6] == playerToCheck)
            {
                return true;
            }
            return false;
        }
        public bool CheckRows(State board, int playerToCheck)
        {
            int checker = (1 - 1) * 3;
            if (board.Get()[checker] == playerToCheck && board.Get()[checker + 1] == playerToCheck && board.Get()[checker + 2] == playerToCheck)
            {
                return true;
            }
            checker = (2 - 1) * 3;
            if (board.Get()[checker] == playerToCheck && board.Get()[checker + 1] == playerToCheck && board.Get()[checker + 2] == playerToCheck)
            {
                return true;
            }
            checker = (3 - 1) * 3;
            if (board.Get()[checker] == playerToCheck && board.Get()[checker + 1] == playerToCheck && board.Get()[checker + 2] == playerToCheck)
            {
                return true;
            }
            return false;
        }
        public bool CheckDiagonal(State board, int playerToCheck)
        {
            int checker = 0;
            if (board.Get()[checker] == playerToCheck && board.Get()[checker + 4] == playerToCheck && board.Get()[checker + 8] == playerToCheck)
            {
                return true;
            }

            checker = 2;
            if (board.Get()[checker] == playerToCheck && board.Get()[checker + 2] == playerToCheck && board.Get()[checker + 4] == playerToCheck)
            {
                return true;
            }
            return false;
        }
        public bool CheckFull(State board)
        {
            for (int i = 0; i < board.Sequence.Length; i++)
            {
                if (board.Get()[i] == 0)
                    return false;
            }
            return true;
        }
    }
}
