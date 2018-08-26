using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIT323_Crozzle.ServiceReferenceGroup;


namespace SIT323Crozzle
{
    static class CreateCrozzle
    {
        private static Dictionary<string, int> PotentialValue = new Dictionary<string, int>();
        private static CrozzlePartial HighScoreCrozzle = new CrozzlePartial();
        private static int averageScorePerWord = 0;
        private static int highScore = 0;

        public static void InitializingData()
        {
            PotentialValue = new Dictionary<string, int>();
            HighScoreCrozzle = new CrozzlePartial();
            averageScorePerWord = 0;
            highScore = 0;
        }

        public static void SetAverageScore(int score)
        {
            averageScorePerWord = score;
        }

        public static void SetHighScore(int score)
        {
            highScore = score;
        }

        public static CrozzlePartial GetBestCrozzle()
        {
            return HighScoreCrozzle;
        }

        public static Dictionary<string,int> GetPotentialValue()
        {
            return PotentialValue;
        }

        public static void SetPotentialValue(List<string> wordlist, Dictionary<char, int> intersectionPointsPerLetter, Dictionary<char, int> nonIntersectionPointsPerLetter)
        {
            averageScorePerWord = 0;
            for (int i = 0; i < wordlist.Count; i++)
            {
                string word = wordlist[i];
                int value = 10;
                for (int j = 0; j < word.Length; j++)
                {
                    value += intersectionPointsPerLetter[word[j]];
                    value += nonIntersectionPointsPerLetter[word[j]];
                }
                averageScorePerWord += value;
                PotentialValue.Add(wordlist[i], value);
            }
            averageScorePerWord /= wordlist.Count;
        }

        public static CrozzlePartial InsertWord(CrozzlePartial cp, Word s, int addScore)
        {
            CrozzlePartial returnCP = new CrozzlePartial();
            returnCP.SetWidth(cp.GetWidth());
            returnCP.SetHeight(cp.GetHeight());
            returnCP.SetMaxHeight(cp.GetMaxHeight());
            returnCP.SetMinHeight(cp.GetMinHeight());
            returnCP.SetMaxWidth(cp.GetMaxWidth());
            returnCP.SetMinWidth(cp.GetMinWidth());
            List<string> wordlist = new List<string>();
            for (int i = 0; i < cp.GetWordlist().Count; i++)
                wordlist.Add(cp.GetWordlist()[i]);
            List<Word> usedWord = new List<Word>();
            for (int i = 0; i < cp.GetUsedWord().Count; i++)
                usedWord.Add(cp.GetUsedWord()[i]);
            Grid grid = new Grid();

            int score = cp.GetScore();
            wordlist.Remove(s.GetWordContent());
            usedWord.Add(s);
            char[,] charGrid = new char[PublicInfo.GetFullRows(), PublicInfo.GetFullColumns()];
            for (int i = 0; i < PublicInfo.GetFullRows(); i++)
            {
                for (int j = 0; j < PublicInfo.GetFullColumns(); j++)
                {
                    charGrid[i, j] = cp.GetGrid().GetGrid()[i, j];
                }
            }
            int minHeight = cp.GetMinHeight();
            int maxHeight = cp.GetMaxHeight();
            int minWidth = cp.GetMinWidth();
            int maxWidth = cp.GetMaxWidth();
            if (s.GetType().Equals("ROW"))
            {
                int rows = s.GetRows() - 1;
                int columnsBegin = s.GetColumns() - 1;
                int columnsEnd = s.GetColumns() + s.GetWordContent().Length - 2;
                if (rows > maxHeight)
                {
                    returnCP.SetMaxHeight(rows);
                }
                if (rows < minHeight)
                {
                    returnCP.SetMinHeight(rows);
                }
                if (columnsBegin < minWidth && columnsEnd > maxWidth)
                {
                    returnCP.SetMaxWidth(columnsEnd);
                    returnCP.SetMinWidth(columnsBegin);
                }
                else if (columnsBegin < minWidth && columnsEnd <= maxWidth)
                {
                    returnCP.SetMinWidth(columnsBegin);
                }
                else if (columnsBegin >= minWidth && columnsEnd > maxWidth)
                {
                    returnCP.SetMaxWidth(columnsEnd);
                }
                for (int i = 0; i < s.GetWordContent().Length; i++)
                {

                    charGrid[s.GetRows() - 1, s.GetColumns() + i - 1] = s.GetWordContent()[i];
                }
            }
            else if (s.GetType().Equals("COLUMN"))
            {
                int columns = s.GetColumns() - 1;
                int rowsBegin = s.GetRows() - 1;
                int rowsEnd = s.GetRows() + s.GetWordContent().Length - 2;
                if (columns > maxWidth)
                {
                    returnCP.SetMaxWidth(columns);
                }
                if (columns < minWidth)
                {
                    returnCP.SetMinWidth(columns);
                }
                if (rowsBegin < minHeight && rowsEnd > maxHeight)
                {
                    returnCP.SetMaxHeight(rowsEnd);
                    returnCP.SetMinHeight(rowsBegin);
                }
                else if (rowsBegin < minHeight && rowsEnd <= maxHeight)
                {
                    returnCP.SetMinHeight(rowsBegin);
                }
                else if (rowsBegin >= minHeight && rowsEnd > maxHeight)
                {
                    returnCP.SetMaxHeight(rowsEnd);
                }
                for (int i = 0; i < s.GetWordContent().Length; i++)
                {

                    charGrid[s.GetRows() + i - 1, s.GetColumns() - 1] = s.GetWordContent()[i];
                }
            }
            score += addScore;
            grid.SetGrid(charGrid);
            returnCP.SetGrid(grid);
            // Set & Return
            returnCP.SetUsedWord(usedWord);
            returnCP.SetWordlist(wordlist);
            returnCP.SetGrid(grid);
            returnCP.SetScore(score);
            return returnCP;
        }

        public static List<Word> CanInsertWords(CrozzlePartial cp)
        {
            List<Word> result = new List<Word>();
            List<string> unUsedWord = cp.GetWordlist();
            int length = unUsedWord.Count();
            int max = 0;
            for (int i = 0; i < length; i++)
            {
                List<Word> words = RelevantWords(cp, unUsedWord[i]);
                int wordsLength = words.Count;
                for (int j = 0; j < wordsLength; j++)
                {
                    int score = CalInsertScore(cp, words[j]);
                    words[j].SetAddScore(score);
                    result.Add(words[j]);
                }

            }
            for(int i=0;i<result.Count;i++)
            {
                if(max<result[i].GetAddScore())
                {
                    max = result[i].GetAddScore();
                }
            }
            if (max == 0)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    if (result[i].GetAddScore() < max)
                        result.Remove(result[i--]);
                }
                return result;
            }
            for (int i = 0; i < result.Count; i++)
            {
                if (result[i].GetAddScore() < max - 1)
                    result.Remove(result[i--]);
            }
            return result;
        }

        public static int CalInsertScore(CrozzlePartial cp, Word word)
        {
            char[,] grid = cp.GetGrid().GetGrid();
            int minHeight = cp.GetMinHeight();
            int maxHeight = cp.GetMaxHeight();
            int minWidth = cp.GetMinWidth();
            int maxWidth = cp.GetMaxWidth();
            int currentHeight = maxHeight - minHeight + 1;
            int currentWidth = maxWidth - minWidth + 1;
            int result = 10;
            if (word.GetType().Equals("ROW"))
            {
                int rows = word.GetRows() - 1;
                int columnsBegin = word.GetColumns() - 1;
                int columnsEnd = word.GetColumns() + word.GetWordContent().Length - 2;
                if (columnsBegin < 0)
                    return -1;
                if (columnsEnd > PublicInfo.GetFullColumns() - 1)
                    return -1;
                if (rows > maxHeight)
                {
                    int newHeight = rows - minHeight + 1;
                    if (newHeight > PublicInfo.GetRows())
                        return -1;
                }
                if (rows < minHeight)
                {
                    int newHeight = maxHeight - rows + 1;
                    if (newHeight > PublicInfo.GetRows())
                        return -1;
                }
                if (columnsBegin < minWidth && columnsEnd > maxWidth)
                {
                    int newWidth = columnsEnd - columnsBegin + 1;
                    if (newWidth > PublicInfo.GetColumns())
                        return -1;
                }
                else if (columnsBegin < minWidth && columnsEnd <= maxWidth)
                {
                    int newWidth = maxWidth - columnsBegin + 1;
                    if (newWidth > PublicInfo.GetColumns())
                        return -1;
                }
                else if (columnsBegin >= minWidth && columnsEnd > maxWidth)
                {
                    int newWidth = columnsEnd - minWidth + 1;
                    if (newWidth > PublicInfo.GetColumns())
                        return -1;
                }
                if (columnsBegin - 1 >= 0)
                {
                    if (grid[rows, columnsBegin - 1] != '\0')
                        return -1;
                }
                if (columnsEnd + 1 <= PublicInfo.GetFullColumns() - 1)
                {
                    if (grid[rows, columnsEnd + 1] != '\0')
                        return -1;
                }
                for (int i = 0; i < word.GetWordContent().Length; i++)
                {
                    result += WordInfo.nonIntersectingPointsPerLetter[word.GetWordContent()[i]];
                    if (grid[rows, columnsBegin + i] != '\0')
                    {
                        if (grid[rows, columnsBegin + i] != word.GetWordContent()[i])
                            return -1;
                        else
                        {
                            result += WordInfo.intersectingPointsPerLetter[word.GetWordContent()[i]];
                            result -= 2 * WordInfo.nonIntersectingPointsPerLetter[word.GetWordContent()[i]];
                        }
                        // new
                        if (columnsBegin + i + 1 <= PublicInfo.GetFullColumns() - 1)
                        {
                            if (grid[rows, columnsBegin + i + 1] != '\0')
                                return -1;
                        }

                    }
                    else
                    {
                        if (rows != 0)
                        {
                            if (grid[rows - 1, columnsBegin + i] != '\0')
                                return -1;
                        }
                        if (rows != PublicInfo.GetFullRows() - 1)
                        {
                            if (grid[rows + 1, columnsBegin + i] != '\0')
                                return -1;
                        }


                    }
                }
            }
            else if (word.GetType().Equals("COLUMN"))
            {
                int columns = word.GetColumns() - 1;
                int rowsBegin = word.GetRows() - 1;
                int rowsEnd = word.GetRows() + word.GetWordContent().Length - 2;
                if (rowsBegin < 0)
                    return -1;
                if (rowsEnd > PublicInfo.GetFullRows() - 1)
                    return -1;
                if (columns > maxWidth)
                {
                    int newWidth = columns - minWidth + 1;
                    if (newWidth > PublicInfo.GetColumns())
                        return -1;
                }
                if (columns < minWidth)
                {
                    int newWidth = maxWidth - columns + 1;
                    if (newWidth > PublicInfo.GetColumns())
                        return -1;
                }
                if (rowsBegin < minHeight && rowsEnd > maxHeight)
                {
                    int newHeight = rowsEnd - rowsBegin + 1;
                    if (newHeight > PublicInfo.GetRows())
                        return -1;
                }
                else if (rowsBegin < minHeight && rowsEnd <= maxHeight)
                {
                    int newHeight = maxHeight - rowsBegin + 1;
                    if (newHeight > PublicInfo.GetRows())
                        return -1;
                }
                else if (rowsBegin >= minHeight && rowsEnd > maxHeight)
                {
                    int newHeight = rowsEnd - minHeight + 1;
                    if (newHeight > PublicInfo.GetRows())
                        return -1;
                }
                if (rowsBegin - 1 >= 0)
                {
                    if (grid[rowsBegin - 1, columns] != '\0')
                        return -1;
                }
                if (rowsEnd + 1 <= PublicInfo.GetFullRows() - 1)
                {
                    if (grid[rowsEnd + 1, columns] != '\0')
                        return -1;
                }
                for (int i = 0; i < word.GetWordContent().Length; i++)
                {
                    result += WordInfo.nonIntersectingPointsPerLetter[word.GetWordContent()[i]];
                    if (grid[rowsBegin + i, columns] != '\0')
                    {
                        if (grid[rowsBegin + i, columns] != word.GetWordContent()[i])
                            return -1;

                        else
                        {
                            result += WordInfo.intersectingPointsPerLetter[word.GetWordContent()[i]];
                            result -= 2 * WordInfo.nonIntersectingPointsPerLetter[word.GetWordContent()[i]];
                        }
                        // new
                        if (rowsBegin + i + 1 <= PublicInfo.GetFullRows() - 1)
                        {
                            if (grid[rowsBegin + i + 1, columns] != '\0')
                                return -1;
                        }
                    }
                    else
                    {
                        if (columns != 0)
                        {
                            if (grid[rowsBegin + i, columns - 1] != '\0')
                                return -1;
                        }
                        if (columns != PublicInfo.GetFullColumns() - 1)
                        {
                            if (grid[rowsBegin + i, columns + 1] != '\0')
                                return -1;
                        }


                    }
                }
            }
            return result;
        }

        public static List<Word> RelevantWords(CrozzlePartial cp, string w)
        {
            List<Word> usedWord = cp.GetUsedWord();
            int length = usedWord.Count();
            List<Word> result = new List<Word>();
            for (int i = 0; i < length; i++)
            {
                if (usedWord[i].GetType().Equals("COLUMN"))
                {
                    for (int m = 0; m < usedWord[i].GetWordContent().Length; m++)
                    {
                        char letter = usedWord[i].GetWordContent()[m];
                        int index = w.IndexOf(letter);
                        if (index == -1)
                            continue;
                        Word newWord = new Word();
                        newWord.SetWordContent(w);
                        newWord.SetType("ROW");
                        newWord.SetRows(usedWord[i].GetRows() + m);
                        newWord.SetColumns(usedWord[i].GetColumns() - index);
                        result.Add(newWord);
                    }
                }
                else if (usedWord[i].GetType().Equals("ROW"))
                {
                    for (int m = 0; m < usedWord[i].GetWordContent().Length; m++)
                    {
                        char letter = usedWord[i].GetWordContent()[m];
                        int index = w.IndexOf(letter);
                        if (index == -1)
                            continue;
                        Word newWord = new Word();
                        newWord.SetWordContent(w);
                        newWord.SetType("COLUMN");
                        newWord.SetRows(usedWord[i].GetRows() - index);
                        newWord.SetColumns(usedWord[i].GetColumns() + m);
                        result.Add(newWord);
                    }
                }
            }
            return result;
        }

        public static void GenerateCrozzle(CrozzlePartial cp)
        {
            if (!Crozzle.aTimer.Enabled)
            {
                return;
            }
            List<Word> wordsToInsert = CanInsertWords(cp);
            int length = wordsToInsert.Count();
            if (length == 0)
            {
                // one crozzle is found
                if (cp.GetScore() > highScore)
                    highScore = cp.GetScore();
                else
                    return;// return if the new crozzle's score is not high enough
                if(PublicInfo.GetMinGroups()>1)
                {
                    cp = SeperateGroups(cp);
                }
                if (cp.GetScore() > HighScoreCrozzle.GetScore())
                {
                    HighScoreCrozzle = cp;

                    // print score of crozzle
                    Console.WriteLine("===============================================================");
                    Console.WriteLine("New Highest Score: " + cp.GetScore());
                    Console.WriteLine("===============================================================");
                }
                return;
            }
            if (cp.GetUsedWord().Count >= PublicInfo.GetRows() && cp.GetUsedWord().Count <= PublicInfo.GetRows() + PublicInfo.GetColumns() && cp.GetScore() <= cp.GetUsedWord().Count * averageScorePerWord * 5 / 6 )
                return;
            for (int i = 0; i < length; i++)
            {
                if (wordsToInsert[i].GetAddScore() < 0)
                    continue;
                CrozzlePartial c = InsertWord(cp, wordsToInsert[i], wordsToInsert[i].GetAddScore());
                GenerateCrozzle(c);
            }
        }

        /// <summary>
        /// Seperate a one-group crozzle into multi-group crozzle
        /// </summary>
        /// <param name="cp">One-group crozzle</param>
        /// <returns>One multi-group crozzle</returns>
        public static CrozzlePartial SeperateGroups(CrozzlePartial cp)
        {
            int bestScore = 0;
            List<Word> bestUsedWords=new List<Word>();
            for(int i=0;i< cp.GetUsedWord().Count;i++)
            {
                List<Word> usedWords = new List<Word>();
                for (int j = 0; j < cp.GetUsedWord().Count; j++)
                    usedWords.Add(cp.GetUsedWord()[j]);
                Word deleteWord = cp.GetUsedWord()[i];
                usedWords.Remove(cp.GetUsedWord()[i]);
                int groups;
                try
                {
                    WordGroupServiceClient ws;
                    string endpoint = "BasicHttpBinding_IWordGroupService";
                    ws = new WordGroupServiceClient(endpoint);
                    Grid grid = new Grid(PublicInfo.GetFullRows(), PublicInfo.GetFullColumns(), usedWords);
                    string[] stringGrid = ConvertToString(grid.GetGrid(), PublicInfo.GetFullRows(), PublicInfo.GetFullColumns());
                    groups = ws.Count(stringGrid);
                }
                catch
                {
                    // use local group calculation method if online method fail
                    groups = CalculateGroup(usedWords);
                } 
                int score = CalculateScoreAfterRemovingAWord(cp, cp.GetUsedWord()[i]);
                if(groups>=PublicInfo.GetMinGroups()&&groups<=PublicInfo.GetMaxGroups()&&score>bestScore&&CheckAllIntersections(usedWords))
                {
                    bestScore = score;
                    bestUsedWords = usedWords;
                }
            }
            // generate a multi-groups crozzle with used words
            cp.SetUsedWord(bestUsedWords);
            cp.SetScore(bestScore);
            cp.SetGrid(new Grid(PublicInfo.GetFullRows(), PublicInfo.GetFullColumns(), bestUsedWords));
            return cp;
        }

        /// <summary>
        /// Local method of calculating groups
        /// </summary>
        /// <param name="usedWordList">List of words</param>
        /// <returns>number of groups</returns>
        private static int CalculateGroup(List<Word> usedWordList)
        {
            int groupNumber = 0;
            List<Word> usedWord = new List<Word>();
            for (int index = 0; index < usedWordList.Count(); index++)
            {
                usedWord.Add(usedWordList[index]);
            }

            // Calculate group numbers
            while (usedWord.Count() != 0)
            {
                List<Word> group = new List<Word>();
                Queue<Word> next = new Queue<Word>();
                next.Enqueue(usedWord[0]);
                group.Add(usedWord[0]);
                usedWord.Remove(usedWord[0]);
                while (next.Count() > 0)
                {
                    Queue<Word> vertical = new Queue<Word>();
                    Queue<Word> horizontal = new Queue<Word>();
                    Word[] temp_array = next.ToArray();
                    for (int index = 0; index < next.Count(); index++)
                    {
                        if (temp_array[index].GetType().Equals("ROW"))
                            horizontal.Enqueue(temp_array[index]);
                        else
                            vertical.Enqueue(temp_array[index]);
                    }
                    int queueLength = next.Count();
                    for (int index = 0; index < usedWord.Count(); index++)
                    {
                        if (usedWord[index].GetType().Equals("ROW"))
                        {
                            if (CrozzleValidation.Crossing(usedWord[index], vertical))
                            {
                                next.Enqueue(usedWord[index]);
                                group.Add(usedWord[index]);
                                usedWord.Remove(usedWord[index--]);
                            }
                        }
                        else
                        {
                            if (CrozzleValidation.Crossing(usedWord[index], horizontal))
                            {
                                next.Enqueue(usedWord[index]);
                                group.Add(usedWord[index]);
                                usedWord.Remove(usedWord[index--]);
                            }
                        }

                    }
                    for (int index = 0; index < queueLength; index++)
                        next.Dequeue();
                }
                groupNumber++;
            }
            return groupNumber;
        }

        private static int CalculateScoreAfterRemovingAWord(CrozzlePartial cp, Word removeWord)
        {
            int score = cp.GetScore();

            // reverse the process of adding a word
            score -= PublicInfo.GetPointsPerWord();
            for(int i=0;i<cp.GetUsedWord().Count;i++)
            {
                if(!cp.GetUsedWord()[i].GetType().Equals(removeWord.GetType()) && CrozzleValidation.Crossing(cp.GetUsedWord()[i], removeWord))
                {
                    char crossingLetter = CrozzleValidation.GetCrossingLetter(cp.GetUsedWord()[i], removeWord);
                    score -= WordInfo.intersectingPointsPerLetter[crossingLetter];
                    score += WordInfo.nonIntersectingPointsPerLetter[crossingLetter];
                }
            }
            return score;
        }

        private static bool CheckAllIntersections(List<Word> usedWord)
        {
            for(int i=0;i<usedWord.Count;i++)
            {
                // calculate number of intersections
                int cross = 0;
                for(int j=0;j<usedWord.Count;j++)
                {
                    if (!usedWord[i].GetType().Equals(usedWord[j].GetType())&&CrozzleValidation.Crossing(usedWord[i], usedWord[j]))
                        cross++;
                }

                // check intersection requirements
                if (cross == 0)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Convert char[,] to string[]
        /// </summary>
        /// <param name="grid">char[,]</param>
        /// <param name="rows">number of rows</param>
        /// <param name="columns">number of columns</param>
        /// <returns>string[]</returns>
        public static string[] ConvertToString(char[,] grid, int rows, int columns)
        {
            string[] result = new string[rows];
            for(int i=0;i< rows;i++)
            {
                string temp = "";
                for(int j=0;j<columns;j++)
                {
                    if (grid[i, j] == '\0')
                        temp += " ";
                    else
                        temp += grid[i, j].ToString();
                }
                result[i] = temp;
            }
            return result;
        }

    }
}
