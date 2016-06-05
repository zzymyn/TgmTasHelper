using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgmTasHelper.Simulation
{
    public class Board
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int InternalHeight { get; private set; }
        private TetrominoType[,] m_Data;

        public Board(int width, int height)
        {
            Width = width;
            Height = height;
            InternalHeight = height + 2;
            m_Data = new TetrominoType[Width, InternalHeight];

            for (int x = 0; x < Width; ++x)
                for (int y = 0; y < InternalHeight; ++y)
                    m_Data[x, y] = TetrominoType.Empty;
        }

        public void ForEach(Action<int, int, TetrominoType> action)
        {
            for (int x = 0; x < Width; ++x)
                for (int y = 0; y < Height; ++y)
                    action(x, y, m_Data[x, y]);
        }

        public TetrominoType Get(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= InternalHeight)
                return TetrominoType.OutOfBounds;
            return m_Data[x, y];
        }

        private void Set(int x, int y, TetrominoType tetrominoType)
        {
            if (x < 0 || x >= Width || y < 0 || y >= InternalHeight)
                return;
            m_Data[x, y] = tetrominoType;
        }
    }
}
