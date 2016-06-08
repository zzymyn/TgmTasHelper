using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgmTasHelper.Undoable
{
    public interface IUndoable
    {
        void Do();
        void Undo();
    }
}
