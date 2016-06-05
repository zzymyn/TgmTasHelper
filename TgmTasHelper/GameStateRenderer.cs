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
    public partial class GameStateRenderer : Control
    {
        private const float BlockSize = 20.0f;

        private GameState m_GameState = null;
        private Font m_Font = new Font(FontFamily.GenericMonospace, 12.0f, FontStyle.Bold);

        private Color m_BackgroundColor = Color.FromArgb(20, 20, 20);
        private Brush m_BorderBrush = new SolidBrush(Color.FromArgb(60, 60, 60));
        private Brush m_BoardBrush = new SolidBrush(Color.FromArgb(0, 0, 0));

        private GameState GameState
        {
            get { return m_GameState; }
            set
            {
                m_GameState = value;
                Invalidate();
            }
        }

        public GameStateRenderer()
        {
            InitializeComponent();
            Size = new System.Drawing.Size(220, 420);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.Opaque, true);
            SetStyle(ControlStyles.UserPaint, true);
            m_GameState = new GameState();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.CompositingMode = CompositingMode.SourceCopy;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

            var midPos = new Vec2F(0.5f * ClientSize.Width, 0.5f * ClientSize.Height);

            e.Graphics.Clear(m_BackgroundColor);

            if (GameState == null)
            {
                e.Graphics.DrawString("no state", m_Font, Brushes.White, midPos, new StringFormat()
                {
                    LineAlignment = StringAlignment.Center,
                    Alignment = StringAlignment.Center,
                });
                return;
            }

            var boardSize = BlockSize * new Vec2F(GameState.Board.Width, GameState.Board.Height);
            var boardOrigin = new Vec2F(midPos.x - 0.5f * boardSize.x, midPos.y + 0.5f * boardSize.y);

            GameState.Board.ForEach((int x, int y, TetrominoType tetrominoType) =>
            {
                if (tetrominoType == TetrominoType.Empty)
                    PaintBlock(e, boardOrigin, x, y, Resources.Empty);
            });

            GameState.Board.ForEach((int x, int y, TetrominoType tetrominoType) =>
            {
                if (tetrominoType != TetrominoType.Empty)
                    PaintBlock(e, boardOrigin, x, y, Resources.White, 1.0f);
            });

            GameState.Board.ForEach((int x, int y, TetrominoType tetrominoType) =>
            {
                if (tetrominoType != TetrominoType.Empty)
                    PaintBlock(e, boardOrigin, x, y, GetBoardBlockBitmap(tetrominoType));
            });

            if (GameState.ActiveTetromino != null)
            {
                foreach (var p in GameState.ActiveTetromino.GetPoints())
                {
                    PaintBlock(e, boardOrigin, p.x, p.y, GetPieceBlockBitmap(GameState.ActiveTetromino.TetrominoType));
                }
            }

            for (int x = -1; x < GameState.Board.Width + 1; ++x)
            {
                PaintBlock(e, boardOrigin, x, -1, Resources.Edge);
                PaintBlock(e, boardOrigin, x, GameState.Board.Height, Resources.Edge);
            }

            for (int y = 0; y < GameState.Board.Height; ++y)
            {
                PaintBlock(e, boardOrigin, -1, y, Resources.Edge);
                PaintBlock(e, boardOrigin, GameState.Board.Width, y, Resources.Edge);
            }
        }

        private static void PaintBlock(PaintEventArgs e, Vec2F boardOrigin, int x, int y, Bitmap bitmap, float sizeMod = 0.0f)
        {
            if (bitmap == null)
                return;
            e.Graphics.DrawImage(bitmap,
                boardOrigin.x + BlockSize * x - sizeMod,
                boardOrigin.y - BlockSize * (y + 1) - sizeMod,
                BlockSize + 2.0f * sizeMod,
                BlockSize + 2.0f * sizeMod);
        }

        private Bitmap GetBoardBlockBitmap(TetrominoType tetrominoType)
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

        private Bitmap GetPieceBlockBitmap(TetrominoType tetrominoType)
        {
            switch (tetrominoType)
            {
                default:
                    return null;
                case TetrominoType.I:
                    return Resources.I;
                case TetrominoType.Z:
                    return Resources.Z;
                case TetrominoType.S:
                    return Resources.S;
                case TetrominoType.J:
                    return Resources.J;
                case TetrominoType.L:
                    return Resources.L;
                case TetrominoType.O:
                    return Resources.O;
                case TetrominoType.T:
                    return Resources.T;
            }
        }
    }
}
