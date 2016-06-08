using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgmTasHelper.Simulation
{
    public class PieceSimulator
    {
        private IGameRules m_GameRules;
        private IBoard m_Board;

        public PieceSimulator(IGameRules gameRules, IBoard board)
        {
            m_GameRules = gameRules;
            m_Board = board;
        }

        public ITetromino SpawnTetromino(TetrominoType tetrominoType, Input input)
        {
            var t = new Tetromino(tetrominoType, m_Board.GetSpawnPos(), 0);

            ApplyRotate(t, input.Rotate);

            if (!TestTetromino(t))
                return null;

            ApplyGravity(t);

            return t;
        }

        public ITetromino StepTetromino(ITetromino prevTetromino, Input input)
        {
            var t = new Tetromino(prevTetromino);

            ApplyRotate(t, input.Rotate);
            ApplyMove(t, input.Move);
            ApplyGravity(t);

            return t;
        }

        private void ApplyRotate(Tetromino t, Rotation rotation)
        {
            switch (rotation)
            {
                case Rotation.A:
                case Rotation.C:
                    TryMove(t, Vec2.Zero, 3);
                    return;
                case Rotation.B:
                    TryMove(t, Vec2.Zero, 3);
                    return;
                default:
                    return;
            }
        }

        private void ApplyMove(Tetromino t, Movement movement)
        {
            switch (movement)
            {
                case Movement.Left:
                    TryMove(t, new Vec2(-1, 0), 0);
                    return;
                case Movement.Right:
                    TryMove(t, new Vec2(1, 0), 0);
                    return;
                default:
                    return;
            }
        }

        private void ApplyGravity(Tetromino t)
        {
            while (TryMove(t, new Vec2(0, -1), 0)) ;
        }

        private bool TryMove(Tetromino t, Vec2 move, int rotate)
        {
            Vec2 newPos = t.Pos + move;
            int newAngle = (t.Angle + rotate) % 4;

            if (!TestTetromino(t.Type, newPos, newAngle))
                return false;

            t.Pos = newPos;
            t.Angle = newAngle;
            return true;
        }

        private bool TestTetromino(Tetromino tetromino)
        {
            return TestTetromino(tetromino.Type, tetromino.Pos, tetromino.Angle);
        }

        private bool TestTetromino(TetrominoType tetrominoType, Vec2 pos, int angle)
        {
            foreach (var p in m_GameRules.GetTetrominoPoints(tetrominoType, pos, angle))
            {
                if (m_Board.Get(p.x, p.y) != TetrominoType.Empty)
                    return false;
            }
            return true;
        }
    }
}
