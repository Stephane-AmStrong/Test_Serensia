namespace Exercice_1.Contracts
{
    public interface IAmTheTest
    {
        // Example: GetSuggestions("gros", new List<string>(){"gros", "gras", "graisse", "agressif", "go"}, 2) returns an IEnumerable with ordered terms {"gros", "gras"}
        public IEnumerable<string> GetSuggestions(string term, IEnumerable<string> choices, int numberOfSuggestions);
        public int? GetDifferenceScore(string dest, string src);
    }
}
