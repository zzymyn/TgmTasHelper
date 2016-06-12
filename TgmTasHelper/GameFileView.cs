using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TgmTasHelper.Simulation;
using TgmTasHelper.Undoable;

namespace TgmTasHelper
{
    public class GameFileView : INotifyPropertyChanged
    {
        private GameFile m_File = null;
        private int m_Index = 0;
        private bool m_SuspendUpdates = false;
        private bool m_ChangePending = false;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler StateChanged;

        public GameFile File
        {
            get { return m_File; }
            set
            {
                if (m_File != null)
                {
                    m_File.Changed -= m_File_Changed;
                }
                m_File = value;
                if (m_File != null)
                {
                    m_File.Changed += m_File_Changed;
                    m_Index = m_File.States.Count - 1;
                }
                else
                {
                    m_Index = 0;
                }
                NotifyChanged();
            }
        }

        public int Index
        {
            get { return m_Index; }
            set
            {
                if (m_File == null || m_File.States.Count <= 0)
                    return;
                if (value < 0)
                    value = 0;
                if (value >= m_File.States.Count)
                    value = m_File.States.Count - 1;
                if (m_Index == value)
                    return;
                m_Index = value;
                NotifyChanged();
            }
        }

        public bool HasFile
        {
            get { return m_File != null; }
        }

        public bool HasPrevious
        {
            get { return m_File != null && m_Index > 0; }
        }

        public bool HasNext
        {
            get { return m_File != null && m_Index < m_File.States.Count - 1; }
        }

        public IGameState State
        {
            get
            {
                if (m_File == null || m_Index < 0 || m_Index >= m_File.States.Count)
                    return null;
                return m_File.States[m_Index];
            }
        }

        public GameStep Step
        {
            get
            {
                if (m_File == null || m_Index < 0 || m_Index >= m_File.Steps.Count)
                    return null;
                return m_File.Steps[m_Index];
            }
        }

        public GameFileView()
        {
        }

        public void Previous()
        {
            if (m_File != null && m_Index > 0)
            {
                --m_Index;
                NotifyChanged();
            }
        }

        public void Next()
        {
            if (m_File != null && m_Index < m_File.States.Count - 1)
            {
                ++m_Index;
                NotifyChanged();
            }
        }

        public IUndoable SelectResult(Solver.Result result)
        {
            var index = m_Index;
            var fileAction = m_File.CreateSelectResult(index, result);

            var r = new GenericUndoable(() =>
            {
                SuspendUpdates();
                fileAction.Do();
                Index = index + 1;
                ResumeUpdates();
            }, () =>
            {
                SuspendUpdates();
                fileAction.Undo();
                Index = index;
                ResumeUpdates();
            });
            return r;
        }
        
        public void SuspendUpdates()
        {
            m_SuspendUpdates = true;
        }

        public void ResumeUpdates()
        {
            m_SuspendUpdates = false;
            if (m_ChangePending)
                NotifyChanged();
        }

        private void m_File_Changed(object sender, EventArgs e)
        {
            NotifyChanged();
        }

        private void NotifyChanged()
        {
            if (m_SuspendUpdates)
            {
                m_ChangePending = true;
                return;
            }
            m_ChangePending = false;
            if (StateChanged != null)
            {
                StateChanged(this, EventArgs.Empty);
            }
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("Index"));
                PropertyChanged(this, new PropertyChangedEventArgs("HasPrevious"));
                PropertyChanged(this, new PropertyChangedEventArgs("HasNext"));
                PropertyChanged(this, new PropertyChangedEventArgs("State"));
                PropertyChanged(this, new PropertyChangedEventArgs("Step"));
            }
        }
    }
}
