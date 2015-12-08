// Eric Chen 11381898

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW13.Trie
{
    class Node
    {
        public char letter;
        public bool m_done;
        public List<Node> sub_tree;

        public Node(char c)
        {
            letter = c;
            sub_tree = new List<Node>();
        }

        /// <summary>
        /// Returns the node with the given letter c from the sub_tree
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public Node GetChild(char c)
        {
            if (sub_tree.Count != 0)        // Check for an empty tree
            {
                foreach (var node in sub_tree)
                {
                    if (node.letter == c)   // Found it!
                    {
                        return node;
                    }
                }
            }

            return null;    // Node with letter c not does not exists in the sub_tree
        }
    }
}
