using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex05.GameLogic
{
    public class MoveEventArgs : EventArgs
    {
        private int m_Row, m_Col;

        public MoveEventArgs(int i_Row, int i_Col)
        {
            m_Row = i_Row;
            m_Col = i_Col;
        }

        public int Row
        {
            get { return m_Row; }
        }

        public int Col
        {
            get { return m_Col; }
        }
    }
}
