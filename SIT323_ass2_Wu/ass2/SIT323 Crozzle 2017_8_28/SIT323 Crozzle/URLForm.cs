using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;


namespace SIT323Crozzle
{
    public partial class URLForm : Form
    {
        public URLForm()
        {
            InitializeComponent();
        }

        private void InitializePublicInfo(CrozzleGenerateFile boot)
        {
            PublicInfo.SetRows(boot.GetRows());
            PublicInfo.SetColumns(boot.GetColumns());
            PublicInfo.SetFullColumns(boot.GetColumns() * 2);
            PublicInfo.SetFullRows(boot.GetRows() + 3);
            PublicInfo.SetConfig(boot.GetConfigurationFile());
            PublicInfo.SetWordlist(boot.GetWordListFile());
        }

        private string GetAnotherURL(string url)
        {
            string anotherurl = "";
            if (url == "http://www.it.deakin.edu.au/SIT323/Task2/MarkingTest1.czl")
                anotherurl = @"http://sit323.azurewebsites.net/Task2/MarkingTest1.czl";
            else if (url == "http://www.it.deakin.edu.au/SIT323/Task2/MarkingTest2.czl")
                anotherurl = @"http://sit323.azurewebsites.net/Task2/MarkingTest2.czl";
            else if (url == "http://www.it.deakin.edu.au/SIT323/Task2/MarkingTest3.czl")
                anotherurl = @"http://sit323.azurewebsites.net/Task2/MarkingTest3.czl";
            else if (url == "http://sit323.azurewebsites.net/Task2/MarkingTest1.czl")
                anotherurl = @"http://www.it.deakin.edu.au/SIT323/Task2/MarkingTest1.czl";
            else if (url == "http://sit323.azurewebsites.net/Task2/MarkingTest2.czl")
                anotherurl = @"http://www.it.deakin.edu.au/SIT323/Task2/MarkingTest2.czl";
            else if (url == "http://sit323.azurewebsites.net/Task2/MarkingTest3.czl")
                anotherurl = @"http://www.it.deakin.edu.au/SIT323/Task2/MarkingTest3.czl";
            return anotherurl;
        }

        private void openURLButton_Click(object sender, EventArgs e)
        {
            string newUrl = historyComboBox.Text;
            string anotherurl = GetAnotherURL(newUrl);

            // initializing data
            WordInfo.intersectingPointsPerLetter = new Dictionary<char, int>();
            WordInfo.nonIntersectingPointsPerLetter = new Dictionary<char, int>();
            CreateCrozzle.InitializingData();

            WebClient webClient = new WebClient();
            // Download IIS file.
            try
            {
                string crozzleTxt = newUrl;
                webClient.DownloadFile(crozzleTxt, @"crozzle.txt");
            }
            catch(WebException)// wrong web connection
            {
                try
                {
                    if(anotherurl!="")// try to change another url
                    webClient.DownloadFile(anotherurl, @"crozzle.txt");
                    else
                    {
                        MessageBox.Show("Internet connection fail");
                        return;
                    }
                }
                catch(WebException)// wrong web connection
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


            CrozzleGenerateFile boot = new CrozzleGenerateFile();
            boot.SetBoot("crozzle.txt");
            boot.SetColumns("crozzle.txt");
            boot.SetRows("crozzle.txt");
            
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
            // Download Azure file. As the file referenced by the following URL has 
            // an SAS (Shared Access Signature) end date of Oct 31, 23:59, 
            // you cannot access this file after this end date.
            string configurationTxt = boot.GetConfigurationFile();
            try
            {
                webClient.DownloadFile(configurationTxt, @"configuration.txt");
            }
            catch(WebException)// wrong web connection
            {
                MessageBox.Show("net connection false");
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


            try
            {
                string wordListTxt = boot.GetWordListFile();
                webClient.DownloadFile(wordListTxt, @"wordList.txt");
            }
            catch (WebException)// wrong web connection
            {
                MessageBox.Show("net connection false");
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
            InitializePublicInfo(boot);
            this.DialogResult = DialogResult.OK;
        }

        private void URLForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Console.WriteLine("close");
        }
    }
}
