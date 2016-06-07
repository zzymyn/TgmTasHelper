using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgmTasHelper.Properties;
using TgmTasHelper.Simulation;

namespace TgmTasHelper.Simulation
{
    public static class Renderer
    {
        public static Bitmap RenderBoard(IReadOnlyBoard board, int blockSize)
        {
            Bitmap bitmap = new Bitmap(board.Width * blockSize, board.HeightVisible * blockSize, PixelFormat.Format24bppRgb);

            using (var g = Graphics.FromImage(bitmap))
            {
                g.CompositingMode = CompositingMode.SourceCopy;
                g.CompositingQuality = CompositingQuality.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.SmoothingMode = SmoothingMode.HighQuality;

                g.Clear(Color.Black);

                board.ForEachVisible((int x, int y, TetrominoType tetrominoType) =>
                {
                    if (tetrominoType == TetrominoType.Empty)
                        PaintBlock(g, x, board.HeightVisible - y - 1, blockSize, Resources.Empty);
                });

                board.ForEachVisible((int x, int y, TetrominoType tetrominoType) =>
                {
                    if (tetrominoType != TetrominoType.Empty)
                        PaintBlock(g, x, board.HeightVisible - y - 1, blockSize, Resources.White, 1.0f);
                });

                board.ForEachVisible((int x, int y, TetrominoType tetrominoType) =>
                {
                    if (tetrominoType != TetrominoType.Empty)
                        PaintBlock(g, x, board.HeightVisible - y - 1, blockSize, GetBoardBlockBitmap(tetrominoType));
                });
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

        private static Bitmap GetBoardBlockBitmap(TetrominoType tetrominoType)
        {
            switch (tetrominoType)
            {
                default:
                    return null;
                case TetrominoType.I:
                    return Resources.IDark;
                case TetrominoType.Z:
                    return Resources.ZDark;
                case TetrominoType.S:
                    return Resources.SDark;
                case TetrominoType.J:
                    return Resources.JDark;
                case TetrominoType.L:
                    return Resources.LDark;
                case TetrominoType.O:
                    return Resources.ODark;
                case TetrominoType.T:
                    return Resources.TDark;
            }
        }
    }
}
