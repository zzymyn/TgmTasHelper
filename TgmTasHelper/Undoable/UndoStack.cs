using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgmTasHelper.Undoable
{
    public class UndoStack
    {
        private Stack<IUndoable> m_UndoStack = new Stack<IUndoable>();
        private Stack<IUndoable> m_RedoStack = new Stack<IUndoable>();

        public delegate void StackChangedHandler(bool undoAvailable, bool redoAvailable);
        public event StackChangedHandler StackChanged;

        public void Add(IUndoable operation)
        {
            m_UndoStack.Push(operation);
            m_RedoStack.Clear();
            CallStackChanged();
        }

        public void AddAndDo(Action doAction, Action undoAction)
        {
            doAction();
            Add(new GenericUndoable(doAction, undoAction));
        }

        public void Clear()
        {
            m_UndoStack.Clear();
            m_RedoStack.Clear();
            CallStackChanged();
        }

        public void Undo()
        {
            if (m_UndoStack.Count > 0)
            {
                var o = m_UndoStack.Pop();
                o.Undo();
                m_RedoStack.Push(o);
                CallStackChanged();
            }
        }

        public void Redo()
        {
            if (m_RedoStack.Count > 0)
            {
                var o = m_RedoStack.Pop();
                o.Do();
                m_UndoStack.Push(o);
                CallStackChanged();
            }
        }

        private void CallStackChanged()
        {
            if (StackChanged != null)
            {
                StackChanged(m_UndoStack.Count > 0, m_RedoStack.Count > 0);
            }
        }
    }
}
