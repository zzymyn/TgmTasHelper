using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgmTasHelper.Simulation
{
    public class TgmGameRules : IGameRules
    {
        private static Dictionary<TetrominoType, Vec2[][]> s_Def = new Dictionary<TetrominoType, Vec2[][]>();

        static TgmGameRules()
        {
            s_Def[TetrominoType.I] = new Vec2[4][] {
				new Vec2[] {new Vec2(-2, 0), new Vec2(-1, 0), new Vec2(0, 0), new Vec2(1, 0)},
				new Vec2[] {new Vec2(0, -2), new Vec2(0, -1), new Vec2(0, 0), new Vec2(0, 1)},
				new Vec2[] {new Vec2(-2, 0), new Vec2(-1, 0), new Vec2(0, 0), new Vec2(1, 0)},
				new Vec2[] {new Vec2(0, -2), new Vec2(0, -1), new Vec2(0, 0), new Vec2(0, 1)},
            };

            s_Def[TetrominoType.T] = new Vec2[4][] {
				new Vec2[] {new Vec2(-2, 0), new Vec2(-1, 0), new Vec2(0, 0), new Vec2(-1, -1)},
				new Vec2[] {new Vec2(-1, -1), new Vec2(-1, 0), new Vec2(-1, 1), new Vec2(-2, 0)},
				new Vec2[] {new Vec2(-2, -1), new Vec2(-1, -1), new Vec2(0, -1), new Vec2(-1, 0)},
				new Vec2[] {new Vec2(-1, -1), new Vec2(-1, 0), new Vec2(-1, 1), new Vec2(0, 0)},
            };

            s_Def[TetrominoType.J] = new Vec2[4][] {
				new Vec2[] {new Vec2(-2, 0), new Vec2(-1, 0), new Vec2(0, 0), new Vec2(0, -1)},
				new Vec2[] {new Vec2(-1, -1), new Vec2(-1, 0), new Vec2(-1, 1), new Vec2(-2, -1)},
				new Vec2[] {new Vec2(-2, -1), new Vec2(-1, -1), new Vec2(0, -1), new Vec2(-2, 0)},
				new Vec2[] {new Vec2(-1, -1), new Vec2(-1, 0), new Vec2(-1, 1), new Vec2(0, 1)},
            };

            s_Def[TetrominoType.L] = new Vec2[4][] {
				new Vec2[] {new Vec2(-2, 0), new Vec2(-1, 0), new Vec2(0, 0), new Vec2(-2, -1)},
				new Vec2[] {new Vec2(-1, -1), new Vec2(-1, 0), new Vec2(-1, 1), new Vec2(-2, 1)},
				new Vec2[] {new Vec2(-2, -1), new Vec2(-1, -1), new Vec2(0, -1), new Vec2(0, 0)},
				new Vec2[] {new Vec2(-1, -1), new Vec2(-1, 0), new Vec2(-1, 1), new Vec2(0, -1)},
            };

            s_Def[TetrominoType.O] = new Vec2[4][] {
				new Vec2[] {new Vec2(0, -1), new Vec2(0, 0), new Vec2(-1, -1), new Vec2(-1, 0)},
				new Vec2[] {new Vec2(0, -1), new Vec2(0, 0), new Vec2(-1, -1), new Vec2(-1, 0)},
				new Vec2[] {new Vec2(0, -1), new Vec2(0, 0), new Vec2(-1, -1), new Vec2(-1, 0)},
				new Vec2[] {new Vec2(0, -1), new Vec2(0, 0), new Vec2(-1, -1), new Vec2(-1, 0)},
            };

            s_Def[TetrominoType.S] = new Vec2[4][] {
				new Vec2[] {new Vec2(-2, -1), new Vec2(-1, -1), new Vec2(-1, 0), new Vec2(0, 0)},
				new Vec2[] {new Vec2(-1, -1), new Vec2(-1, 0), new Vec2(-2, 0), new Vec2(-2, 1)},
				new Vec2[] {new Vec2(-2, -1), new Vec2(-1, -1), new Vec2(-1, 0), new Vec2(0, 0)},
				new Vec2[] {new Vec2(-1, -1), new Vec2(-1, 0), new Vec2(-2, 0), new Vec2(-2, 1)},
            };

            s_Def[TetrominoType.Z] = new Vec2[4][] {
				new Vec2[] {new Vec2(-2, 0), new Vec2(-1, 0), new Vec2(-1, -1), new Vec2(0, -1)},
				new Vec2[] {new Vec2(-1, -1), new Vec2(-1, 0), new Vec2(0, 0), new Vec2(0, 1)},
				new Vec2[] {new Vec2(-2, 0), new Vec2(-1, 0), new Vec2(-1, -1), new Vec2(0, -1)},
				new Vec2[] {new Vec2(-1, -1), new Vec2(-1, 0), new Vec2(0, 0), new Vec2(0, 1)},
            };
        }

        public IEnumerable<int> GetAllowedKicks(IBoard board, ITetromino tetromino, int targetAngle)
        {
            if (tetromino.Type == TetrominoType.I)
                yield break;

            bool canKick = false;

            foreach (var p in s_Def[tetromino.Type][targetAngle])
            {
                if (p.x == -2)
                {
                    if (board.Get(tetromino.Pos.x + p.x, tetromino.Pos.y + p.y) != TetrominoType.Empty)
                    {
                        canKick = true;
                    }
                }
                else if (p.x == 0)
                {
                    if (board.Get(tetromino.Pos.x + p.x, tetromino.Pos.y + p.y) != TetrominoType.Empty)
                    {
                        canKick = true;
                    }
                }
            }

            // always try kick right first
            if (canKick)
            {
                yield return 1;
                yield return -1;
            }
        }

        public IEnumerable<Vec2> GetTetrominoPoints(ITetromino tetromino)
        {
            return GetTetrominoPoints(tetromino.Type, tetromino.Pos, tetromino.Angle);
        }

        public IEnumerable<Vec2> GetTetrominoPoints(TetrominoType tetrominoType, Vec2 pos, int angle)
        {
            return s_Def[tetrominoType][angle].Select(a => pos + a);
        }

        public int GetNextTime(int time, int level, List<Input> inputs, int linesCleared)
        {
            time += inputs.Count;
            if (linesCleared > 0)
            {
                time += LineClearTime(level + linesCleared);
                time += LineClearAreTime(level + linesCleared);
            }
            else
            {
                time += AreTime(level);
            }
            time += 2;
            return time;
        }

        public int GetNextLevel(int level, int linesCleared)
        {
            level += linesCleared;
            if (level % 100 != 99)
                ++level;
            return level;
        }

        private int AreTime(int level)
        {
            if (level <= 99) return 16;
            if (level <= 199) return 12;
            if (level <= 299) return 12;
            if (level <= 399) return 6;
            if (level <= 499) return 5;
            return 4;
        }

        private int LineClearAreTime(int level)
        {
            if (level <= 99) return 12;
            if (level <= 199) return 6;
            if (level <= 299) return 6;
            if (level <= 399) return 6;
            if (level <= 499) return 5;
            return 4;
        }

        private int LineClearTime(int level)
        {
            if (level <= 99) return 12;
            if (level <= 199) return 6;
            if (level <= 299) return 6;
            if (level <= 399) return 6;
            if (level <= 499) return 5;
            return 4;
        }
    }
}
