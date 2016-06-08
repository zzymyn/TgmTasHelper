using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgmTasHelper.Simulation
{
    public class GameState : IGameState
    {
        public IGameState PreviousGameState { get; private set; }
        public List<Input> Inputs { get; private set; }
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
            PreviousGameState = null;
            Inputs = null;
            NextTetromino = firstBlock;
            GameRules = gameRules;
            Rng = rng;
            Board = new MutableBoard(gameRules);
            Time = 0;
            Level = 0;
        }

        public GameState(IGameState other, ITetromino tetromino, List<Input> inputs)
        {
            PreviousGameState = other;
            Inputs = inputs;
            NextTetromino = other.Rng.Peek().First();
            GameRules = other.GameRules;
            Rng = other.Rng.Next();

            int linesCleared;
            Board = other.Board.LockTetromino(tetromino, out linesCleared);
            Time = GameRules.GetNextTime(other.Time, other.Level, inputs, linesCleared);
            Level = GameRules.GetNextLevel(other.Level, linesCleared);
        }

        public IGameState Next(ITetromino tetromino, List<Input> inputs)
        {
            return new GameState(this, tetromino, inputs);
        }
    }
}
