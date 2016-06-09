using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgmTasHelper.Simulation
{
    public class ResultScorer
    {
        /// <summary>
        /// Score a result, lower scores are better.
        /// </summary>
        public double ScoreResult(Solver.Result result)
        {
            double score = 0;

            score += result.Step.Inputs.Count;
            score += 100 * CountHoles(result.NextState.Board);

            return score;
        }

        private int CountHoles(IBoard board)
        {
            int holeCount = 0;

            for (int x = 0; x < board.Width; ++x)
            {
                int emptyCount = 0;

                for (int y = 0; y < board.Height; ++y)
                {
                    if (board.Get(x, y) == TetrominoType.Empty)
                    {
                        ++emptyCount;
                    }
                    else
                    {
                        if (emptyCount > 0)
                            ++holeCount;
                        emptyCount = 0;
                    }
                }
            }

            return holeCount;
        }

        private double GetBumpiness(IBoard board)
        {
            double bumpiness = 0;
            List<int> heights = new List<int>();

            for (int x = 0; x < board.Width; ++x)
            {
                heights.Add(GetHeight(board, x));
            }

            double avgHeight = heights.Average();

            for (int x = 0; x < board.Width; ++x)
            {
                bumpiness += (heights[x] - avgHeight) * (heights[x] - avgHeight);
            }

            return Math.Sqrt(bumpiness);
        }

        private int GetHeight(IBoard board, int x)
        {
            int h = 0;
            for (int y = 0; y < board.Height; ++y)
            {
                if (board.Get(x, y) != TetrominoType.Empty)
                {
                    h = y + 1;
                }
            }
            return h;
        }
    }
}
