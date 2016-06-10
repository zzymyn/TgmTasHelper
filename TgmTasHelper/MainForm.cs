using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TgmTasHelper.Undoable;
using TgmTasHelper.Simulation;
using System.IO;

namespace TgmTasHelper
{
    public partial class MainForm : Form
    {
        private FileView m_FileView = new FileView();
        private CancellableTaskHelper m_CancellableTaskHelper = new CancellableTaskHelper();
        private UndoStack m_UndoStack = new UndoStack();

        public MainForm()
        {
            InitializeComponent();
            m_UndoStack.StackChanged += m_UndoStack_StackChanged;
            m_FileView.StateChanged += m_FileView_StateChanged;

            m_PrevButton.DataBindings.Add("Enabled", m_FileView, "HasPrevious");
            m_NextButton.DataBindings.Add("Enabled", m_FileView, "HasNext");
        }

        public void Reset(IGameState newState)
        {
            m_UndoStack.Clear();
            m_FileView.File = new File(newState);
        }

        public void SelectResult(Solver.Result result)
        {
            var o = FileOperationFactory.SelectResult(m_FileView, result);
            o.Do();
            m_UndoStack.Add(o);
        }

        private void m_UndoStack_StackChanged(bool undoAvailable, bool redoAvailable)
        {
            m_UndoMenuItem.Enabled = undoAvailable;
            m_RedoMenuItem.Enabled = redoAvailable;
        }

        private void m_FileView_StateChanged(object sender, EventArgs e)
        {
            var gameState = m_FileView.State;

            m_CancellableTaskHelper.Start(async (CancellationToken ct) =>
            {
                m_Choices.Controls.Clear();

                if (gameState == null)
                {
                    m_Time.Text = string.Empty;
                    m_Level.Text = string.Empty;
                    m_CurrentBoardRenderer.Reset();
                    m_Preview.Reset();
                    m_PreviewStrip.Reset();
                }
                else
                {
                    m_Time.Text = gameState.TimeString;
                    m_Level.Text = string.Format("Level: {0}", gameState.Level);
                    m_CurrentBoardRenderer.SetBoard(gameState.Board);
                    m_Preview.SetPreview(gameState);
                    m_PreviewStrip.SetPreviewStrip(gameState);

                    ct.ThrowIfCancellationRequested();

                    var scorer = new ResultScorer();

                    var results = await Task.Run(() =>
                    {
                        return Solver.BruteForce(gameState)
                            .Select(a => new { Result = a, Score = scorer.ScoreResult(a) })
                            .Where(a => a.Score < 100)
                            .OrderBy(a => a.Score)
                            .Select(a => a.Result);
                    });

                    ct.ThrowIfCancellationRequested();

                    try
                    {
                        m_Choices.SuspendLayout();
                        foreach (var a in results)
                        {
                            ct.ThrowIfCancellationRequested();
                            SolverResultControl r = CreateSolverResultControl();
                            r.Result = a;
                            r.Visible = true;
                        }
                    }
                    finally
                    {
                        m_Choices.ResumeLayout();
                    }
                }
            });
        }

        private SolverResultControl CreateSolverResultControl()
        {
            var r = new SolverResultControl();
            r.Result = null;
            r.Click += (object sender, EventArgs e) =>
            {
                SelectResult(r.Result);
            };
            m_Choices.Controls.Add(r);
            return r;
        }

        private void m_NewMenuItem_Click(object sender, EventArgs e)
        {
            Reset(new GameState(
                TetrominoType.L,
                new TgmGameRules(),
                new TgmRng(0x809CCF4Eu, TetrominoType.L)));
        }

        private void m_UndoMenuItem_Click(object sender, EventArgs e)
        {
            m_UndoStack.Undo();
        }

        private void m_RedoMenuItem_Click(object sender, EventArgs e)
        {
            m_UndoStack.Redo();
        }

        private void m_BackButton_Click(object sender, EventArgs e)
        {
            m_FileView.Previous();
        }

        private void m_NextButton_Click(object sender, EventArgs e)
        {
            m_FileView.Next();
        }

        private void m_SaveMenuItem_Click(object sender, EventArgs e)
        {
            if (m_FileView.File != null)
            {
                using (var a = new MemoryStream())
                {
                    m_FileView.File.Save(a);
                    var b = Encoding.UTF8.GetString(a.ToArray()); ;
                    b = b + b;
                }
            }
        }
    }
}
