using BeFaster.App.Solutions.CHK;
using NUnit.Framework;

namespace BeFaster.App.Tests.Solutions.CHK
{
    [TestFixture]
    public class CheckoutSolutionTest
    {
        [TestCase()]
        public int Checkout(string? skus)
        {
            return new CheckoutSolution().Checkout(skus);
        }
    }
}

