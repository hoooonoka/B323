using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIT323Crozzle
{
    /// <summary>
    /// Static class store all error messages
    /// </summary>
    static class Error
    {
        private static List<string> configurationError = new List<string>();
        private static List<string> crozzleFileError = new List<string>();
        private static List<string> wordListError = new List<string>();
        private static List<string> crozzleError = new List<string>();

        /// <summary>
        /// Remove all error messages from lists
        /// </summary>
        public static void InitializeErrorList()
        {
            configurationError.RemoveRange(0,configurationError.Count());
            crozzleFileError.RemoveRange(0, crozzleFileError.Count());
            wordListError.RemoveRange(0, wordListError.Count());
            crozzleError.RemoveRange(0, crozzleError.Count());
        }

        /// <summary>
        /// Add an error message in configuration error list
        /// </summary>
        /// <param name="error">String contains error information</param>
        public static void AddConfigurationError(string error)
        {
            configurationError.Add(error);
        }

        /// <summary>
        /// Add an error message in crozzle file error list
        /// </summary>
        /// <param name="error">String contains error information</param>
        public static void AddCrozzleFileError(string error)
        {
            crozzleFileError.Add(error);
        }

        /// <summary>
        /// Add an error message in word list error list
        /// </summary>
        /// <param name="error">String contains error information</param>
        public static void AddWordListError(string error)
        {
            wordListError.Add(error);
        }

        /// <summary>
        /// Add an error message in crozzle error list
        /// </summary>
        /// <param name="error">String contains error information</param>
        public static void AddCrozzleError(string error)
        {
            crozzleError.Add(error);
        }

        /// <summary>
        /// Get all configuration errors
        /// </summary>
        /// <returns>List of all configuration errors</returns>
        public static List<string> GetConfigurationError()
        {
            return configurationError;
        }

        /// <summary>
        /// Get all crozzle file errors
        /// </summary>
        /// <returns>List of all crozzle file errors</returns>
        public static List<string> GetCrozzleFileError()
        {
            return crozzleFileError;
        }

        /// <summary>
        /// Get all word list errors
        /// </summary>
        /// <returns>List of all word list errors</returns>
        public static List<string> GetWordListError()
        {
            return wordListError;
        }

        /// <summary>
        /// Get all crozzle errors
        /// </summary>
        /// <returns>List of all crozzle errors</returns>
        public static List<string> GetCrozzleError()
        {
            return crozzleError;
        }
    }
}
