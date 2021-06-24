using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex05.GameLogic
{
    public class Player
    {
        private readonly char r_Symbol;
        private readonly bool r_IsHuman;
        private int m_Score;

        public Player(bool i_IsHuman, char i_Symbol)
        {
            m_Score = 0;
            r_Symbol = i_Symbol;
            r_IsHuman = i_IsHuman;
        }

        public int Score
        {
            get { return m_Score; }

            set { m_Score = value; }
        }

        public char Symbol
        {
            get { return r_Symbol; }
        }

        public bool IsAI
        {
            get { return !r_IsHuman; }
        }
    }
}
