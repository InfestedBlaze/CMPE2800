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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1_ChemicalFormula
{
    class BindingSourceMethods
    {
        /// <summary>
        /// Sort the list of elements by their name 
        /// </summary>
        /// <param name="_elements">A list of elements to sort</param>
        /// <returns>A binding source for the form to use</returns>
        public static BindingSource SortByElementName(List<Element> _elements)
        {
            BindingSource _bind = new BindingSource();

            //Set the datasource to the ideal way using linq 
            _bind.DataSource = from n in _elements
                               orderby n.Name
                               select new
                               {
                                   AtomicNumber = n.AtomicNum,
                                   Name = n.Name,
                                   ElementAbrv = n.Symbol,
                                   Mass = n.mass
                               };

            return _bind;
        }

        /// <summary>
        /// Sort the list of elements by single Character only
        /// </summary>
        /// <param name="_elements">A list of elements to sort</param>
        /// <returns>A binding source for the form to use</returns>
        public static BindingSource SortByOneSymbol(List<Element> _elements)
        {
            BindingSource _bind = new BindingSource();

            //Set the datasource to the ideal way using linq 
            _bind.DataSource = from n in _elements
                               orderby n.Symbol
                               where n.Symbol.Count().Equals(1)
                               select new
                               {
                                   AtomicNumber = n.AtomicNum,
                                   Name = n.Name,
                                   ElementAbrv = n.Symbol,
                                   Mass = n.mass
                               };
            return _bind;
        }

        /// <summary>
        /// Sort the list of elements by their Atomic Number
        /// </summary>
        /// <param name="_elements">A list of elements to sort</param>
        /// <returns>A binding source for the form to use</returns>
        public static BindingSource SortByAtomicNumber(List<Element> _elements)
        {
            BindingSource _bind = new BindingSource();

            //Set the datasource to the ideal way using linq 
            _bind.DataSource = from n in _elements
                               orderby n.AtomicNum
                               select new
                               {
                                   AtomicNumber = n.AtomicNum,
                                   Name = n.Name,
                                   ElementAbrv = n.Symbol,
                                   Mass = n.mass
                               };
            return _bind;
        }

        /// <summary>
        /// Takes the Dictionary that was made from the text box and update the GridView to show what Element is being used
        /// </summary>
        /// <param name="_dic">Dictionary of all the elements you currently have</param>
        /// <param name="_elements">List of all the elements</param>
        /// <returns>A binding source for the form to use</returns>
        public static BindingSource GridTextUpdate(Dictionary<string, int> _dic, List<Element> _elements)
        {
            BindingSource _bind = new BindingSource();

            //Set the datasource to what the output should be
            //Meaning im checking the key of the dictionary with the symbol of the element
            _bind.DataSource = from d in _dic
                               join l in _elements on d.Key equals l.Symbol
                               select new
                               {
                                   Element = l.Name,
                                   Count = d.Value,
                                   Mass = l.mass,
                                   TotalMass = d.Value * l.mass
                               };

            return _bind;

        }

        /// <summary>
        /// Takes a Dictionary of specify element and list of element to calculate the total molar mass the user wants 
        /// </summary>
        /// <param name="_dic">Dictionary of all the elements you currently have</param>
        /// <param name="_elements">List of all the elements</param>
        /// <returns>The total mass of all the elements inputted</returns>
        public static double MolarMassCalculation(Dictionary<string, int> _dic, List<Element> _elements)
        {
            double _result = 0.0;

            //Join the two collection into one list made out of the total mass of each specify Element
            var _mass = (from d in _dic
                         join l in _elements on d.Key equals l.Symbol
                         select d.Value * l.mass).ToList();

            //Foreach the list and add it into a variable to be returned to the main form
            _mass.ForEach(q => _result += q);

            return _result;
        }
    }
}
