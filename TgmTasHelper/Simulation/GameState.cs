using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgmTasHelper.Simulation
{
    public class GameState : IGameState
    {
        public TetrominoType NextTetromino { get; private set; }
        public IGameRules GameRules { get; private set; }
        public IRng Rng { get; private set; }
        public IBoard Board { get; private set; }
        public int Time { get; private set; }
        public int Level { get; private set; }

        public string TimeString
        {
            get
            {
                int t = Time;
                int ms = 100 * (t % 60) / 60;
                int s = (t / 60) % 60;
                int m = t / 60 / 60;
                return string.Format("{0:00}:{1:00}:{2:00}", m, s, ms);
            }
        }

        public GameState(TetrominoType firstBlock, IGameRules gameRules, IRng rng)
        {
            NextTetromino = firstBlock;
            GameRules = gameRules;
            Rng = rng;
            Board = new MutableBoard(gameRules);
            Time = 0;
            Level = 0;
        }

        public GameState(IGameState o, ITetromino tetromino, List<Input> inputs)
        {
            NextTetromino = o.Rng.Peek().First();
            GameRules = o.GameRules;
            Rng = o.Rng.Next();
            int linesCleared;
            Board = o.Board.LockTetromino(tetromino, out linesCleared);
            Time = GameRules.GetNextTime(o.Time, o.Level, inputs, linesCleared);
            Level = GameRules.GetNextLevel(o.Level, linesCleared);
        }

        public IGameState Next(ITetromino tetromino, List<Input> inputs)
        {
            return new GameState(this, tetromino, inputs);
        }
    }
}
