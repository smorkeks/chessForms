using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChessForms.src;

namespace ChessForms.AI
{
    class TreeNode
    {
        int score;
        TreeNode parent;
        List<TreeNode> children;
        Tuple<uint, uint, uint, uint> move;
        Board board;

        public TreeNode(int score)
        {
            this.score = score;
            this.parent = null;
            this.children = new List<TreeNode>();
        }

        public TreeNode(int score, TreeNode parent, Tuple<uint, uint, uint, uint> move, Board board)
        {
            this.score = score;
            this.parent = parent;
            this.move = move;
            this.board = board;
            this.children = new List<TreeNode>();
        }

        public TreeNode(int score, TreeNode parent, List<TreeNode> children, Tuple<uint, uint, uint, uint> move, Board board)
        {
            this.score = score;
            this.parent = parent;
            this.children = children;
            this.move = move;
            this.board = board;
        }

        public void addChild(TreeNode child)
        {
            children.Add(child);
        }

        public int getScore()
        {
            return score;
        }

        public int getMax(TreeNode node)
        {
            int max = -100;
            foreach (TreeNode child in children)
            {
                if (child.getScore() > max)
                    max = child.score;
            }
            return max;
        }

        public int getMin(TreeNode node)
        {
            int min = 100;
            foreach (TreeNode child in children)
            {
                if (child.getScore() > min)
                    min = child.score;
            }
            return min;
        }

        
    }
}
