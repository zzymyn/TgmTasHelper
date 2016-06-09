using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgmTasHelper.Simulation
{
    public static class Solver
    {
        public class Result
        {
            public IGameState PrevState { get; private set; }
            public GameStep Step { get; private set; }
            public IGameState NextState { get; private set; }

            internal Result(GameStep step, IGameState prevState, IGameState nextState)
            {
                PrevState = prevState;
                Step = step;
                NextState = nextState;
            }
        }

        public static IEnumerable<Result> BruteForce(IGameState state)
        {
            // TODO: parallelize this
            TetrominoType tetrominoType = state.NextTetromino;

            var simulator = new PieceSimulator(state.GameRules, state.Board);
            var comparer = new TetrominoComparer(state.GameRules);

            var seenStates = new HashSet<ITetromino>(comparer);
            var steps = new Queue<GameStep>();

            // IRS:
            foreach (var input in Input.Initials())
            {
                var t = simulator.SpawnTetromino(tetrominoType, input);
                if (t == null)
                    continue;
                if (seenStates.Contains(t))
                    continue;
                seenStates.Add(t);
                steps.Enqueue(new GameStep(t, input));
            }

            while (steps.Count > 0)
            {
                var prev = steps.Dequeue();

                if (prev.Tetromino.Locked)
                {
                    var nextState = state.Next(prev.Tetromino, prev.Inputs);
                    yield return new Result(
                        new GameStep(prev.Tetromino, prev.Inputs),
                        state,
                        nextState);
                    continue;
                }

                foreach (var input in Input.Successors(prev.Inputs))
                {
                    var t = simulator.StepTetromino(prev.Tetromino, input);

                    if (seenStates.Contains(t))
                        continue;
                    seenStates.Add(t);

                    steps.Enqueue(new GameStep(t, prev.Inputs, input));
                }
            }
        }
    }
}
