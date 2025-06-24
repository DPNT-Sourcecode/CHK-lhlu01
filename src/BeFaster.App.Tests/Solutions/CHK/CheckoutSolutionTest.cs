using BeFaster.App.Solutions.CHK;
using NUnit.Framework;

namespace BeFaster.App.Tests.Solutions.CHK
{
    [TestFixture]
    public class CheckoutSolutionTest
    {
        [TestCase(null, ExpectedResult = -1)]
        [TestCase("", ExpectedResult = 0)]
        [TestCase("xyz", ExpectedResult = -1)]
        [TestCase("ABC", ExpectedResult = 100)]
        [TestCase("ABDABCCA", ExpectedResult = 230)]
        [TestCase("BBy", ExpectedResult = -1)]
        [TestCase("ABACADADAEA", ExpectedResult = 370)]
        [TestCase("BBEE", ExpectedResult = 110)]
        [TestCase("BEE", ExpectedResult = 80)]
        [TestCase("EEEB", ExpectedResult = 120)]
        [TestCase("EEEEBB", ExpectedResult = 160)]
        [TestCase("DFFF", ExpectedResult = 35)]
        [TestCase("CFFFF", ExpectedResult = 50)]
        [TestCase("CFFFFF", ExpectedResult = 60)]
        [TestCase("CFFFFFF", ExpectedResult = 60)]
        [TestCase("STXZ", ExpectedResult = 62)]
        [TestCase("ZZXZ", ExpectedResult = 62)]
        [TestCase("ZZSS", ExpectedResult = 65)]
        [TestCase("ZZSSSSS", ExpectedResult = 110)]
        [TestCase("STXSTX", ExpectedResult = 90)]
        [TestCase("STXSTX", ExpectedResult = 90)]
        [TestCase("ABCDEFGHIJKLMNOPQRSTUVWXYZABCDEFGHIJKLMNOPQRSTUVWXYZ", ExpectedResult = 1602)]
        [TestCase("LGCKAQXFOSKZGIWHNRNDITVBUUEOZXPYAVFDEPTBMQLYJRSMJCWH", ExpectedResult = 1602)]
        public int Checkout(string? skus)
        {
            return new CheckoutSolution().Checkout(skus);
        }
    }
}
