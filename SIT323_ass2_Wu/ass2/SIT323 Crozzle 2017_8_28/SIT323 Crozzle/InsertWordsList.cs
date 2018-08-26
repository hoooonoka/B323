using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIT323Crozzle
{
    class InsertWordsList
    {
        private List<Word> wordList;
        private int score;

        public InsertWordsList(List<Word> wordList, int score)
        {
            this.wordList = wordList;
            this.score = score;
        }

        public List<Word> GetWordList()
        {
            return this.wordList;
        }

        public int GetScore()
        {
            return this.score;
        }
    }
}
