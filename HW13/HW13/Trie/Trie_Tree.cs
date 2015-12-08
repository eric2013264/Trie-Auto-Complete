// Eric Chen 11381898

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW13.Trie
{
    class Trie_Tree
    {
        private List<string> branches;
        private Node m_root;

        public Trie_Tree()
        {
            m_root = new Node(' ');
            branches = new List<string>();
        }

        /// <summary>
        /// Adds a string word to the trie. 
        /// </summary>
        /// <param name="word"></param>
        public void AddWord(string word)
        {
            Node currentNode = m_root;
            Node child = null;

            char[] chars = word.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                child = currentNode.GetChild(word[i]);

                if (child == null)
                {
                    var newNode = new Node(word[i]);
                    currentNode.sub_tree.Add(newNode);
                    currentNode = newNode;
                }
                else
                {
                    currentNode = child;
                }

                if (i == chars.Length - 1)  // For parsing
                {
                    currentNode.m_done = true;
                }
            }
        }

        /// <summary>
        /// Searches the trie for string prefix, returns true if it exits, false otherwise.
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public bool Search(string prefix)
        {
            Node currentNode = m_root;
            Node child = null;

            char[] chars = prefix.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                child = currentNode.GetChild(chars[i]);
                if (child == null)
                {
                    return false;
                }

                currentNode = child;
            }

            return true;
        }

        /// <summary>
        /// Finds all the words in the trie that contain or match the passed prefix string
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public List<string> Get_Words(string prefix)
        {
            StringBuilder sb = new StringBuilder();
            Node currentNode = m_root;
            Node child = null;

            char[] chars = prefix.ToCharArray();

            for (int counter = 0; counter < chars.Length; counter++)
            {
                if (counter < chars.Length - 1)
                {
                    sb.Append(chars[counter]);
                }

                child = currentNode.GetChild(chars[counter]);

                if (child == null)  // At the end, we're done here
                { 
                    break; 
                }

                currentNode = child;
            }

            branches.Clear();  // Clear original linked strings

            Create_Branches(currentNode, sb.ToString());

            return branches;
        }

        /// <summary>
        /// Makes the branches of a node from the rest of the word
        /// </summary>
        /// <param name="node"></param>
        /// <param name="sub_string"></param>
        private void Create_Branches(Node node, string sub_string)
        {
            if (node == null)
            {
                return;
            }

            sub_string = sub_string + node.letter;

            if (node.m_done)  // Until we've reached the end of the word, keep adding
            {
                branches.Add(sub_string);
            }

            foreach (var n in node.sub_tree)   // Recursively generate branches for the subnodes
            {
                Create_Branches(n, sub_string);
            }
        }

    }
}