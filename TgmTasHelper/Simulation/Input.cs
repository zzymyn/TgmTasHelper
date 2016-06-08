using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgmTasHelper.Simulation
{
    [DebuggerDisplay("Move: {Move}, Rotate: {Rotate}")]
    public struct Input
    {
        public Movement Move { get; private set; }
        public Rotation Rotate { get; private set; }

        public static IEnumerable<Input> Initials()
        {
            foreach (var r in new[] { Rotation.None, Rotation.A, Rotation.B, })
            {
                yield return new Input() { Move = Movement.None, Rotate = r, };
            }
        }

        public static IEnumerable<Input> Successors(IList<Input> previousInputs)
        {
            Debug.Assert(previousInputs.Count > 0);

            Input prevInput = previousInputs.Last();

            // check DAS still charged:
            bool dasCharged = GetDasCharged(previousInputs);

            foreach (Movement tryMove in Enum.GetValues(typeof(Movement)))
            {
                // can't do the same movement twice in a row, unless we have DAS charge:
                if (tryMove.IsLeftRight() && tryMove == prevInput.Move && !dasCharged)
                    continue;

                foreach (Rotation tryRotate in Enum.GetValues(typeof(Rotation)))
                {
                    // don't return do-nothing inputs:
                    if (tryMove == Movement.None && tryRotate == Rotation.None)
                        continue;

                    // can't do the same rotation button twice in a row:
                    if (tryRotate != Rotation.None && tryRotate == prevInput.Rotate)
                        continue;

                    yield return new Input()
                    {
                        Move = tryMove,
                        Rotate = tryRotate,
                    };
                }
            }
        }

        private static bool GetDasCharged(IList<Input> previousInputs)
        {
            // we ignore the first input as it's the IRS frame and the piece can't move:
            if (previousInputs.Count < 2)
                return true;

            var move = previousInputs[1].Move;

            if (!move.IsLeftRight())
                return false;

            return previousInputs.Skip(1).All(a => a.Move == move);
        }

        private Input(Movement move, Rotation rotate) : this()
        {
            Move = move;
            Rotate = rotate;
        }
    }
}
