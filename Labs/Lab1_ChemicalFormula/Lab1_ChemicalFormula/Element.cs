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

namespace Lab1_ChemicalFormula
{
    class Element
    {
        //Fields
        public int AtomicNum;   //Atomic number of the element
        public double mass;     //Molar mass of the element
        public string Name;     //The english name of the element
        public string Symbol;   //The abbreviation of the element

        public Element(int _Num, string _name, string _symbol, double _mass)
        {
            AtomicNum = _Num;
            Name = _name;
            Symbol = _symbol;
            mass = _mass;
        }
    }
}
