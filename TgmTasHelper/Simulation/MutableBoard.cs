﻿using System;
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
    public class MutableBoard : IBoard
    {
        public IGameRules GameRules { get; private set; }
        public int Width { get; private set; }
        public int HeightVisible { get; private set; }
        public int HeightLogical { get; private set; }
        private TetrominoType[,] m_Data;

        public MutableBoard(IBoard other)
        {
            GameRules = other.GameRules;
            Width = other.Width;
            HeightVisible = other.HeightVisible;
            HeightLogical = other.HeightLogical;
            m_Data = new TetrominoType[Width, HeightLogical];
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
            HeightLogical = heightVisible + 2;
            m_Data = new TetrominoType[Width, HeightLogical];
            for (int x = 0; x < Width; ++x)
                for (int y = 0; y < HeightLogical; ++y)
                    m_Data[x, y] = TetrominoType.Empty;
        }

        public void ForEachVisible(Action<int, int, TetrominoType> action)
        {
            for (int x = 0; x < Width; ++x)
                for (int y = 0; y < HeightVisible; ++y)
                    action(x, y, m_Data[x, y]);
        }

        public void ForEach(Action<int, int, TetrominoType> action)
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

        public TetrominoType Get(int x, int y)
        {
            if (x < 0 || x >= Width || y < 0 || y >= HeightLogical)
                return TetrominoType.OutOfBounds;
            return m_Data[x, y];
        }

        public Vec2 GetSpawnPos()
        {
            return new Vec2(Width / 2, HeightLogical - 3);
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
            if (x < 0 || x >= Width || y < 0 || y >= HeightLogical)
                return;
            m_Data[x, y] = tetrominoType;
        }

        public int ClearCompletedLines()
        {
            int completedLines = 0;

            for (int y = HeightLogical - 1; y >= 0; --y)
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
            if (Width != other.Width || HeightLogical != other.HeightLogical)
                return false;
            for (int x = 0; x < Width; ++x)
                for (int y = 0; y < HeightLogical; ++y)
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
            h = h *= 31 + HeightLogical.GetHashCode();
            for (int x = 0; x < Width; ++x)
                for (int y = 0; y < HeightLogical; ++y)
                    h = h *= 31 + m_Data[x, y].GetHashCode();
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
            int yEnd = HeightLogical - 1;

            for (int y = yStart; y < yEnd; ++y)
            {
                for (int x = 0; x < Width; ++x)
                {
                    m_Data[x, y] = m_Data[x, y + 1];
                }
            }

            for (int x = 0; x < Width; ++x)
            {
                m_Data[x, yEnd] = TetrominoType.Empty;
            }
        }
    }
}