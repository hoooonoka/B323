using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace SIT323Crozzle
{
    /// <summary>
    /// The class store word used in crozzle and methods used for crozzle file validation
    /// </summary>
    public class WordResult
    {
        const int CompareTrue = 0;
        const int CantFind = -1;
        const char EqualSymbol = '=';
        const char SpaceSymbol = ' ';
        const char SlashSymbol = '/';
        const char QuoteSymbol = '"';
        const char CommaSymbol = ',';
        const string EmptyString = "";

        private int score;
        private List<Word> usedWord = new List<Word>();
        private int rows;
        private int columns;
        private Dictionary<string, bool> parameterHasSetValue = new Dictionary<string, bool>();

        /// <summary>
        /// Initialize dictionary which contains assigning information
        /// </summary>
        public void InitializeSetValue()
        {
            this.parameterHasSetValue.Add("ROWS", false);
            this.parameterHasSetValue.Add("COLUMNS", false);
        }

        /// <summary>
        /// Get assigning information from dictionary
        /// </summary>
        /// <param name="parameter">Rows or columns</param>
        /// <returns>True if value is assigned, false if has not assigned</returns>
        public bool GetSetInformation(string parameter)
        {
            return this.parameterHasSetValue[parameter];
        }

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
                            this.parameterHasSetValue["ROWS"] = true;
                        }
                        catch
                        {
                            Error.AddCrozzleFileError("ROWS: invalid value");
                        }


                    }
                }
            }
            crozzleReader.Close();
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
                            this.parameterHasSetValue["COLUMNS"] = true;
                        }
                        catch
                        {
                            Error.AddCrozzleFileError("COLUMNS: invalid value");
                        }

                    }
                }
            }
            crozzleReader.Close();

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
        /// Calculate score for crozzle and store it in the class
        /// </summary>
        /// <param name="eachWordScore">Points for each words</param>
        /// <param name="crossingLetterScore">Dictionary contains points per letter for intersecting letter</param>
        /// <param name="nonCrossingLetterScore">Dictionary contains points per letter for non-intersecting letter</param>
        public void CalculateScore(int eachWordScore)
        {
            List<Word> horizontalWord = new List<Word>();
            List<Word> verticalWord = new List<Word>();
            List<char> crossingLetter = new List<char>();
            for (int usedWordIndex = 0; usedWordIndex < this.usedWord.Count(); usedWordIndex++)
            {
                if (this.usedWord[usedWordIndex].GetType().CompareTo("ROW") == CompareTrue)
                    horizontalWord.Add(this.usedWord[usedWordIndex]);
                else
                    verticalWord.Add(this.usedWord[usedWordIndex]);
            }
            for (int horizontalWordIndex = 0; horizontalWordIndex < horizontalWord.Count(); horizontalWordIndex++)
            {

                for (int verticalWordIndex = 0; verticalWordIndex < verticalWord.Count(); verticalWordIndex++)
                {
                    int distanceColumn = verticalWord[verticalWordIndex].GetColumns() - horizontalWord[horizontalWordIndex].GetColumns();
                    int distanceRow = horizontalWord[horizontalWordIndex].GetRows() - verticalWord[verticalWordIndex].GetRows();
                    if (distanceColumn < 0 || distanceColumn > horizontalWord[horizontalWordIndex].GetWordContent().Length - 1 || distanceRow < 0 || distanceRow > verticalWord[verticalWordIndex].GetWordContent().Length - 1)
                        continue;
                    if (horizontalWord[horizontalWordIndex].GetWordContent()[verticalWord[verticalWordIndex].GetColumns() - horizontalWord[horizontalWordIndex].GetColumns()] != verticalWord[verticalWordIndex].GetWordContent()[horizontalWord[horizontalWordIndex].GetRows() - verticalWord[verticalWordIndex].GetRows()])
                        continue;
                    crossingLetter.Add(horizontalWord[horizontalWordIndex].GetWordContent()[verticalWord[verticalWordIndex].GetColumns() - horizontalWord[horizontalWordIndex].GetColumns()]);
                }
            }
            int score = this.usedWord.Count * eachWordScore;
            for (int i = 0; i < this.usedWord.Count(); i++)
            {
                for (int j = 0; j < this.usedWord[i].GetWordContent().Length; j++)
                {
                    score += WordInfo.nonIntersectingPointsPerLetter[this.usedWord[i].GetWordContent()[j]];
                }
            }
            for (int usedWordIndex = 0; usedWordIndex < crossingLetter.Count(); usedWordIndex++)
            {
                score -= 2 * WordInfo.nonIntersectingPointsPerLetter[crossingLetter[usedWordIndex]];
                score += WordInfo.intersectingPointsPerLetter[crossingLetter[usedWordIndex]];
            }
            this.score = score;
        }

        /// <summary>
        /// Get score of crozzle
        /// </summary>
        /// <returns>Value of score of crozzle</returns>
        public int GetScore()
        {
            return this.score;
        }

        /// <summary>
        /// Read crozzle.txt and set all word used in crozzle in the class
        /// </summary>
        /// <param name="path">String contains path of crozzle.txt</param>
        public void SetUsedWord(string path)
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
                                Error.AddCrozzleFileError(line + "(" + separateWithComma[2] + " is not an integer"+")");
                                error = true;
                            }
                            try
                            {
                                int row = int.Parse(separateWithComma[0]);
                                newWord.SetRows(row);
                            }
                            catch
                            {
                                Error.AddCrozzleFileError(line + "(" + separateWithComma[0] + " is not an integer"+")");
                                error = true;
                            }
                            if (separateWithComma[1].Equals(EmptyString))
                            {
                                Error.AddCrozzleFileError(line+" ( word is missing )");
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
                            this.usedWord.Add(newWord);
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
                                Error.AddCrozzleFileError(line + "(" + line + ": 3 fields expected"+")");
                                continue;
                            }
                            try
                            {
                                int column = int.Parse(separateWithComma[0]);
                                newWord.SetColumns(column);
                            }
                            catch
                            {
                                Error.AddCrozzleFileError(line + "(" + separateWithComma[0] + " is not an integer"+")");
                                error = true;
                            }
                            try
                            {
                                int row = int.Parse(separateWithComma[2]);
                                newWord.SetRows(row);
                            }
                            catch
                            {
                                Error.AddCrozzleFileError(line + "(" + separateWithComma[2] + " is not an integer"+")");
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
                            this.usedWord.Add(newWord);
                            int columnOfWord = int.Parse(separateWithComma[0]);
                            int rowOfWord = int.Parse(separateWithComma[2]);
                            if (columnOfWord > columns)
                                Error.AddCrozzleFileError(line + " ( need more columns ) ");
                            if(rowOfWord + separateWithComma[1].Length - 1 > rows)
                                Error.AddCrozzleFileError(line + " ( need more rows ) ");

                        }
                    }
                    else
                    {
                        char[] separatorEqual = { EqualSymbol };
                        string[] separateWithEqual = line.Split(separatorEqual);
                        if ((!separateWithEqual[0].Equals("CONFIGURATION_FILE")) && (!separateWithEqual[0].Equals("WORDLIST_FILE")) && (!separateWithEqual[0].Equals("ROWS")) && (!separateWithEqual[0].Equals("COLUMNS")))
                        {
                            Error.AddCrozzleFileError(line+"("+separateWithEqual[0] + " : not a reserved word"+")");
                            continue;
                        }
                    }
                }
            }
            crozzleReader.Close();
        }

        /// <summary>
        /// Get words used in crozzle
        /// </summary>
        /// <returns>List of words used in crozzle</returns>
        public List<Word> GetUsedWord()
        {
            return this.usedWord;
        }


        private void CheckMinAndMax(Config config)
        {
            int rowsMax = config.GetMaximumNumberOfRows();
            int rowsMin = config.GetMinimumNumberOfRows();
            int columnsMax = config.GetMaximumNumberOfColumns();
            int columnsMin = config.GetMinimumNumberOfColumns();
            if (config.GetSetInformation("MAXIMUM_NUMBER_OF_ROWS") && this.GetSetInformation("ROWS") && this.rows > rowsMax)
                Error.AddCrozzleFileError("ROWS: larger than maximum number of rows");
            if (config.GetSetInformation("MAXIMUM_NUMBER_OF_COLUMNS") && this.GetSetInformation("COLUMNS") && this.columns > columnsMax)
                Error.AddCrozzleFileError("COLUMNS: larger than maximum number of columns");
            if (config.GetSetInformation("MINIMUM_NUMBER_OF_ROWS") && this.GetSetInformation("ROWS") && this.rows < rowsMin)
                Error.AddCrozzleFileError("ROWS: smaller than minimum number of rows");
            if (config.GetSetInformation("MINIMUM_NUMBER_OF_COLUMNS") && this.GetSetInformation("COLUMNS") && this.columns < columnsMin)
                Error.AddCrozzleFileError("COLUMNS: smaller than minimum number of columns");
        }

        /// <summary>
        /// Validate rows and columns
        /// Check if they meet requirements
        /// </summary>
        /// <param name="config">Config class contains data from configuration.txt</param>
        public void ValidateRowsColumns(Config config)
        {
            Log.AddLogInformation("crozzle file validation : begin");

            // Check minimum value and maximum value
            CheckMinAndMax(config);

            Log.AddLogInformation("crozzle file validation : end");

        }
    }
}
