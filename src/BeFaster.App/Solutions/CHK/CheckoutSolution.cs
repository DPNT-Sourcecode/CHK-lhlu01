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
            // the special priceOffers should be sorted descending by quantity
            // maintain a dictionary to hold the get free SKUs special offers quantities
            // maintain a dictionary to hold the cart SKU counts
            // maintain a dictionary to hold the cart SKU prices
            // go through the skus string
            // if an unknown SKU is found return -1
            // increment the SKU count in the SKU counts dictionary
            // calculate the SKU total price by:
            // use a while loop to keep trying to apply special offers until no more can be applied
            // use a current SKU count to keep track of the remaining SKU count
            // check if the SKU count has a special offer if the SKU count is greater than or equal to the special offer quantity
            // divide the SKU count by the special offer quantity
            // calculate the new total price for the SKU by multiplying the special offer price by the result of the floor division
            // update the current SKU count by taking the modulus of the SKU count and the special offer quantity
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
            var priceOffers = new Dictionary<string, List<(int, int)>>
            {
                { "A", new () { (5, 200), (3, 130) } },
                { "B", new () { (2, 45) } },
            };
            var getFreeOffers = new Dictionary<string, List<(string, int)>>
            {
                { "E", new () { ("B", 1) } },
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
                var currentCount = skuCounts[skuStr];
                int skuTotal = 0;
                if (priceOffers.TryGetValue(skuStr, out var skuOffers))
                {
                    foreach (var offer in skuOffers)
                    {
                        var offerCount = offer.Item1;
                        var offerPrice = offer.Item2;

                        if (currentCount >= offerCount)
                        {
                            skuTotal += currentCount / offerCount * offerPrice;
                            currentCount %= offerCount;
                        }
                    }
                }
                skuTotal += currentCount * skuPrices[skuStr];
                skuPrices[skuStr] = skuTotal;

                if (getFreeOffers.TryGetValue(skuStr, out var freeOffers))
                {
                    foreach (var offer in freeOffers)
                    {
                        var freeSku = offer.Item1;
                        var freeCount = offer.Item2;

                        if (skuCounts.ContainsKey(freeSku))
                        {
                            skuPrices[freeSku] -= Math.Max(freeCount * skuPrices[freeSku], 0);
                        }
                    }
                }
            }

            int total = 0;
            foreach (var sku in skuPrices)
            {
                var price = sku.Value;
                total += price;
            }

            return total;
        }
    }
}
