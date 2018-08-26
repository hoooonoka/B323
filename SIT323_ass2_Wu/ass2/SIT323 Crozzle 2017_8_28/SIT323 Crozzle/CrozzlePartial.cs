using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIT323Crozzle
{
    class CrozzlePartial
    {
        private Grid grid;
        private List<Word> usedWord = new List<Word>();
        private List<string> wordlist = new List<string>();
        private int maxWidth;
        private int maxHeight;
        private int minWidth;
        private int minHeight;
        private int width;
        private int height;
        private int score;

        public void SetGrid(Grid g)
        {
            this.grid = g;
        }
        public Grid GetGrid()
        {
            return this.grid;
        }
        public void SetUsedWord(List<Word> uw)
        {
            this.usedWord = uw;
        }
        public List<Word> GetUsedWord()
        {
            return this.usedWord;
        }
        public void SetWordlist(List<string> wl)
        {
            this.wordlist = wl;
        }
        public List<string> GetWordlist()
        {
            return this.wordlist;
        }
        public void SetWidth(int w)
        {
            this.width = w;
        }
        public int GetWidth()
        {
            return this.width;
        }
        public void SetHeight(int h)
        {
            this.height = h;
        }
        public int GetHeight()
        {
            return this.height;
        }
        public void SetScore(int s)
        {
            this.score = s;
        }
        public int GetScore()
        {
            return this.score;
        }
        public void SetMaxHeight(int h)
        {
            this.maxHeight = h;
        }
        public int GetMaxHeight()
        {
            return this.maxHeight;
        }
        public void SetMaxWidth(int w)
        {
            this.maxWidth = w;
        }
        public int GetMaxWidth()
        {
            return this.maxWidth;
        }
        public void SetMinWidth(int w)
        {
            this.minWidth = w;
        }
        public int GetMinWidth()
        {
            return this.minWidth;
        }
        public void SetMinHeight(int h)
        {
            this.minHeight = h;
        }
        public int GetMinHeight()
        {
            return this.minHeight;
        }
    }
}
