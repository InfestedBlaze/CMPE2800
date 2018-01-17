/*
 * Nicholas Wasylyshyn
 * January 16, 2018
 * This program will read in a file, taking in each line as a string.
 * Some manipulation will take place to create a dictionary with <int, List<string>>
 * Then it will display the lowest string, and the highest string
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ICA2_NicW
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void UI_button_Read_Click(object sender, EventArgs e)
        {
            if(UI_openFileDialog.ShowDialog() == DialogResult.OK)
            {
                //Variable declarations----------------------------------------------------------
                var sReadIn = "";
                List<string> sList;
                Dictionary<string, int> lineSumDict = new Dictionary<string, int>();
                Dictionary<int, List<string>> sumListDict = new Dictionary<int, List<string>>();
                StreamReader sr = new StreamReader(UI_openFileDialog.FileName);
                //-------------------------------------------------------------------------------

                //Read in all info, close stream
                sReadIn = sr.ReadToEnd();
                sr.Close();

                //Split the string, on \r \n
                sList = sReadIn.Split(new char[] { '\n', '\r' }).ToList();

                //Remove empty lines, or only whitespace
                sList.RemoveAll(line => line.Length == 0 || char.IsWhiteSpace(line[0]));

                //Load the dictionary with <line, sum of each character>
                sList.ForEach(line => lineSumDict[line] = line.Sum(character => character));
                
                //Flipping the key/values, making a list of the keys
                foreach(var kvp in lineSumDict)
                {
                    //Our dictionary doesn't have this key yet
                    if (!sumListDict.ContainsKey(kvp.Value))
                    {
                        //Make a new list
                        sumListDict[kvp.Value] = new List<string>();
                    }
                    //Add to our list
                    sumListDict[kvp.Value].Add(kvp.Key);
                }

                //Order by our key, now the int sum of characters
                sumListDict = sumListDict.OrderBy(k => k.Key).ToDictionary(k => k.Key, v => v.Value);

                //Display Code-------------------------------------------------------------
                UI_label.Text = "";
                UI_label.Text += $"Lowest ASCII Sum: {sumListDict.First().Key}\n";
                UI_label.Text += $"Lowest String: {sumListDict.First().Value.Min()}\n";
                UI_label.Text += $"Highest ASCII Sum: {sumListDict.Last().Key}\n";
                UI_label.Text += $"Highest String: {sumListDict.Last().Value.Max()}/{new string(sumListDict.Last().Value.Max().OrderBy(c => c).ToArray())}";
            }
        }
    }
}
