namespace Exercice_2.Repository
{
    public class AmTheTest : IAmTheTest
    {
        public bool CheckSirenValidity(string siren)
        {
            if (string.IsNullOrWhiteSpace(siren) || siren.Length != 9 || !siren.All(char.IsDigit)) return false;

            int controlDigit = CalculateControlDigit(siren[..8]);
            int lastDigit = int.Parse(siren[8].ToString());

            return controlDigit == lastDigit;
        }

        public string ComputeFullSiren(string sirenWithoutControlNumber)
        {
            if (string.IsNullOrWhiteSpace(sirenWithoutControlNumber) || sirenWithoutControlNumber.Length != 8 || !sirenWithoutControlNumber.All(char.IsDigit))
            {
                throw new ArgumentException("Invalid SIREN without control number.");
            }

            int controlDigit = CalculateControlDigit(sirenWithoutControlNumber);
            return sirenWithoutControlNumber + controlDigit;
        }

        private int CalculateControlDigit(string sirenDigits)
        {
            int sum = 0;

            for (int i = 0; i < 8; i++)
            {
                int weight = (i % 2 == 0) ? 1 : 2;
                int digit = int.Parse(sirenDigits[i].ToString()) * weight;
                sum += digit / 10 + digit % 10;
            }

            return (10 - (sum % 10)) % 10;
        }
    }
}
