using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICA1_NicW
{
    class Program
    {
        static void Main(string[] args)
        {
            //Integers
            List<int> iNums = new List<int>(new int[] { 4, 12, 4, 3, 5, 6, 7, 6, 12 });
            //Strings
            List <string> Names = new List<string>(new string[]
                { "Rick","Glenn","Rick","Carl","Michonne","Rick","Glenn" });
            //Llist of char
            Random randNum = new Random();
            LinkedList<char> llfloats = new LinkedList<char>();
            while (llfloats.Count < 1000)
                llfloats.AddLast((char)randNum.Next('A', 'Z' + 1));
            //chars in string
            string TestString = "This is a test string, do not panic!!!!!!!!";

            //Checks for ordering on Popular
            List<int> lowFirst  = new List<int>(new int[] { 6, 8, 8, 2, 9, 9 });
            List<int> highFirst = new List<int>(new int[] { 6, 9, 9, 2, 8, 8 });

            //Shuffle Bias Testing
            string alphabet = "abcdefghijk";

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Categorize testing");

            //Check int, Categorize
            foreach (KeyValuePair<int, int> scan in iNums.Categorize())
                Console.WriteLine(scan.Key.ToString("d3") + " x " + scan.Value.ToString("d5"));
            Console.WriteLine();
            //Check string, Categorize
            foreach (KeyValuePair<string, int> scan in Names.Categorize())
                Console.WriteLine(scan.Key + " x " + scan.Value.ToString("d5"));
            Console.WriteLine();
            //Check linked list and char, Categorize
            foreach (KeyValuePair<char, int> scan in llfloats.Categorize())
                Console.WriteLine(scan.Key + " x " + scan.Value.ToString("d5"));
            Console.WriteLine();
            //check string, Categorize
            foreach (KeyValuePair<char, int> scan in TestString.Categorize())
                Console.WriteLine(scan.Key + " x " + scan.Value.ToString("d5"));
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Popular testing");

            //Check int, Popular
            Console.WriteLine($"Int test: {iNums.Popular()}");
            Console.WriteLine();
            //Check string, Popular
            Console.WriteLine($"String test: {Names.Popular()}");
            Console.WriteLine();
            //Check char linked list, Popular
            Console.WriteLine($"Char test, linked list: {llfloats.Popular()}");
            Console.WriteLine();
            //Check char string, Popular
            Console.WriteLine($"Char test, string: {TestString.Popular()}");
            Console.WriteLine();
            //Check order of appearance, Popular
            Console.WriteLine($"Low int first test: {lowFirst.Popular()}");
            Console.WriteLine();
            Console.WriteLine($"High int first test: {highFirst.Popular()}");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Shuffle testing");

            //Check int, Shuffle
            foreach (int scan in iNums.Shuffle())
                Console.Write($"{scan}, ");
            Console.WriteLine(); Console.WriteLine();
            //Check string, shuffle
            foreach (string scan in Names.Shuffle())
                Console.Write($"{scan}, ");
            Console.WriteLine(); Console.WriteLine();
            //Check linked list and char, shuffle
            foreach (char scan in llfloats.Shuffle())
                Console.Write($"{scan}, ");
            Console.WriteLine(); Console.WriteLine();
            //check string, shuffle
            foreach (char scan in TestString.Shuffle())
                Console.Write($"{scan}, ");
            Console.WriteLine(); Console.WriteLine();

            
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("Shuffle bias Testing\nInitial string is alphabet");

            List<char> shuffled = alphabet.Shuffle().ToList();
            Dictionary<char, List<int>> dict = new Dictionary<char, List<int>>();

            foreach(char c in alphabet)
            {
                dict[c] = new List<int>();
            }

            for (int i = 0; i < 10000000; i++) {
                shuffled = alphabet.Shuffle().ToList();
                for (int c = 0; c < shuffled.Count; c++)
                {
                    dict[shuffled[c]].Add(c);
                }
            }

            foreach(KeyValuePair<char, List<int>> kvp in dict)
            {
                double avg = kvp.Value.Average();
                Console.WriteLine($"Key: {kvp.Key}, Avg: {avg.ToString("F3")}, Deviation: {((dict.Count / 2 - avg) / (dict.Count / 2)).ToString("F3")}");
            }

            //Wait to close
            Console.ReadLine();
        }
    }

    public static class IENumerableExtension
    {
        static Random randNum = new Random();

        public static Dictionary<T, int> Categorize<T>(this IEnumerable<T> InCollection)
        {
            //Make a dictionary to put our info into
            Dictionary<T, int> outputDict = new Dictionary<T, int>();

            //Go through each item and count how many occurences
            foreach (T item in InCollection)
            {
                if (outputDict.ContainsKey(item))
                {
                    //The dictionary has the item, increment its count
                    outputDict[item]++;
                }
                else
                {
                    //The dictionary doesn't have the item, add it with one count
                    outputDict[item] = 1;
                }
            }

            //Sort the dictionary according to the key. Return it to dict type.
            return outputDict.OrderBy(key => key.Key).ToDictionary( k => k.Key, v => v.Value);
        }

        public static T Popular<T>(this IEnumerable<T> InCollection)
        {
            //Make a dictionary to put our info into
            Dictionary<T, int> outputDict = new Dictionary<T, int>();
            //Go through each item and count how many occurences
            foreach (T item in InCollection)
            {
                if (outputDict.ContainsKey(item))
                {
                    //The dictionary has the item, increment its count
                    outputDict[item]++;
                }
                else
                {
                    //The dictionary doesn't have the item, add it with one count
                    outputDict[item] = 1;
                }
            }
            //^^ Above is the Categorize code, without sorting
            //vv Unique code for Popular

            //Hold onto the most popular key value pair
            KeyValuePair<T, int> mostPop = new KeyValuePair<T, int>();

            //Look through all the pairs and find the most frequent
            foreach (KeyValuePair<T, int> kvp in outputDict)
                if (kvp.Value > mostPop.Value) //Check if the frequency is higher
                    mostPop = kvp;

            //Return the highest key
            return mostPop.Key;
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> InCollection)
        {
            //An easier enumerable type to sort with
            List<T> outList = new List<T>(InCollection);

            //Do a shuffle! Fischer-Yates
            T temp;
            for (int i = outList.Count - 1; i > 1; i--)
            {
                int j = randNum.Next(0, i + 1); // 0 <= j <= i
                //Hold onto i
                temp = outList[i];
                //swap j to i
                outList[i] = outList[j];
                //swap i to j
                outList[j] = temp;
            }

            //Automatically cast back as IEnumerable
            return outList;
        }
    }
}
