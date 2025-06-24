using BeFaster.Runner.Exceptions;

namespace BeFaster.App.Solutions.CHK
{
    public class CheckoutSolution
    {
        public int Checkout(string? skus)
        {
            // if empty return 0
            // if null return -1
            // maintain a dictionary to hold the SKU prices
            // maintain a dictionary to hold the SKU special offers counts
            // the special offers should be sorted descending by quantity
            // maintain a dictionary to hold the cart SKU counts
            // go through the skus string, count the occurrences of each SKU
            // calculate the total price by:
            // floor division of the SKU count by the special offer quantity
            // if the result is greater than 0 multiply it by the special offer price
            // if the result is 0, move on to the next special offer
            // if no special offer is found, multiply the SKU count by the SKU price
            // if an unknown SKU is found return -1

            if (skus == null)
            {
                return -1;
            }
            if (skus.Length == 0)
            {
                return 0;
            }
            var skuPrices = new Dictionary<string, int>
            {
                { "A", 50 },
                { "B", 30 },
                { "C", 20 },
                { "D", 15 },
            };
            var offersPrices = new Dictionary<string, int>
            {
                { "A", 130 },
                { "B", 45 },
            };
            var offersQuantity = new Dictionary<string, int>
            {
                { "A", 3 },
                { "B", 2 },
            };
            var skuCounts = new Dictionary<string, int>();
            var appliedOffers = new Dictionary<string, int>();
            foreach (var sku in skus)
            {
                var skuStr = sku.ToString();
                if (!skuPrices.ContainsKey(skuStr))
                {
                    return -1;
                }
                skuCounts[skuStr] = skuCounts.GetValueOrDefault(skuStr, 0) + 1;
                if (offersQuantity.ContainsKey(skuStr) && skuCounts[skuStr] == offersQuantity[skuStr])
                {
                    appliedOffers[skuStr] = appliedOffers.GetValueOrDefault(skuStr, 0) + 1;
                    skuCounts[skuStr] = 0;
                }
            }
            int total = 0;
            foreach (var sku in skuCounts)
            {
                var skuStr = sku.Key;
                var count = sku.Value;
                total += count * skuPrices[skuStr];
                total += appliedOffers.GetValueOrDefault(skuStr, 0) * offersPrices.GetValueOrDefault(skuStr, 0);
            }
            return total;
        }
    }
}

