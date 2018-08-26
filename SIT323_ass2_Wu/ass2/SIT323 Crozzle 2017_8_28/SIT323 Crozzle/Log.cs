using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIT323Crozzle
{
    /// <summary>
    /// Class store Log file information
    /// </summary>
    static class Log
    {
        // List stores Log file information
        private static List<string> logInformation = new List<string>();

        /// <summary>
        /// Add a new log file information into the list of log information
        /// </summary>
        /// <param name="information">A string stroe log information</param>
        public static void AddLogInformation(string information)
        {
            logInformation.Add(information);
        }

        /// <summary>
        /// return the list of log information
        /// </summary>
        /// <returns>list of log information</returns>
        public static List<string> GetLogInformation()
        {
            return logInformation;
        }

        /// <summary>
        /// Remove all information stored in the log information list
        /// </summary>
        public static void RemoveLogInformation()
        {
            logInformation.RemoveRange(0, logInformation.Count());
        }
    }
}
