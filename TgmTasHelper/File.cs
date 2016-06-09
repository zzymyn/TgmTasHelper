using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgmTasHelper.Simulation;

namespace TgmTasHelper
{
    public class File
    {
        public List<IGameState> States { get; private set; }
        public List<GameStep> Steps { get; private set; }

        public File()
        {
            States = new List<IGameState>();
            Steps = new List<GameStep>();
        }

        public File(IGameState initialState) : this()
        {
            States.Add(initialState);
        }
    }
}
