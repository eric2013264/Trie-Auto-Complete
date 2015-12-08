// Eric Chen 11381898

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HW13.Trie;
using System.IO;
using System.Threading;

namespace HW13
{
    public partial class Form1 : Form
    {
        private delegate void ObjectDelegate(object obj);

        Trie_Tree Trie = new Trie_Tree();
        //int count = 0;

        public Form1()
        {
            InitializeComponent();

            GenerateTrie(); // Look! No threads!
        }

        /// <summary>
        /// Generates the trie tree. Reads textfile from path within the project file (File path in comments of function)
        /// </summary>
        /// <param name="obj"></param>
        public void GenerateTrie()
        {
            if (!File.Exists("wordsEn.txt"))    // File is located in HW13>bin>Debug For some reason simply putting the file name defaulted to search there so why not
            {
                MessageBox.Show("Error reading text file.");    // This should get the users attention, will cause failure to add words
                return;
            }
            else
            {
                // Open the file to read from.
                using (StreamReader sr = File.OpenText("wordsEn.txt"))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Trie.AddWord(s);
                        //count++;
                    }
                }
            }

            // DEBUG 
            // MessageBox.Show("Added " + count + " words");
        }

        /// <summary>
        /// Originally this was a threaded Update_Status function that invoked when required
        /// But after emailing Evan about why invoking to a textBox was slow, he basically sent me the following function
        /// TextChanged event that 1. clears the output and 2. searches for and adds all words containing the prefix to the output
        /// Output has changed from textBox to the weirdly fast, listBox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void textBox1_TextChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();

            var input = sender;                     // Shoutout to inferred types

            string input_prefix_string = "";

            input_prefix_string = textBox1.Text;    // Read the prefix from the user input textBox

            if (string.IsNullOrEmpty(input_prefix_string))
            {
                return;
            }

            List<string> words = new List<string>();
            search(input_prefix_string, words);

            listBox1.Items.AddRange(words.ToArray());
        }

        /// <summary>
        /// Searches for the input prefix string given by the user and finds all the words that match (or contain it) in the trie containing it.
        /// Then outputs all of them to a list.
        /// </summary>
        /// <param name="input_prefix_string"></param>
        /// <param name="words"></param>
        public void search(string input_prefix_string, List<string> words)
        {

            if (Trie.Search(input_prefix_string) && !(string.IsNullOrEmpty(input_prefix_string))) // first see if we have anything that even includes it, check for blank string
            {
                var matches = Trie.Get_Words(input_prefix_string);

                if (matches.Count > 0)
                {
                    foreach (var m in matches)  // Threading this loop quickly, multiple times gives an error "collection was modified"
                    {                           // Solution was to add them to a list first, then output, then add, then output, and so on...
                        words.Add(m);   
                    }
                }
            }

            else // Word containing the prefix does not exist or input prefix string was blank
            {
                return;
            }
        }
    }
}
