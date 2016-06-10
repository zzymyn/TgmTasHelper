using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TgmTasHelper.Simulation
{
    [DataContract]
    public class GameStep
    {
        [DataMember]
        public ITetromino Tetromino { get; private set; }
        [DataMember]
        public List<Input> Inputs { get; private set; }

        public GameStep(ITetromino tetromino, List<Input> inputs)
        {
            Tetromino = tetromino;
            Inputs = inputs;
        }

        public GameStep(ITetromino tetromino, Input input)
        {
            Tetromino = tetromino;
            Inputs = new List<Input>() { input };
        }

        public GameStep(ITetromino t, List<Input> inputs, Input input)
        {
            Tetromino = t;
            Inputs = new List<Input>(inputs);
            Inputs.Add(input);
        }
    }
}
