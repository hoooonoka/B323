using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIT323Crozzle
{
    /// <summary>
    /// Class store information about word used in crozzle
    /// </summary>
    public class Word
    {
        private int rows;
        private int columns;
        private string wordContent;
        private string type;
        private int addScore;

        /// <summary>
        /// Constructor of Word
        /// </summary>
        public Word()
        { }

        /// <summary>
        /// Constructor of Word
        /// An overload methods setting row, column, type and word content
        /// </summary>
        /// <param name="row">Number of row of beginning letter</param>
        /// <param name="column">Number of column of beginning letter</param>
        /// <param name="type">String specify if it is horizontal or vertical</param>
        /// <param name="wordContent">String contains word content</param>
        public Word(int row,int column,string type,string wordContent)
        {
            this.rows = row;
            this.columns = column;
            this.wordContent = wordContent;
            this.type = type;
        }

        /// <summary>
        /// Set number of row of beginning letter
        /// </summary>
        /// <param name="row">Number of row of beginning letter</param>
        public void SetRows(int row)
        {
            this.rows = row;
        }

        /// <summary>
        /// Get number of row of beginning letter
        /// </summary>
        /// <returns>Number of row of beginning letter</returns>
        public int GetRows()
        {
            return this.rows;
        }

        /// <summary>
        /// Set number of column of beginning letter
        /// </summary>
        /// <param name="column">Number of column of beginning letter</param>
        public void SetColumns(int column)
        {
            this.columns = column;
        }

        /// <summary>
        /// Get number of column of beginning letter
        /// </summary>
        /// <returns>Number of column of beginning letter</returns>
        public int GetColumns()
        {
            return this.columns;
        }

        /// <summary>
        /// Set word content
        /// </summary>
        /// <param name="content">String contains word content</param>
        public void SetWordContent(string content)
        {
            this.wordContent = content;
        }

        /// <summary>
        /// Get word content
        /// </summary>
        /// <returns>String contains word content</returns>
        public string GetWordContent()
        {
            return this.wordContent;
        }

        /// <summary>
        /// Set type for word
        /// </summary>
        /// <param name="type">String specify if it is horizontal or vertical</param>
        public void SetType(string type)
        {
            this.type = type;
        }

        /// <summary>
        /// Get type for word
        /// </summary>
        /// <returns>String specify if it is horizontal or vertical</returns>
        public new string GetType()
        {
            return this.type;
        }

        public void SetAddScore(int score)
        {
            this.addScore = score;
        }

        public int GetAddScore()
        {
            return this.addScore;
        }
    }
}
