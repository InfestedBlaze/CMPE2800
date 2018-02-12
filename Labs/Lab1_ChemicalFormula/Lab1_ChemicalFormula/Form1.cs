/*
 * Authors : Nicholas Wasylyshyn, Jonas Buro, Alec Au
 * January 24, 2018
 * This program will display elements from the periodic table.
 * You have an input field to type in elements symbols and 
 * receive the molar mass of the compound.
 * Three buttons are available to sort a list of all elements
 * in various ways.
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
using System.Text.RegularExpressions;
using System.IO;

namespace Lab1_ChemicalFormula
{
    public partial class MainForm : Form
    {
        private static string pat = @"([A-Z][a-z]?\d*)";
        private Dictionary<string, int> elementDict = new Dictionary<string, int>();
        private List<Element> elementList = new List<Element>();

        public MainForm()
        {
            InitializeComponent();

            //Load in the list of elements
            var fileText = File.ReadAllText("elements.txt");
            var strings = Regex.Split(fileText, @"[^\S-']").ToList();
            strings.RemoveAll(q => q.Equals(""));
            for (int i = 0; i < strings.Count() - 3; i += 4)
            {
                elementList.Add(new Element(int.Parse(strings[i]), strings[i + 1], strings[i + 2], double.Parse(strings[i + 3])));
            }

            //Default View
            UI_dataGridView_Chemicals.DataSource = BindingSourceMethods.SortByElementName(elementList);

        }

        private void UI_textBox_ChemFormula_TextChanged(object sender, EventArgs e)
        {
            //clear the element/value collection
            elementDict.Clear();

            //if the textbox is empty, show the defaults
            if (UI_textBox_ChemFormula.Text.Length.Equals(0))
            {
                UI_dataGridView_Chemicals.DataSource = BindingSourceMethods.SortByElementName(elementList);

                //reset the information label
                UI_label_MassCalculation.Text = "0 g/mol";

                //reset the error indicator
                UI_label_MassCalculation.BackColor = Color.White;
                return;
            }

            try
            {
                //group the input string by regex pattern
                MatchCollection matches = Regex.Matches(UI_textBox_ChemFormula.Text.Trim(), pat);

                //iterate through all regex matches
                foreach (Match m in matches)
                {
                    //split the matches
                    string[] parts = Regex.Split(m.Groups[0].Value, @"(?=\d)(?<!\d)");

                    //populate the dictionary with element names and element values
                    switch (parts.Length)
                    {
                        case 1:
                            if (!elementDict.ContainsKey(parts[0]))
                            {
                                elementDict.Add(parts[0], 1);
                            }
                            else
                            {
                                elementDict[parts[0]]++;
                            }
                            break;

                        case 2:
                            if (!elementDict.ContainsKey(parts[0]))
                            {
                                elementDict.Add(parts[0], Convert.ToInt32(parts[1]));
                            }
                            else
                            {
                                elementDict[parts[0]] += Convert.ToInt32(parts[1]);
                            }

                            break;

                        default:
                            break;

                    }

                }
                //determine the accuracy of input:
                bool found = false;

                //check if each input entry is in fact an element in the table
                foreach (KeyValuePair<string, int> kvp in elementDict)
                {
                    found = false;

                    foreach (Element element in elementList)
                    {
                        if (kvp.Key.Equals(element.Symbol))
                        {
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        break;
                    }
                }

                //concatenate all input regex matches into a string
                string matchString = string.Join("", matches.Cast<Match>().Select(m => m.Value).ToArray());

                //compare the length of the input to the length of the concatenated match string
                if (UI_textBox_ChemFormula.TextLength.Equals(matchString.Length) && found)
                {
                    UI_label_MassCalculation.BackColor = Color.LightGreen;
                }
                else
                {
                    UI_label_MassCalculation.BackColor = Color.PaleVioletRed;
                }
            }

            catch (Exception regexE)
            {
                Console.WriteLine(regexE.ToString());
            }

            //Update for the GridView will be down here after all the checks
            UI_dataGridView_Chemicals.DataSource = BindingSourceMethods.GridTextUpdate(elementDict, elementList);
            
            //Update for the label to view the total molar mass that was printed on the textbox 
            UI_label_MassCalculation.Text = BindingSourceMethods.MolarMassCalculation(elementDict, elementList).ToString("f3") + " g/mol";
        }

        private void UI_button_SortName_Click(object sender, EventArgs e)
        {
            UI_dataGridView_Chemicals.DataSource = BindingSourceMethods.SortByElementName(elementList);
        }

        private void UI_button_SingleCharacter_Click(object sender, EventArgs e)
        {
            UI_dataGridView_Chemicals.DataSource = BindingSourceMethods.SortByOneSymbol(elementList);
        }

        private void UI_button_SortAtomic_Click(object sender, EventArgs e)
        {
            UI_dataGridView_Chemicals.DataSource = BindingSourceMethods.SortByAtomicNumber(elementList);
        }
    }
}
