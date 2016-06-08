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
    public partial class SimulationRenderer : PictureBox
    {
        private CancellableTaskHelper m_CancellableTaskHelper = new CancellableTaskHelper();

        public SimulationRenderer()
        {
            InitializeComponent();
        }

        public void Reset()
        {
            m_CancellableTaskHelper.Reset();
            Image = null;
        }

        public void SetBoard(IBoard board)
        {
            if (board == null)
            {
                Reset();
                return;
            }

            DoLoad((CancellationToken ct) =>
            {
                return Renderer.RenderBoard(board, 15, ct);
            });
        }

        public void SetBoardAndTetromino(IBoard board, ITetromino tetromino)
        {
            if (board == null)
            {
                Reset();
                return;
            }

            DoLoad((CancellationToken ct) =>
            {
                return Renderer.RenderBoard(board, tetromino, 15, ct);
            });
        }

        public void SetPreview(IGameState gameState)
        {
            if (gameState == null)
            {
                Reset();
                return;
            }

            DoLoad((CancellationToken ct) =>
            {
                return Renderer.RenderPreview(gameState, 15, ct);
            });
        }

        public void SetPreviewStrip(IGameState gameState)
        {
            if (gameState == null)
            {
                Reset();
                return;
            }

            DoLoad((CancellationToken ct) =>
            {
                return Renderer.RenderPreviewStrip(gameState, 10, 15, ct);
            });
        }

        public void SetBitmap(Bitmap bitmap)
        {
            Image = bitmap;
        }

        private void DoLoad(Func<CancellationToken, Bitmap> func)
        {
            m_CancellableTaskHelper.Start(async (CancellationToken ct) =>
            {
                var bitmap = await Task.Run(() =>
                {
                    return func(ct);
                }, ct);

                ct.ThrowIfCancellationRequested();

                SetBitmap(bitmap);
            });
        }
    }
}
