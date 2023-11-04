namespace Exercice_2.Contracts
{
    public interface IAmTheTest
    {
        bool CheckSirenValidity(string siren);
        string ComputeFullSiren(string sirenWithoutControlNumber);
    }
}
