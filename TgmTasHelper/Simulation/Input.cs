using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgmTasHelper.Simulation
{
    [DebuggerDisplay("Move: {Move}, Rotate: {Rotate}")]
    public class Input
    {
        public Input PreviousInput { get; private set; }
        public Movement Move { get; private set; }
        public Rotation Rotate { get; private set; }

        public static Input Null = new Input()
        {
            PreviousInput = null,
            Move = Movement.None,
            Rotate = Rotation.None,
        };

        public static IEnumerable<Input> Initials()
        {
            foreach (var r in new[] { Rotation.None, Rotation.A, Rotation.B, })
            {
                yield return new Input() { PreviousInput = null, Move = Movement.None, Rotate = r, };
            }
        }

        public IEnumerable<Input> Successors()
        {
            // check DAS still charged:
            bool dasCharged = GetDasCharged();

            foreach (Movement tryMove in Enum.GetValues(typeof(Movement)))
            {
                // can't do the same movement twice in a row, unless we have DAS charge:
                if (tryMove.IsLeftRight() && tryMove == Move && !dasCharged)
                    continue;

                foreach (Rotation tryRotate in Enum.GetValues(typeof(Rotation)))
                {
                    // don't return do-nothing inputs:
                    if (tryMove == Movement.None && tryRotate == Rotation.None)
                        continue;

                    // can't do the same rotation button twice in a row:
                    if (tryRotate != Rotation.None && tryRotate == Rotate)
                        continue;

                    yield return new Input()
                    {
                        PreviousInput = this,
                        Move = tryMove,
                        Rotate = tryRotate,
                    };
                }
            }
        }

        private bool GetDasCharged()
        {
            // DAS is always charged at the start:
            if (PreviousInput == null)
                return true;

            if (!Move.IsLeftRight())
                return false;

            for (Input i = this; i != null; i = i.PreviousInput)
            {
                if (i.PreviousInput == null)
                    break;

                if (i.Move != Move)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
