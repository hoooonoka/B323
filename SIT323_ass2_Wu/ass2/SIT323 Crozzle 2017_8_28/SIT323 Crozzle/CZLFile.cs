using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;


namespace SIT323Crozzle
{
    class CZLFile
    {
        const int CompareTrue = 0;
        const int CantFind = -1;
        const char EqualSymbol = '=';
        const char SpaceSymbol = ' ';
        const char SlashSymbol = '/';
        const char QuoteSymbol = '"';
        const char CommaSymbol = ',';
        const string EmptyString = "";

        private int rows;
        private int columns;
        private int pointPerWord;
        private Dictionary<char, int> intersectionPerLetter = new Dictionary<char, int>();
        private Dictionary<char, int> nonIntersectionPerLetter = new Dictionary<char, int>();
        private List<Word> wordList = new List<Word>();
        private int score;
        private char[,] grid;

        /// <summary>
        /// Read and set rows in crozzle.txt
        /// </summary>
        /// <param name="path">String contains path of crozzle.txt</param>
        public void SetRows(string path)
        {
            StreamReader crozzleReader = new StreamReader(path, Encoding.Default);
            String line;
            while ((line = crozzleReader.ReadLine()) != null)
            {
                if (line.IndexOf('=') != CantFind)
                {
                    if (line.IndexOf("ROWS") != CantFind)
                    {
                        int equalIndex = line.IndexOf(EqualSymbol);
                        int length = line.Length;

                        string value = line.Substring(equalIndex + 1, length - equalIndex - 1);

                        char[] trimcase = { SpaceSymbol };
                        value = value.Trim(trimcase);

                        for (int charIndexInValue = 0; charIndexInValue < value.Length; charIndexInValue++)
                        {
                            if (charIndexInValue != value.Length - 1)
                            {
                                if (value[charIndexInValue] == SlashSymbol && value[charIndexInValue + 1] != SlashSymbol)
                                {
                                    value = value.Substring(0, charIndexInValue - 1);
                                    value = value.Trim(trimcase);
                                    break;
                                }
                            }
                        }
                        try
                        {
                            int result = int.Parse(value);
                            this.rows = result;
                        }
                        catch
                        {
                            Error.AddCrozzleFileError("ROWS: invalid value");
                        }


                    }
                }
            }
        }

        /// <summary>
        /// Get rows information from class
        /// </summary>
        /// <returns>Number of rows</returns>
        public int GetRows()
        {
            return this.rows;
        }

        /// <summary>
        /// Read and set columns in crozzle.txt
        /// </summary>
        /// <param name="path">String contains path of crozzle.txt</param>
        public void SetColumns(string path)
        {
            StreamReader crozzleReader = new StreamReader(path, Encoding.Default);
            String line;
            while ((line = crozzleReader.ReadLine()) != null)
            {
                if (line.IndexOf(EqualSymbol) != -1)
                {
                    if (line.IndexOf("COLUMNS") != CantFind)
                    {
                        int equalIndex = line.IndexOf('=');
                        int length = line.Length;

                        string value = line.Substring(equalIndex + 1, length - equalIndex - 1);

                        char[] trimcase = { SpaceSymbol };
                        value = value.Trim(trimcase);

                        for (int charIndexInValue = 0; charIndexInValue < value.Length; charIndexInValue++)
                        {
                            if (charIndexInValue != value.Length - 1)
                            {
                                if (value[charIndexInValue] == SlashSymbol && value[charIndexInValue + 1] != SlashSymbol)
                                {
                                    value = value.Substring(0, charIndexInValue - 1);
                                    value = value.Trim(trimcase);
                                    break;
                                }
                            }
                        }
                        try
                        {
                            int result = int.Parse(value);
                            this.columns = result;
                        }
                        catch
                        {
                            Error.AddCrozzleFileError("COLUMNS: invalid value");
                        }

                    }
                }
            }
        }

        /// <summary>
        /// Get columns information from class
        /// </summary>
        /// <returns>Number of columns</returns>
        public int GetColumns()
        {
            return this.columns;
        }

        /// <summary>
        /// Read crozzle.txt and set all word used in crozzle in the class
        /// </summary>
        /// <param name="path">String contains path of crozzle.txt</param>
        public void SetWordList(string path)
        {
            StreamReader crozzleReader = new StreamReader(path, Encoding.Default);
            String line;
            while ((line = crozzleReader.ReadLine()) != null)
            {
                if (line.IndexOf(EqualSymbol) != CantFind)
                {
                    if (line.IndexOf("ROW") != CantFind || line.IndexOf("COLUMN") != CantFind)
                    {
                        char[] separatorEqual = { EqualSymbol };
                        string[] separateWithEqual = line.Split(separatorEqual);
                        Word newWord = new Word();
                        newWord.SetType(separateWithEqual[0]);

                        if (separateWithEqual[0].Equals("ROW"))
                        {
                            char[] trimcase = { SpaceSymbol };
                            for (int charIndex = 0; charIndex < separateWithEqual[1].Length; charIndex++)
                            {
                                if (charIndex != separateWithEqual[1].Length - 1)
                                {
                                    if (separateWithEqual[1][charIndex] == SlashSymbol && separateWithEqual[1][charIndex + 1] != SlashSymbol)
                                    {
                                        separateWithEqual[1] = separateWithEqual[1].Substring(0, charIndex - 1);
                                        separateWithEqual[1] = separateWithEqual[1].Trim(trimcase);
                                        break;
                                    }
                                }
                            }
                            char[] separatorComma = { CommaSymbol };
                            string[] separateWithComma = separateWithEqual[1].Split(separatorComma);
                            bool error = false;
                            const int correctLength = 3;
                            if (separateWithComma.Length < correctLength)
                            {
                                Error.AddCrozzleFileError(line + "(" + " 3 fields expected" + ")");
                                continue;
                            }
                            try
                            {
                                int column = int.Parse(separateWithComma[2]);
                                newWord.SetColumns(column);
                            }
                            catch
                            {
                                Error.AddCrozzleFileError(line + "(" + separateWithComma[2] + " is not an integer" + ")");
                                error = true;
                            }
                            try
                            {
                                int row = int.Parse(separateWithComma[0]);
                                newWord.SetRows(row);
                            }
                            catch
                            {
                                Error.AddCrozzleFileError(line + "(" + separateWithComma[0] + " is not an integer" + ")");
                                error = true;
                            }
                            if (separateWithComma[1].Equals(EmptyString))
                            {
                                Error.AddCrozzleFileError(line + " ( word is missing )");
                                error = true;
                            }
                            else
                            {
                                if (!Regex.IsMatch(separateWithComma[1], @"^[A-Z]+$"))
                                {
                                    Error.AddCrozzleFileError(line + "(" + separateWithComma[1] + " is not alphabetic" + ")");
                                    error = true;
                                }
                            }
                            if (error == true)
                                continue;
                            newWord.SetWordContent(separateWithComma[1]);
                            this.wordList.Add(newWord);
                            int columnOfWord = int.Parse(separateWithComma[2]);
                            int rowOfWord = int.Parse(separateWithComma[0]);
                            if (columnOfWord + separateWithComma[1].Length - 1 > columns)
                                Error.AddCrozzleFileError(line + " ( need more columns) ");
                            if (rowOfWord > rows)
                                Error.AddCrozzleFileError(line + " ( need more rows) ");
                        }
                        else if (separateWithEqual[0].Equals("COLUMN"))
                        {
                            char[] trimcase = { SpaceSymbol };
                            for (int charIndex = 0; charIndex < separateWithEqual[1].Length; charIndex++)
                            {
                                if (charIndex != separateWithEqual[1].Length - 1)
                                {
                                    if (separateWithEqual[1][charIndex] == SlashSymbol && separateWithEqual[1][charIndex + 1] != SlashSymbol)
                                    {
                                        separateWithEqual[1] = separateWithEqual[1].Substring(0, charIndex - 1);
                                        separateWithEqual[1] = separateWithEqual[1].Trim(trimcase);
                                        break;
                                    }
                                }
                            }
                            char[] separator_comma = { CommaSymbol };
                            string[] separateWithComma = separateWithEqual[1].Split(separator_comma);
                            bool error = false;
                            const int correctLength = 3;
                            if (separateWithComma.Length < correctLength)
                            {
                                Error.AddCrozzleFileError(line + "(" + line + ": 3 fields expected" + ")");
                                continue;
                            }
                            try
                            {
                                int column = int.Parse(separateWithComma[0]);
                                newWord.SetColumns(column);
                            }
                            catch
                            {
                                Error.AddCrozzleFileError(line + "(" + separateWithComma[0] + " is not an integer" + ")");
                                error = true;
                            }
                            try
                            {
                                int row = int.Parse(separateWithComma[2]);
                                newWord.SetRows(row);
                            }
                            catch
                            {
                                Error.AddCrozzleFileError(line + "(" + separateWithComma[2] + " is not an integer" + ")");
                                error = true;
                            }
                            if (separateWithComma[1].Equals(EmptyString))
                            {
                                Error.AddCrozzleFileError(line + " ( word is missing )");
                                error = true;
                            }
                            else
                            {
                                if (!Regex.IsMatch(separateWithComma[1], @"^[A-Z]+$"))
                                {
                                    Error.AddCrozzleFileError(line + "(" + separateWithComma[1] + " is not alphabetic" + ")");
                                    error = true;
                                }
                            }
                            if (error == true)
                                continue;
                            newWord.SetWordContent(separateWithComma[1]);
                            this.wordList.Add(newWord);
                            int columnOfWord = int.Parse(separateWithComma[0]);
                            int rowOfWord = int.Parse(separateWithComma[2]);
                            if (columnOfWord > columns)
                                Error.AddCrozzleFileError(line + " ( need more columns ) ");
                            if (rowOfWord + separateWithComma[1].Length - 1 > rows)
                                Error.AddCrozzleFileError(line + " ( need more rows ) ");

                        }
                    }
                }
            }
        }

        /// <summary>
        /// Get words used in crozzle
        /// </summary>
        /// <returns>List of words used in crozzle</returns>
        public List<Word> GetWordList()
        {
            return this.wordList;
        }

        public int GetScore()
        {
            return this.score;
        }

        public void SetScore()
        {
            grid = new char[rows, columns];
            score = 0;
            for (int index = 0; index < wordList.Count; index++)
            {
                score += pointPerWord;
                string word = wordList[index].GetWordContent();
                if (wordList[index].GetType().Equals("COLUMN"))
                {
                    for (int charIndex = 0; charIndex < word.Length; charIndex++)
                    {
                        if (grid[wordList[index].GetRows() + charIndex - 1, wordList[index].GetColumns() - 1] == '\0')
                            score += nonIntersectionPerLetter[word[charIndex]];
                        else
                        {
                            score -= nonIntersectionPerLetter[grid[wordList[index].GetRows() + charIndex - 1, wordList[index].GetColumns() - 1]];
                            score += intersectionPerLetter[grid[wordList[index].GetRows() + charIndex - 1, wordList[index].GetColumns() - 1]];
                        }
                        grid[wordList[index].GetRows() + charIndex - 1, wordList[index].GetColumns() - 1] = word[charIndex];
                    }
                }
                else
                {
                    for (int charIndex = 0; charIndex < word.Length; charIndex++)
                    {
                        if (grid[wordList[index].GetRows() - 1, wordList[index].GetColumns() + charIndex - 1] == '\0')
                            score += nonIntersectionPerLetter[word[charIndex]];
                        else
                        {
                            score -= nonIntersectionPerLetter[grid[wordList[index].GetRows() - 1, wordList[index].GetColumns() + charIndex - 1]];
                            score += intersectionPerLetter[grid[wordList[index].GetRows() - 1, wordList[index].GetColumns() + charIndex - 1]];
                        }
                        grid[wordList[index].GetRows() - 1, wordList[index].GetColumns() + charIndex - 1] = word[charIndex];
                    }
                }
            }
        }

        public void SetPointPerWord(string path)
        {
            StreamReader crozzleReader = new StreamReader(path, Encoding.Default);
            String line;
            while ((line = crozzleReader.ReadLine()) != null)
            {
                if (line.IndexOf('=') != CantFind)
                {
                    if (line.IndexOf("POINTS_PER_WORD") != CantFind)
                    {
                        int equalIndex = line.IndexOf(EqualSymbol);
                        int length = line.Length;

                        string value = line.Substring(equalIndex + 1, length - equalIndex - 1);

                        char[] trimcase = { SpaceSymbol };
                        value = value.Trim(trimcase);

                        for (int charIndexInValue = 0; charIndexInValue < value.Length; charIndexInValue++)
                        {
                            if (charIndexInValue != value.Length - 1)
                            {
                                if (value[charIndexInValue] == SlashSymbol && value[charIndexInValue + 1] != SlashSymbol)
                                {
                                    value = value.Substring(0, charIndexInValue - 1);
                                    value = value.Trim(trimcase);
                                    break;
                                }
                            }
                        }
                        try
                        {
                            int result = int.Parse(value);
                            this.pointPerWord = result;
                        }
                        catch
                        {
                            Error.AddCrozzleFileError("POINTS_PER_WORD: invalid value");
                        }


                    }
                }
            }
        }

        public int GetPointPerWord()
        {
            return this.pointPerWord;
        }

        public void SetIntersectionAndNonIntersectionPointPerLetter(string path)
        {
            StreamReader ConfigReader = new StreamReader(path, Encoding.Default);
            String line;
            while ((line = ConfigReader.ReadLine()) != null)
            {
                int length = line.Length;
                int equalIndex = line.IndexOf(EqualSymbol);
                if (equalIndex == CantFind || equalIndex == 0)
                    continue;
                string parameter = line.Substring(0, equalIndex);
                string value = line.Substring(equalIndex + 1, length - equalIndex - 1);
                if (equalIndex != CantFind)
                {
                    char[] trimcase = { SpaceSymbol };
                    parameter = parameter.Trim(trimcase);
                    value = value.Trim(trimcase);
                    if (value.Length == 0)
                    {
                        Error.AddConfigurationError(parameter + " value is missing");
                        continue;
                    }
                    for (int charIndex = 0; charIndex < value.Length; charIndex++)
                    {
                        if (charIndex != value.Length - 1)
                        {
                            if (value[charIndex] == SlashSymbol && value[charIndex + 1] == SlashSymbol)
                            {
                                value = value.Substring(0, charIndex - 1);
                                value = value.Trim(trimcase);
                                break;
                            }
                        }
                    }
                    int valueLength = value.Length;
                    if (parameter.CompareTo("INTERSECTING_POINTS_PER_LETTER") == CompareTrue)
                    {
                        if (value[0] == QuoteSymbol && value[valueLength - 1] == QuoteSymbol)
                            value = value.Substring(1, valueLength - 2);
                        SetIntersection(value);
                    }
                    else if ( parameter.CompareTo("NON_INTERSECTING_POINTS_PER_LETTER") == CompareTrue)
                    {
                        if (value[0] == QuoteSymbol && value[valueLength - 1] == QuoteSymbol)
                            value = value.Substring(1, valueLength - 2);
                        SetNonIntersection(value);
                    }

                }
                else
                {
                    continue;
                }

            }
        }

        public void SetIntersection(string data)
        {
            char[] separator = { CommaSymbol };
            string[] allCharsAndValues = data.Split(separator);
            const int correctLength = 2;
            for (int arrayIndex = 0; arrayIndex < allCharsAndValues.Length; arrayIndex++)
            {
                char[] separatorForAllCharsAndValues = { EqualSymbol };
                string[] eachCharAndValue = allCharsAndValues[arrayIndex].Split(separatorForAllCharsAndValues);
                if (eachCharAndValue.Length == correctLength && Regex.IsMatch(eachCharAndValue[0], @"^[A-Z]$") && Regex.IsMatch(eachCharAndValue[1], @"^\d+$"))
                {
                    int value = int.Parse(eachCharAndValue[1]);
                    char letter = eachCharAndValue[0][0];
                    intersectionPerLetter.Add(letter, value);
                }
                else
                {
                    continue;
                }
            }
        }

        public void SetNonIntersection(string data)
        {
            char[] separator = { CommaSymbol };
            string[] allCharsAndValues = data.Split(separator);
            const int correctLength = 2;
            for (int arrayIndex = 0; arrayIndex < allCharsAndValues.Length; arrayIndex++)
            {
                char[] separatorForAllCharsAndValues = { EqualSymbol };
                string[] eachCharAndValue = allCharsAndValues[arrayIndex].Split(separatorForAllCharsAndValues);
                if (eachCharAndValue.Length == correctLength && Regex.IsMatch(eachCharAndValue[0], @"^[A-Z]$") && Regex.IsMatch(eachCharAndValue[1], @"^\d+$"))
                {
                    int value = int.Parse(eachCharAndValue[1]);
                    char letter = eachCharAndValue[0][0];
                    nonIntersectionPerLetter.Add(letter, value);
                }
                else
                {
                    continue;
                }
            }
        }

        public void SetGrid()
        {
            grid = new char[rows, columns];
            for(int index=0;index< wordList.Count; index++)
            {
                string word = wordList[index].GetWordContent();
                if(wordList[index].GetType().Equals("COLUMN"))
                {
                    for (int charIndex = 0; charIndex < word.Length; charIndex++)
                    {
                        grid[wordList[index].GetRows() + charIndex - 1, wordList[index].GetColumns() - 1] = word[charIndex];
                    }
                }
                else
                {
                    for (int charIndex = 0; charIndex < word.Length; charIndex++)
                    {
                        grid[wordList[index].GetRows() - 1, wordList[index].GetColumns() + charIndex - 1] = word[charIndex];
                    }
                }
            }
        }

        public char[,] GetGrid()
        {
            return this.grid;
        }

        public void SetAllValue(string path)
        {
            SetRows(path);
            SetColumns(path);
            SetWordList(path);
            SetPointPerWord(path);
            SetIntersectionAndNonIntersectionPointPerLetter(path);
            SetGrid();
            SetScore();
        }
    }
}
