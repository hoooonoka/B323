using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace SIT323Crozzle
{
    class CrozzleGenerateFile
    {
        const int CantFind = -1;
        const char EqualSymbol = '=';
        const char SpaceSymbol = ' ';
        const char SlashSymbol = '/';
        const char QuoteSymbol = '"';
        private int rows;
        private int columns;
        private string configurationFile;
        private string wordListFile;
        private Dictionary<string, bool> pathHaveSet = new Dictionary<string, bool>();

        /// <summary>
        /// Set path of configuration.txt in the class
        /// </summary>
        /// <param name="configuration">String contains path of configuration.txt</param>
        public void SetConfigurationFile(string configuration)
        {
            this.configurationFile = configuration;
        }

        /// <summary>
        /// Return path of configuration.txt
        /// </summary>
        /// <returns>String contains path of configuration.txt</returns>
        public string GetConfigurationFile()
        {
            return this.configurationFile;
        }

        /// <summary>
        /// Set path of wordlist.txt in the class
        /// </summary>
        /// <param name="configuration">String contains path of wordlist.txt</param>
        public void SetWordListFile(string wordList)
        {
            this.wordListFile = wordList;
        }

        /// <summary>
        /// Return path of wordlist.txt
        /// </summary>
        /// <returns>String contains path of wordlist.txt</returns>
        public string GetWordListFile()
        {
            return this.wordListFile;
        }

        /// <summary>
        /// Initialize dictionary contains file path assigning information
        /// </summary>
        public void InitializePathHaveSet()
        {
            pathHaveSet["CONFIGURATION_FILE"] = false;
            pathHaveSet["WORDLIST_FILE"] = false;
        }

        /// <summary>
        /// Return dictionary contains file path assigning information
        /// </summary>
        /// <param name="file">File name (wordlist.txt or configuration.txt)</param>
        /// <returns>True if path is set, false if path has not set</returns>
        public bool GetPathHaveSet(string file)
        {
            return this.pathHaveSet[file];
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
        /// Read crozzle.txt and get path of configuration.txt and wordlist.txt, then stored in the class
        /// </summary>
        /// <param name="path">String contains path of crozzle.txt</param>
        public void SetBoot(string path)
        {
            InitializePathHaveSet();
            Log.AddLogInformation("read crozzle file " + path + " : begin");
            StreamReader crozzleFileReader = new StreamReader(path, Encoding.Default);
            String line;
            while ((line = crozzleFileReader.ReadLine()) != null)
            {
                if (line.IndexOf(EqualSymbol) != CantFind)
                {
                    if ((line.IndexOf("CONFIGURATION_FILE") != CantFind) || (line.IndexOf("WORDLIST_FILE") != CantFind))
                    {
                        int equalPosition = line.IndexOf(EqualSymbol);
                        int length = line.Length;
                        string parameter = line.Substring(0, equalPosition);
                        string value = line.Substring(equalPosition + 1, length - equalPosition - 1);

                        char[] trimcase = { SpaceSymbol };
                        parameter = parameter.Trim(trimcase);
                        value = value.Trim(trimcase);

                        //for (int charIndexInValue = 0; charIndexInValue < value.Length; charIndexInValue++)
                        //{
                        //    if (charIndexInValue != value.Length - 1)
                        //    {
                        //        if (value[charIndexInValue] == SlashSymbol && value[charIndexInValue + 1] != SlashSymbol)
                        //        {
                        //            value = value.Substring(0, charIndexInValue - 1);
                        //            value = value.Trim(trimcase);
                        //            break;
                        //        }
                        //    }
                        //}

                        int valueLength = value.Length;
                        if (value[0] == QuoteSymbol && value[valueLength - 1] == QuoteSymbol)
                            value = value.Substring(1, valueLength - 2);

                        if (parameter.CompareTo("CONFIGURATION_FILE") == 0)
                        {
                            SetConfigurationFile(value);
                            pathHaveSet["CONFIGURATION_FILE"] = true;

                        }
                        else if (parameter.CompareTo("WORDLIST_FILE") == 0)
                        {
                            SetWordListFile(value);
                            pathHaveSet["WORDLIST_FILE"] = true;
                        }
                    }
                }
            }
            Log.AddLogInformation("read crozzle file: end");
        }
    }
}
