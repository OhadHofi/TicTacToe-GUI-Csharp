using System;
using System.Text;

namespace Ex05.GameLogic
{
    public class Board
    {
        private readonly int r_Size;
        private char[,] m_Board = null;
        private int m_EmptyCells;

        public Board(int i_Size)
        {
            r_Size = i_Size;
            m_EmptyCells = i_Size * i_Size;
            m_Board = new char[i_Size, i_Size];
            for (int i = 0; i < i_Size; i++)
            {
                for (int j = 0; j < i_Size; j++)
                {
                    m_Board[i, j] = ' ';
                }
            }
        }

        public int NumberOfEmptyCells
        {
            get
            {
                return m_EmptyCells;
            }
        }

        public bool IsFull
        {
            get
            {
                return m_EmptyCells == 0;
            }
        }

        public int Size
        {
            get
            {
                return r_Size;
            }
        }

        public char GetValue(int i_X, int i_Y)
        {
            return m_Board[i_X, i_Y];
        }

        public bool AddMove(int i_X, int i_Y, char i_Sign)
        {
            bool addedMove = false;
            if (checkValidRange(i_X, i_Y) && isEmptySquare(i_X, i_Y))
            {
                m_Board[i_X, i_Y] = i_Sign;
                m_EmptyCells--;
                addedMove = true;
            }

            return addedMove;
        }

        public void ClearSquare(int i_X, int i_Y)
        {
            m_Board[i_X, i_Y] = ' ';
            m_EmptyCells++;
        }

        private bool isEmptySquare(int i_X, int i_Y)
        {
            return m_Board[i_X, i_Y] == ' ';
        }

        private bool checkValidRange(int i_X, int i_Y)
        {
            return i_X < r_Size &&
                   i_Y < r_Size &&
                   i_X >= 0 && i_Y >= 0;
        }

        public void PrintBoard()
        {
            bool firstRowCol = true;
            StringBuilder firstLine = new StringBuilder();
            string separator = new string('=', (r_Size * 4) + 1);

            for (int i = 0; i <= r_Size; i++)
            {
                for (int j = 0; j <= r_Size; j++)
                {
                    if (firstRowCol)
                    {
                        if (j != r_Size)
                        {
                            firstLine.AppendFormat(@"   {0}", j + 1);
                        }
                    }
                    else
                    {
                        if (j == 0)
                        {
                            Console.Write(@"{0}|", i);
                        }
                        else
                        {
                            Console.Write(@" {0} |", m_Board[i - 1, j - 1]);
                        }
                    }
                }

                if (firstRowCol)
                {
                    Console.WriteLine(firstLine + Environment.NewLine);
                    firstRowCol = false;
                }
                else
                {
                    Console.Write(Environment.NewLine);
                    Console.WriteLine(@" {0}", separator);
                }
            }
        }

        public void ClearBoard()
        {
            m_EmptyCells = r_Size * r_Size;

            for (int i = 0; i < r_Size; i++)
            {
                for (int j = 0; j < r_Size; j++)
                {
                    m_Board[i, j] = ' ';
                }
            }
        }
    }
}
