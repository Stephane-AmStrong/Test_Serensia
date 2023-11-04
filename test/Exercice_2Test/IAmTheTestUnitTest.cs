using Exercice_2.Repository;
using Moq;

namespace Exercice_2Test
{
    public class IAmTheTestUnitTest
    {
        private readonly IAmTheTest _iAmTheTest;

        public IAmTheTestUnitTest()
        {
            _iAmTheTest = new AmTheTest();
        }

        [Theory]
        [InlineData("434189163", true)]
        [InlineData("732829320", true)]
        [InlineData("972487086", true)]
        [InlineData("987654322", false)]
        [InlineData("5432104", false)]
        [InlineData("12345678a", false)]
        public void CheckSirenValidity_ValidatesSirenCorrectly(string siren, bool expected)
        {
            bool isValid = _iAmTheTest.CheckSirenValidity(siren);

            Assert.Equal(expected, isValid);
        }

        [Theory]
        [InlineData("12345678", "123456782")]
        [InlineData("98765432", "987654324")]
        [InlineData("11112222", "111122222")]
        [InlineData("99998888", "999988884")]
        public void ComputeFullSiren_CalculatesFullSiren(string sirenWithoutControl, string expectedFullSiren)
        {
            string fullSiren = _iAmTheTest.ComputeFullSiren(sirenWithoutControl);

            Assert.Equal(expectedFullSiren, fullSiren);
        }

        [Theory]
        [InlineData("123")]
        [InlineData("45678")]
        [InlineData("5432104")]
        public void ComputeFullSiren_ThrowsExceptionForInvalidInput(string siren)
        {
            Assert.Throws<ArgumentException>(() => _iAmTheTest.ComputeFullSiren(siren));
        }
    }
}