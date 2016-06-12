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
        private GameFileView m_FileView = new GameFileView();
        private string m_FilePath = null;
        private CancellableTaskHelper m_CancellableTaskHelper = new CancellableTaskHelper();
        private List<SolverResultControl> m_SolverResultControls = new List<SolverResultControl>();
        private UndoStack m_UndoStack = new UndoStack();

        public MainForm()
        {
            InitializeComponent();

            m_UndoStack.StackChanged += m_UndoStack_StackChanged;
            m_FileView.StateChanged += m_FileView_StateChanged;

            m_UndoStack_StackChanged(this, EventArgs.Empty);
            m_FileView_StateChanged(this, EventArgs.Empty);
        }

        public void Reset(GameFile file, string filePath)
        {
            m_UndoStack.Clear();
            m_FileView.File = file;
            m_FilePath = filePath;
        }

        public void SelectResult(Solver.Result result)
        {
            var o = m_FileView.SelectResult(result);
            o.Do();
            m_UndoStack.Add(o);
        }

        private void Open()
        {
            m_OpenFileDialog.FileName = string.Empty;
            try
            {
                m_OpenFileDialog.InitialDirectory = Path.GetFullPath(m_FilePath);
            }
            catch (Exception)
            {
                m_OpenFileDialog.InitialDirectory = string.Empty;
            }

            var result = m_OpenFileDialog.ShowDialog();

            if (result != DialogResult.OK)
                return;

            Open(m_OpenFileDialog.FileName);
        }

        private void Open(string filePath)
        {
            try
            {
                using (var s = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    Reset(GameFile.Read(s), filePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Open Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Save()
        {
            if (string.IsNullOrEmpty(m_FilePath))
            {
                SaveAs();
            }
            else
            {
                Save(m_FilePath);
            }
        }

        private void SaveAs()
        {
            m_SaveFileDialog.FileName = m_FilePath;
            try
            {
                m_SaveFileDialog.InitialDirectory = Path.GetFullPath(m_FilePath);
            }
            catch (Exception)
            {
                m_SaveFileDialog.InitialDirectory = string.Empty;
            }

            var result = m_SaveFileDialog.ShowDialog();

            if (result != DialogResult.OK)
                return;

            Save(m_SaveFileDialog.FileName);
        }

        private void Save(string filePath)
        {
            try
            {
                using (var s = File.Open(filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.Read))
                {
                    m_FileView.File.Write(s);
                }
                m_FilePath = filePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void m_UndoStack_StackChanged(object sender, EventArgs e)
        {
            m_UndoMenuItem.Enabled = m_UndoStack.HasUndo;
            m_RedoMenuItem.Enabled = m_UndoStack.HasRedo;
        }

        private void m_FileView_StateChanged(object sender, EventArgs e)
        {
            var gameState = m_FileView.State;

            m_CancellableTaskHelper.Start(async (CancellationToken ct) =>
            {
                m_SaveMenuItem.Enabled = m_FileView.HasFile;
                m_SaveAsMenuItem.Enabled = m_FileView.HasFile;
                m_PrevButton.Enabled = m_FileView.HasPrevious;
                m_NextButton.Enabled = m_FileView.HasNext;

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
                }

                await Task.Delay(1, ct);
                ct.ThrowIfCancellationRequested();
                m_Choices.Controls.Clear();

                if (gameState != null)
                {
                    var scorer = new ResultScorer();

                    var results = await Task.Run(() =>
                    {
                        return Solver.BruteForce(gameState)
                            .Select(a => new { Result = a, Score = scorer.ScoreResult(a) })
                            .Where(a => a.Score < 100)
                            .OrderBy(a => a.Score)
                            .Select(a => a.Result);
                    });

                    var controls = new List<SolverResultControl>();

                    foreach (var a in results)
                    {
                        await Task.Delay(1, ct);
                        ct.ThrowIfCancellationRequested();
                        SolverResultControl r = GetOrCreateSolverResultControl(controls.Count);
                        r.Result = a;
                        controls.Add(r);
                    }

                    m_Choices.SuspendLayout();
                    m_Choices.Controls.AddRange(controls.ToArray());
                    m_Choices.ResumeLayout();
                }
            });
        }

        private SolverResultControl GetOrCreateSolverResultControl(int index)
        {
            if (index < m_SolverResultControls.Count)
            {
                return m_SolverResultControls[index];
            }
            else
            {
                var r = new SolverResultControl();
                r.Result = null;
                r.Click += (object sender, EventArgs e) =>
                {
                    SelectResult(r.Result);
                };
                m_SolverResultControls.Add(r);
                return r;
            }
        }

        private void m_NewMenuItem_Click(object sender, EventArgs e)
        {
            Reset(new GameFile(new GameState(
                TetrominoType.L,
                new TgmGameRules(),
                new TgmRng(0x809CCF4Eu, TetrominoType.L))), null);
        }

        private void m_OpenMenuItem_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void m_SaveMenuItem_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void m_SaveAsMenuItem_Click(object sender, EventArgs e)
        {
            SaveAs();
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
    }
}
