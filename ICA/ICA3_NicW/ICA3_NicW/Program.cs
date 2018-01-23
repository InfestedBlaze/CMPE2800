using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ICA3_NicW
{
    class Program
    {
        static void Main(string[] args)
        {
            //Source of strings for part A
            List<string> sourcestrings = new List<string>(new string[] { "Caballo", "Gato", "Perro", "Conejo", "Tortuga", "Cangrejo"});

            //Part A
            //[1]
            //Get all the animals with and ASCII value less than 600
            var smallString = (from animal in sourcestrings where animal.Sum(c => c) < 600 select animal).ToList();
            //Write them to the console
            smallString.ForEach(animal => Console.WriteLine(animal));
            Console.WriteLine();

            //[2]
            //Same as A, except our list is a new data type. String and the char sum of that string
            var objList = (from animal in sourcestrings where animal.Sum(c => c) < 600 select new { Str = animal, Sum = animal.Sum(c => c) }).ToList();
            objList.ForEach(obj => Console.WriteLine(obj.ToString()));
            Console.WriteLine();

            //[3]
            //Same as B, descending ASCII value though
            objList = (from animal in sourcestrings
                       where animal.Sum(c => c) < 600
                       select new { Str = animal, Sum = animal.Sum(c => c) }
                       into q orderby q.Sum descending
                       select q).ToList();
            objList.ForEach(obj => Console.WriteLine(obj.ToString()));
            Console.WriteLine();
            Console.ReadLine();

            //Part B
            Console.Clear();
            //[1]
            string filebits = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString() + "/junk.txt");
            //[2]
            var lines = (from l in filebits.Split(new char[] { ' ', '\r', '\n', '\t'})
                        where l.Length > 0
                        select l).ToList();
            //[3]
            var summed = (from l in lines
                          group l by l.Sum(c => c)).ToList();
            summed = (from l in summed
                      orderby l.Key
                      select l).ToList();
            //[4]
            Console.ForegroundColor = ConsoleColor.Green;
            foreach(var v in lines)
            {
                Console.WriteLine(v);
            }
            Console.WriteLine();
            Console.ReadLine();

            Console.ForegroundColor = ConsoleColor.Red;
            foreach (var sum in summed)
            {
                Console.WriteLine("Lines with a sum: " + sum.Key);
                foreach (var line in sum)
                {
                    Console.WriteLine("\t" + line);
                }
            }
            Console.ReadLine();
        }
    }
}
