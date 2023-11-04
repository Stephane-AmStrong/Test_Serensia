using System;

namespace Exercice_1.Repository
{
    public class AmTheTest : IAmTheTest
    {
        public IEnumerable<string> GetSuggestions(string term, IEnumerable<string> choices, int numberOfSuggestions)
        {
            // Filtrer les termes qui ont une longueur suffisante pour être considérés comme similaires
            choices = choices.Where(c => c.Length >= term.Length).ToList();

            // Calculer les scores de différence pour chaque terme
            var scores = choices.ToDictionary(choice => choice, choice => GetDifferenceScore(term, choice));

            // Trier les termes par score de différence, en tenant compte du nombre de suggestions
            var sortedSuggestions = choices
                .Where(choice => scores[choice] is not null)
                .OrderBy(choice => scores[choice])
                .Take(numberOfSuggestions);

            return sortedSuggestions;
        }


        public int? GetDifferenceScore(string term, string choice)
        {
            if (!term.Any() || !choice.Any()) throw new ArgumentException("Le terme et le choix doivent avoir au moins un caractère.");

            int score = 0;
            int indexTerm = 0;
            int indexChoice = choice.IndexOf(term[indexTerm]);

            if (indexChoice < 0) return null;

            while (indexTerm < term.Length && indexChoice < choice.Length)
            {
                if (term[indexTerm] != choice[indexChoice]) score++;
                indexTerm++;
                indexChoice++;
            }

            return score;
        }
    }
}
