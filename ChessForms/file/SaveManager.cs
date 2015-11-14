using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Linq;

namespace ChessForms.file
{
    public class SaveManager
    {
        private static string savePath = AppDomain.CurrentDomain.BaseDirectory + "../../saveFiles/";
        private static string fileEnding = "txt";
        private static string STATE = "state";
        private static string MOVES = "moves";
        private static string SNAPSHOT = "snapshot";
        
        public static void saveState(src.Board b, string fileName)
        {
            // Create directory if not exists
            bool exists = System.IO.Directory.Exists(savePath);
            if (!exists)
            {
                System.IO.Directory.CreateDirectory(savePath);
            }

            // Clear file, might be a better way
            string fullName = savePath + fileName + "_" + STATE + "." + fileEnding;
            File.WriteAllText(fullName, string.Empty);

            // Create Xml tree root
            XElement tree = new XElement("Board");
            tree.Add(new XElement("Turn", b.getTurn()));
            XElement pieces = new XElement("Pieces");

            // Get all squares with a piece
            var saveSquares = from s in b.getAllSquares()
                              where s.getPiece() != null
                              select s.getPiece();

            // Create squares subtree tree
            foreach (src.Piece p in saveSquares)
            {
                XElement piece = new XElement("Piece",
                                              new XElement("X-coordinate", p.getX()),
                                              new XElement("Y-coordinate", p.getY()),
                                              new XElement("Type", p.GetType().Name),
                                              new XElement("Colour", p.getColour()),
                                              new XElement("Moved", p.movedFromInit()));
                if (p is src.Pawn)
                {
                    piece.Add(new XElement("DoubleStepTurn", ((src.Pawn) p).getDoubleStepTurn()));
                }
                pieces.Add(piece);
            }
            tree.Add(pieces);
            
            using (StreamWriter sw = File.AppendText(fullName))
            {
                sw.WriteLine(tree);
            }
        }

        public static void saveMove(src.Move m, string fileName)
        {
            // Create directory if not exists
            bool exists = System.IO.Directory.Exists(savePath);
            if (!exists)
            {
                System.IO.Directory.CreateDirectory(savePath);
            }

            // Append move to file
            using (StreamWriter sw = File.AppendText(savePath + fileName + "_" + MOVES + "." + fileEnding))
            {
                sw.WriteLine(m.ToString());
            }
        }

        public static List<src.Move> loadMoves(string fileName)
        {
            string fullName = savePath + fileName + "_" + MOVES + "." + fileEnding;

            // Check if file exists
            bool exists = System.IO.File.Exists(fullName);
            if (!exists)
            {
                return new List<src.Move>();
            }

            // Read moves from file
            List<src.Move> moves = new List<src.Move>();
            string[] lines = File.ReadAllLines(fullName);
            foreach (string line in lines)
            {
                uint fx = uint.Parse(line.Substring(1, 1));
                uint fy = uint.Parse(line.Substring(4, 1));
                uint tx = uint.Parse(line.Substring(7, 1));
                uint ty = uint.Parse(line.Substring(10, 1));
                moves.Add(new src.Move(fx, fy, tx, ty));
            }

            return moves;
        }
    }
}
