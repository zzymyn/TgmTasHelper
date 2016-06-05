using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgmTasHelper.Simulation
{
    public class GameState
    {
        public Board Board { get; private set; }
        public Tetromino ActiveTetromino { get; private set; }
        public int Level { get; private set; }

        public GameState()
        {
            Board = new Board(10, 20);
            ActiveTetromino = null;
            Level = 0;
        }
    }
}
