using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TgmTasHelper.Simulation
{
    /// <summary>
    /// The TGM2+ Random Number Generator
    /// </summary>
    [DataContract]
    public class TgmRng : IRng
    {
        private const int HistorySize = 4;
        private const int HistoryRetry = 5;

        [DataMember]
        private readonly UInt32 m_State;
        [DataMember]
        private readonly TetrominoType[] m_History;

        public TgmRng(UInt32 state, TetrominoType initialBlock)
        {
            m_State = state;
            m_History = new TetrominoType[HistorySize];
            m_History[0] = TetrominoType.Z;
            m_History[1] = TetrominoType.Z;
            m_History[2] = TetrominoType.S;
            m_History[3] = TetrominoType.S;
            AddHistory(m_History, initialBlock);
        }

        public TgmRng(TgmRng rng, int steps = 0)
        {
            m_State = rng.m_State;
            m_History = new TetrominoType[HistorySize];
            Array.Copy(rng.m_History, m_History, m_History.Length);
            while (steps-- > 0)
            {
                NextBlock(ref m_State, m_History);
            }
        }

        public IRng Next()
        {
            return new TgmRng(this, 1);
        }

        public IEnumerable<TetrominoType> Peek()
        {
            var state = m_State;
            var history = m_History.ToArray();
            while (true)
            {
                yield return NextBlock(ref state, history);
            }
        }

        private static UInt32 Next(ref UInt32 state)
        {
            const UInt32 m = 0x41C64E6Du;
            const UInt32 c = 0x3039u;
            const UInt32 mask = 0x7FFFu;
            state = state * m + c;
            return (state >> 10) & mask;
        }

        private static TetrominoType NextBlock(ref UInt32 state, TetrominoType[] history)
        {
            TetrominoType r = TetrominoType.Empty;

            for (int i = 0; i < HistoryRetry; ++i)
            {
                r = (TetrominoType)(Next(ref state) % 7);

                if (!history.Contains(r)) // piece is not in history
                    break;

                r = (TetrominoType)(Next(ref state) % 7);
            }

            AddHistory(history, r);

            return r;
        }

        private static void AddHistory(TetrominoType[] history, TetrominoType tetrominoType)
        {
            Array.Copy(history, 0, history, 1, HistorySize - 1);
            history[0] = tetrominoType;
        }
    }
}
