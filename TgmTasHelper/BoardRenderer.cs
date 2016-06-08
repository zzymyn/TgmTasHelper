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
        private CancellationTokenSource m_CancelTokenSource;
        private Bitmap m_Bitmap = null;
        private Font m_Font = new Font(FontFamily.GenericMonospace, 12.0f, FontStyle.Bold);

        private Color m_BackgroundColor = Color.FromArgb(20, 20, 20);
        private Brush m_BorderBrush = new SolidBrush(Color.FromArgb(60, 60, 60));
        private Brush m_BoardBrush = new SolidBrush(Color.FromArgb(0, 0, 0));

        public BoardRenderer()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.UserPaint, true);
        }

        public void Reset()
        {
            if (m_CancelTokenSource != null)
            {
                m_CancelTokenSource.Cancel();
                m_CancelTokenSource = null;
            }

            m_CancelTokenSource = new CancellationTokenSource();
            m_Bitmap = null;
            MinimumSize = new Size(150, 150);
            MaximumSize = new Size(150, 150);
            Size = new Size(150, 150);
            Invalidate(false);
        }

        public void SetBoard(IBoard board)
        {
            DoLoad((CancellationToken ct) =>
            {
                return Renderer.RenderBoard(board, 20, ct);
            });
        }

        public void SetBoardAndTetromino(IBoard board, ITetromino tetromino, IGameRules gameRules)
        {
            DoLoad((CancellationToken ct) =>
            {
                return Renderer.RenderBoard(board, tetromino, gameRules, 20, ct);
            });
        }

        public void SetBitmap(Bitmap bitmap)
        {
            m_Bitmap = bitmap;
            MinimumSize = m_Bitmap.Size;
            MaximumSize = m_Bitmap.Size;
            Size = m_Bitmap.Size;
            Invalidate(false);
        }

        private async void DoLoad(Func<CancellationToken, Bitmap> func)
        {
            Reset();

            try
            {
                var tokenSource = m_CancelTokenSource;

                var bitmap = await Task.Run(() =>
                {
                    return func(tokenSource.Token);
                }, tokenSource.Token);

                tokenSource.Token.ThrowIfCancellationRequested();

                SetBitmap(bitmap);
            }
            catch (OperationCanceledException)
            {
                // abort
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var midPos = new Vec2F(0.5f * ClientSize.Width, 0.5f * ClientSize.Height);

            e.Graphics.Clear(m_BackgroundColor);

            if (m_Bitmap == null)
            {
                e.Graphics.DrawString("rendering...", m_Font, Brushes.White, midPos, new StringFormat()
                {
                    LineAlignment = StringAlignment.Center,
                    Alignment = StringAlignment.Center,
                });
                return;
            }
            else if (m_Bitmap != null)
            {
                e.Graphics.DrawImage(m_Bitmap, 0.0f, 0.0f);
            }
        }
    }
}
