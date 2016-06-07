using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgmTasHelper.Properties;

namespace TgmTasHelper.Simulation
{
    public class Board : IReadWriteBoard
    {
        public int Width { get; private set; }
        public int HeightVisible { get; private set; }
        public int HeightLogical { get; private set; }
        private TetrominoType[,] m_Data;

        public Board(Board board)
        {
            Width = board.Width;
            HeightVisible = board.HeightVisible;
            HeightLogical = board.HeightLogical;
            m_Data = new TetrominoType[Width, HeightLogical];
            Array.Copy(board.m_Data, m_Data, m_Data.Length);
        }

        public Board(int width = 10, int heightVisible = 20)
        {
            Width = width;
            HeightVisible = heightVisible;
            HeightLogical = heightVisible + 2;
            m_Data = new TetrominoType[Width, HeightLogical];

            for (int x = 0; x < Width; ++x)
                for (int y = 0; y < HeightLogical; ++y)
                    m_Data[x, y] = TetrominoType.Empty;
        }

        public IReadWriteBoard CreateCopy()
        {
            return new Board(this);
        }

        public void ForEachVisible(Action<int, int, TetrominoType> action)
        {
            for (int x = 0; x < Width; ++x)
                for (int y = 0; y < HeightVisible; ++y)
                    action(x, y, m_Data[x, y]);
        }

        public void ForEachLogical(Action<int, int, TetrominoType> action)
        {
            for (int x = 0; x < Width; ++x)
                for (int y = 0; y < HeightLogical; ++y)
                    action(x, y, m_Data[x, y]);
        }

        public TetrominoType GetVisible(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= HeightVisible)
                return TetrominoType.OutOfBounds;
            return m_Data[x, y];
        }

        public TetrominoType GetLogical(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= HeightLogical)
                return TetrominoType.OutOfBounds;
            return m_Data[x, y];
        }

        private void Set(int x, int y, TetrominoType tetrominoType)
        {
            if (x < 0 || x >= Width || y < 0 || y >= HeightLogical)
                return;
            m_Data[x, y] = tetrominoType;
        }
    }
}
