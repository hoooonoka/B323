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
    /// Class store all configuration items from configuration.txt
    /// </summary>
    public class Config
    {
        const int NotAssigned = 0;
        const int AssignedFail = 1;
        const int AssignSuccess = 2;
        const int CompareTrue = 0;
        const int ValueZero = 0;
        const int CantFind = -1;
        const char EqualSymbol = '=';
        const char SpaceSymbol = ' ';
        const char SlashSymbol = '/';
        const char QuoteSymbol = '"';
        const char SharpSymbol = '#';
        const char CommaSymbol = ',';
        const int colourLength = 7;

        private string logfileName;
        private int minimumNumberOfUniqueWords;
        private int maximumNumberOfUniqueWords;
        private string invalidCrozzleScore;
        private bool uppercase;
        private string style;
        private string bgcolourEmptyTD;
        private string bgcolourNonEmptyTD;
        private int minimumNumberOfRows;
        private int maximumNumberOfRows;
        private int minimumNumberOfColumns;
        private int maximumNumberOfColumns;
        private int minimumHorizontalWords;
        private int maximumHorizontalWords;
        private int minimumVerticalWords;
        private int maximumVerticalWords;
        private int minimumIntersectionsInHorizontalWords;
        private int maximumIntersectionsInHorizontalWords;
        private int minimumIntersectionsInVerticalWords;
        private int maximumIntersectionsInVerticalWords;
        private int minimumNumberOfTheSameWord;
        private int maximumNumberOfTheSameWord;
        private int minimumNumberOfGroups;
        private int maximumNumberOfGroups;
        private int pointsPerWord;
        private string intersectingPointsPerLetter;
        private string nonIntersectingPointsPerLetter;

        // A dictionary stores parameter assigning information
        private Dictionary<string, int> parameterHasSetValue = new Dictionary<string, int>();

        /// <summary>
        /// Initialize the dictionary all parameter NotAssigned
        /// </summary>
        private void InitializeSetValue()
        {
            parameterHasSetValue.Add("LOGFILE_NAME", NotAssigned);
            parameterHasSetValue.Add("MINIMUM_NUMBER_OF_UNIQUE_WORDS", NotAssigned);
            parameterHasSetValue.Add("MAXIMUM_NUMBER_OF_UNIQUE_WORDS", NotAssigned);
            parameterHasSetValue.Add("INVALID_CROZZLE_SCORE", NotAssigned);
            parameterHasSetValue.Add("UPPERCASE", NotAssigned);
            parameterHasSetValue.Add("STYLE", NotAssigned);
            parameterHasSetValue.Add("BGCOLOUR_EMPTY_TD", NotAssigned);
            parameterHasSetValue.Add("BGCOLOUR_NON_EMPTY_TD", NotAssigned);
            parameterHasSetValue.Add("MINIMUM_NUMBER_OF_ROWS", NotAssigned);
            parameterHasSetValue.Add("MAXIMUM_NUMBER_OF_ROWS", NotAssigned);
            parameterHasSetValue.Add("MINIMUM_NUMBER_OF_COLUMNS", NotAssigned);
            parameterHasSetValue.Add("MAXIMUM_NUMBER_OF_COLUMNS", NotAssigned);
            parameterHasSetValue.Add("MINIMUM_HORIZONTAL_WORDS", NotAssigned);
            parameterHasSetValue.Add("MAXIMUM_HORIZONTAL_WORDS", NotAssigned);
            parameterHasSetValue.Add("MINIMUM_VERTICAL_WORDS", NotAssigned);
            parameterHasSetValue.Add("MAXIMUM_VERTICAL_WORDS", NotAssigned);
            parameterHasSetValue.Add("MINIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS", NotAssigned);
            parameterHasSetValue.Add("MAXIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS", NotAssigned);
            parameterHasSetValue.Add("MINIMUM_INTERSECTIONS_IN_VERTICAL_WORDS", NotAssigned);
            parameterHasSetValue.Add("MAXIMUM_INTERSECTIONS_IN_VERTICAL_WORDS", NotAssigned);
            parameterHasSetValue.Add("MINIMUM_NUMBER_OF_THE_SAME_WORD", NotAssigned);
            parameterHasSetValue.Add("MAXIMUM_NUMBER_OF_THE_SAME_WORD", NotAssigned);
            parameterHasSetValue.Add("MINIMUM_NUMBER_OF_GROUPS", NotAssigned);
            parameterHasSetValue.Add("MAXIMUM_NUMBER_OF_GROUPS", NotAssigned);
            parameterHasSetValue.Add("POINTS_PER_WORD", NotAssigned);
            parameterHasSetValue.Add("NON_INTERSECTING_POINTS_PER_LETTER", NotAssigned);
            parameterHasSetValue.Add("INTERSECTING_POINTS_PER_LETTER", NotAssigned);
        }

        /// <summary>
        /// Change path to Log file folder
        /// </summary>
        /// <param name="path">Path</param>
        /// <returns>Path to Log file folder</returns>
        public string ChangePath(string path, string rootPath)
        {
            if (path.IndexOf(@":\") != CantFind)
                return path;
            else
            {
                string returnPath = path;
                if (path[0].Equals('.') && path[1].Equals('\\'))
                    returnPath = path.Substring(2, path.Length - 2);
                returnPath = rootPath+"../" + returnPath;
                return returnPath;

            }
        }

        /// <summary>
        /// Get information of assigning configuration item
        /// </summary>
        /// <param name="parameter">String stores configuration item</param>
        /// <returns>True if configuration item assigned successfully, false if assigned fail</returns>
        public bool GetSetInformation(string parameter)
        {
            if (this.parameterHasSetValue[parameter] == AssignSuccess)
                return true;
            return false;
        }

        /// <summary>
        /// Set log file name in the class
        /// </summary>
        /// <param name="name">String contains log file name</param>
        public void SetLogfileName(string name)
        {
            this.logfileName = name;
            this.parameterHasSetValue["LOGFILE_NAME"] = AssignSuccess;
        }

        /// <summary>
        /// Get log file name
        /// </summary>
        /// <returns>String of log file name</returns>
        public string GetLogfileName()
        {
            return this.logfileName;
        }

        /// <summary>
        /// Set number of minimum number of unique words
        /// </summary>
        /// <param name="number">Number of minimum number of unique words</param>
        public void SetMinimumNumberOfUniqueWords(int number)
        {
            this.minimumNumberOfUniqueWords = number;
            this.parameterHasSetValue["MINIMUM_NUMBER_OF_UNIQUE_WORDS"] = AssignSuccess;
        }

        /// <summary>
        /// Get number of minimum number of unique words
        /// </summary>
        /// <returns>Number of minimum number of unique words</returns>
        public int GetMinimumNumberOfUniqueWords()
        {
            return this.minimumNumberOfUniqueWords;
        }

        /// <summary>
        /// Set number of maximum number of unique words
        /// </summary>
        /// <param name="number">Number of maximum number of unique words</param>
        public void SetMaximumNumberOfUniqueWords(int number)
        {
            this.maximumNumberOfUniqueWords = number;
            this.parameterHasSetValue["MAXIMUM_NUMBER_OF_UNIQUE_WORDS"] = AssignSuccess;
        }

        /// <summary>
        /// Get number of maximum number of unique words
        /// </summary>
        /// <returns>Number of maximum number of unique words</returns>
        public int GetMaximumNumberOfUniqueWords()
        {
            return this.maximumNumberOfUniqueWords;
        }

        /// <summary>
        /// Set score of invalid crozzle
        /// </summary>
        /// <param name="score">String contains score of invalid crozzle</param>
        public void SetInvalidCrozzleScore(string score)
        {
            this.invalidCrozzleScore = score;
            this.parameterHasSetValue["INVALID_CROZZLE_SCORE"] = AssignSuccess;
        }

        /// <summary>
        /// Get score of invalid crozzle
        /// </summary>
        /// <returns>String contains score of invalid crozzle</returns>
        public string GetInvalidCrozzleScore()
        {
            return this.invalidCrozzleScore;
        }

        /// <summary>
        /// Set case for displaying crozzle
        /// </summary>
        /// <param name="uppercase">True if uppercase, false if lower case</param>
        public void SetUppercase(bool uppercase)
        {
            this.uppercase = uppercase;
            this.parameterHasSetValue["UPPERCASE"] = AssignSuccess;
        }

        /// <summary>
        /// Get case for displaying crozzle
        /// </summary>
        /// <returns>True if uppercase, false if lower case</returns>
        public bool GetUppercase()
        {
            return this.uppercase;
        }

        /// <summary>
        /// Set style of crozzle
        /// </summary>
        /// <param name="style">String contains style information</param>
        public void SetStyle(string style)
        {
            if (style[0].Equals('"') && style[style.Length - 1].Equals('"'))
                style = style.Substring(1, style.Length - 3);
            this.style = style;
            this.parameterHasSetValue["STYLE"] = AssignSuccess;
        }

        /// <summary>
        /// Get style of crozzle
        /// </summary>
        /// <returns>String contains style information</returns>
        public string GetStyle()
        {
            return this.style;
        }

        /// <summary>
        /// Set background colour of crozzle
        /// </summary>
        /// <param name="colour">String contains background colour of crozzle</param>
        public void SetBgcolourEmptyTD(string colour)
        {
            this.bgcolourEmptyTD = colour;
            this.parameterHasSetValue["BGCOLOUR_EMPTY_TD"] = AssignSuccess;
        }

        /// <summary>
        /// Get background colour of crozzle
        /// </summary>
        /// <returns>String contains background colour of crozzle</returns>
        public string GetBgcolourEmptyTD()
        {
            return this.bgcolourEmptyTD;
        }

        /// <summary>
        /// Set background colour of crozzle
        /// </summary>
        /// <param name="colour">String contains background colour of crozzle</param>
        public void SetBgcolourNonEmptyTD(string colour)
        {
            this.bgcolourNonEmptyTD = colour;
            this.parameterHasSetValue["BGCOLOUR_NON_EMPTY_TD"] = AssignSuccess;
        }

        /// <summary>
        /// Get background colour of crozzle
        /// </summary>
        /// <returns>String contains background colour of crozzle</returns>
        public string GetBgcolourNonEmptyTD()
        {
            return this.bgcolourNonEmptyTD;
        }

        /// <summary>
        /// Set minimum number of rows
        /// </summary>
        /// <param name="number">Number of minimum number of rows</param>
        public void SetMinimumNumberOfRows(int number)
        {
            this.minimumNumberOfRows = number;
            this.parameterHasSetValue["MINIMUM_NUMBER_OF_ROWS"] = AssignSuccess;
        }

        /// <summary>
        /// Get minimum number of rows
        /// </summary>
        /// <returns>Number of minimum number of rows</returns>
        public int GetMinimumNumberOfRows()
        {
            return this.minimumNumberOfRows;
        }

        /// <summary>
        /// Set maximum number of rows
        /// </summary>
        /// <param name="number">Number of maximum number of rows</param>
        public void SetMaximumNumberOfRows(int number)
        {
            this.maximumNumberOfRows = number;
            this.parameterHasSetValue["MAXIMUM_NUMBER_OF_ROWS"] = AssignSuccess;
        }

        /// <summary>
        /// Get maximum number of rows
        /// </summary>
        /// <returns>Number of maximum number of rows</returns>
        public int GetMaximumNumberOfRows()
        {
            return this.maximumNumberOfRows;
        }

        /// <summary>
        /// Set minimum number of columns
        /// </summary>
        /// <param name="number">Number of minimum number of columns</param>
        public void SetMinimumNumberOfColumns(int number)
        {
            this.minimumNumberOfColumns = number;
            this.parameterHasSetValue["MINIMUM_NUMBER_OF_COLUMNS"] = AssignSuccess;
        }

        /// <summary>
        /// Get minimum number of columns
        /// </summary>
        /// <returns>Number of minimum number of columns</returns>
        public int GetMinimumNumberOfColumns()
        {
            return this.minimumNumberOfColumns;
        }

        /// <summary>
        /// Set maximum number of columns
        /// </summary>
        /// <param name="number">Number of maximum number of columns</param>
        public void SetMaximumNumberOfColumns(int number)
        {
            this.maximumNumberOfColumns = number;
            this.parameterHasSetValue["MAXIMUM_NUMBER_OF_COLUMNS"] = AssignSuccess;
        }

        /// <summary>
        /// Get maximum number of columns
        /// </summary>
        /// <returns>Number of maximum number of columns</returns>
        public int GetMaximumNumberOfColumns()
        {
            return this.maximumNumberOfColumns;
        }

        /// <summary>
        /// Set minimum horizontal words
        /// </summary>
        /// <param name="number">Number of minimum number of horizontal words</param>
        public void SetMinimumHorizontalWords(int number)
        {
            this.minimumHorizontalWords = number;
            this.parameterHasSetValue["MINIMUM_HORIZONTAL_WORDS"] = AssignSuccess;
        }

        /// <summary>
        /// Get minimum horizontal words
        /// </summary>
        /// <returns>Number of minimum number of horizontal words</returns>
        public int GetMinimumHorizontalWords()
        {
            return this.minimumHorizontalWords;
        }

        /// <summary>
        /// Set maximum horizontal words
        /// </summary>
        /// <param name="number">Number of maximum number of horizontal words</param>
        public void SetMaximumHorizontalWords(int number)
        {
            this.maximumHorizontalWords = number;
            this.parameterHasSetValue["MAXIMUM_HORIZONTAL_WORDS"] = AssignSuccess;
        }

        /// <summary>
        /// Get maximum horizontal words
        /// </summary>
        /// <returns>Number of maximum number of horizontal words</returns>
        public int GetMaximumHorizontalWords()
        {
            return this.maximumHorizontalWords;
        }

        /// <summary>
        /// Set minimum vertical words
        /// </summary>
        /// <param name="number">Number of minimum number of vertical words</param>
        public void SetMinimumVerticalWords(int number)
        {
            this.minimumVerticalWords = number;
            this.parameterHasSetValue["MINIMUM_VERTICAL_WORDS"] = AssignSuccess;
        }

        /// <summary>
        /// Get minimum vertical words
        /// </summary>
        /// <returns>Number of minimum number of vertical words</returns>
        public int GetMinimumVerticalWords()
        {
            return this.minimumVerticalWords;
        }

        /// <summary>
        /// Set maximum vertical words
        /// </summary>
        /// <param name="number">Number of maximum vertical words</param>
        public void SetMaximumVerticalWords(int number)
        {
            this.maximumVerticalWords = number;
            this.parameterHasSetValue["MAXIMUM_VERTICAL_WORDS"] = AssignSuccess;
        }

        /// <summary>
        /// Get maximum vertical words
        /// </summary>
        /// <returns>Number of maximum vertical words</returns>
        public int GetMaximumVerticalWords()
        {
            return this.maximumVerticalWords;
        }

        /// <summary>
        /// Set minimum intersections in horizontal words
        /// </summary>
        /// <param name="number">Number of maximum intersections in horizontal words</param>
        public void SetMinimumIntersectionsInHorizontalWords(int number)
        {
            this.minimumIntersectionsInHorizontalWords = number;
            this.parameterHasSetValue["MINIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS"] = AssignSuccess;
        }

        /// <summary>
        /// Get minimum intersections in horizontal words
        /// </summary>
        /// <returns>Number of maximum intersections in horizontal words</returns>
        public int GetMinimumIntersectionsInHorizontalWords()
        {
            return this.minimumIntersectionsInHorizontalWords;
        }

        /// <summary>
        /// Set maximum intersections in horizontal words
        /// </summary>
        /// <param name="number">Number of maximum intersections in horizontal words</param>
        public void SetMaximumIntersectionsInHorizontalWords(int number)
        {
            this.maximumIntersectionsInHorizontalWords = number;
            this.parameterHasSetValue["MAXIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS"] = AssignSuccess;
        }

        /// <summary>
        /// Get maximum intersections in horizontal words
        /// </summary>
        /// <returns>Number of maximum intersections in horizontal words</returns>
        public int GetMaximumIntersectionsInHorizontalWords()
        {
            return this.maximumIntersectionsInHorizontalWords;
        }

        /// <summary>
        /// Set minimum intersections in vertical words
        /// </summary>
        /// <param name="number">Number of minimum intersections in vertical words</param>
        public void SetMinimumIntersectionsInVerticalWords(int number)
        {
            this.minimumIntersectionsInVerticalWords = number;
            this.parameterHasSetValue["MINIMUM_INTERSECTIONS_IN_VERTICAL_WORDS"] = AssignSuccess;
        }

        /// <summary>
        /// Get minimum intersections in vertical words
        /// </summary>
        /// <returns>Number of minimum intersections in vertical words</returns>
        public int GetMinimumIntersectionsInVerticalWords()
        {
            return this.minimumIntersectionsInVerticalWords;
        }

        /// <summary>
        /// Set maximum intersections in vertical words
        /// </summary>
        /// <param name="number">Number of maximum intersections in vertical words</param>
        public void SetMaximumIntersectionsInVerticalWords(int number)
        {
            this.maximumIntersectionsInVerticalWords = number;
            this.parameterHasSetValue["MAXIMUM_INTERSECTIONS_IN_VERTICAL_WORDS"] = AssignSuccess;
        }

        /// <summary>
        /// Get maximum intersections in vertical words
        /// </summary>
        /// <returns>Number of maximum intersections in vertical words</returns>
        public int GetMaximumIntersectionsInVerticalWords()
        {
            return this.maximumIntersectionsInVerticalWords;
        }

        /// <summary>
        /// Set minimum number of the same word
        /// </summary>
        /// <param name="number">Number of minimum number of the same word</param>
        public void SetMinimumNumberOfTheSameWord(int number)
        {
            this.minimumNumberOfTheSameWord = number;
            this.parameterHasSetValue["MINIMUM_NUMBER_OF_THE_SAME_WORD"] = AssignSuccess;
        }

        /// <summary>
        /// Get minimum number of the same word
        /// </summary>
        /// <returns>Number of minimum number of the same word</returns>
        public int GetMinimumNumberOfTheSameWord()
        {
            return this.minimumNumberOfTheSameWord;
        }

        /// <summary>
        /// Set maximum number of the same word
        /// </summary>
        /// <param name="number">Number of maximum number of the same word</param>
        public void SetMaximumNumberOfTheSameWord(int number)
        {
            this.maximumNumberOfTheSameWord = number;
            this.parameterHasSetValue["MAXIMUM_NUMBER_OF_THE_SAME_WORD"] = AssignSuccess;
        }

        /// <summary>
        /// Get maximum number of the same word
        /// </summary>
        /// <returns>Number of maximum number of the same word</returns>
        public int GetMaximumNumberOfTheSameWord()
        {
            return this.maximumNumberOfTheSameWord;
        }

        /// <summary>
        /// Set minimum number of groups
        /// </summary>
        /// <param name="number">Number of minimum number of groups</param>
        public void SetMinimumNumberOfGroups(int number)
        {
            this.minimumNumberOfGroups = number;
            this.parameterHasSetValue["MINIMUM_NUMBER_OF_GROUPS"] = AssignSuccess;
        }

        /// <summary>
        /// Get minimum number of groups
        /// </summary>
        /// <returns>Number of minimum number of groups</returns>
        public int GetMinimumNumberOfGroups()
        {
            return this.minimumNumberOfGroups;
        }

        /// <summary>
        /// Set maximum number of groups
        /// </summary>
        /// <param name="number">Number of maximum number of groups</param>
        public void SetMaximumNumberOfGroups(int number)
        {
            this.maximumNumberOfGroups = number;
            this.parameterHasSetValue["MAXIMUM_NUMBER_OF_GROUPS"] = AssignSuccess;
        }

        /// <summary>
        /// Get maximum number of groups
        /// </summary>
        /// <returns>Number of maximum number of groups</returns>
        public int GetMaximumNumberOfGroups()
        {
            return this.maximumNumberOfGroups;
        }

        /// <summary>
        /// Set points per word
        /// </summary>
        /// <param name="points">Value of points per word</param>
        public void SetPointsPerWord(int points)
        {
            this.pointsPerWord = points;
            this.parameterHasSetValue["POINTS_PER_WORD"] = AssignSuccess;
        }

        /// <summary>
        /// Get points per word
        /// </summary>
        /// <returns>Value of points per word</returns>
        public int GetPointsPerWord()
        {
            return this.pointsPerWord;
        }

        /// <summary>
        /// Set intersecting points per letter
        /// </summary>
        /// <param name="intersecting_points_per_letter">String contains information of intersecting points per letter</param>
        public void SetIntersectingPointsPerLetter(string intersecting_points_per_letter)
        {
            this.intersectingPointsPerLetter = intersecting_points_per_letter;
            this.parameterHasSetValue["INTERSECTING_POINTS_PER_LETTER"] = AssignSuccess;
        }

        /// <summary>
        /// Get intersecting points per letter
        /// </summary>
        /// <returns>String contains information of intersecting points per letter</returns>
        public string GetIntersectingPointsPerLetter()
        {
            return this.intersectingPointsPerLetter;
        }

        /// <summary>
        /// Set non intersecting points per letter
        /// </summary>
        /// <param name="non_intersecting_points_per_letter">String contains information of non intersecting points per letter</param>
        public void SetNonIntersectingPointsPerLetter(string non_intersecting_points_per_letter)
        {
            this.nonIntersectingPointsPerLetter = non_intersecting_points_per_letter;
            this.parameterHasSetValue["NON_INTERSECTING_POINTS_PER_LETTER"] = AssignSuccess;
        }

        /// <summary>
        /// Get non intersecting points per letter
        /// </summary>
        /// <returns>String contains information of non intersecting points per letter</returns>
        public string GetNonIntersectingPointsPerLetter()
        {
            return this.nonIntersectingPointsPerLetter;
        }

        /// <summary>
        /// Read all configuration information from configuration.txt and parse them into different data types(string, int, bool),
        /// then stored in the class
        /// </summary>
        /// <param name="path">string contains the path of configuration.txt</param>
        public void SetConfig(string path)
        {
            InitializeSetValue();
            StreamReader ConfigReader = new StreamReader(path, Encoding.Default);
            String line;
            Log.AddLogInformation("read configuration file "+path+" : begin");
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
                    if (parameter.CompareTo("LOGFILE_NAME") == CompareTrue || parameter.CompareTo("INVALID_CROZZLE_SCORE") == CompareTrue || parameter.CompareTo("STYLE") == CompareTrue || parameter.CompareTo("BGCOLOUR_EMPTY_TD") == CompareTrue || parameter.CompareTo("BGCOLOUR_NON_EMPTY_TD") == CompareTrue || parameter.CompareTo("INTERSECTING_POINTS_PER_LETTER") == CompareTrue || parameter.CompareTo("NON_INTERSECTING_POINTS_PER_LETTER") == CompareTrue)
                    {
                        if (value[0] == QuoteSymbol && value[valueLength - 1] == QuoteSymbol)
                            value = value.Substring(1, valueLength - 2);
                    }
                    if (parameterHasSetValue.ContainsKey(parameter) && parameterHasSetValue[parameter]== AssignSuccess)
                        Error.AddConfigurationError(parameter + ": duplicated assignment");

                    SetAttribute(parameter, value);

                }
                else
                {
                    continue;
                }

            }
            Log.AddLogInformation("read configuration file : end");
            ConfigReader.Close();
        }

        /// <summary>
        /// Set values of configuration items into the class
        /// </summary>
        /// <param name="parameter">Configuration items in configuration.txt</param>
        /// <param name="value">Value of configuration items</param>
        public void SetAttribute(string parameter, string value)
        {
            if (parameter.CompareTo("LOGFILE_NAME") == CompareTrue)
            {
                SetLogfileName(value);
            }
            else if (parameter.CompareTo("MINIMUM_NUMBER_OF_UNIQUE_WORDS") == CompareTrue)
            {
                try
                {
                    int number = int.Parse(value);
                    SetMinimumNumberOfUniqueWords(number);
                }
                catch
                {
                    parameterHasSetValue["MINIMUM_NUMBER_OF_UNIQUE_WORDS"] = AssignedFail;
                    Error.AddConfigurationError("MINIMUM_NUMBER_OF_UNIQUE_WORDS: value is not an integer");
                }

            }
            else if (parameter.CompareTo("MAXIMUM_NUMBER_OF_UNIQUE_WORDS") == CompareTrue)
            {
                try
                {
                    int number = int.Parse(value);
                    SetMaximumNumberOfUniqueWords(number);
                }
                catch
                {
                    parameterHasSetValue["MAXIMUM_NUMBER_OF_UNIQUE_WORDS"] = AssignedFail;
                    Error.AddConfigurationError("MAXIMUM_NUMBER_OF_UNIQUE_WORDS: value is not an integer");
                }
            }
            else if (parameter.CompareTo("INVALID_CROZZLE_SCORE") == CompareTrue)
            {
                SetInvalidCrozzleScore(value);
            }
            else if (parameter.CompareTo("UPPERCASE") == CompareTrue)
            {
                bool result;
                if (value.CompareTo("true") == CompareTrue)
                    result = true;
                else if (value.CompareTo("false") == CompareTrue)
                    result = false;
                else
                {
                    Error.AddConfigurationError("Uppercase: value is not boolean");
                    return;
                }
                SetUppercase(result);
            }
            else if (parameter.CompareTo("STYLE") == CompareTrue)
            {
                SetStyle(value);
            }
            else if (parameter.CompareTo("BGCOLOUR_EMPTY_TD") == CompareTrue)
            {
                SetBgcolourEmptyTD(value);
            }
            else if (parameter.CompareTo("BGCOLOUR_NON_EMPTY_TD") == CompareTrue)
            {
                SetBgcolourNonEmptyTD(value);
            }
            else if (parameter.CompareTo("MINIMUM_NUMBER_OF_ROWS") == CompareTrue)
            {
                try
                {
                    int number = int.Parse(value);
                    SetMinimumNumberOfRows(number);
                }
                catch
                {
                    parameterHasSetValue["MINIMUM_NUMBER_OF_ROWS"] = AssignedFail;
                    Error.AddConfigurationError("MINIMUM_NUMBER_OF_ROWS: value is not an integer");
                }
            }
            else if (parameter.CompareTo("MAXIMUM_NUMBER_OF_ROWS") == CompareTrue)
            {
                try
                {
                    int number = int.Parse(value);
                    SetMaximumNumberOfRows(number);
                }
                catch
                {
                    parameterHasSetValue["MAXIMUM_NUMBER_OF_ROWS"] = AssignedFail;
                    Error.AddConfigurationError("MAXIMUM_NUMBER_OF_ROWS: value is not an integer");
                }
            }
            else if (parameter.CompareTo("MINIMUM_NUMBER_OF_COLUMNS") == CompareTrue)
            {
                try
                {
                    int number = int.Parse(value);
                    SetMinimumNumberOfColumns(number);
                }
                catch
                {
                    parameterHasSetValue["MINIMUM_NUMBER_OF_COLUMNS"] = AssignedFail;
                    Error.AddConfigurationError("MINIMUM_NUMBER_OF_COLUMNS: value is not an integer");
                }
            }
            else if (parameter.CompareTo("MAXIMUM_NUMBER_OF_COLUMNS") == CompareTrue)
            {
                try
                {
                    int number = int.Parse(value);
                    SetMaximumNumberOfColumns(number);
                }
                catch
                {
                    parameterHasSetValue["MAXIMUM_NUMBER_OF_COLUMNS"] = AssignedFail;
                    Error.AddConfigurationError("MAXIMUM_NUMBER_OF_COLUMNS: value is not an integer");
                }
            }
            else if (parameter.CompareTo("MINIMUM_HORIZONTAL_WORDS") == CompareTrue)
            {
                try
                {
                    int number = int.Parse(value);
                    SetMinimumHorizontalWords(number);
                }
                catch
                {
                    parameterHasSetValue["MINIMUM_HORIZONTAL_WORDS"] = AssignedFail;
                    Error.AddConfigurationError("MINIMUM_HORIZONTAL_WORDS: value is not an integer");
                }
            }
            else if (parameter.CompareTo("MAXIMUM_HORIZONTAL_WORDS") == CompareTrue)
            {
                try
                {
                    int number = int.Parse(value);
                    SetMaximumHorizontalWords(number);
                }
                catch
                {
                    parameterHasSetValue["MAXIMUM_HORIZONTAL_WORDS"] = AssignedFail;
                    Error.AddConfigurationError("MAXIMUM_HORIZONTAL_WORDS: value is not an integer");
                }
            }
            else if (parameter.CompareTo("MINIMUM_VERTICAL_WORDS") == CompareTrue)
            {
                try
                {
                    int number = int.Parse(value);
                    SetMinimumVerticalWords(number);
                }
                catch
                {
                    parameterHasSetValue["MINIMUM_VERTICAL_WORDS"] = AssignedFail;
                    Error.AddConfigurationError("MINIMUM_VERTICAL_WORDS: value is not an integer");
                }
            }
            else if (parameter.CompareTo("MAXIMUM_VERTICAL_WORDS") == CompareTrue)
            {
                try
                {
                    int number = int.Parse(value);
                    SetMaximumVerticalWords(number);
                }
                catch
                {
                    parameterHasSetValue["MAXIMUM_VERTICAL_WORDS"] = AssignedFail;
                    Error.AddConfigurationError("MAXIMUM_VERTICAL_WORDS: value is not an integer");
                }
            }
            else if (parameter.CompareTo("MINIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS") == CompareTrue)
            {
                try
                {
                    int number = int.Parse(value);
                    SetMinimumIntersectionsInHorizontalWords(number);
                }
                catch
                {
                    parameterHasSetValue["MINIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS"] = AssignedFail;
                    Error.AddConfigurationError("MINIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS: value is not an integer");
                }
            }
            else if (parameter.CompareTo("MAXIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS") == CompareTrue)
            {
                try
                {
                    int number = int.Parse(value);
                    SetMaximumIntersectionsInHorizontalWords(number);
                }
                catch
                {
                    parameterHasSetValue["MAXIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS"] = AssignedFail;
                    Error.AddConfigurationError("MAXIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS: value is not an integer");
                }
            }
            else if (parameter.CompareTo("MINIMUM_INTERSECTIONS_IN_VERTICAL_WORDS") == CompareTrue)
            {
                try
                {
                    int number = int.Parse(value);
                    SetMinimumIntersectionsInVerticalWords(number);
                }
                catch
                {
                    parameterHasSetValue["MINIMUM_INTERSECTIONS_IN_VERTICAL_WORDS"] = AssignedFail;
                    Error.AddConfigurationError("MINIMUM_INTERSECTIONS_IN_VERTICAL_WORDS: value is not an integer");
                }
            }
            else if (parameter.CompareTo("MAXIMUM_INTERSECTIONS_IN_VERTICAL_WORDS") == CompareTrue)
            {
                try
                {
                    int number = int.Parse(value);
                    SetMaximumIntersectionsInVerticalWords(number);
                }
                catch
                {
                    parameterHasSetValue["MAXIMUM_INTERSECTIONS_IN_VERTICAL_WORDS"] = AssignedFail;
                    Error.AddConfigurationError("MAXIMUM_INTERSECTIONS_IN_VERTICAL_WORDS: value is not an integer");
                }
            }
            else if (parameter.CompareTo("MINIMUM_NUMBER_OF_THE_SAME_WORD") == CompareTrue)
            {
                try
                {
                    int number = int.Parse(value);
                    SetMinimumNumberOfTheSameWord(number);
                }
                catch
                {
                    parameterHasSetValue["MINIMUM_NUMBER_OF_THE_SAME_WORD"] = AssignedFail;
                    Error.AddConfigurationError("MINIMUM_NUMBER_OF_THE_SAME_WORD: value is not an integer");
                }
            }
            else if (parameter.CompareTo("MAXIMUM_NUMBER_OF_THE_SAME_WORD") == CompareTrue)
            {
                try
                {
                    int number = int.Parse(value);
                    SetMaximumNumberOfTheSameWord(number);
                }
                catch
                {
                    parameterHasSetValue["MAXIMUM_NUMBER_OF_THE_SAME_WORD"] = AssignedFail;
                    Error.AddConfigurationError("MAXIMUM_NUMBER_OF_THE_SAME_WORD: value is not an integer");
                }
            }
            else if (parameter.CompareTo("MINIMUM_NUMBER_OF_GROUPS") == CompareTrue)
            {
                try
                {
                    int number = int.Parse(value);
                    SetMinimumNumberOfGroups(number);
                }
                catch
                {
                    parameterHasSetValue["MINIMUM_NUMBER_OF_GROUPS"] = AssignedFail;
                    Error.AddConfigurationError("MINIMUM_NUMBER_OF_GROUPS: value is not an integer");
                }
            }
            else if (parameter.CompareTo("MAXIMUM_NUMBER_OF_GROUPS") == CompareTrue)
            {
                try
                {
                    int number = int.Parse(value);
                    SetMaximumNumberOfGroups(number);
                }
                catch
                {
                    parameterHasSetValue["MAXIMUM_NUMBER_OF_GROUPS"] = AssignedFail;
                    Error.AddConfigurationError("MAXIMUM_NUMBER_OF_GROUPS: value is not an integer");
                }
            }
            else if (parameter.CompareTo("POINTS_PER_WORD") == CompareTrue)
            {
                try
                {
                    int number = int.Parse(value);
                    SetPointsPerWord(number);
                }
                catch
                {
                    parameterHasSetValue["POINTS_PER_WORD"] = AssignedFail;
                    Error.AddConfigurationError("POINTS_PER_WORD: value is not an integer");
                }
            }
            else if (parameter.CompareTo("INTERSECTING_POINTS_PER_LETTER") == CompareTrue)
            {
                SetIntersectingPointsPerLetter(value);
            }
            else if (parameter.CompareTo("NON_INTERSECTING_POINTS_PER_LETTER") == CompareTrue)
            {
                SetNonIntersectingPointsPerLetter(value);
            }
        }

        private void CheckNegative()
        {
            if (this.GetMaximumHorizontalWords() < ValueZero)
                Error.AddConfigurationError("MAXIMUM_HORIZONTAL_WORDS value < 0");
            if (this.GetMinimumHorizontalWords() < ValueZero)
                Error.AddConfigurationError("MINIMUM_HORIZONTAL_WORDS value < 0");
            if (this.GetMaximumVerticalWords() < ValueZero)
                Error.AddConfigurationError("MAXIMUM_VERTICAL_WORDS value < 0");
            if (this.GetMinimumVerticalWords() < ValueZero)
                Error.AddConfigurationError("MINIMUM_VERTICAL_WORDS value < 0");
            if (this.GetMaximumIntersectionsInHorizontalWords() < ValueZero)
                Error.AddConfigurationError("MAXIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS value < 0");
            if (this.GetMinimumIntersectionsInHorizontalWords() < ValueZero)
                Error.AddConfigurationError("MINIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS value < 0");
            if (this.GetMaximumIntersectionsInVerticalWords() < ValueZero)
                Error.AddConfigurationError("MAXIMUM_INTERSECTIONS_IN_VERTICAL_WORDS value < 0");
            if (this.GetMinimumIntersectionsInVerticalWords() < ValueZero)
                Error.AddConfigurationError("MINIMUM_INTERSECTIONS_IN_VERTICAL_WORDS value < 0");
            if (this.GetMinimumNumberOfUniqueWords() < ValueZero)
                Error.AddConfigurationError("MINIMUM_NUMBER_OF_UNIQUE_WORDS value < 0");
            if (this.GetMaximumNumberOfUniqueWords() < ValueZero)
                Error.AddConfigurationError("MAXIMUM_NUMBER_OF_UNIQUE_WORDS value < 0");
            if (this.GetMinimumNumberOfColumns() < ValueZero)
                Error.AddConfigurationError("MINIMUM_NUMBER_OF_COLUMNS value < 0");
            if (this.GetMaximumNumberOfColumns() < ValueZero)
                Error.AddConfigurationError("MAXIMUM_NUMBER_OF_COLUMNS value < 0");
            if (this.GetMinimumNumberOfRows() < ValueZero)
                Error.AddConfigurationError("MINIMUM_NUMBER_OF_ROWS value < 0");
            if (this.GetMaximumNumberOfRows() < ValueZero)
                Error.AddConfigurationError("MAXIMUM_NUMBER_OF_ROWS value < 0");
            if (this.GetMaximumNumberOfGroups() < ValueZero)
                Error.AddConfigurationError("MINIMUM_NUMBER_OF_GROUPS value < 0");
            if (this.GetMinimumNumberOfGroups() < ValueZero)
                Error.AddConfigurationError("MINIMUM_NUMBER_OF_GROUPS value < 0");
            if (this.GetMinimumNumberOfTheSameWord() < ValueZero)
                Error.AddConfigurationError("MINIMUM_NUMBER_OF_THE_SAME_WORD value < 0");
            if (this.GetMaximumNumberOfTheSameWord() < ValueZero)
                Error.AddConfigurationError("MAXIMUM_NUMBER_OF_THE_SAME_WORD value < 0");

            if (this.GetPointsPerWord() < 0)
                Error.AddConfigurationError("POINTS_PER_WORD value < 0");
        }

        private void CheckAssignment()
        {
            if (parameterHasSetValue["MAXIMUM_HORIZONTAL_WORDS"] == AssignSuccess && parameterHasSetValue["MINIMUM_HORIZONTAL_WORDS"] == AssignSuccess && this.GetMaximumHorizontalWords() < this.GetMinimumHorizontalWords())
                Error.AddConfigurationError("horizontal words: maximum value < minimum value");
            else
            {
                if (parameterHasSetValue["MAXIMUM_HORIZONTAL_WORDS"] == NotAssigned)
                    Error.AddConfigurationError("MAXIMUM_HORIZONTAL_WORDS: keyword missing");
                if (parameterHasSetValue["MINIMUM_HORIZONTAL_WORDS"] == NotAssigned)
                    Error.AddConfigurationError("MINIMUM_HORIZONTAL_WORDS: keyword missing");
            }
            if (parameterHasSetValue["MAXIMUM_VERTICAL_WORDS"] == AssignSuccess && parameterHasSetValue["MINIMUM_VERTICAL_WORDS"] == AssignSuccess && this.GetMaximumVerticalWords() < this.GetMinimumVerticalWords())
                Error.AddConfigurationError("vertical words: maximum value < minimum value");
            else
            {
                if (parameterHasSetValue["MAXIMUM_VERTICAL_WORDS"] == NotAssigned)
                    Error.AddConfigurationError("MAXIMUM_VERTICAL_WORDS: keyword missing");
                if (parameterHasSetValue["MINIMUM_VERTICAL_WORDS"] == NotAssigned)
                    Error.AddConfigurationError("MINIMUM_VERTICAL_WORDS: keyword missing");
            }
            if (parameterHasSetValue["MAXIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS"] == AssignSuccess && parameterHasSetValue["MINIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS"] == AssignSuccess && this.GetMaximumIntersectionsInHorizontalWords() < this.GetMinimumIntersectionsInHorizontalWords())
                Error.AddConfigurationError("intersections in horizontal words: maximum value < minimum value");
            else
            {
                if (parameterHasSetValue["MAXIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS"] == NotAssigned)
                    Error.AddConfigurationError("MAXIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS: keyword missing");
                if (parameterHasSetValue["MINIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS"] == NotAssigned)
                    Error.AddConfigurationError("MINIMUM_INTERSECTIONS_IN_HORIZONTAL_WORDS: keyword missing");
            }
            if (parameterHasSetValue["MAXIMUM_INTERSECTIONS_IN_VERTICAL_WORDS"] == AssignSuccess && parameterHasSetValue["MINIMUM_INTERSECTIONS_IN_VERTICAL_WORDS"] == AssignSuccess && this.GetMaximumIntersectionsInVerticalWords() < this.GetMinimumIntersectionsInVerticalWords())
                Error.AddConfigurationError("intersections in vertical words: maximum value < minimum value");
            else
            {
                if (parameterHasSetValue["MAXIMUM_INTERSECTIONS_IN_VERTICAL_WORDS"] == NotAssigned)
                    Error.AddConfigurationError("MAXIMUM_INTERSECTIONS_IN_VERTICAL_WORDS: keyword missing");
                if (parameterHasSetValue["MINIMUM_INTERSECTIONS_IN_VERTICAL_WORDS"] == NotAssigned)
                    Error.AddConfigurationError("MINIMUM_INTERSECTIONS_IN_VERTICAL_WORDS: keyword missing");
            }
            if (parameterHasSetValue["MAXIMUM_NUMBER_OF_UNIQUE_WORDS"] == AssignSuccess && parameterHasSetValue["MINIMUM_NUMBER_OF_UNIQUE_WORDS"] == AssignSuccess && this.GetMaximumNumberOfUniqueWords() < this.GetMinimumNumberOfUniqueWords())
                Error.AddConfigurationError("number of unique words: maximum value < minimum value");
            else
            {
                if (parameterHasSetValue["MAXIMUM_NUMBER_OF_UNIQUE_WORDS"] == NotAssigned)
                    Error.AddConfigurationError("MAXIMUM_NUMBER_OF_UNIQUE_WORDS: keyword missing");
                if (parameterHasSetValue["MINIMUM_NUMBER_OF_UNIQUE_WORDS"] == NotAssigned)
                    Error.AddConfigurationError("MINIMUM_NUMBER_OF_UNIQUE_WORDS: keyword missing");
            }
            if (parameterHasSetValue["MAXIMUM_NUMBER_OF_COLUMNS"] == AssignSuccess && parameterHasSetValue["MINIMUM_NUMBER_OF_COLUMNS"] == AssignSuccess && this.GetMaximumNumberOfColumns() < this.GetMinimumNumberOfColumns())
                Error.AddConfigurationError("number of columns: maximum value < minimum value");
            else
            {
                if (parameterHasSetValue["MAXIMUM_NUMBER_OF_COLUMNS"] == NotAssigned)
                    Error.AddConfigurationError("MAXIMUM_NUMBER_OF_COLUMNS: keyword missing");
                if (parameterHasSetValue["MINIMUM_NUMBER_OF_COLUMNS"] == NotAssigned)
                    Error.AddConfigurationError("MINIMUM_NUMBER_OF_COLUMNS: keyword missing");
            }
            if (parameterHasSetValue["MAXIMUM_NUMBER_OF_ROWS"] == AssignSuccess && parameterHasSetValue["MINIMUM_NUMBER_OF_ROWS"] == AssignSuccess && this.GetMaximumNumberOfRows() < this.GetMinimumNumberOfRows())
                Error.AddConfigurationError("number of rows: maximum value < minimum value");
            else
            {
                if (parameterHasSetValue["MAXIMUM_NUMBER_OF_ROWS"] == NotAssigned)
                    Error.AddConfigurationError("MAXIMUM_NUMBER_OF_ROWS: keyword missing");
                if (parameterHasSetValue["MINIMUM_NUMBER_OF_ROWS"] == NotAssigned)
                    Error.AddConfigurationError("MINIMUM_NUMBER_OF_ROWS: keyword missing");
            }
            if (parameterHasSetValue["MAXIMUM_NUMBER_OF_GROUPS"] == AssignSuccess && parameterHasSetValue["MINIMUM_NUMBER_OF_GROUPS"] == AssignSuccess && this.GetMaximumNumberOfGroups() < this.GetMinimumNumberOfGroups())
                Error.AddConfigurationError("number of groups: maximum value < minimum value");
            else
            {
                if (parameterHasSetValue["MAXIMUM_NUMBER_OF_GROUPS"] == NotAssigned)
                    Error.AddConfigurationError("MAXIMUM_NUMBER_OF_GROUPS: keyword missing");
                if (parameterHasSetValue["MINIMUM_NUMBER_OF_GROUPS"] == NotAssigned)
                    Error.AddConfigurationError("MINIMUM_NUMBER_OF_GROUPS: keyword missing");
            }
            if (parameterHasSetValue["MAXIMUM_NUMBER_OF_THE_SAME_WORD"] == AssignSuccess && parameterHasSetValue["MINIMUM_NUMBER_OF_THE_SAME_WORD"] == AssignSuccess && this.GetMaximumNumberOfTheSameWord() < this.GetMinimumNumberOfTheSameWord())
                Error.AddConfigurationError("number of the same word: maximum value < minimum value");
            else
            {
                if (parameterHasSetValue["MAXIMUM_NUMBER_OF_THE_SAME_WORD"] == NotAssigned)
                    Error.AddConfigurationError("MAXIMUM_NUMBER_OF_THE_SAME_WORD: keyword missing");
                if (parameterHasSetValue["MINIMUM_NUMBER_OF_THE_SAME_WORD"] == NotAssigned)
                    Error.AddConfigurationError("MINIMUM_NUMBER_OF_THE_SAME_WORD: keyword missing");
            }
        }

        private void CheckColor()
        {
            string colourEmpty = this.GetBgcolourEmptyTD();
            if (colourEmpty.Length != colourLength)
                Error.AddConfigurationError("BGCOLOUR_EMPTY: not colour code");
            else
            {
                if (colourEmpty[0] != SharpSymbol)
                    Error.AddConfigurationError("BGCOLOUR_EMPTY: not colour code");
                else
                {
                    if (!Regex.IsMatch(colourEmpty, @"^#(\d|[A-Fa-f]){6,6}$"))
                        Error.AddConfigurationError("BGCOLOUR_EMPTY: not colour code");
                }
            }

            string colourNonEmpty = this.GetBgcolourNonEmptyTD();
            if (colourNonEmpty.Length != colourLength)
                Error.AddConfigurationError("BGCOLOUR_NON_EMPTY: not colour code");
            else
            {
                if (colourNonEmpty[0] != SharpSymbol)
                    Error.AddConfigurationError("BGCOLOUR_NON_EMPTY: not colour code");
                else
                {
                    if (!Regex.IsMatch(colourNonEmpty, @"^#(\d|[A-Fa-f]){6,6}$"))
                        Error.AddConfigurationError("BGCOLOUR_NON_EMPTY: not colour code");
                }
            }
        }

        private void CheckIntersectingPointsPerLetter()
        {
            char[] separator = { CommaSymbol };
            string[] intersectionPerLetter = this.GetIntersectingPointsPerLetter().Split(separator);
            for (int index = 0; index < intersectionPerLetter.Length; index++)
            {
                if (!Regex.IsMatch(intersectionPerLetter[index], @"^[A-Z]=\d+$"))
                {
                    Error.AddConfigurationError(intersectionPerLetter[index] + ": false assigning expression");
                }
            }
        }

        private void CheckNonIntersectingPointsPerLetter()
        {
            char[] separator = { CommaSymbol };
            string[] nonIntersectionPerLetter = this.GetNonIntersectingPointsPerLetter().Split(separator);
            for (int index = 0; index < nonIntersectionPerLetter.Length; index++)
            {
                if (!Regex.IsMatch(nonIntersectionPerLetter[index], @"^[A-Z]=\d+$"))
                {
                    Error.AddConfigurationError(nonIntersectionPerLetter[index] + ": false assigning expression");
                }
            }
        }

        /// <summary>
        /// Validate configuration.txt
        /// </summary>
        public void ValidateConfig()
        {
            Log.AddLogInformation("configuration validation : begin");
            CheckNegative();
            CheckAssignment();
            CheckColor();
            CheckIntersectingPointsPerLetter();
            CheckNonIntersectingPointsPerLetter();
            Log.AddLogInformation("configuration validation : end");

        }


    }
}
