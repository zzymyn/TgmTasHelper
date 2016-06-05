using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgmTasHelper.Simulation
{
    /// <summary>
    /// The TGM2+ Random Number Generator
    /// </summary>
    public class Rng
    {
        private const int HistorySize = 4;
        private const int HistoryRetry = 5;

        private UInt32 m_State = 0;
        private TetrominoType[] m_History = new TetrominoType[HistorySize];

        public Rng(UInt32 state, TetrominoType initialBlock)
        {
            m_State = state;
            m_History[0] = TetrominoType.Z;
            m_History[1] = TetrominoType.Z;
            m_History[2] = TetrominoType.S;
            m_History[3] = TetrominoType.S;
            AddHistory(initialBlock);
        }

        public UInt32 Next()
        {
            const UInt32 m = 0x41C64E6Du;
            const UInt32 c = 0x3039u;
            const UInt32 mask = 0x7FFFu;
            m_State = m_State * m + c;
            return (m_State >> 10) & mask;
        }

        public TetrominoType NextBlock()
        {
	        TetrominoType r = TetrominoType.Empty;

	        for (int i = 0; i < HistoryRetry; ++i)
	        {
		        r = (TetrominoType)(Next() % 7);

		        if (!m_History.Contains(r)) // piece is not in history
			        break;

                r = (TetrominoType)(Next() % 7);
            }

            AddHistory(r);

	        return r;
        }

        private void AddHistory(TetrominoType tetrominoType)
        {
            Array.Copy(m_History, 0, m_History, 1, HistorySize - 1);
            m_History[0] = tetrominoType;
        }
    }
}
