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
        public TetrominoType NextTetromino { get; private set; }
        public IGameRules GameRules { get; private set; }
        public IRng Rng { get; private set; }
        public IBoard Board { get; private set; }
        public int Level { get; private set; }

        public GameState(TetrominoType firstBlock, IGameRules gameRules, IRng rng)
        {
            PreviousGameState = null;
            NextTetromino = firstBlock;
            GameRules = gameRules;
            Rng = rng;
            Board = new MutableBoard(gameRules);
            Level = 0;
        }

        public GameState(IGameState other, ITetromino tetromino)
        {
            PreviousGameState = other;
            NextTetromino = other.Rng.Peek().First();
            GameRules = other.GameRules;
            Rng = other.Rng.Next();

            int linesCleared;
            Board = other.Board.LockTetromino(tetromino, out linesCleared);
            Level = other.Level;
        }

        public IGameState Next(ITetromino tetromino)
        {
            return new GameState(this, tetromino);
        }
    }
}
