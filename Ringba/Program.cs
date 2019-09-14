using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;

namespace Ringba
{
    class Program
    {
        /// <summary>
        /// function taking a string and returning a Dictionary of all the words present and their count
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static Dictionary<string,int> CalculateCountOfWordsInString(string input)
        {
            Dictionary<string, int> wordsDict = new Dictionary<string, int>();

                int i = 1;
                string word = input[0].ToString();
                while (i < input.Length)
                {
                    if (char.IsUpper(input[i]))
                    {
                        if (wordsDict.ContainsKey(word))
                            wordsDict[word]++;
                        else
                            wordsDict[word] = 1;
                        word = input[i].ToString();
                    }
                    else
                    {
                        word += input[i];
                    }
                    i++;
                }
            
            return wordsDict;
         }
        static void CalculateAndPrintOutput(Dictionary<char,int> charDict,Dictionary<char,int> capDict,Dictionary<string,int> wordsDict,Dictionary<string,int>prefixDict )
        {
            //printing ans for 1st part
            Console.WriteLine("Number of Occurances of each character in the file");
            Console.WriteLine("Character - Count");
            foreach (var pair in charDict.OrderBy(a => a.Key))
            {
                Console.WriteLine(string.Format("{0} - {1}", pair.Key, pair.Value));
            }
            //printing the ans for 2nd part
            Console.WriteLine("\n\nNumber of Occurances of each character in the file as capitals");
            Console.WriteLine("Character - Count");
            foreach (var pair in charDict.OrderBy(a => a.Key))
            {
                Console.WriteLine(string.Format("{0} - {1}", pair.Key, pair.Value));
            }
            //calculating ans for 3rd part
            KeyValuePair<string, int> maxWord = new KeyValuePair<string, int>();
            
            foreach (var pair in wordsDict)
            {
                if(maxWord.Value<pair.Value)
                {
                    maxWord = pair;
                }
            }
            //printing the ans for 3rd part
            Console.WriteLine(String.Format("Most Comman word is - {0} and number of occurances are - {1}", maxWord.Key, maxWord.Value));

            //calculating ans for 3rd part
            KeyValuePair<string, int> maxPrefix = new KeyValuePair<string, int>();

            foreach (var pair in prefixDict)
            {
                if (maxPrefix.Value < pair.Value)
                {
                    maxPrefix = pair;
                }
            }
            //printing the ans for 3rd part
            Console.WriteLine(String.Format("Most Comman prefix is - {0} and number of occurances are - {1}", maxPrefix.Key, maxPrefix.Value));


        }
        static void Main(string[] args)
        {
            //dictionary to calculate count of each words
            Dictionary<string, int> wordsDict = new Dictionary<string, int>();
            //dictionary to calculate count of each two letter prefix
            Dictionary<string, int> prefixDict = new Dictionary<string, int>();
            //dictionary to calculate count of each character
            Dictionary<char, int> charDict = new Dictionary<char, int>();
            //dictionary to calculate count of each capital character
            Dictionary<char, int> capDict = new Dictionary<char, int>();
            string input="";
            try
            {
                //reading file into the string using webclient
                WebClient wc = new WebClient();
                input = wc.DownloadString("https://ringba-test-html.s3-us-west-1.amazonaws.com/TestQuestions/output.txt");
            }
            catch (WebException we)
            {
                // add some kind of error processing
                Console.WriteLine(we.ToString());
            }
            if(!string.IsNullOrEmpty(input))
            {
                int i;
                wordsDict = CalculateCountOfWordsInString(input);
                //did not write a separate function for calculating each dictionary as wanted to do all calculation in a same loop
                foreach (var pair in wordsDict)
                {

                    if(capDict.ContainsKey(pair.Key[0]))
                    {
                        capDict[pair.Key[0]]+=pair.Value;
                    }
                    else
                    {
                        capDict[pair.Key[0]] = pair.Value;
                    }
                    i = 0;
                    while(i<pair.Key.Length)
                    {
                        if(charDict.ContainsKey(char.ToLower(pair.Key[i])))
                        {
                            charDict[char.ToLower(pair.Key[i])] += pair.Value;
                        }                    
                        else
                        {
                            charDict[char.ToLower(pair.Key[i])] = pair.Value;
                        }
                        i++;
                    }
                    if(pair.Key.Length>2)
                    {
                        if(prefixDict.ContainsKey(pair.Key.Substring(0,2)))
                        {
                            prefixDict[pair.Key.Substring(0, 2)] += pair.Value;
                        }
                        else
                        {
                            prefixDict[pair.Key.Substring(0, 2)] = pair.Value;
                        }
                    }
                }
                CalculateAndPrintOutput(charDict, capDict, wordsDict, prefixDict);
            }
            //Console.WriteLine("Hello World!");
        }
    }
}
