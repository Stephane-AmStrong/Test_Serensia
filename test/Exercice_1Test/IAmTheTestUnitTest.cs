namespace Exercice_1Test
{
    public class IAmTheTestUnitTest
    {
        private readonly IAmTheTest _repo;

        public IAmTheTestUnitTest()
        {
            _repo = new AmTheTest();
        }

        public static TheoryData<string, IEnumerable<string>, int, IEnumerable<string>> TestData()
        {
            var data = new TheoryData<string, IEnumerable<string>, int, IEnumerable<string>>();

            data.Add("gros", new List<string> { "gros", "gras", "graisse", "agressif", "go" }, 2, new List<string> { "gros", "gras" });
            data.Add("gros", new List<string> { "gros", "gras", "graisse", "agressif", "go" }, 3, new List<string> { "gros", "gras", "agressif" });
            return data;
        }



        [Theory]
        [MemberData(nameof(TestData))]
        public void GetSuggestions_ReturnsMatchingSuggestions(string term, IEnumerable<string> choices, int numberOfSuggestions, IEnumerable<string> expectedResult)
        {
            //Arrange


            //Act
            var suggestions = _repo.GetSuggestions(term, choices, numberOfSuggestions);

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

            //Act
            var suggestions = _repo.GetSuggestions(term, choices, numberOfSuggestions);

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

            //Act
            var suggestions = _repo.GetSuggestions(term, choices, numberOfSuggestions);

            //Assert
            // Aucune suggestion ne doit être trouvée
            Assert.Empty(suggestions);
        }

        [Fact]
        public void GetDifferenceScore_ReturnsZeroForIdenticalStrings()
        {
            //Arrange
            var term = "pomme";
            var choice = "pomme";
            var expectedResult = 0;

            //Act
            var score = _repo.GetDifferenceScore(term, choice);

            //Assert
            Assert.Equal(expectedResult, score);
        }

        [Fact]
        public void GetDifferenceScore_ReturnsCorrectScoreForDifferentStrings()
        {
            //Arrange
            var term = "gros";
            var choice = "gras";
            var expectedResult = 1;

            //Act
            var score = _repo.GetDifferenceScore(term, choice);

            //Assert
            // Les caractères 'o' et 'a' diffèrent, le score doit être 1
            Assert.Equal(expectedResult, score);
        }

        [Theory]
        [InlineData("", "")]
        [InlineData("pomme", "")]
        [InlineData("", "pomme")]
        public void GetDifferenceScore_ThrowsExceptionForDifferentLengthStringsAsync(string term, string choice)
        {
            //Assert
            Assert.Throws<ArgumentException>(() => _repo.GetDifferenceScore(term, choice));

        }

    }
}