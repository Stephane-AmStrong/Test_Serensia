using Exercice_2.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exercice_2.Repository
{
    public class AmTheTest : IAmTheTest
    {
        public bool CheckSirenValidity(string siren)
        {
            if (string.IsNullOrWhiteSpace(siren) || siren.Length != 9 || !siren.All(char.IsDigit))
            {
                return false;
            }

            int sum = 0;

            for (int i = 0; i < 8; i++)
            {
                int weight = (i % 2 == 0) ? 1 : 2;
                int digit = int.Parse(siren[i].ToString()) * weight;
                sum += digit / 10 + digit % 10;
            }

            int controlDigit = (10 - (sum % 10)) % 10;

            return controlDigit == int.Parse(siren[8].ToString());
        }

        public string ComputeFullSiren(string sirenWithoutControlNumber)
        {
            if (string.IsNullOrWhiteSpace(sirenWithoutControlNumber) || sirenWithoutControlNumber.Length != 8 || !sirenWithoutControlNumber.All(char.IsDigit))
            {
                throw new ArgumentException("Invalid SIREN without control number.");
            }

            string siren = sirenWithoutControlNumber;
            int sum = 0;

            for (int i = 0; i < 8; i++)
            {
                int weight = (i % 2 == 0) ? 1 : 2;
                int digit = int.Parse(sirenWithoutControlNumber[i].ToString()) * weight;
                sum += digit / 10 + digit % 10;
            }

            int controlDigit = (10 - (sum % 10)) % 10;
            siren += controlDigit;

            return siren;
        }
    }
}
