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
            public ITetromino Tetromino { get; private set; }
            public List<Input> Inputs { get; private set; }
            public IGameState NextState { get; private set; }

            internal Result(ITetromino tetromino, List<Input> inputs, IGameState nextState)
            {
                Tetromino = tetromino;
                Inputs = inputs;
                NextState = nextState;
            }
        }

        private class SubState
        {
            public ITetromino Tetromino { get; set; }
            public List<Input> Inputs { get; set; }

            public SubState(ITetromino t, Input input)
            {
                Tetromino = t;
                Inputs = new List<Input>(){ input};
            }

            public SubState(ITetromino t, SubState other, Input input)
            {
                Tetromino = t;
                Inputs = new List<Input>(other.Inputs);
                Inputs.Add(input);
            }
        }

        public static IEnumerable<Result> BruteForce(IGameState state)
        {
            // TODO: parallelize this
            TetrominoType tetrominoType = state.NextTetromino;

            var simulator = new PieceSimulator(state.GameRules, state.Board);
            var comparer = new TetrominoComparer(state.GameRules);

            var seenStates = new HashSet<ITetromino>(comparer);
            var subStates = new Queue<SubState>();

            // IRS:
            foreach (var input in Input.Initials())
            {
                var t = simulator.SpawnTetromino(tetrominoType, input);
                if (t == null)
                    continue;
                if (seenStates.Contains(t))
                    continue;
                seenStates.Add(t);
                subStates.Enqueue(new SubState(t, input));
            }

            while (subStates.Count > 0)
            {
                var prev = subStates.Dequeue();

                {
                    var nextState = state.Next(prev.Tetromino);
                    yield return new Result(prev.Tetromino, prev.Inputs, nextState);
                }

                foreach (var input in Input.Successors(prev.Inputs))
                {
                    var t = simulator.StepTetromino(prev.Tetromino, input);
                    if (seenStates.Contains(t))
                        continue;
                    seenStates.Add(t);
                    subStates.Enqueue(new SubState(t, prev, input));
                }
            }
        }
    }
}
