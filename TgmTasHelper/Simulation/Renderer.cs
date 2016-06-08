using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TgmTasHelper.Properties;
using TgmTasHelper.Simulation;

namespace TgmTasHelper.Simulation
{
    public static class Renderer
    {
        private static ThreadLocal<Bitmaps> s_Bitmaps = new ThreadLocal<Bitmaps>(() => { return new Bitmaps(); });

        private class Bitmaps
        {
            public TextureBrush EmptyBrush;
            public Bitmap White;
            public Dictionary<TetrominoType, Bitmap> Locked;
            public Dictionary<TetrominoType, Bitmap> Active;

            public Bitmaps()
            {
                EmptyBrush = new TextureBrush(Resources.Empty, WrapMode.Tile);
                White = Resources.White;
                Locked = new Dictionary<TetrominoType, Bitmap>()
                {
                    {TetrominoType.I, Resources.Locked},
                    {TetrominoType.Z, Resources.Locked},
                    {TetrominoType.S, Resources.Locked},
                    {TetrominoType.J, Resources.Locked},
                    {TetrominoType.L, Resources.Locked},
                    {TetrominoType.O, Resources.Locked},
                    {TetrominoType.T, Resources.Locked},
                };
                Active = new Dictionary<TetrominoType, Bitmap>()
                {
                    {TetrominoType.I, Resources.I},
                    {TetrominoType.Z, Resources.Z},
                    {TetrominoType.S, Resources.S},
                    {TetrominoType.J, Resources.J},
                    {TetrominoType.L, Resources.L},
                    {TetrominoType.O, Resources.O},
                    {TetrominoType.T, Resources.T},
                };
            }
        }

        public static Bitmap RenderBoard(IBoard board, int blockSize, CancellationToken cancel)
        {
            return RenderBoard(board, null, blockSize, cancel);
        }

        public static Bitmap RenderBoard(IBoard board, ITetromino tetromino, int blockSize, CancellationToken cancel)
        {
            var bitmaps = s_Bitmaps.Value;

            cancel.ThrowIfCancellationRequested();
            Bitmap bitmap = new Bitmap(board.Width * blockSize, board.HeightVisible * blockSize, PixelFormat.Format24bppRgb);

            using (var g = Graphics.FromImage(bitmap))
            {
                g.CompositingMode = CompositingMode.SourceCopy;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;

                cancel.ThrowIfCancellationRequested();

                {
                    float scale = (float)blockSize / bitmaps.EmptyBrush.Image.Width;
                    bitmaps.EmptyBrush.ResetTransform();
                    bitmaps.EmptyBrush.ScaleTransform(scale, scale);
                    g.FillRectangle(bitmaps.EmptyBrush, 0, 0, blockSize * board.Width, blockSize * board.HeightVisible);
                }

                board.ForEachVisible((int x, int y, TetrominoType tetrominoType) =>
                {
                    if (tetrominoType != TetrominoType.Empty)
                    {
                        cancel.ThrowIfCancellationRequested();
                        PaintBlock(g, x, board.HeightVisible - y - 1, blockSize, bitmaps.White, 1.0f);
                    }
                });

                board.ForEachVisible((int x, int y, TetrominoType tetrominoType) =>
                {
                    if (tetrominoType != TetrominoType.Empty)
                    {
                        cancel.ThrowIfCancellationRequested();
                        PaintBlock(g, x, board.HeightVisible - y - 1, blockSize, bitmaps.Locked[tetrominoType]);
                    }
                });

                if (tetromino != null)
                {
                    foreach (var p in board.GameRules.GetTetrominoPoints(tetromino))
                    {
                        cancel.ThrowIfCancellationRequested();
                        PaintBlock(g, p.x, board.HeightVisible - p.y - 1, blockSize, bitmaps.Active[tetromino.Type]);
                    }
                }
            }

            return bitmap;
        }

        public static Bitmap RenderPreview(IGameState gameState, int blockSize, CancellationToken cancel)
        {
            var bitmaps = s_Bitmaps.Value;

            cancel.ThrowIfCancellationRequested();
            Bitmap bitmap = new Bitmap(gameState.Board.Width * blockSize, 4 * blockSize, PixelFormat.Format24bppRgb);

            using (var g = Graphics.FromImage(bitmap))
            {
                g.CompositingMode = CompositingMode.SourceCopy;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;

                foreach (var p in gameState.GameRules.GetTetrominoPoints(gameState.NextTetromino, Vec2.Zero, 0))
                {
                    cancel.ThrowIfCancellationRequested();
                    PaintBlock(g, gameState.Board.GetSpawnPos().x + p.x, 2 - p.y - 1, blockSize, bitmaps.Active[gameState.NextTetromino]);
                }
            }

            return bitmap;
        }

        public static Bitmap RenderPreviewStrip(IGameState gameState, int pieceCount, int blockSize, CancellationToken cancel)
        {
            var bitmaps = s_Bitmaps.Value;

            cancel.ThrowIfCancellationRequested();
            Bitmap bitmap = new Bitmap((5 * pieceCount + 1) * blockSize, 4 * blockSize, PixelFormat.Format24bppRgb);

            using (var g = Graphics.FromImage(bitmap))
            {
                g.CompositingMode = CompositingMode.SourceCopy;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;

                int x = 3;
                foreach (var t in gameState.Rng.Peek().Take(pieceCount))
                {
                    foreach (var p in gameState.GameRules.GetTetrominoPoints(t, Vec2.Zero, 0))
                    {
                        cancel.ThrowIfCancellationRequested();
                        PaintBlock(g, x + p.x, 2 - p.y - 1, blockSize, bitmaps.Active[t]);
                    }
                    x += 5;
                }
            }

            return bitmap;
        }

        private static void PaintBlock(Graphics g, int x, int y, int blockSize, Bitmap bitmap, float sizeMod = 0.0f)
        {
            if (bitmap == null)
                return;
            g.DrawImage(bitmap,
                blockSize * x - sizeMod,
                blockSize * y - sizeMod,
                blockSize + 2.0f * sizeMod,
                blockSize + 2.0f * sizeMod);
        }
    }
}
