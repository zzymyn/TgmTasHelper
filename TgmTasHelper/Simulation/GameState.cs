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
        public Tetromino NextTetromino { get; private set; }
        public int Level { get; private set; }

        public GameState()
        {
            Board = new Board();
            NextTetromino = null;
            Level = 0;
        }
    }
}
