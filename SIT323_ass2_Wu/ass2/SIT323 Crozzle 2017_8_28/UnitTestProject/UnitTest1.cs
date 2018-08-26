using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SIT323Crozzle;
using System.Collections.Generic;



namespace UnitTestProject
{
    [TestClass]
    public class UnitTest
    {
        /// <summary>
        /// Test if intersecting points per letter is set to WordInfo class
        /// </summary>
        [TestMethod]
        public void TestMethodSetIntersectingPointsPerLetter()
        {
            // Arrange
            string allIntersectingPointsPerLetter = @"A=1,B=2,C=2,D=2,E=1,F=2,G=2,H=2,I=1,J=4,K=4,L=4,M=4,N=4,O=1,P=8,Q=8,R=8,S=8,T=8,U=1,V=16,W=16,X=32,Y=32,Z=64";
            Dictionary<char, int> expectedDictionary = new Dictionary<char, int>();
            expectedDictionary.Add('A', 1);
            expectedDictionary.Add('B', 2);
            expectedDictionary.Add('C', 2);
            expectedDictionary.Add('D', 2);
            expectedDictionary.Add('E', 1);
            expectedDictionary.Add('F', 2);
            expectedDictionary.Add('G', 2);
            expectedDictionary.Add('H', 2);
            expectedDictionary.Add('I', 1);
            expectedDictionary.Add('J', 4);
            expectedDictionary.Add('K', 4);
            expectedDictionary.Add('L', 4);
            expectedDictionary.Add('M', 4);
            expectedDictionary.Add('N', 4);
            expectedDictionary.Add('O', 1);
            expectedDictionary.Add('P', 8);
            expectedDictionary.Add('Q', 8);
            expectedDictionary.Add('R', 8);
            expectedDictionary.Add('S', 8);
            expectedDictionary.Add('T', 8);
            expectedDictionary.Add('U', 1);
            expectedDictionary.Add('V', 16);
            expectedDictionary.Add('W', 16);
            expectedDictionary.Add('X', 32);
            expectedDictionary.Add('Y', 32);
            expectedDictionary.Add('Z', 64);
            const int LetterLength = 26;
            const int ValueOfA = 65;

            // Act
            WordInfo wordInfo = new WordInfo();
            wordInfo.SetIntersectingPointsPerLetter(allIntersectingPointsPerLetter);
            Dictionary<char, int> intersectingPointsPerLetter = wordInfo.GetIntersectingPointsPerLetter();

            // Assert
            Assert.AreEqual(expectedDictionary.Count, intersectingPointsPerLetter.Count, "Intersecting Points Per Letter are Different");
            for(int letterIndex=0;letterIndex< LetterLength; letterIndex++)
                Assert.AreEqual(expectedDictionary[(char)(ValueOfA + letterIndex)], intersectingPointsPerLetter[(char)(ValueOfA + letterIndex)], "Intersecting Points Per Letter are Different");


        }

        /// <summary>
        /// Test if values are set in Config class
        /// </summary>
        [TestMethod]
        public void TestMethodSetValueInConfig()
        {
            // Arrange
            int expectedMaximumIntersectionsInVerticalWords = 2;
            string expectedLogfileNmae = "log.txt";
            string expectedBgcolourEmptyTD = "#fff777";
            bool expectedUppercase = true;
            Config config = new Config();

            // Act: function here convert values from string into different types(string, int, bool), then stored in class
            config.SetAttribute("MAXIMUM_INTERSECTIONS_IN_VERTICAL_WORDS", "2");
            config.SetAttribute("LOGFILE_NAME", "log.txt");
            config.SetAttribute("BGCOLOUR_EMPTY_TD", "#fff777");
            config.SetAttribute("UPPERCASE", "true");

            // Assert
            Assert.AreEqual(expectedMaximumIntersectionsInVerticalWords,config.GetMaximumIntersectionsInVerticalWords(),"fail in assigning maximum intersections in vertical words");
            Assert.AreEqual(expectedLogfileNmae,config.GetLogfileName(),"fail in assigning logfile name");
            Assert.AreEqual(expectedBgcolourEmptyTD,config.GetBgcolourEmptyTD(),"fail in assigning bgcolour");
            Assert.AreEqual(expectedUppercase,config.GetUppercase(),"fail in assigning uppercase");

        }

        /// <summary>
        /// Test if intersections checking method work on well
        /// </summary>
        [TestMethod]
        public void TestMethodCheckCrossing()
        {
            // Arrange
            Word word = new Word();
            Word anotherWord = new Word();
            word.SetColumns(4);
            anotherWord.SetColumns(4);
            word.SetRows(1);
            anotherWord.SetRows(1);
            word.SetType("ROW");
            anotherWord.SetType("COLUMN");
            word.SetWordContent("BETTY");
            anotherWord.SetWordContent("BILL");
            bool expectedResult = true;

            // Act
            bool result = CrozzleValidation.Crossing(word, anotherWord);

            // Assert
            Assert.AreEqual(expectedResult, result, "check word intersecting function fail");
        }

        /// <summary>
        /// Test if Grid is store right data
        /// </summary>
        [TestMethod]
        public void TestMethodDisplayGrid()
        {
            // Arrange
            List<Word> wordList = new List<Word>();
            wordList.Add(new Word(1, 1, "ROW", "APPLE"));
            wordList.Add(new Word(1, 1, "COLUMN", "ANT"));
            wordList.Add(new Word(1, 4, "COLUMN", "LEAF"));
            wordList.Add(new Word(3, 1, "ROW", "TRIAL"));
            int rows = 4;
            int columns = 5;
            char[,] expectedGrid = new char[4, 5];
            expectedGrid[0, 0] = 'A';
            expectedGrid[0, 1] = 'P';
            expectedGrid[0, 2] = 'P';
            expectedGrid[0, 3] = 'L';
            expectedGrid[0, 4] = 'E';
            expectedGrid[1, 0] = 'N';
            expectedGrid[2, 0] = 'T';
            expectedGrid[2, 1] = 'R';
            expectedGrid[2, 2] = 'I';
            expectedGrid[2, 3] = 'A';
            expectedGrid[2, 4] = 'L';
            expectedGrid[1, 3] = 'E';
            expectedGrid[2, 3] = 'A';
            expectedGrid[3, 3] = 'F';

            // Act
            Grid crozzleGrid = new Grid(rows, columns, wordList);
            char[,] grid = crozzleGrid.GetGrid();

            // Assert
            for (int i=0;i<4;i++)
            {
                for(int j=0;j<5;j++)
                {
                    Assert.AreEqual(expectedGrid[i, j], grid[i,j],  i+" Grid generation failed "+j);

                }
            }



        }
    }
}
