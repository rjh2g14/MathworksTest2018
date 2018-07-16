using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace FolderFileRead
{
    class Program
    {
        static void Main(string[] args)
        {

            var directory = @"C:\Users\rhbox\Desktop\Test";    // Set the file directory here. This will also be where the results file is created

            StreamWriter sw = new StreamWriter(directory + @"\Results.rtf");

            var Totalcounts = new Dictionary<string, int>();  //Dictionary containing words accross all files

            foreach (string file in Directory.EnumerateFiles(directory, "*.txt"))  //Enumerates through all files in the directory and sub-directoraries endining in .txt
            {
                var Wordcounts = new Dictionary<string, int>(); // A dictionary for each individual file

                string contents = File.ReadAllText(file);

                contents = contents.Replace("\n", " ");
                contents = contents.Replace("\r", " ");
                contents = contents.Replace("\t", " ");

                char[] seperators = new char[] { ' ', '.', ',' };

                string[] Words = contents.Split(seperators, StringSplitOptions.RemoveEmptyEntries);  //Modifies the contents to be put into an array, seperated by delimiters and removing 'empty' entries

                //Each of the loops below works through the array for each file, adding it to the unique dictionary and the world dictionary
                foreach (var item in Words)
                {
                    //If the item is in the dictionary, it will only increase its value by one. If not, it will add the word to the dictionary.
                    if (Wordcounts.Keys.Contains(item))
                    {
                        Wordcounts[item]++;
                    }
                    else
                    {
                        Wordcounts.Add(item, 1);
                    }

                }

                foreach (var item in Words)
                {
                    if (Totalcounts.Keys.Contains(item))
                    {
                        Totalcounts[item]++;
                    }
                    else
                    {
                        Totalcounts.Add(item, 1);
                    }

                }

                Console.WriteLine(' ');
                Console.WriteLine(file);
                sw.WriteLine(file);

                //Orders the unique dictionary in order of most used words and takes the top ten values
                var topten = (from entry in Wordcounts orderby entry.Value descending select entry).ToDictionary(pair => pair.Key, pair => pair.Value).Take(10);

                foreach (var item in topten)
                {
                    Console.WriteLine("Word: {0} | Frequency: {1}", item.Key, item.Value);
                    sw.WriteLine("Word: {0} | Frequency: {1}", item.Key, item.Value);
                }

            }



            //Orders the world dictionary and takes the top ten values
            var toptentotal = (from entry in Totalcounts orderby entry.Value descending select entry).ToDictionary(pair => pair.Key, pair => pair.Value).Take(10);

            Console.WriteLine("\n\nTotal List");
            sw.WriteLine("\n\nTotal List");

            foreach (var item in toptentotal)
            {
                Console.WriteLine("Word: {0} | Frequency: {1}", item.Key, item.Value);
                sw.WriteLine("Word: {0} | Frequency: {1}", item.Key, item.Value);
            }

            sw.Close();
            Console.Read();
        }
    }
}
