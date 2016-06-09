using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TgmTasHelper.Simulation;

namespace TgmTasHelper
{
    public partial class SolverResultControl : UserControl
    {
        private Solver.Result m_Result;

        public Solver.Result Result
        {
            get { return m_Result; }
            set { SetResult(value); }
        }

        public new event EventHandler Click
        {
            add
            {
                base.Click += value;
                foreach (Control control in Controls)
                {
                    control.Click += value;
                }
            }
            remove
            {
                base.Click -= value;
                foreach (Control control in Controls)
                {
                    control.Click -= value;
                }
            }
        }

        public SolverResultControl()
        {
            InitializeComponent();
        }

        private void SetResult(Solver.Result result)
        {
            m_Result = result;
            if (result == null)
            {
                m_BoardRenderer.Reset();
                m_Time.Text = string.Empty;
                m_Level.Text = string.Empty;
            }
            else
            {
                m_BoardRenderer.SetBoardAndTetromino(result.PrevState.Board, result.Step.Tetromino);
                m_Time.Text = result.NextState.TimeString;
                m_Level.Text = string.Format("Level: {0}", result.NextState.Level.ToString());
                m_BMT.Text = string.Format("BMT: {0}", result.Step.Inputs.Count - 2);
            }
        }
    }
}
