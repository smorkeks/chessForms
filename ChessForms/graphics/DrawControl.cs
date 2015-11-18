using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace ChessForms.graphics
{
    public class DrawControl : Control
    {

        private src.Board board;
        ImageHandler imageHandler;

        public delegate src.Move getMove();
        private getMove getSelectedMove;

        public DrawControl(getMove gm)
        {
            getSelectedMove = gm;

            //InitializeComponent();

            this.SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer, true);

            imageHandler = new ImageHandler(AppDomain.CurrentDomain.BaseDirectory + "../../resources/", "png");
        }

        public void setBoard(src.Board board)
        {
            this.board = board;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            // we draw the progressbar normally with 
            // the flags sets to our settings

            // Setup graphics objects
            //Graphics g = drawControl.CreateGraphics();
            Graphics g = pe.Graphics;

            // Calculate size parameters
            int squareWidth = this.Width / 9;
            int squareHeight = this.Height / 9;

            // Draw numbers and letters
            SolidBrush brushText = new SolidBrush(Color.Black);
            for (int i = 1; i <= 8; i++)
            {
                g.DrawString((char)(i + 'A' - 1) + "", new Font("Arial", 20), brushText, new Point((int)((i + 0.25) * squareWidth), (int)(squareHeight * 0.3)));
                g.DrawString((9 - i).ToString(), new Font("Arial", 20), brushText, new Point((int)(squareWidth * 0.3), (int)((i + 0.2) * squareHeight)));
            }

            // Draw squares
            SolidBrush brushWhite = new SolidBrush(Color.LightGray);
            SolidBrush brushBlack = new SolidBrush(Color.Gray);
            for (int y = 0; y <= 7; y++)
            {
                for (int x = 0; x <= 7; x++)
                {
                    int drawX = (x + 1) * squareWidth;
                    int drawY = (y + 1) * squareHeight;
                    Rectangle rect = new Rectangle(drawX, drawY, squareWidth, squareHeight);

                    // Draw square
                    if ((y + x) % 2 == 0)
                    {
                        // White
                        g.FillRectangle(brushWhite, rect);
                    }
                    else
                    {
                        // White
                        g.FillRectangle(brushBlack, rect);
                    }

                    src.Square square = board.getSquareAt((uint)x, (uint)(7 - y));
                    if (square.getPiece() != null)
                    {
                        // Draw piece
                        /*g.DrawString(square.getPiece().getColour().Replace('w', 'W').Replace('b', 'B'),
                                     new Font("Arial", 10),
                                     brushText,
                                     new Point((int)(drawX + squareWidth * 0.2), (int)(drawY + squareHeight * 0.35)));
                        g.DrawString(square.getPiece().GetType().ToString().Substring(15),
                                     new Font("Arial", 10),
                                     brushText,
                                     new Point((int)(drawX + squareWidth * 0.2), (int)(drawY + squareHeight * 0.55)));*/
                        src.Piece p = square.getPiece();
                        string type = p.GetType().Name;
                        string colour = p.getColour().Replace('w', 'W').Replace('b', 'B');
                        string filename = type + "_" + colour;
                        g.DrawImage(imageHandler.getImage(filename),
                                    new Point[] {new Point(drawX + 8, drawY + 8),
                                                 new Point(drawX + squareWidth - 8, drawY + 8),
                                                 new Point(drawX + 8, drawY + squareHeight - 8)});
                    }
                }
            }

            // Selection, moves and cover
            SolidBrush brushSelect = new SolidBrush(Color.Green);
            SolidBrush brushMoves = new SolidBrush(Color.Red);
            src.Move selectedMove = getSelectedMove();

            if (selectedMove.hasFrom())
            {
                src.Piece p = board.getPieceAt(selectedMove.FromX, selectedMove.FromY);
                if (p != null)
                {
                    int drawX = ((int)selectedMove.FromX + 1) * squareWidth;
                    int drawY = (7 - (int)selectedMove.FromY + 1) * squareHeight;

                    // Mark selected square
                    drawBorder(g, brushSelect, drawX, drawY, squareWidth, squareHeight, 5);

                    foreach (Tuple<uint, uint> t in ChessForms.rules.Rules.getPossibleMoves(board, p))
                    {
                        drawX = (int)(t.Item1 + 1) * squareWidth;
                        drawY = (int)(7 - t.Item2 + 1) * squareHeight;
                        drawBorder(g, brushMoves, drawX, drawY, squareWidth, squareHeight, 5);
                    }
                }
            }

            brushWhite.Dispose();
            brushBlack.Dispose();
            //g.Dispose();
        }

        // Draw a border of specified width, height and thickness
        private void drawBorder(Graphics g, SolidBrush b, int x, int y, int w, int h, int t)
        {
            g.FillRectangle(b, x, y, w, t);
            g.FillRectangle(b, x, y, t, h);
            g.FillRectangle(b, x, y + h - t, w, t);
            g.FillRectangle(b, x + w - t, y, t, h);
        }
    }
}
