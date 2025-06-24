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
            // maintain a dictionary to hold the SKU special offers quantities and prices
            // the special offers should be sorted descending by quantity
            // maintain a dictionary to hold the cart SKU counts
            // go through the skus string
            // if an unknown SKU is found return -1
            // calculate the total price by:
            // floor division of the SKU count by the special offer quantity
            // if the result is greater than 0 multiply it by the special offer price, update the SKU count
            // if the result is 0, move on to the next special offer
            // if no special offer is found, multiply the SKU count by the SKU price

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
                { "E", 40 },
            };
            var offers = new Dictionary<string, List<(int, int)>>
            {
                { "A", new () { (5, 200), (3, 130) } },
                { "B", new () { (2, 45) } },
            };
            var skuCounts = new Dictionary<string, int>();

            foreach (var sku in skus)
            {
                var skuStr = sku.ToString();
                if (!skuPrices.ContainsKey(skuStr))
                {
                    return -1;
                }
                skuCounts[skuStr] = skuCounts.GetValueOrDefault(skuStr, 0) + 1;
            }

            int total = 0;
            foreach (var sku in skuCounts)
            {
                var skuStr = sku.Key;
                var count = sku.Value;
                if (offers.ContainsKey(skuStr))
                {
                    foreach (var offer in offers[skuStr])
                    {
                        var offerCount = offer.Item1;
                        var offerPrice = offer.Item2;

                        if (count >= offerCount)
                        {
                            total += count / offerCount * offerPrice;
                            count %= offerCount;
                        }
                    }
                }

                total += count * skuPrices[skuStr];
            }
            return total;
        }
    }
}

