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

        public static Vec2[] GetTetrominoPoints(TetrominoType tetrominoType, int angle)
        {
            return s_Def[tetrominoType][angle];
        }

        public IEnumerable<Vec2> GetTetrominoPoints(ITetromino tetromino)
        {
            return GetTetrominoPoints(tetromino.Type, tetromino.Pos, tetromino.Angle);
        }

        public IEnumerable<Vec2> GetTetrominoPoints(TetrominoType tetrominoType, Vec2 pos, int angle)
        {
            return s_Def[tetrominoType][angle].Select(a => pos + a);
        }
    }
}
