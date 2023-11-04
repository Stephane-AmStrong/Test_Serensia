using Exercice_1.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            // Trier les termes par score de différence, puis par longueur, puis par ordre alphabétique
            var sortedSuggestions = choices
                .OrderBy(choice => scores[choice])
                .ThenBy(choice => choice.Length)
                .ThenBy(choice => choice)
                .Take(numberOfSuggestions);

            return sortedSuggestions;
        }

        public int GetDifferenceScore(string dest, string src)
        {
            if (dest.Length != src.Length) throw new ArgumentException("Les chaînes doivent avoir la même longueur.");

            int score = 0;
            for (int i = 0; i < dest.Length; i++)
            {
                if (dest[i] != src[i]) score++;
            }

            return score;
        }
    }
}
