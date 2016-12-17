using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConcordanceApplication
{
    //Given an arbitrary text document written in English, write a program that will generate a concordance, i.e. an alphabetical list of all word occurrences, labeled with word frequencies. Bonus: label each word with the sentence numbers in which each occurrence appeared.
    public class Concordance
    {
        //Case Insensitive dictionary
        //Note: Case Insensitivity is assumed; remove StringComparer.OrdinalIgnoreCase in constructor if this is not true
        SortedDictionary<string, WordStats> _wordStatsDict;
        
        public Concordance()
        {
            _wordStatsDict = new SortedDictionary<string, WordStats>(StringComparer.OrdinalIgnoreCase);
        }

        //class to store the statistics such as frequency and sentence List associated with each word
        class WordStats
        {

            public WordStats(int pFrequency, string pSentenceList)
            {
                Frequency = pFrequency;
                SentenceList = pSentenceList;
            }
            public WordStats(string pSentenceList)
            {
                Frequency = 1;
                SentenceList = pSentenceList;
            }
            public int Frequency {get;set;}
            public string SentenceList { get; set; }
 
        }

        //Get File Name from user
        public void GetFileName(ref string pFileName)
        {
            string DEFAULT_FILE = @"C:\test.txt";
            Console.WriteLine("Please enter file name with full path; if none is entered the default test.txt will be used:\n");
            pFileName = Console.ReadLine();
            if (pFileName == null || pFileName.Length == 0)
            {
                Console.WriteLine( "Using default test.txt file since no file name was entered.\n");
                pFileName = DEFAULT_FILE;
            }
        }

        //Populate the SortedDictionary using the input string
        public void PopulateDictionary(string pStringFromFile, string pLineNum)
        {
            // The typical separators between words are used for splitting
            string[] source = pStringFromFile.Split(new char[] { '.', '?', '!', ' ', ';', ':', ',', '\n', '\r', '\t', '\f', '\v' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string name in source)
            {
                if (!_wordStatsDict.ContainsKey(name))
                {
                    _wordStatsDict.Add(name, new WordStats(pLineNum));
                }
                else
                {
                    _wordStatsDict[name].Frequency++;
                    _wordStatsDict[name].SentenceList = _wordStatsDict[name].SentenceList + "," + pLineNum;
                }
            }
        }
        public void PrintDictionary()
        {
            Console.WriteLine("List of words with word frequencies and sentence numbers:\n");
            Console.WriteLine("Word\tFrequency:SentenceNumber");
            foreach (KeyValuePair<string, WordStats> kvp in _wordStatsDict)
            {
                Console.WriteLine(" {0}\t {1}:{2}",
                    kvp.Key, kvp.Value.Frequency, kvp.Value.SentenceList);
            }
        }
        public static void Main()
        {
            //declarations
            string strFileName=null;
            string strStringFromFile = null;
            Concordance ConcordanceApp = new Concordance();
            
            //Get File Name to read text from
            ConcordanceApp.GetFileName(ref strFileName);
            if (!File.Exists(strFileName))
            {
                Console.WriteLine("ERROR: The file name you specified: {0} does not exist. Please check the path and try again", strFileName);
                Console.ReadKey();
                return;
            }
            
            //Read the text file and populate the dictionary
            using (StreamReader reader = new StreamReader(strFileName)) 
            {
                int nLineNum = 0;
                while ((strStringFromFile = reader.ReadLine()) != null)
                {
                    ConcordanceApp.PopulateDictionary(strStringFromFile, (++nLineNum).ToString());
                }
            }

            // Output to console window
            ConcordanceApp.PrintDictionary();
            Console.ReadKey();
         }
    }
}

