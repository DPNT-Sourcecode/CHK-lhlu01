using BeFaster.Runner.Exceptions;

namespace BeFaster.App.Solutions.CHK
{
    public class CheckoutSolution
    {
        public int Checkout(string? skus)
        {
            throw new SolutionNotImplementedException();
            // if null or empty return -1
            // maintain a dictionary to hold the SKU prices and special offers
            // maintain a dictionary to hold the SKU special offers counts
            // maintain a dictionary to hold the SKU counts
            // go through the skus string, count the occurrences of each SKU
            // if an unknown SKU is found return -1
        }
    }
}

