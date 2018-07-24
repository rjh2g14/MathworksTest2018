using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace FolderFileRead
{

    public class WordFrequency
    {
        public string FilePath { get; set; }

        Dictionary<string, int> Totalcounts = null;

        public WordFrequency()
        {
            Totalcounts = new Dictionary<string, int>();
        }

        public Dictionary<string, int> processFile(string filename)
        {
            Dictionary<string, int> Wordcounts = new Dictionary<string, int>();

            string contents = File.ReadAllText(filename);

            contents = contents.Replace("\n", " ");
            contents = contents.Replace("\r", " ");
            contents = contents.Replace("\t", " ");

            char[] seperators = new char[] { ' ', '.', ',' };

            string[] Words = contents.Split(seperators, StringSplitOptions.RemoveEmptyEntries);  //Modifies the contents to be put into an array, seperated by delimiters and removing 'empty' entries

            //Each of the loops below works through the array for each file, adding it to the unique dictionary and the world dictionary

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

            foreach (var item in Words)
            {
                if (Wordcounts.Keys.Contains(item))
                {
                    Wordcounts[item]++;
                }
                else
                {
                    Wordcounts.Add(item, 1);
                }

            }

            return Wordcounts;
        }

        public Dictionary<string, int> getTotalCounts()
        {
            return Totalcounts;
        }

    }


    class Program
    {
        static void Main(string[] args)
        {
            var directory = @"C:\Users\rhbox\Desktop\Test";    // Set the file directory here. This will also be where the results file is created
            StreamWriter sw = new StreamWriter(directory + @"\Results.rtf");

            var Totalcounts = new Dictionary<string, int>();  //Dictionary containing words accross all files

            WordFrequency wf = new WordFrequency();

            foreach (string file in Directory.EnumerateFiles(directory, "*.txt"))  //Enumerates through all files in the directory and sub-directoraries endining in .txt
            {

                Dictionary<string, int> Wordcounts = wf.processFile(file);

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

            Totalcounts = wf.getTotalCounts();

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
