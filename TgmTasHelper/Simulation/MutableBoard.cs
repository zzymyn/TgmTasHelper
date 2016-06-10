using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using TgmTasHelper.Properties;

namespace TgmTasHelper.Simulation
{
    [DataContract]
    public class MutableBoard : IBoard
    {
        [DataMember]
        public IGameRules GameRules { get; private set; }
        [DataMember]
        public int Width { get; private set; }
        [DataMember]
        public int HeightVisible { get; private set; }
        [DataMember]
        public int Height { get; private set; }
        [DataMember]
        private TetrominoType[] m_Data;

        public MutableBoard(IBoard other)
        {
            GameRules = other.GameRules;
            Width = other.Width;
            HeightVisible = other.HeightVisible;
            Height = other.Height;
            m_Data = new TetrominoType[Width * Height];
            other.ForEach((int x, int y, TetrominoType tetrominoType) =>
                {
                    Set(x, y, tetrominoType);
                });
        }

        public MutableBoard(IGameRules gameRules, int width = 10, int heightVisible = 20)
        {
            GameRules = gameRules;
            Width = width;
            HeightVisible = heightVisible;
            Height = heightVisible + 2;
            m_Data = new TetrominoType[Width * Height];
            for (int i = 0; i < m_Data.Length; ++i)
                m_Data[i] = TetrominoType.Empty;
        }

        public void ForEachVisible(Action<int, int, TetrominoType> action)
        {
            for (int x = 0; x < Width; ++x)
                for (int y = 0; y < HeightVisible; ++y)
                    action(x, y, Get(x, y));
        }

        public void ForEach(Action<int, int, TetrominoType> action)
        {
            for (int x = 0; x < Width; ++x)
                for (int y = 0; y < Height; ++y)
                    action(x, y, Get(x, y));
        }

        public TetrominoType GetVisible(int x, int y)
        {
            if (y >= HeightVisible)
                return TetrominoType.OutOfBounds;
            return Get(x, y);
        }

        public TetrominoType Get(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                return TetrominoType.OutOfBounds;
            return m_Data[x + y * Width];
        }

        public Vec2 GetSpawnPos()
        {
            return new Vec2(Width / 2, Height - 3);
        }

        public IBoard LockTetromino(ITetromino tetromino, out int clearedLines)
        {
            MutableBoard board = new MutableBoard(this);

            foreach (var p in GameRules.GetTetrominoPoints(tetromino))
            {
                board.Set(p.x, p.y, tetromino.Type);
            }

            clearedLines = board.ClearCompletedLines();

            return board;
        }

        public void Set(int x, int y, TetrominoType tetrominoType)
        {
            if (x < 0 || x >= Width || y < 0 || y >= Height)
                return;
            m_Data[x + y * Width] = tetrominoType;
        }

        public int ClearCompletedLines()
        {
            int completedLines = 0;

            for (int y = Height - 1; y >= 0; --y)
            {
                if (!IsFullRow(y))
                    continue;

                ++completedLines;
                RemoveRow(y);
            }

            return completedLines;
        }

        public bool Equals(IBoard other)
        {
            if (other == null)
                return false;
            if (Width != other.Width || Height != other.Height)
                return false;
            for (int x = 0; x < Width; ++x)
                for (int y = 0; y < Height; ++y)
                    if (Get(x, y) != other.Get(x, y))
                        return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as IBoard);
        }

        public override int GetHashCode()
        {
            int h = 17;
            h = h *= 31 + Width.GetHashCode();
            h = h *= 31 + Height.GetHashCode();
            for (int i = 0; i < m_Data.Length; ++i)
                h = h *= 31 + m_Data[i].GetHashCode();
            return h;
        }

        private bool IsFullRow(int y)
        {
            for (int x = 0; x < Width; ++x)
            {
                if (Get(x, y) == TetrominoType.Empty)
                    return false;
            }
            return true;
        }

        private void RemoveRow(int yStart)
        {
            int yEnd = Height - 1;

            for (int y = yStart; y < yEnd; ++y)
            {
                for (int x = 0; x < Width; ++x)
                {
                    Set(x, y, Get(x, y + 1));
                }
            }

            for (int x = 0; x < Width; ++x)
            {
                Set(x, yEnd, TetrominoType.Empty);
            }
        }
    }
}
