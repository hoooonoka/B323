using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIT323Crozzle
{
    static class PublicInfo
    {
        private static int highScoreLetter=16;
        private static int rows;
        private static int columns;
        private static int fullrows;
        private static int fullcolumns;
        private static int minGroups;
        private static int maxGroups;
        private static int pointPerWord;
        private static string wordlistUrl;
        private static string configurationUrl;
        public static int ContainHighValueLetter(string s)
        {
            int n = 0;
            
            for (int i = 0; i < s.Length; i++)
            {
                char temp = s[i];
                if (WordInfo.intersectingPointsPerLetter[temp] >= highScoreLetter || WordInfo.nonIntersectingPointsPerLetter[temp] >= highScoreLetter)
                    n++;
                    
            }
            return n;
        }
        public static void SetRows(int r)
        {
            rows = r;
        }
        public static int GetRows()
        {
            return rows;
        }
        public static void SetColumns(int r)
        {
            columns = r;
        }
        public static int GetColumns()
        {
            return columns;
        }
        public static void SetFullRows(int r)
        {
            fullrows = r;
        }
        public static int GetFullRows()
        {
            return fullrows;
        }
        public static void SetFullColumns(int r)
        {
            fullcolumns = r;
        }
        public static int GetFullColumns()
        {
            return fullcolumns;
        }
        public static void SetMinGroups(int r)
        {
            minGroups = r;
        }
        public static int GetMinGroups()
        {
            return minGroups;
        }
        public static void SetMaxGroups(int r)
        {
            maxGroups = r;
        }
        public static int GetMaxGroups()
        {
            return maxGroups;
        }
        public static void SetPointsPerWord(int r)
        {
            pointPerWord = r;
        }
        public static int GetPointsPerWord()
        {
            return pointPerWord;
        }
        public static void SetWordlist(string url)
        {
            wordlistUrl = url;
        }
        public static void SetConfig(string url)
        {
            configurationUrl = url;
        }
        public static string GetWordlist()
        {
            return wordlistUrl;
        }
        public static string GetConfig()
        {
            return configurationUrl;
        }

    }
}
