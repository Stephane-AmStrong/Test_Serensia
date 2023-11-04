namespace Exercice_1Test
{
    public class IAmTheTestUnitTest
    {
        private readonly Mock<IAmTheTest> _mockAmTheTest;
        //private readonly AmTheTest _mockAmTheTes;

        public IAmTheTestUnitTest()
        {
            _mockAmTheTest = new Mock<IAmTheTest>();
        }

        #region UnitTestUsingTheory

        [Theory]
        [MemberData(nameof(TestData))]
        public void GetSuggestionsUnitTest(string term, IEnumerable<string> choices, int numberOfSuggestions, IEnumerable<string> expectedResult)
        {
            //Arrange
            _mockAmTheTest.Setup(repo => repo.GetSuggestions(term, choices, numberOfSuggestions))
                .Returns(expectedResult);

            //Act
            var result = _mockAmTheTest.Object.GetSuggestions(term, choices, numberOfSuggestions);

            //Assert
            Assert.Equal(expectedResult, result);
        }



        public static TheoryData<string, IEnumerable<string>, int, IEnumerable<string>> TestData()
        {
            var data = new TheoryData<string, IEnumerable<string>, int, IEnumerable<string>>();

            data.Add("gros", new List<string> { "gros", "gras", "graisse", "aggressif", "go" }, 2, new List<string> { "gros", "gras" });
            data.Add("pomme", new List<string> { "banane", "cerise", "orange" }, 2, new List<string>());
            data.Add("gros", new List<string>(), 2, new List<string>());
            return data;
        }

        #endregion


        #region UnitTestUsingFacts

        [Fact]
        public void GetSuggestions_ReturnsMatchingSuggestions()
        {
            //Arrange
            var term = "gros";
            var choices = new List<string> { "gros", "gras", "graisse", "aggressif", "go", "ros", "gro" };
            var numberOfSuggestions = 2;
            var expectedResult = new List<string> { "gros", "gras" };

            _mockAmTheTest.Setup(repo => repo.GetSuggestions(term, choices, numberOfSuggestions))
                .Returns(expectedResult);

            //Act
            var suggestions = _mockAmTheTest.Object.GetSuggestions(term, choices, numberOfSuggestions);

            //Assert
            Assert.Equal(expectedResult, suggestions);
        }

        [Fact]
        public void GetSuggestions_ReturnsEmptyListForNoMatches()
        {
            //Arrange
            var term = "pomme";
            var choices = new List<string> { "banane", "cerise", "orange" };
            var numberOfSuggestions = 2;
            var expectedResult = new List<string>();

            _mockAmTheTest.Setup(repo => repo.GetSuggestions(term, choices, numberOfSuggestions))
                .Returns(expectedResult);

            //Act
            var suggestions = _mockAmTheTest.Object.GetSuggestions(term, choices, numberOfSuggestions);

            //Assert
            // Aucune suggestion ne doit être trouvée
            Assert.Empty(suggestions);
        }

        [Fact]
        public void GetSuggestions_ReturnsEmptyListForEmptyChoices()
        {
            //Arrange
            var term = "gros";
            var choices = new List<string>();
            var numberOfSuggestions = 2;
            var expectedResult = new List<string>();

            _mockAmTheTest.Setup(repo => repo.GetSuggestions(term, choices, numberOfSuggestions))
                .Returns(expectedResult);

            //Act
            var suggestions = _mockAmTheTest.Object.GetSuggestions(term, choices, numberOfSuggestions);

            //Assert
            // Aucune suggestion ne doit être trouvée
            Assert.Empty(suggestions);
        }

        [Fact]
        public void GetDifferenceScore_ReturnsZeroForIdenticalStrings()
        {
            //Arrange
            var dest = "pomme";
            var src = "pomme";
            var expectedResult = 0;

            _mockAmTheTest.Setup(repo => repo.GetDifferenceScore(dest, src))
                .Returns(expectedResult);

            //Act
            var score = _mockAmTheTest.Object.GetDifferenceScore(dest, src);

            //Assert
            Assert.Equal(expectedResult, score);
        }

        [Fact]
        public void GetDifferenceScore_ReturnsCorrectScoreForDifferentStrings()
        {
            //Arrange
            var dest = "gros";
            var src = "gras";
            var expectedResult = 1;

            _mockAmTheTest.Setup(repo => repo.GetDifferenceScore(dest, src))
                .Returns(expectedResult);

            //Act
            var score = _mockAmTheTest.Object.GetDifferenceScore(dest, src);

            //Assert
            // Les caractères 'o' et 'a' diffèrent, le score doit être 1
            Assert.Equal(expectedResult, score);
        }

        [Fact]
        public void GetDifferenceScore_ThrowsExceptionForDifferentLengthStringsAsync()
        {
            //Arrange
            var dest = "pomme";
            var src = "banane";
            var expectedResult = new ArgumentException("Les chaînes doivent avoir la même longueur.");

            _mockAmTheTest.Setup(repo => repo.GetDifferenceScore(dest, src))
                .Throws(expectedResult);

            //Assert
            Assert.Throws<ArgumentException>(() => _mockAmTheTest.Object.GetDifferenceScore(dest, src));

        }

        #endregion

    }
}