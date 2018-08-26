using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace SIT323Crozzle
{
    /// <summary>
    /// Class store path of wordlist.txt and configuration.txt
    /// Read crozzle.txt and get path of wordlist.txt and configuration.txt, then stored it in the class
    /// </summary>
    public class Boot
    {
        const int CantFind = -1;
        const char EqualSymbol = '=';
        const char SpaceSymbol = ' ';
        const char SlashSymbol = '/';
        const char QuoteSymbol = '"';
        private string configurationFile;
        private string wordListFile;
        private string rootPath;
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
        /// Set root path, in order to convert relative path into absolute path later
        /// </summary>
        /// <param name="path">String contains path of crozzle.txt</param>
        public void SetRootPath(string path)
        {
            int position = 0;
            for(int index=path.Length-1;index>=0; index--)
            {
                if (path[index] == '\\')
                {
                    position = index;
                    break;
                }
            }

            rootPath = path.Substring(0, position+1);

        }

        /// <summary>
        /// Get root path, in order to convert relative path into absolute path later
        /// </summary>
        /// <returns>String contains root path</returns>
        public string GetRootPath()
        {
            return rootPath;
        }

        /// <summary>
        /// Convert relative path into absolute path
        /// </summary>
        /// <param name="path">String contains relative path</param>
        /// <returns>String contains absolute path</returns>
        public string ChangePath(string path)
        {
            if (path.IndexOf(@":\")!= CantFind)
                return path;
            else
            {
                string returnPath=path;
                if (path[0].Equals('.') && path[1].Equals('\\'))
                    returnPath = path.Substring(2, path.Length - 2);
                returnPath = rootPath + returnPath;
                return returnPath;

            }
        }

        /// <summary>
        /// Read crozzle.txt and get path of configuration.txt and wordlist.txt, then stored in the class
        /// </summary>
        /// <param name="path">String contains path of crozzle.txt</param>
        public void SetBoot(string path)
        {
            InitializePathHaveSet();
            SetRootPath(path);
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

                        int valueLength = value.Length;
                        if (value[0] == QuoteSymbol && value[valueLength - 1] == QuoteSymbol)
                            value = value.Substring(1, valueLength - 2);

                        if (parameter.CompareTo("CONFIGURATION_FILE") == 0)
                        {
                            value = ChangePath(value);
                            SetConfigurationFile(value);
                            pathHaveSet["CONFIGURATION_FILE"] = true;

                        }
                        else if (parameter.CompareTo("WORDLIST_FILE") == 0)
                        {
                            value = ChangePath(value);
                            SetWordListFile(value);
                            pathHaveSet["WORDLIST_FILE"] = true;
                        }
                    }
                }
            }
            Log.AddLogInformation("read crozzle file: end");
            crozzleFileReader.Close();
        }
    }
}
