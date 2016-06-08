using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgmTasHelper.Simulation;

namespace TgmTasHelper.Undoable
{
    public class GenericUndoable : IUndoable
    {
        private Action m_DoAction;
        private Action m_UndoAction;

        public GenericUndoable(Action doAction, Action undoAction)
        {
            m_DoAction = doAction;
            m_UndoAction = undoAction;
        }

        public void Do()
        {
            m_DoAction();
        }

        public void Undo()
        {
            m_UndoAction();
        }
    }
}
