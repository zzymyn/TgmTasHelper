using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TgmTasHelper.Simulation;

namespace TgmTasHelper
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            GameState g = new GameState(
                TetrominoType.L,
                new TgmGameRules(),
                new TgmRng(0x809CCF4E, TetrominoType.L)
                );
            m_CurrentBoardRenderer.SetBoard(g.Board);

            foreach (var a in Solver.BruteForce(g))
            {
                var r = new BoardRenderer();
                m_Choices.Controls.Add(r);
                r.SetBoardAndTetromino(g.Board, a.Tetromino, g.GameRules);
            }
        }
    }
}
