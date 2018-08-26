using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIT323Crozzle
{
    /// <summary>
    /// Class that contains methods used for crozzle validation
    /// </summary>
    public class CrozzleValidation
    {
        private List<Word> horizontalWord = new List<Word>();
        private List<Word> verticalWord = new List<Word>();
        const string StringRow = "ROW";
        const string StringColumn = "COLUMN";
        const char SpaceSymbol = ' ';

        /// <summary>
        /// Calculate maximum number of rows used according to words information in crozzle.txt
        /// </summary>
        /// <returns>Number of rows</returns>
        public int GetMaxRow()
        {
            int rowMax = 0;
            for (int index = 0; index < horizontalWord.Count(); index++)
            {
                if (rowMax < horizontalWord[index].GetRows())
                    rowMax = horizontalWord[index].GetRows();
            }
            for (int index = 0; index < verticalWord.Count(); index++)
            {
                if (rowMax < verticalWord[index].GetRows() + verticalWord[index].GetWordContent().Length - 1)
                    rowMax = verticalWord[index].GetRows() + verticalWord[index].GetWordContent().Length - 1;
            }
            return rowMax;
        }

        /// <summary>
        /// Calculate maximum number of columns used according to words information in crozzle.txt
        /// </summary>
        /// <returns>Number of columns</returns>
        public int GetMaxColumn()
        {
            int columnMax = 0;
            for (int index = 0; index < verticalWord.Count(); index++)
            {
                if (columnMax < verticalWord[index].GetColumns())
                    columnMax = verticalWord[index].GetColumns();
            }
            for (int index = 0; index < horizontalWord.Count(); index++)
            {
                if (columnMax < horizontalWord[index].GetColumns() + horizontalWord[index].GetWordContent().Length - 1)
                    columnMax = horizontalWord[index].GetColumns() + horizontalWord[index].GetWordContent().Length - 1;
            }
            return columnMax;
        }

        /// <summary>
        /// Check if a word is linked(has intersections) with a group of words
        /// </summary>
        /// <param name="word">A word</param>
        /// <param name="queue">A group of words, stored in queue</param>
        /// <returns>True if it is linked, false if not linked</returns>
        public static bool Crossing(Word word, Queue<Word> queue)
        {
            Word[] wordList = queue.ToArray();
            string wordContent = word.GetWordContent();
            int row = word.GetRows();
            int column = word.GetColumns();

            for (int index = 0; index < wordList.Length; index++)
            {
                if (wordList[index].GetType().Equals(StringRow) && word.GetType().Equals(StringColumn))
                {
                    string anotherWordContent = wordList[index].GetWordContent();
                    int anotherWordRow = wordList[index].GetRows();
                    int anotherWordColumn = wordList[index].GetColumns();

                    if (column < anotherWordColumn || row > anotherWordRow)
                        continue;
                    if (column - anotherWordColumn >= anotherWordContent.Length || anotherWordRow - row >= wordContent.Length)
                        continue;
                    if (anotherWordContent[column - anotherWordColumn] == wordContent[anotherWordRow - row])
                        return true;
                    else
                        Error.AddCrozzleError(wordList[index].GetWordContent() + " " + word.GetWordContent() + ": false intersection");

                }
                else if (wordList[index].GetType().Equals(StringColumn) && word.GetType().Equals(StringRow))
                {
                    string anotherWordContent = wordList[index].GetWordContent();
                    int anotherWordRow = wordList[index].GetRows();
                    int anotherWordColumn = wordList[index].GetColumns();

                    if (anotherWordColumn < column || row < anotherWordRow)
                        continue;
                    if (anotherWordColumn - column >= wordContent.Length || row - anotherWordRow >= anotherWordContent.Length)
                        continue;
                    if (wordContent[anotherWordColumn - column] == anotherWordContent[row - anotherWordRow])
                        return true;
                    else
                        Error.AddCrozzleError(wordList[index].GetWordContent() + " " + word.GetWordContent() + ": false intersection");

                }
            }
            return false;

        }

        /// <summary>
        /// Check if a word is linked(has intersections) with another words
        /// An overload function
        /// </summary>
        /// <param name="word">A word</param>
        /// <param name="anotherWord">Another word</param>
        /// <returns>True if it is linked, false if not linked</returns>
        public static bool Crossing(Word word, Word anotherWord)
        {
            string wordContent = word.GetWordContent();
            string anotherWordContent = anotherWord.GetWordContent();
            int row = word.GetRows();
            int anotherWordRow = anotherWord.GetRows();
            int column = word.GetColumns();
            int anotherWordColumn = anotherWord.GetColumns();
            if (anotherWord.GetType().Equals(StringRow))
            {
                if (column < anotherWordColumn || row > anotherWordRow)
                    return false;
                if (column - anotherWordColumn >= anotherWordContent.Length || anotherWordRow - row >= wordContent.Length)
                    return false;
                if (anotherWordContent[column - anotherWordColumn] == wordContent[anotherWordRow - row])
                    return true;
                else
                    Error.AddCrozzleError(anotherWord.GetWordContent() + " " + word.GetWordContent() + ": false intersection");
            }
            else
            {
                if (anotherWordColumn < column || row < anotherWordRow)
                    return false;
                if (anotherWordColumn - column >= wordContent.Length || row - anotherWordRow >= anotherWordContent.Length)
                    return false;
                if (wordContent[anotherWordColumn - column] == anotherWordContent[row - anotherWordRow])
                    return true;
                else
                    Error.AddCrozzleError(anotherWord.GetWordContent() + " " + word.GetWordContent() + ": false intersection");
            }
            return false;
        }


        public static char GetCrossingLetter(Word word, Word anotherWord)
        {
            string wordContent = word.GetWordContent();
            string anotherWordContent = anotherWord.GetWordContent();
            int row = word.GetRows();
            int anotherWordRow = anotherWord.GetRows();
            int column = word.GetColumns();
            int anotherWordColumn = anotherWord.GetColumns();
            if (anotherWord.GetType().Equals(StringRow))
            {
                return anotherWordContent[column - anotherWordColumn];
            }
            else
            {
                return wordContent[anotherWordColumn - column];
            }
        }

        private int CalculateIntersections(Word word)
        {
            String type = word.GetType();
            int intersection = 0;
            if (type.Equals(StringRow))
            {
                for (int index = 0; index < verticalWord.Count(); index++)
                {
                    if (Crossing(word, verticalWord[index]))
                        intersection++;
                }
            }
            else
            {
                for (int index = 0; index < horizontalWord.Count(); index++)
                {
                    if (Crossing(word, horizontalWord[index]))
                        intersection++;
                }
            }
            return intersection;
        }

        /// <summary>
        /// Calculate the number of intersections for a word
        /// Generate error information if the number larger than maximum number or smaller than minimum number
        /// </summary>
        /// <param name="word"></param>
        /// <param name="max"></param>
        /// <param name="min"></param>
        public void CheckIntersections(Word word, Config config)
        {
            int min, max;
            int intersection = CalculateIntersections(word);
            if (word.GetType().Equals(StringRow))
            {
                min = config.GetMinimumIntersectionsInHorizontalWords();
                max = config.GetMaximumIntersectionsInHorizontalWords();
            }
            else
            {
                min = config.GetMinimumIntersectionsInVerticalWords();
                max = config.GetMaximumIntersectionsInVerticalWords();
            }

            if (intersection < min)
                Error.AddCrozzleError(word.GetWordContent() + ": less than minimum intersection value");
            if (intersection > max)
                Error.AddCrozzleError(word.GetWordContent() + ": larger than maximum intersection value");
        }

        private int CalculateGroup(WordResult wordResult)
        {
            int groupNumber = 0;
            List<Word> usedWord = new List<Word>();
            for (int index = 0; index < wordResult.GetUsedWord().Count(); index++)
            {
                usedWord.Add(wordResult.GetUsedWord()[index]);
            }

            // Calculate group numbers
            while (usedWord.Count() != 0)
            {
                List<Word> group = new List<Word>();
                Queue<Word> next = new Queue<Word>();
                next.Enqueue(usedWord[0]);
                group.Add(usedWord[0]);
                usedWord.Remove(usedWord[0]);
                while (next.Count() > 0)
                {
                    Queue<Word> vertical = new Queue<Word>();
                    Queue<Word> horizontal = new Queue<Word>();
                    Word[] temp_array = next.ToArray();
                    for (int index = 0; index < next.Count(); index++)
                    {
                        if (temp_array[index].GetType().Equals(StringRow))
                            horizontal.Enqueue(temp_array[index]);
                        else
                            vertical.Enqueue(temp_array[index]);
                    }
                    int queueLength = next.Count();
                    for (int index = 0; index < usedWord.Count(); index++)
                    {
                        if (usedWord[index].GetType().Equals(StringRow))
                        {
                            if (Crossing(usedWord[index], vertical))
                            {
                                next.Enqueue(usedWord[index]);
                                group.Add(usedWord[index]);
                                usedWord.Remove(usedWord[index--]);
                            }
                        }
                        else
                        {
                            if (Crossing(usedWord[index], horizontal))
                            {
                                next.Enqueue(usedWord[index]);
                                group.Add(usedWord[index]);
                                usedWord.Remove(usedWord[index--]);
                            }
                        }

                    }
                    for (int index = 0; index < queueLength; index++)
                        next.Dequeue();
                }
                groupNumber++;
            }
            return groupNumber;
        }

        /// <summary>
        /// Calculate groups for a crozzle
        /// Generate error information if the number larger than maximum number or smaller than minimum number
        /// </summary>
        /// <param name="config">Config class which store data of configuration.txt</param>
        /// <param name="wordInfo">WordInfo class which store data of wordlist.txt</param>
        /// <param name="wordResult">WordResult class which store data of crozzle.txt</param>
        public void CheckGroup(Config config, WordResult wordResult)
        {
            int groupNumber = CalculateGroup(wordResult);

            // Generate error information if group number has not meet requirements
            if (config.GetSetInformation("MAXIMUM_NUMBER_OF_GROUPS") && groupNumber > config.GetMaximumNumberOfGroups())
            {
                Error.AddCrozzleError("group number larger than maximum number");
            }
            else if (config.GetSetInformation("MINIMUM_NUMBER_OF_GROUPS") && groupNumber < config.GetMinimumNumberOfGroups())
            {
                Error.AddCrozzleError("group number smaller than maximum number");
            }
        }

        /// <summary>
        /// Search if a word is in wordlist.txt
        /// </summary>
        /// <param name="word">A word</param>
        /// <param name="wordList">List contians words in wordlist.txt</param>
        /// <returns>True if the word is in wordlist.txt, false if it is not in wordlist.txt</returns>
        public bool Search(string word, List<string> wordList)
        {
            for (int index = 0; index < wordList.Count(); index++)
            {
                if (wordList[index].Equals(word))
                    return true;
            }
            return false;
        }

        private Dictionary<string, int> GetUsedTime(WordResult wordResult)
        {
            Dictionary<string, int> usedTimes = new Dictionary<string, int>();
            List<Word> usedWord = wordResult.GetUsedWord();

            // Initialize usedTimes
            for (int index = 0; index < usedWord.Count(); index++)
            {
                try
                {
                    if (usedWord[index].GetType().Equals(StringRow))
                        this.horizontalWord.Add(usedWord[index]);
                    else
                        this.verticalWord.Add(usedWord[index]);
                    usedTimes.Add(usedWord[index].GetWordContent(), 0);
                }
                catch
                {
                    continue;
                }
            }
            return usedTimes;
        }

        private void CheckSameWord(Dictionary<string, int> usedTimes, int maxSame)
        {
            foreach (string key in usedTimes.Keys)
            {
                if (usedTimes[key] > maxSame)
                    Error.AddCrozzleError(key + @": number of same words larger than limitted");
            }
        }

        private void ValidateCore(Config config, WordResult wordResult)
        {
            int row = wordResult.GetRows();
            int column = wordResult.GetColumns();
            int rowMax = config.GetMaximumHorizontalWords();
            int columnMax = config.GetMaximumVerticalWords();
            int rowMin = config.GetMinimumVerticalWords();
            int columnMin = config.GetMinimumVerticalWords();
            bool setMaximumIntersectionsHorizonotal = config.GetSetInformation("MAXIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS");
            bool setMinimumIntersectionsHorizontal = config.GetSetInformation("MINIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS");
            bool setMaximumIntersectionsVertical = config.GetSetInformation("MAXIMUM_INTERSECTIONS_IN_VERTICAL_WORDS");
            bool setMinimumIntersectionsVertical = config.GetSetInformation("MINIMUM_INTERSECTIONS_IN_VERTICAL_WORDS");
            bool setMaximumHorizontalWords = config.GetSetInformation("MAXIMUM_HORIZONTAL_WORDS");
            bool setMaximumVerticalWords = config.GetSetInformation("MAXIMUM_VERTICAL_WORDS");
            bool setRows = wordResult.GetSetInformation("ROWS");
            bool setColumns = wordResult.GetSetInformation("COLUMNS");
            int rowWord = 0;
            int columnWord = 0;
            int maxSame = config.GetMaximumNumberOfTheSameWord();
            List<Word> usedWord = wordResult.GetUsedWord();
            Dictionary<string, int> usedTimes = GetUsedTime(wordResult);


            // Validation
            for (int index = 0; index < usedWord.Count(); index++)
            {
                if (usedWord[index].GetType().Equals(StringRow))
                {
                    if (setMaximumIntersectionsHorizonotal && setMinimumIntersectionsHorizontal)
                        CheckIntersections(usedWord[index], config);
                    if (setMaximumHorizontalWords && rowWord >= rowMax)
                    {
                        Error.AddCrozzleError(usedWord[index].GetWordContent() + ": too many horizontal words");
                    }
                    if (setRows && usedWord[index].GetRows() > row)
                    {
                        Error.AddCrozzleError(usedWord[index].GetWordContent() + ": need more rows");
                    }
                    if (setColumns && usedWord[index].GetColumns() + usedWord[index].GetWordContent().Length - 1 > column)
                    {
                        Error.AddCrozzleError(usedWord[index].GetWordContent() + ": need more columns");
                    }
                    rowWord++;
                    usedTimes[usedWord[index].GetWordContent()]++;
                }
                else if (usedWord[index].GetType().Equals(StringColumn))
                {
                    if (setMaximumIntersectionsVertical && setMinimumIntersectionsVertical)
                        CheckIntersections(usedWord[index], config);

                    if (setMaximumVerticalWords && columnWord >= columnMax)
                    {
                        Error.AddCrozzleError(usedWord[index].GetWordContent() + ": too many vertical words");
                    }
                    if (setColumns && usedWord[index].GetColumns() > column)
                    {
                        Error.AddCrozzleError(usedWord[index].GetWordContent() + ": need more columns");
                    }
                    if (setRows && usedWord[index].GetRows() + usedWord[index].GetWordContent().Length - 1 > row)
                    {
                        Error.AddCrozzleError(usedWord[index].GetWordContent() + ": need more rows");
                    }
                    columnWord++;
                    usedTimes[usedWord[index].GetWordContent()]++;
                }
            }
            if (columnWord < columnMin)
                Error.AddCrozzleError("number of vertical word: less than minimum number of vertical word");
            if (rowWord < rowMin)
                Error.AddCrozzleError("number of horizontal word: less than minimum number of horizontal word");
            // Check number of same words
            CheckSameWord(usedTimes, maxSame);
        }


        private List<string> CheckWordExist(WordInfo wordInfo, WordResult wordResult)
        {
            List<Word> usedWord = wordResult.GetUsedWord();
            List<string> wordList = wordInfo.GetWordList();

            // Check if word exists
            List<string> unknownWord = new List<string>();
            bool found;
            for (int i = 0; i < usedWord.Count(); i++)
            {
                found = false;
                for (int j = 0; j < wordList.Count(); j++)
                {
                    if (usedWord[i].GetWordContent().Equals(wordList[j]))
                    {
                        found = true;
                        break;
                    }
                }
                if (found == false)
                {
                    unknownWord.Add(usedWord[i].GetWordContent());
                    Error.AddCrozzleError(usedWord[i].GetWordContent() + ": not exist");
                }
            }
            return unknownWord;
        }

        private char[,] DrawGrid(int row, int column)
        {
            // Draw grid
            
            char[,] grid = new char[row, column];
            for (int i = 0; i < row; i++)
                for (int j = 0; j < column; j++)
                    grid[i, j] = ' ';
            for (int i = 0; i < horizontalWord.Count(); i++)
            {
                int beginRow = horizontalWord[i].GetRows();
                int beginColumn = horizontalWord[i].GetColumns();
                for (int j = 0; j < horizontalWord[i].GetWordContent().Length; j++)
                    grid[beginRow - 1, beginColumn + j - 1] = horizontalWord[i].GetWordContent()[j];
            }
            for (int i = 0; i < verticalWord.Count(); i++)
            {
                int beginRow = verticalWord[i].GetRows();
                int beginColumn = verticalWord[i].GetColumns();
                for (int j = 0; j < verticalWord[i].GetWordContent().Length; j++)
                    grid[beginRow + j - 1, beginColumn - 1] = verticalWord[i].GetWordContent()[j];
            }
            return grid;
        }

        private void CheckWordsCombined(WordInfo wordInfo, List<string> unknownWord)
        {
            List<string> wordList = wordInfo.GetWordList();
            int row = GetMaxRow();
            int column = GetMaxColumn();
            char[,] grid = DrawGrid(row, column);
            // Check words in grid: if they combined and formed a longer word which does not record in wordlist.txt
            for (int i = 0; i < row; i++)
            {
                char[] lineOfChar = new char[column];
                for (int j = 0; j < column; j++)
                    lineOfChar[j] = grid[i, j];
                string line = new string(lineOfChar);
                char[] separator = { SpaceSymbol };
                string[] words = line.Split();
                for (int j = 0; j < words.Length; j++)
                {
                    if (words[j].Length == 1 || words[j].Length == 0)
                        continue;
                    if (!Search(words[j], wordList))
                        if (!Search(words[j], unknownWord))
                            Error.AddCrozzleError("row " + (i + 1) + " " + words[j] + ": 2 word placed together and formed a new word which does not exist in the word list");
                }

            }
            for (int i = 0; i < column; i++)
            {
                char[] lineOfChar = new char[row];
                for (int j = 0; j < row; j++)
                    lineOfChar[j] = grid[j, i];
                string line = new string(lineOfChar);
                char[] separator = { SpaceSymbol };
                string[] words = line.Split();
                for (int j = 0; j < words.Length; j++)
                {
                    if (words[j].Length == 1 || words[j].Length == 0)
                        continue;
                    if (!Search(words[j], wordList))
                        if (!Search(words[j], unknownWord))
                            Error.AddCrozzleError("column " + (i + 1) + " " + words[j] + ": 2 word placed together and formed a new word which does not exist in the word list");
                }

            }
        }

        /// <summary>
        /// Validate crozzle using data from configuration.txt, crozzle.txt and wordlist.txt
        /// </summary>
        /// <param name="config">Config class which store data of configuration.txt</param>
        /// <param name="wordInfo">WordInfo class which store data of wordlist.txt</param>
        /// <param name="wordResult">WordResult class which store data of crozzle.txt</param>
        public void ValidateCrozzle(Config config, WordInfo wordInfo, WordResult wordResult)
        {
            ValidateCore(config, wordResult);

            List<string> unknownWord = CheckWordExist(wordInfo, wordResult);

            CheckWordsCombined(wordInfo, unknownWord);

        }
    }
}
