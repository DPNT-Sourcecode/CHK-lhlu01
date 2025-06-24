using BeFaster.Runner.Exceptions;

namespace BeFaster.App.Solutions.CHK
{
    public class CheckoutSolution
    {
        public int Checkout(string? skus)
        {
            // if empty return 0
            // if null return -1
            // maintain a dictionary to hold the SKU prices and special offers
            // maintain a dictionary to hold the SKU special offers counts
            // maintain a dictionary to hold the SKU counts
            // go through the skus string, count the occurrences of each SKU
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
