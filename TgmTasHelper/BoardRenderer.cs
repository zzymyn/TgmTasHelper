using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TgmTasHelper.Properties;
using System.Drawing.Drawing2D;
using TgmTasHelper.Simulation;
using System.Threading;

namespace TgmTasHelper
{
    public partial class BoardRenderer : Control
    {
        private IReadOnlyBoard m_Board = null;
        private Bitmap m_GameStateBitmap = null;
        private Font m_Font = new Font(FontFamily.GenericMonospace, 12.0f, FontStyle.Bold);

        private Color m_BackgroundColor = Color.FromArgb(20, 20, 20);
        private Brush m_BorderBrush = new SolidBrush(Color.FromArgb(60, 60, 60));
        private Brush m_BoardBrush = new SolidBrush(Color.FromArgb(0, 0, 0));

        public IReadOnlyBoard Board
        {
            get { return m_Board; }
            set
            {
                m_Board = value;
                m_GameStateBitmap = null;
                Invalidate();
            }
        }

        public BoardRenderer()
        {
            InitializeComponent();
            Size = new System.Drawing.Size(200, 400);
            MinimumSize = Size;
            MaximumSize = Size;
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.UserPaint, true);
            Board = new Board();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var midPos = new Vec2F(0.5f * ClientSize.Width, 0.5f * ClientSize.Height);

            e.Graphics.Clear(m_BackgroundColor);

            if (m_Board == null)
            {
                e.Graphics.DrawString("no state", m_Font, Brushes.White, midPos, new StringFormat()
                {
                    LineAlignment = StringAlignment.Center,
                    Alignment = StringAlignment.Center,
                });
                return;
            }

            if (m_GameStateBitmap == null)
            {
                m_GameStateBitmap = Renderer.RenderBoard(m_Board, 20);
            }

            e.Graphics.DrawImage(m_GameStateBitmap, 0.0f, 0.0f);
        }
    }
}
