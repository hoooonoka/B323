using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIT323Crozzle
{
    /// <summary>
    /// Grid stores all words used in crozzle
    /// </summary>
    public class Grid
    {
        private int rows;
        private int columns;
        private char[,] grid;

        public Grid() { }

        /// <summary>
        /// Constructor of Grid class
        /// </summary>
        /// <param name="rows">Number of rows</param>
        /// <param name="columns">Number of columns</param>
        /// <param name="wordList">List of words</param>
        public Grid(int rows, int columns, List<Word> wordList)
        {
            this.rows = rows;
            this.columns = columns;
            this.grid = new char[rows, columns];
            for (int i = 0; i < wordList.Count(); i++)
            {
                Word word = wordList[i];
                if (word.GetType().CompareTo("ROW") == 0)
                {
                    int length = word.GetWordContent().Length;
                    for (int j = 0; j < length; j++)
                    {
                        try
                        {
                            grid[word.GetRows() - 1, word.GetColumns() - 1 + j] = word.GetWordContent()[j];
                        }
                        catch
                        {
                            break;
                        }
                    }
                }
                else if (word.GetType().CompareTo("COLUMN") == 0)
                {
                    int length = word.GetWordContent().Length;
                    for (int j = 0; j < length; j++)
                    {
                        try
                        {
                            grid[word.GetRows() - 1 + j, word.GetColumns() - 1] = word.GetWordContent()[j];
                        }
                        catch
                        {
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Return Grid
        /// </summary>
        /// <returns>A grid with data type: char[,]</returns>
        public char[,] GetGrid()
        {
            return this.grid;
        }

        public void SetGrid(char[,] g)
        {
            this.grid = g;
        }


    }
}
