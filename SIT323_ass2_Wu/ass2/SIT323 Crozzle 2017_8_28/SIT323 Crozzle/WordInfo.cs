using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace SIT323Crozzle
{
    /// <summary>
    /// Class contains information of words
    /// Store word information from wordlist.txt
    /// </summary>
    public class WordInfo
    {
        const int CompareTrue = 0;
        const int ValueOfA = 65;
        const int LetterLength = 26;
        const char EqualSymbol = '=';
        const char CommaSymbol = ',';
        const string EmptySymbol = "";

        private List<string> wordList = new List<string>();
        public static Dictionary<char, int> intersectingPointsPerLetter = new Dictionary<char, int>();
        public static Dictionary<char, int> nonIntersectingPointsPerLetter = new Dictionary<char, int>();


        /// <summary>
        /// Add all words used in crozzle into list in the class
        /// </summary>
        /// <param name="path">String contains path of wordlist.txt</param>
        public void SetWordList(string path)
        {
            Log.AddLogInformation("read word list file " + path + " : end");
            StreamReader wordListReader = new StreamReader(path, Encoding.Default);
            String line;
            while ((line = wordListReader.ReadLine()) != null)
            {
                string[] words;
                char[] separator = { CommaSymbol };
                words = line.Split(separator);
                for (int wordsIndex = 0; wordsIndex < words.Length; wordsIndex++)
                {
                    this.wordList.Add(words[wordsIndex]);
                }
            }
            Log.AddLogInformation("read word list file: end");
            wordListReader.Close();
        }

        /// <summary>
        /// Return list of word
        /// </summary>
        /// <returns>List of word</returns>
        public List<string> GetWordList()
        {
            return this.wordList;
        }

        /// <summary>
        /// Get points information from Config class
        /// </summary>
        /// <param name="allIntersectingPointsPerLetter">String contains points information</param>
        public void SetIntersectingPointsPerLetter(string allIntersectingPointsPerLetter)
        {
            char[] separator = { CommaSymbol };
            string[] allCharsAndValues = allIntersectingPointsPerLetter.Split(separator);
            const int correctLength = 2;
            for (int arrayIndex = 0; arrayIndex < allCharsAndValues.Length; arrayIndex++)
            {
                char[] separatorForAllCharsAndValues = { EqualSymbol };
                string[] eachCharAndValue = allCharsAndValues[arrayIndex].Split(separatorForAllCharsAndValues);
                if (eachCharAndValue.Length == correctLength && Regex.IsMatch(eachCharAndValue[0], @"^[A-Z]$") && Regex.IsMatch(eachCharAndValue[1], @"^\d+$"))
                {
                    int value = int.Parse(eachCharAndValue[1]);
                    char letter = eachCharAndValue[0][0];
                    intersectingPointsPerLetter[letter] = value;
                }
                else
                {
                    continue;
                }
            }
        }

        /// <summary>
        /// Return points information
        /// </summary>
        /// <returns>Dictionary contains letter and points</returns>
        public Dictionary<char, int> GetIntersectingPointsPerLetter()
        {
            return intersectingPointsPerLetter;
        }

        /// <summary>
        /// Get points information from Config class
        /// </summary>
        /// <param name="allNonIntersectingPointsPerLetter">String contains points information</param>
        public void SetNonIntersectingPointsPerLetter(string allNonIntersectingPointsPerLetter)
        {
            char[] separator = { CommaSymbol };
            string[] allCharsAndValues = allNonIntersectingPointsPerLetter.Split(separator);
            const int correctLength = 2;
            for (int arrayIndex = 0; arrayIndex < allCharsAndValues.Length; arrayIndex++)
            {
                char[] separatorForAllCharsAndValues = { EqualSymbol };
                string[] eachCharAndValue = allCharsAndValues[arrayIndex].Split(separatorForAllCharsAndValues);
                if (eachCharAndValue.Length == correctLength && Regex.IsMatch(eachCharAndValue[0], @"^[A-Z]$") && Regex.IsMatch(eachCharAndValue[1], @"^\d+$"))
                {
                    int value = int.Parse(eachCharAndValue[1]);
                    char letter = eachCharAndValue[0][0];
                    nonIntersectingPointsPerLetter[letter] = value;
                }
                else
                {
                    continue;
                }
            }
        }

        /// <summary>
        /// Return points information
        /// </summary>
        /// <returns>Dictionary contains letter and points</returns>
        public Dictionary<char, int> GetNonIntersectingPointsPerLetter()
        {
            return nonIntersectingPointsPerLetter;
        }

        private void CheckAlphabetic()
        {
            for (int wordListIndex = 0; wordListIndex < this.GetWordList().Count(); wordListIndex++)
            {
                if (wordList[wordListIndex] == EmptySymbol)
                {
                    Error.AddWordListError("empty value");
                    wordList.Remove(wordList[wordListIndex--]);
                    continue;
                }
                if (!Regex.IsMatch(wordList[wordListIndex], @"^[A-Z]+$"))
                {
                    Error.AddWordListError(wordList[wordListIndex] + ": not alphabetic");
                    wordList.Remove(wordList[wordListIndex--]);
                }
            }
        }

        private void SortWordList()
        {
            for (int i = 0; i < this.wordList.Count() - 1; i++)
            {
                for (int j = i + 1; j < this.wordList.Count(); j++)
                {
                    if (wordList[i].CompareTo(wordList[j]) < CompareTrue)
                    {
                        string temp = wordList[j];
                        wordList[j] = wordList[i];
                        wordList[i] = temp;
                    }
                }
            }
        }

        private void RemoveWordCore()
        {
            for (int wordListIndex = 0; wordListIndex < this.wordList.Count() - 1; wordListIndex++)
            {
                if (wordList[wordListIndex].CompareTo(wordList[wordListIndex + 1]) == CompareTrue)
                {
                    Error.AddWordListError(wordList[wordListIndex] + ": duplicated");
                    wordList.Remove(wordList[wordListIndex + 1]);
                }
            }
        }

        private void RemoveDuplicateWord()
        {
            // Sort the word list
            SortWordList();
            // Remove duplicated word in the word list
            RemoveWordCore();
        }

        private void CheckNumberOfUniqueWords(Config config)
        {
            int listLengthMax = config.GetMaximumNumberOfUniqueWords();
            int listLengthMin = config.GetMinimumNumberOfUniqueWords();
            if (config.GetSetInformation("MAXIMUM_NUMBER_OF_UNIQUE_WORDS") && wordList.Count() > listLengthMax)
                Error.AddWordListError("number of unique words in word list larger than MAXIMUM_NUMBER_OF_UNIQUE_WORDS");
            if (config.GetSetInformation("MINIMUM_NUMBER_OF_UNIQUE_WORDS") && wordList.Count() < listLengthMin)
                Error.AddWordListError("number of unique words in word list smaller than MINIMUM_NUMBER_OF_UNIQUE_WORDS");
        }

        private void CheckIntersectingAndNonIntersectingValues()
        {
            for (int letterIndex = 0; letterIndex < LetterLength; letterIndex++)
            {
                char letter = (char)(ValueOfA + letterIndex);
                if (!intersectingPointsPerLetter.ContainsKey(letter))
                    Error.AddConfigurationError("letter " + letter + @"'s intersecting value not exist");
                if (!nonIntersectingPointsPerLetter.ContainsKey(letter))
                    Error.AddConfigurationError("letter " + letter + @"'s non intersecting value not exist");
            }
        }

        /// <summary>
        /// Validate word list with configuration
        /// </summary>
        /// <param name="config">String contains path of configuration.txt</param>
        public void ValidateWordList(Config config)
        {
            Log.AddLogInformation("word list validation : begin");

            // Check word in the word list, determine if it is alphabetic
            CheckAlphabetic();
            // Remove duplicated words in list
            RemoveDuplicateWord();
            // Check word list size
            CheckNumberOfUniqueWords(config);
            // Check intersecting values and non intersecting values
            CheckIntersectingAndNonIntersectingValues();
            
            Log.AddLogInformation("word list validation : end");

        }

    }
}
