using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgmTasHelper.Simulation
{
    public static class PieceSimulator
    {
        public class Result
        {
            public IReadOnlyBoard Board { get; private set; }
            public Input Inputs { get; private set; }
            public int ClearedLines { get; private set; }

            internal Result(SubState subState, Input inputs)
            {
                var r = subState.LockPiece();
                Board = r.Item1;
                Inputs = inputs;
                ClearedLines = r.Item2;
            }
        }

        internal class SubState : IEquatable<SubState>
        {
            private IReadWriteBoard m_Board;
            private TetrominoType m_TetrominoType;
            private Vec2 m_TetrominoPos;
            private int m_TetrominoAngle;
            private bool m_Locked;

            public SubState(IReadOnlyBoard board, TetrominoType tetrominoType, Input initialInput)
            {
                m_Board = board.CreateCopy();
                m_TetrominoType = tetrominoType;
                m_TetrominoPos = board.GetSpawnPos();
                m_TetrominoAngle = 0;
                m_Locked = false;
                Apply(initialInput.Rotate);
                ApplyGravity();
            }

            public SubState(SubState previousState, Input nextInput)
            {
                Debug.Assert(!previousState.m_Locked);

                m_Board = previousState.m_Board.CreateCopy();
                m_TetrominoType = previousState.m_TetrominoType;
                m_TetrominoPos = previousState.m_TetrominoPos;
                m_TetrominoAngle = previousState.m_TetrominoAngle;
                m_Locked = previousState.m_Locked;
                Apply(nextInput.Rotate);
                Apply(nextInput.Move);
                ApplyGravity();
            }

            public Tuple<IReadWriteBoard, int> LockPiece()
            {
                IReadWriteBoard board = m_Board.CreateCopy();

                foreach (var p in GetTetrominoPoints())
                {
                    board.Set(m_TetrominoPos.x + p.x, m_TetrominoPos.y + p.y, m_TetrominoType);
                }

                int clearedLines = board.ClearCompletedLines();

                return Tuple.Create(board, clearedLines);
            }

            public override bool Equals(object obj)
            {
                return Equals(obj as SubState);
            }

            public override int GetHashCode()
            {
                int h = 17;
                h = h * 31 + m_Board.GetHashCode();
                h = h * 31 + m_TetrominoType.GetHashCode();
                h = h * 31 + m_TetrominoPos.GetHashCode();
                foreach (var p in GetTetrominoPoints())
                    h = h * 31 + p.GetHashCode();
                return h;
            }

            public bool Equals(SubState other)
            {
                return m_Board.Equals(other.m_Board)
                    && m_TetrominoType == other.m_TetrominoType
                    && m_TetrominoPos == other.m_TetrominoPos
                    && Enumerable.SequenceEqual(GetTetrominoPoints(), other.GetTetrominoPoints());
            }

            private void Apply(Movement movement)
            {
                switch (movement)
                {
                    default:
                        return;
                    case Movement.Left:
                        ApplyMove(-1);
                        return;
                    case Movement.Right:
                        ApplyMove(1);
                        return;
                }
            }

            private void Apply(Rotation rotate)
            {

                switch (rotate)
                {
                    default:
                    case Rotation.None:
                        return;
                    case Rotation.A:
                    case Rotation.C:
                        ApplyRotate(3);
                        return;
                    case Rotation.B:
                        ApplyRotate(1);
                        return;
                }
            }

            private void ApplyMove(int xMove)
            {
                Vec2 tryPos = new Vec2(m_TetrominoPos.x + xMove, m_TetrominoPos.y);
                if (TestTetromino(tryPos, m_TetrominoAngle))
                {
                    m_TetrominoPos = tryPos;
                }
            }

            private void ApplyRotate(int angleMove)
            {
                int tryAngle = (m_TetrominoAngle + angleMove) % 4;
                if (TestTetromino(m_TetrominoPos, tryAngle))
                {
                    m_TetrominoAngle = tryAngle;
                }
            }

            private void ApplyGravity()
            {
                while (true)
                {
                    Vec2 newPos = new Vec2(m_TetrominoPos.x, m_TetrominoPos.y - 1);
                    if (TestTetromino(newPos, m_TetrominoAngle))
                    {
                        m_TetrominoPos = newPos;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            private bool TestTetromino(Vec2 pos, int angle)
            {
                foreach (var p in GetTetrominoPoints(angle))
                {
                    if (m_Board.GetLogical(pos.x + p.x, pos.y + p.y) != TetrominoType.Empty)
                        return false;
                }
                return true;
            }

            private Vec2[] GetTetrominoPoints()
            {
                return RotationSystem.GetTetrominoPoints(m_TetrominoType, m_TetrominoAngle);
            }

            private Vec2[] GetTetrominoPoints(int angle)
            {
                return RotationSystem.GetTetrominoPoints(m_TetrominoType, angle);
            }
        }

        public static IEnumerable<Result> BruteForce(GameState initialState, TetrominoType tetrominoType)
        {
            var seenStates = new HashSet<SubState>();
            var subStates = new Queue<Tuple<SubState, Input>>();

            // IRS:
            foreach (var input in Input.Initials())
            {
                var k = new SubState(initialState.Board, tetrominoType, input);
                if (seenStates.Contains(k))
                    continue;
                seenStates.Add(k);
                subStates.Enqueue(Tuple.Create(k, input));
            }

            while (subStates.Count > 0)
            {
                var prev = subStates.Dequeue();

                yield return new Result(prev.Item1, prev.Item2);

                foreach (var input in prev.Item2.Successors())
                {
                    var k = new SubState(prev.Item1, input);
                    if (seenStates.Contains(k))
                        continue;
                    seenStates.Add(k);
                    subStates.Enqueue(Tuple.Create(k, input));
                }
            }
        }
    }
}
