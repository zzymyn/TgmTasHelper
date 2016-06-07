using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgmTasHelper.Simulation
{
    public class GameState
    {
        public IReadOnlyBoard Board { get; private set; }
        public int Level { get; private set; }

        public GameState()
        {
            Board = new Board();
            Level = 0;
        }
    }
}
