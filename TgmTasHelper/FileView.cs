using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgmTasHelper.Simulation;

namespace TgmTasHelper
{
    public class FileView : INotifyPropertyChanged
    {
        private File m_File = null;
        private int m_Index = 0;

        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler StateChanged;

        public File File
        {
            get { return m_File; }
            set
            {
                m_File = value;
                if (m_File != null)
                {
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

        public bool HasPrevious
        {
            get
            {
                return m_File != null && m_Index > 0;
            }
        }

        public bool HasNext
        {
            get
            {
                return m_File != null && m_Index < m_File.States.Count - 1;
            }
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

        public FileView()
        {
        }

        public void Previous()
        {
            if (m_Index > 0)
            {
                --m_Index;
                NotifyChanged();
            }
        }

        public void Next()
        {
            if (m_Index < m_File.States.Count - 1)
            {
                ++m_Index;
                NotifyChanged();
            }
        }

        private void NotifyChanged()
        {
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
