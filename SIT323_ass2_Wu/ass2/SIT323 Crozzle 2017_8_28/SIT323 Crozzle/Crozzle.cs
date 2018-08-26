using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace SIT323Crozzle
{
    /// <summary>
    /// Application
    /// </summary>
    public partial class Crozzle : Form
    {
        private string lastDirectory = @"c:\";
        private URLForm urlForm=new URLForm();
        public static System.Timers.Timer aTimer;
        private const int ValueOfA = 65;
        private const int LetterLength = 26;

        /// <summary>
        /// Initialize component
        /// </summary>
        public Crozzle()
        {
            InitializeComponent();
           aTimer = new System.Timers.Timer(300000);
            aTimer.Elapsed += aTimer_Elapsed;
        }
        private void aTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            aTimer.Stop();           // stop the timer when the event occurs
        }


        /// <summary>
        /// Load crozzle and record errors
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">Events</param>
        private void LoadCrozzleToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Close application
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">Event</param>
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Show about message box
        /// </summary>
        /// <param name="sender">Object</param>
        /// <param name="e">Event</param>
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Design & Implement by Zhouhui Wu\nFor SIT 323 Assignment, 2017 T2\n Email: wzhdeyx@gmail.com", "About");
        }

        private void createCrozzleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            urlForm.ShowDialog();
            if(urlForm.DialogResult==DialogResult.OK)
            {
                Error.InitializeErrorList();
                CreateCrozzle();
            }
        }

        private void InitializePublicInfo(Config configuration)
        {
            PublicInfo.SetMinGroups(configuration.GetMinimumNumberOfGroups());
            PublicInfo.SetMaxGroups(configuration.GetMaximumNumberOfGroups());
            PublicInfo.SetPointsPerWord(configuration.GetPointsPerWord());
        }

        private void SortWordList(List<string> wlist)
        {
            for (int i = 0; i < wlist.Count - 1; i++)
            {
                for (int j = i + 1; j < wlist.Count; j++)
                {
                    if (SIT323Crozzle.CreateCrozzle.GetPotentialValue()[wlist[i]] < SIT323Crozzle.CreateCrozzle.GetPotentialValue()[wlist[j]])
                    {
                        string s = wlist[i];
                        wlist[i] = wlist[j];
                        wlist[j] = s;
                    }
                }
            }
        }

        private void RemoveWords(List<string> wlist)
        {
            for (int i = 0; i < wlist.Count; i++)
            {
                if (wlist[i].Length >= 4 && PublicInfo.ContainHighValueLetter(wlist[i]) < 1)
                {
                    wlist.Remove(wlist[i--]);
                    continue;
                }
                if (wlist[i].Length >= 6 && PublicInfo.ContainHighValueLetter(wlist[i]) < 2)
                {
                    wlist.Remove(wlist[i--]);
                    continue;
                }
                if (wlist[i].Length >= PublicInfo.GetColumns() / 2 - 1)
                    wlist.Remove(wlist[i--]);

            }
        }

        private string PreProcess(List<string> wlist)
        {
            // sort wordlist
            SortWordList(wlist);

            // get first word
            string initialWordContent = wlist[0];
            wlist.Remove(initialWordContent);

            // remove low value words and long words
            RemoveWords(wlist);

            return initialWordContent;
        }

        private int GetInitialScore(string initialWordContent)
        {
            int initialScore = 0;
            for (int i = 0; i < initialWordContent.Length; i++)
            {
                initialScore += WordInfo.nonIntersectingPointsPerLetter[initialWordContent[i]];
            }
            return initialScore;
        }

        private Word SetInitialWordPositionAndCrozzlePosition(string initialWordContent, CrozzlePartial cp)
        {
            Word initialWord = new Word();
            initialWord.SetWordContent(initialWordContent);
            initialWord.SetType("ROW");
            double temp = (PublicInfo.GetFullRows() - 1) / 2.0;
            int y = (int)Math.Ceiling(temp);
            initialWord.SetRows(y);
            temp = (PublicInfo.GetFullColumns() - initialWordContent.Length) / 2.0;
            int x = (int)Math.Ceiling(temp);
            initialWord.SetColumns(x);
            cp.SetHeight(PublicInfo.GetFullRows());
            cp.SetWidth(PublicInfo.GetFullColumns());
            cp.SetMaxHeight(y);
            cp.SetMaxWidth(x + initialWordContent.Length - 1);
            cp.SetMinHeight(y);
            cp.SetMinWidth(x);
            return initialWord;
        }

        private CrozzlePartial GenerateInitialCrozzle(string initialWordContent, List<string> wlist)
        {
            CrozzlePartial cp = new CrozzlePartial();

            // set initial word position
            Word initialWord = SetInitialWordPositionAndCrozzlePosition(initialWordContent, cp);

            // set initial crozzle
            List<Word> usedWord = new List<Word>();
            usedWord.Add(initialWord);
            Grid g = new Grid(PublicInfo.GetFullRows(), PublicInfo.GetFullColumns(), usedWord);
            cp.SetGrid(g);
            cp.SetUsedWord(usedWord);
            cp.SetWordlist(wlist);
            int initialScore = GetInitialScore(initialWordContent);
            cp.SetScore(initialScore + PublicInfo.GetPointsPerWord());
            return cp;
        }

        private void CreateCrozzle()
        {

            Config configuration = new Config();
            configuration.SetConfig("configuration.txt");
            string intersectionsPerPoints = configuration.GetIntersectingPointsPerLetter();
            string nonIntersectionsPerPoints = configuration.GetNonIntersectingPointsPerLetter();
            InitializePublicInfo(configuration);

            WordInfo wordlist = new WordInfo();
            wordlist.SetWordList("wordList.txt");
            wordlist.SetIntersectingPointsPerLetter(intersectionsPerPoints);
            wordlist.SetNonIntersectingPointsPerLetter(nonIntersectionsPerPoints);
            List<string> wlist = wordlist.GetWordList();


            // preprocess wordlist
            SIT323Crozzle.CreateCrozzle.SetPotentialValue(wlist, wordlist.GetIntersectingPointsPerLetter(), wordlist.GetNonIntersectingPointsPerLetter());
            string initialWordContent = PreProcess(wlist);

            // get initial crozzle
            CrozzlePartial initialCrozzle = GenerateInitialCrozzle(initialWordContent,wlist);

            aTimer.Start();
            SIT323Crozzle.CreateCrozzle.GenerateCrozzle(initialCrozzle);
            CrozzlePartial goodCrozzle = SIT323Crozzle.CreateCrozzle.GetBestCrozzle();
            aTimer.Stop();

            // get grid and show crozzle
            string style = configuration.GetStyle();
            bool uppercase = configuration.GetUppercase();
            Grid crozzleGrid = goodCrozzle.GetGrid();
            char[,] largeGrid = crozzleGrid.GetGrid();
            char[,] grid = new char[PublicInfo.GetRows(), PublicInfo.GetColumns()];
            for (int i = 0; i < PublicInfo.GetRows(); i++)
            {
                for (int j = 0; j < PublicInfo.GetColumns(); j++)
                {
                    if(i + goodCrozzle.GetMinHeight()<=goodCrozzle.GetMaxHeight()&& j + goodCrozzle.GetMinWidth()<=goodCrozzle.GetMaxWidth())
                    grid[i, j] = largeGrid[i + goodCrozzle.GetMinHeight(), j + goodCrozzle.GetMinWidth()];
                }
            }
            String crozzleHTML = @"<!DOCTYPE html>
                                <html>
                                <head>" + style + @"</head><body><table>";

            for (int row = 0; row < PublicInfo.GetRows(); row++)
            {
                String tr = "<tr>";
                for (int column = 0; column < PublicInfo.GetColumns(); column++)
                {
                    if (grid[row, column].CompareTo('\0') != 0)
                    {
                        tr += @"<td style='background:" + configuration.GetBgcolourNonEmptyTD();
                        if (uppercase == true)
                            tr += "'>" + grid[row, column].ToString().ToUpper() + @"</td>";
                        if (uppercase == false)
                            tr += "'>" + grid[row, column].ToString().ToLower() + @"</td>";
                    }
                    else
                    {
                        tr += @"<td style='background:" + configuration.GetBgcolourEmptyTD();
                        if (uppercase == true)
                            tr += "'>" + grid[row, column].ToString().ToUpper() + @"</td>";
                        if (uppercase == false)
                            tr += "'>" + grid[row, column].ToString().ToLower() + @"</td>";
                    }
                }
                tr += "</tr>";

                crozzleHTML += tr;
            }
            crozzleHTML += @"</table>";
            crozzleHTML += "<p>score: ";
            crozzleHTML += goodCrozzle.GetScore();
            crozzleHTML += "</p>";
            crozzleHTML += "</body></html>";
            CrozzleBrowser.DocumentText = crozzleHTML;
            ErrorBrowser.DocumentText = " ";
            
        }

        private void saveCrozzleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SIT323Crozzle.CreateCrozzle.GetBestCrozzle().GetScore()==0)
            {
                MessageBox.Show("No Crozzle Generated, please Generate a Crozzle first", "ERROR");
                return;
            }

            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = "czl file(*.czl) | *.czl";
            saveDialog.DefaultExt = "czl";
            saveDialog.AddExtension = true;
            saveDialog.RestoreDirectory = true;
            if(saveDialog.ShowDialog() == DialogResult.OK)
            {
                string savePath = saveDialog.FileName;
                FileStream stream = new FileStream(savePath, FileMode.Create);
                StreamWriter writer = new StreamWriter(stream);
                CrozzlePartial bs = SIT323Crozzle.CreateCrozzle.GetBestCrozzle();

                writer.WriteLine("ROWS="+PublicInfo.GetRows());
                writer.WriteLine("COLUMNS="+ PublicInfo.GetColumns());
                writer.WriteLine("CONFIGURATION_FILE=\""+PublicInfo.GetConfig()+"\"");
                writer.WriteLine("WORDLIST_FILE=\"" + PublicInfo.GetWordlist() + "\"");
                for (int i = 0; i < bs.GetUsedWord().Count; i++)
                {
                    string s = "";
                    if (bs.GetUsedWord()[i].GetType().Equals("COLUMN"))
                    {
                        s += "COLUMN=";
                        s += bs.GetUsedWord()[i].GetColumns() - bs.GetMinWidth();
                        s += ",";
                        s += bs.GetUsedWord()[i].GetWordContent();
                        s += ",";
                        s += bs.GetUsedWord()[i].GetRows() - bs.GetMinHeight();
                    }
                    else if (bs.GetUsedWord()[i].GetType().Equals("ROW"))
                    {
                        s += "ROW=";
                        s += bs.GetUsedWord()[i].GetRows() - bs.GetMinHeight();
                        s += ",";
                        s += bs.GetUsedWord()[i].GetWordContent();
                        s += ",";
                        s += bs.GetUsedWord()[i].GetColumns() - bs.GetMinWidth();
                    }
                    writer.WriteLine(s);
                }
                writer.Close();
                stream.Close();
            }
        }

        private void loadCrozzletxtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Log.RemoveLogInformation();
            Error.InitializeErrorList();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Load Crozzle";
            dialog.InitialDirectory = lastDirectory;
            dialog.Filter = "text file（*.txt）|*.txt";
            dialog.FilterIndex = 2;
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string path = dialog.FileName;
                lastDirectory = path;

                // Start loading files
                Boot boot = new Boot();
                boot.SetBoot(path);
                if (!boot.GetPathHaveSet("CONFIGURATION_FILE"))
                {
                    MessageBox.Show("Can't find path of configuration file in the crozzle file", "ERROR");
                    CrozzleBrowser.DocumentText = "Can't read file: please choose the correct crozzle file";
                    ErrorBrowser.DocumentText = "Can't read file: please choose the correct crozzle file";
                    return;
                }
                if (!boot.GetPathHaveSet("WORDLIST_FILE"))
                {
                    MessageBox.Show("Can't find path of word list file in the crozzle file", "ERROR");
                    CrozzleBrowser.DocumentText = "Can't read file: please choose the correct crozzle file";
                    ErrorBrowser.DocumentText = "Can't read file: please choose the correct crozzle file";
                    return;
                }
                string rootPath = boot.GetRootPath();
                Config config = new Config();
                config.SetConfig(boot.GetConfigurationFile());
                config.ValidateConfig();
                config.SetLogfileName(config.ChangePath(config.GetLogfileName(), rootPath));
                bool error = false;

                WordInfo wordInfo = new WordInfo();
                wordInfo.SetWordList(boot.GetWordListFile());
                wordInfo.SetIntersectingPointsPerLetter(config.GetIntersectingPointsPerLetter());
                wordInfo.SetNonIntersectingPointsPerLetter(config.GetNonIntersectingPointsPerLetter());
                wordInfo.ValidateWordList(config);

                WordResult result = new WordResult();
                result.InitializeSetValue();
                result.SetRows(path);
                result.SetColumns(path);
                result.SetUsedWord(path);
                result.ValidateRowsColumns(config);
                List<Word> wordList = result.GetUsedWord();

                // Crozzle validation Start
                CrozzleValidation crozzle = new CrozzleValidation();
                Log.AddLogInformation("crozzle validation : begin");
                crozzle.ValidateCrozzle(config, wordInfo, result);
                crozzle.CheckGroup(config, result);
                Log.AddLogInformation("crozzle validation : end");
                List<string> crozzleError = Error.GetCrozzleError();
                List<string> configurationError = Error.GetConfigurationError();
                List<string> crozzleFileError = Error.GetCrozzleFileError();
                List<string> wordListError = Error.GetWordListError();

                // Set default value of uppercase: false
                bool uppercase = false;
                if (config.GetSetInformation("UPPERCASE"))
                    uppercase = config.GetUppercase();

                // Set default value for style
                string style = @"<style>table, td {border: 1px solid black;border - collapse: collapse;}td {width: 24px; text - align: center;}</ style > ";//default style value
                if (config.GetSetInformation("STYLE"))
                    style = config.GetStyle();

                // Display error information and stored in log file
                string errorHTML = @"<!DOCTYPE html>
                                <html>
                                <head></head><body><h3>file validation</h3>";
                if (configurationError.Count() != 0)
                {
                    error = true;
                    errorHTML += "<p>configuration.txt</p>";
                    if (configurationError.Count() == 0)
                        errorHTML += "<p>configuration.txt: valid</p>";
                    else
                        errorHTML += "<p>configuration.txt: invalid</p>";
                    for (int i = 0; i < configurationError.Count(); i++)
                    {
                        errorHTML += @"<p>error";
                        errorHTML += (i + 1);
                        errorHTML += @": ";
                        errorHTML += configurationError[i];
                        errorHTML += @"</p>";
                    }
                }

                if (wordListError.Count() != 0)
                {
                    error = true;
                    errorHTML += "<p>word list.txt</p>";
                    if (wordListError.Count() == 0)
                        errorHTML += "<p>wordlist.txt: valid</p>";
                    else
                        errorHTML += "<p>wordlist.txt: invalid</p>";
                    for (int i = 0; i < wordListError.Count(); i++)
                    {
                        errorHTML += @"<p>error";
                        errorHTML += (i + 1);
                        errorHTML += @": ";
                        errorHTML += wordListError[i];
                        errorHTML += @"</p>";
                    }
                }

                if (crozzleFileError.Count() != 0)
                {
                    error = true;
                    errorHTML += "<p>crozzle.txt</p>";
                    if (crozzleFileError.Count() == 0)
                        errorHTML += "<p>crozzle.txt: valid</p>";
                    else
                        errorHTML += "<p>crozzle.txt: invalid</p>";
                    for (int i = 0; i < crozzleFileError.Count(); i++)
                    {
                        errorHTML += @"<p>error";
                        errorHTML += (i + 1);
                        errorHTML += @": ";
                        errorHTML += crozzleFileError[i];
                        errorHTML += @"</p>";
                    }
                }

                if (error == false)
                {
                    errorHTML += "<p>configuration.txt: valid</p><p>wordlist.txt: valid</p><p>crozzle.txt: valid</p>";
                }

                if (crozzleError.Count > 0)
                {
                    error = true;
                    errorHTML += "<h3>crozzle validation</h3>";
                    if (crozzleError.Count() == 0)
                        errorHTML += "<p>crozzle: valid</p>";
                    else
                        errorHTML += "<p>crozzle: invalid</p>";
                    for (int i = 0; i < crozzleError.Count(); i++)
                    {
                        errorHTML += @"<p>error";
                        errorHTML += (i + 1);
                        errorHTML += @": ";
                        errorHTML += crozzleError[i];
                        errorHTML += @"</p>";
                    }
                }


                if (error == true)
                {
                    errorHTML += @"</body></html>";
                    ErrorBrowser.DocumentText = errorHTML;
                    FileStream stream = new FileStream(config.GetLogfileName(), FileMode.Append);
                    StreamWriter writer = new StreamWriter(stream);
                    writer.WriteLine("\r\n\r\n==================begin test " + path + " " + DateTime.Now.ToString() + "==================");
                    List<string> logInfo = Log.GetLogInformation();
                    for (int i = 0; i < logInfo.Count; i++)
                        writer.WriteLine(logInfo[i]);
                    if (configurationError.Count() == 0)
                        writer.WriteLine("configuration.txt: valid");
                    else
                        writer.WriteLine("configuration.txt: invalid");
                    if (wordListError.Count() == 0)
                        writer.WriteLine("wordlist.txt: valid");
                    else
                        writer.WriteLine("wordlist.txt: invalid");
                    if (crozzleFileError.Count() == 0)
                        writer.WriteLine("crozzle.txt: valid");
                    else
                        writer.WriteLine("crozzle.txt: invalid");
                    if (crozzleError.Count() == 0)
                        writer.WriteLine("crozzle: valid");
                    else
                        writer.WriteLine("crozzle: invalid");
                    writer.WriteLine("errors:");
                    writer.WriteLine("files validation");
                    writer.WriteLine("configuration.txt");
                    for (int i = 0; i < configurationError.Count(); i++)
                        writer.WriteLine(configurationError[i]);
                    writer.WriteLine("wordlist.txt");
                    for (int i = 0; i < wordListError.Count(); i++)
                        writer.WriteLine(wordListError[i]);
                    writer.WriteLine("crozzle.txt");
                    for (int i = 0; i < crozzleFileError.Count(); i++)
                        writer.WriteLine(crozzleFileError[i]);
                    writer.WriteLine("crozzle validation");
                    for (int i = 0; i < crozzleError.Count(); i++)
                        writer.WriteLine(crozzleError[i]);

                    writer.WriteLine("==================end test " + path + "==================");
                    writer.Close();
                    stream.Close();
                }
                else
                {
                    ErrorBrowser.DocumentText = @"<p>All files valid</p><p>Crozzle valid</p><p>No error</p>";
                    FileStream stream = new FileStream(config.GetLogfileName(), FileMode.Append);
                    StreamWriter writer = new StreamWriter(stream);
                    writer.WriteLine("\r\n\r\n==================begin test " + path + " " + DateTime.Now.ToString() + "==================");
                    List<string> logInfo = Log.GetLogInformation();
                    for (int i = 0; i < logInfo.Count; i++)
                        writer.WriteLine(logInfo[i]);
                    writer.WriteLine("configuration.txt: valid");
                    writer.WriteLine("wordlist.txt: valid");
                    writer.WriteLine("crozzle.txt: valid");
                    writer.WriteLine("crozzle: valid");
                    writer.WriteLine("NO ERROR");
                    writer.WriteLine("==================end test " + path + "==================");
                    writer.Close();
                    stream.Close();
                }

                // Display Crozzle
                int rows = result.GetRows();
                int columns = result.GetColumns();
                Grid crozzleGrid = new Grid(rows, columns, wordList);
                char[,] grid = crozzleGrid.GetGrid();
                String crozzleHTML = @"<!DOCTYPE html>
                                <html>
                                <head>" + style + @"</head><body><table>";

                for (int row = 0; row < rows; row++)
                {
                    String tr = "<tr>";
                    for (int column = 0; column < columns; column++)
                    {
                        if (grid[row, column].CompareTo('\0') != 0)
                        {
                            tr += @"<td style='background:" + config.GetBgcolourNonEmptyTD();
                            if (uppercase == true)
                                tr += "'>" + grid[row, column].ToString().ToUpper() + @"</td>";
                            if (uppercase == false)
                                tr += "'>" + grid[row, column].ToString().ToLower() + @"</td>";
                        }
                        else
                        {
                            tr += @"<td style='background:" + config.GetBgcolourEmptyTD();
                            if (uppercase == true)
                                tr += "'>" + grid[row, column].ToString().ToUpper() + @"</td>";
                            if (uppercase == false)
                                tr += "'>" + grid[row, column].ToString().ToLower() + @"</td>";
                        }
                    }
                    tr += "</tr>";

                    crozzleHTML += tr;
                }
                crozzleHTML += @"</table>";
                crozzleHTML += @"<br><p>score: ";

                if (error == true)
                    crozzleHTML += config.GetInvalidCrozzleScore();
                else
                {
                    result.CalculateScore(config.GetPointsPerWord());
                    crozzleHTML += result.GetScore();
                }
                crozzleHTML += "</p></body></html>";

                CrozzleBrowser.DocumentText = crozzleHTML;
            }
        }

        private void loadCrozzleczlToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "Load Crozzle";
            dialog.Filter = "czl file（*.czl）|*.czl";
            dialog.FilterIndex = 2;
            dialog.RestoreDirectory = true;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string path = dialog.FileName;
                Error.InitializeErrorList();

                CrozzleGenerateFile boot = new CrozzleGenerateFile();
                boot.SetBoot(path);
                boot.SetColumns(path);
                boot.SetRows(path);
                if (!boot.GetPathHaveSet("CONFIGURATION_FILE"))
                {
                    MessageBox.Show("Can't find path of configuration file in the crozzle file", "ERROR");
                    return;
                }
                if (!boot.GetPathHaveSet("WORDLIST_FILE"))
                {
                    MessageBox.Show("Can't find path of word list file in the crozzle file", "ERROR");
                    return;
                }
                WebClient webClient = new WebClient();
                // Download Azure file. As the file referenced by the following URL has 
                // an SAS (Shared Access Signature) end date of Oct 31, 23:59, 
                // you cannot access this file after this end date.
                try
                {
                    string configurationTxt = boot.GetConfigurationFile();
                    webClient.DownloadFile(configurationTxt, "configuration.txt");
                    string wordListTxt = boot.GetWordListFile();
                    webClient.DownloadFile(wordListTxt, "wordList.txt");
                }
                catch (WebException)// wrong web connection
                {
                    MessageBox.Show("Internet connection fail");
                    return;
                }
                catch (ArgumentNullException)// url is null
                {
                    MessageBox.Show("url is null");
                    return;
                }
                catch (NotSupportedException)// write file error
                {
                    MessageBox.Show("fail to write file");
                    return;
                }

                Config configuration = new Config();
                configuration.SetConfig("configuration.txt");
                string intersectionsPerPoints = configuration.GetIntersectingPointsPerLetter();
                string nonIntersectionsPerPoints = configuration.GetNonIntersectingPointsPerLetter();

                WordInfo wordlist = new WordInfo();
                wordlist.SetWordList("wordList.txt");
                wordlist.SetIntersectingPointsPerLetter(intersectionsPerPoints);
                wordlist.SetNonIntersectingPointsPerLetter(nonIntersectionsPerPoints);

                WordResult result = new WordResult();
                result.InitializeSetValue();
                result.SetRows(path);
                result.SetColumns(path);
                result.SetUsedWord(path);
                result.ValidateRowsColumns(configuration);

                CrozzleValidation crozzle = new CrozzleValidation();
                Log.AddLogInformation("crozzle validation : begin");
                crozzle.ValidateCrozzle(configuration, wordlist, result);
                crozzle.CheckGroup(configuration, result);
                Log.AddLogInformation("crozzle validation : end");
                List<string> crozzleError = Error.GetCrozzleError();
                List<string> configurationError = Error.GetConfigurationError();
                List<string> crozzleFileError = Error.GetCrozzleFileError();
                List<string> wordListError = Error.GetWordListError();
                bool error = false;
                // Set default value of uppercase: false
                bool uppercase = false;
                if (configuration.GetSetInformation("UPPERCASE"))
                    uppercase = configuration.GetUppercase();

                // Set default value for style
                string style = @"<style>table, td {border: 1px solid black;border - collapse: collapse;}td {width: 24px; text - align: center;}</ style > ";//default style value
                if (configuration.GetSetInformation("STYLE"))
                    style = configuration.GetStyle();
                
                // Display error information and stored in log file
                string errorHTML = @"<!DOCTYPE html>
                                <html>
                                <head></head><body><h3>file validation</h3>";
                if (configurationError.Count() != 0)
                {
                    error = true;
                    errorHTML += "<p>configuration.txt</p>";
                    if (configurationError.Count() == 0)
                        errorHTML += "<p>configuration.txt: valid</p>";
                    else
                        errorHTML += "<p>configuration.txt: invalid</p>";
                    for (int i = 0; i < configurationError.Count(); i++)
                    {
                        errorHTML += @"<p>error";
                        errorHTML += (i + 1);
                        errorHTML += @": ";
                        errorHTML += configurationError[i];
                        errorHTML += @"</p>";
                    }
                }

                if (wordListError.Count() != 0)
                {
                    error = true;
                    errorHTML += "<p>word list.txt</p>";
                    if (wordListError.Count() == 0)
                        errorHTML += "<p>wordlist.txt: valid</p>";
                    else
                        errorHTML += "<p>wordlist.txt: invalid</p>";
                    for (int i = 0; i < wordListError.Count(); i++)
                    {
                        errorHTML += @"<p>error";
                        errorHTML += (i + 1);
                        errorHTML += @": ";
                        errorHTML += wordListError[i];
                        errorHTML += @"</p>";
                    }
                }

                if (crozzleFileError.Count() != 0)
                {
                    error = true;
                    errorHTML += "<p>crozzle.txt</p>";
                    if (crozzleFileError.Count() == 0)
                        errorHTML += "<p>crozzle.txt: valid</p>";
                    else
                        errorHTML += "<p>crozzle.txt: invalid</p>";
                    for (int i = 0; i < crozzleFileError.Count(); i++)
                    {
                        errorHTML += @"<p>error";
                        errorHTML += (i + 1);
                        errorHTML += @": ";
                        errorHTML += crozzleFileError[i];
                        errorHTML += @"</p>";
                    }
                }

                if (error == false)
                {
                    errorHTML += "<p>configuration.txt: valid</p><p>wordlist.txt: valid</p><p>crozzle.txt: valid</p>";
                }

                if (crozzleError.Count > 0)
                {
                    error = true;
                    errorHTML += "<h3>crozzle validation</h3>";
                    if (crozzleError.Count() == 0)
                        errorHTML += "<p>crozzle: valid</p>";
                    else
                        errorHTML += "<p>crozzle: invalid</p>";
                    for (int i = 0; i < crozzleError.Count(); i++)
                    {
                        errorHTML += @"<p>error";
                        errorHTML += (i + 1);
                        errorHTML += @": ";
                        errorHTML += crozzleError[i];
                        errorHTML += @"</p>";
                    }
                }


                if (error == true)
                {
                    errorHTML += @"</body></html>";
                    ErrorBrowser.DocumentText = errorHTML;
                    FileStream stream = new FileStream(configuration.GetLogfileName(), FileMode.Append);
                    StreamWriter writer = new StreamWriter(stream);
                    writer.WriteLine("\r\n\r\n==================begin test " + path + " " + DateTime.Now.ToString() + "==================");
                    List<string> logInfo = Log.GetLogInformation();
                    for (int i = 0; i < logInfo.Count; i++)
                        writer.WriteLine(logInfo[i]);
                    if (configurationError.Count() == 0)
                        writer.WriteLine("configuration.txt: valid");
                    else
                        writer.WriteLine("configuration.txt: invalid");
                    if (wordListError.Count() == 0)
                        writer.WriteLine("wordlist.txt: valid");
                    else
                        writer.WriteLine("wordlist.txt: invalid");
                    if (crozzleFileError.Count() == 0)
                        writer.WriteLine("crozzle.txt: valid");
                    else
                        writer.WriteLine("crozzle.txt: invalid");
                    if (crozzleError.Count() == 0)
                        writer.WriteLine("crozzle: valid");
                    else
                        writer.WriteLine("crozzle: invalid");
                    writer.WriteLine("errors:");
                    writer.WriteLine("files validation");
                    writer.WriteLine("configuration.txt");
                    for (int i = 0; i < configurationError.Count(); i++)
                        writer.WriteLine(configurationError[i]);
                    writer.WriteLine("wordlist.txt");
                    for (int i = 0; i < wordListError.Count(); i++)
                        writer.WriteLine(wordListError[i]);
                    writer.WriteLine("crozzle.txt");
                    for (int i = 0; i < crozzleFileError.Count(); i++)
                        writer.WriteLine(crozzleFileError[i]);
                    writer.WriteLine("crozzle validation");
                    for (int i = 0; i < crozzleError.Count(); i++)
                        writer.WriteLine(crozzleError[i]);

                    writer.WriteLine("==================end test " + path + "==================");
                    writer.Close();
                    stream.Close();
                }
                else
                {
                    ErrorBrowser.DocumentText = @"<p>All files valid</p><p>Crozzle valid</p><p>No error</p>";
                    FileStream stream = new FileStream(configuration.GetLogfileName(), FileMode.Append);
                    StreamWriter writer = new StreamWriter(stream);
                    writer.WriteLine("\r\n\r\n==================begin test " + path + " " + DateTime.Now.ToString() + "==================");
                    List<string> logInfo = Log.GetLogInformation();
                    for (int i = 0; i < logInfo.Count; i++)
                        writer.WriteLine(logInfo[i]);
                    writer.WriteLine("configuration.txt: valid");
                    writer.WriteLine("wordlist.txt: valid");
                    writer.WriteLine("crozzle.txt: valid");
                    writer.WriteLine("crozzle: valid");
                    writer.WriteLine("NO ERROR");
                    writer.WriteLine("==================end test " + path + "==================");
                    writer.Close();
                    stream.Close();
                }

                // Display Crozzle
                int rows = result.GetRows();
                int columns = result.GetColumns();
                Grid crozzleGrid = new Grid(rows, columns, result.GetUsedWord());
                char[,] grid = crozzleGrid.GetGrid();
                String crozzleHTML = @"<!DOCTYPE html>
                                <html>
                                <head>" + style + @"</head><body><table>";

                for (int row = 0; row < rows; row++)
                {
                    String tr = "<tr>";
                    for (int column = 0; column < columns; column++)
                    {
                        if (grid[row, column].CompareTo('\0') != 0)
                        {
                            tr += @"<td style='background:" + configuration.GetBgcolourNonEmptyTD();
                            if (uppercase == true)
                                tr += "'>" + grid[row, column].ToString().ToUpper() + @"</td>";
                            if (uppercase == false)
                                tr += "'>" + grid[row, column].ToString().ToLower() + @"</td>";
                        }
                        else
                        {
                            tr += @"<td style='background:" + configuration.GetBgcolourEmptyTD();
                            if (uppercase == true)
                                tr += "'>" + grid[row, column].ToString().ToUpper() + @"</td>";
                            if (uppercase == false)
                                tr += "'>" + grid[row, column].ToString().ToLower() + @"</td>";
                        }
                    }
                    tr += "</tr>";

                    crozzleHTML += tr;
                }
                crozzleHTML += @"</table>";
                crozzleHTML += @"<br><p>score: ";

                if (error == true)
                    crozzleHTML += configuration.GetInvalidCrozzleScore();
                else
                {
                    result.CalculateScore(configuration.GetPointsPerWord());
                    crozzleHTML += result.GetScore();
                }
                crozzleHTML += "</p></body></html>";

                CrozzleBrowser.DocumentText = crozzleHTML;
            }
        }
    }
}
