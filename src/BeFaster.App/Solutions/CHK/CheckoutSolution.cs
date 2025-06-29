﻿using BeFaster.Runner.Exceptions;

namespace BeFaster.App.Solutions.CHK
{
    public class CheckoutSolution
    {
        private readonly Dictionary<string, int> _skuPrices = new()
        {
            { "A", 50 },
            { "B", 30 },
            { "C", 20 },
            { "D", 15 },
            { "E", 40 },
            { "F", 10 },
            { "G", 20 },
            { "H", 10 },
            { "I", 35 },
            { "J", 60 },
            { "K", 70 },
            { "L", 90 },
            { "M", 15 },
            { "N", 40 },
            { "O", 10 },
            { "P", 50 },
            { "Q", 30 },
            { "R", 50 },
            { "S", 20 },
            { "T", 20 },
            { "U", 40 },
            { "V", 50 },
            { "W", 20 },
            { "X", 17 },
            { "Y", 20 },
            { "Z", 21 }
        };
        private readonly Dictionary<string, List<(int, int)>> _priceOffers = new()
        {
            { "A", new () { (5, 200), (3, 130) } },
            { "B", new () { (2, 45) } },
            { "H", new () { (10, 80), (5, 45) } },
            { "K", new () { (2, 120) } },
            { "P", new () { (5, 200) } },
            { "Q", new () { (3, 80) } },
            { "V", new () { (3, 130), (2, 90) } },
        };
        private readonly Dictionary<string, List<(int, (string, int))>> _getFreeOffers = new()
        {
            { "E", new () { (2, ("B", 1)) } },
            { "F", new () { (2, ("F", 1)) } },
            { "N", new () { (3, ("M", 1)) } },
            { "R", new () { (3, ("Q", 1)) } },
            { "U", new () { (3, ("U", 1)) } },
        };
        private readonly Dictionary<string, (int, int)> _groupOffers = new()
        {
            { "ZSTYX", (3, 45) }
        };

        public int Checkout(string? skus)
        {
            // if empty return 0
            // if null return -1
            // maintain a dictionary to hold the SKU prices
            // maintain a dictionary to hold the SKU special offers quantities and prices
            // the special priceOffers should be sorted descending by quantity
            // maintain a dictionary to hold the get free SKUs special offers quantities
            // maintain a dictionary to hold the cart SKU counts
            // maintain a dictionary to hold the cart SKU totals
            // go through the skus string
            // if an unknown SKU is found return -1
            // increment the SKU count in the SKU counts dictionary
            // calculate the SKU total price by:
            // use a for loop to keep trying to apply special offers until no more can be applied
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

            var skuCounts = new Dictionary<string, int>();
            var skuTotals = new Dictionary<string, int>();

            foreach (var sku in skus)
            {
                var skuStr = sku.ToString();
                if (!_skuPrices.ContainsKey(skuStr))
                {
                    return -1;
                }

                skuCounts[skuStr] = skuCounts.GetValueOrDefault(skuStr, 0) + 1;
                skuTotals[skuStr] = CalculateSkuTotal(skuStr, skuCounts[skuStr]);
            }

            ApplyGetFreeOffers(skuCounts, skuTotals);
            ApplyGroupOffers(skuCounts, skuTotals);
            return skuTotals.Values.Sum();
        }

        private int CalculateSkuTotal(string skuStr, int currentCount)
        {
            int skuTotal = 0;
            if (_priceOffers.TryGetValue(skuStr, out var skuOffers))
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
            skuTotal += currentCount * _skuPrices[skuStr];
            return skuTotal;
        }

        private void ApplyGetFreeOffers(Dictionary<string, int> skuCounts, Dictionary<string, int> skuTotals)
        {
            foreach (var sku in skuCounts)
            {
                var skuStr = sku.Key;
                var skuCount = sku.Value;
                if (_getFreeOffers.TryGetValue(skuStr, out var freeOffers))
                {
                    foreach (var offer in freeOffers)
                    {
                        var requiredCountForOffer = offer.Item1;
                        var freeSku = offer.Item2.Item1;
                        var freeCount = offer.Item2.Item2;

                        while (skuCount >= requiredCountForOffer && skuCounts.ContainsKey(freeSku))
                        {
                            skuCount -= requiredCountForOffer;
                            if (freeSku == skuStr)
                            {
                                skuCount -= freeCount;
                                if (skuCount < 0)
                                {
                                    break;
                                }
                            }
                            skuCounts[freeSku] = Math.Max(skuCounts[freeSku] - freeCount, 0);
                            skuTotals[freeSku] = CalculateSkuTotal(freeSku, skuCounts[freeSku]);
                        }
                    }
                }
            }
        }

        private void ApplyGroupOffers(Dictionary<string, int> skuCounts, Dictionary<string, int> skuTotals)
        { 
            foreach (var groupOffer in _groupOffers)
            {
                var groupSkus = groupOffer.Key;
                var requiredCount = groupOffer.Value.Item1;
                var offerPrice = groupOffer.Value.Item2;
                var offerPricePerSku = offerPrice / requiredCount;

                var currentSkuCounts = new Dictionary<string, int>();
                foreach (var sku in groupSkus)
                {
                    var skuStr = sku.ToString();
                    if (skuCounts.TryGetValue(skuStr, out var count))
                    {
                        currentSkuCounts[skuStr] = currentSkuCounts.GetValueOrDefault(skuStr, 0) + count;
                        var numberOfGroupOffers = currentSkuCounts.Values.Sum() / requiredCount;
                        if (numberOfGroupOffers > 0)
                        {
                            var numberOfSkusToUpdate = numberOfGroupOffers * requiredCount;
                            foreach (var groupSku in currentSkuCounts)
                            {
                                var groupSkuStr = groupSku.Key;
                                var groupSkuCount = groupSku.Value;
                                if (groupSkuCount == 0)
                                {
                                    continue;
                                }
                                if (groupSkuCount <= numberOfSkusToUpdate)
                                {
                                    skuTotals[groupSkuStr] = skuCounts[groupSkuStr] * offerPricePerSku;
                                    numberOfSkusToUpdate -= groupSkuCount;
                                    currentSkuCounts[groupSkuStr] = 0;
                                }
                                else
                                {
                                    skuTotals[groupSkuStr] = numberOfSkusToUpdate * offerPricePerSku +
                                        (groupSkuCount - numberOfSkusToUpdate) * _skuPrices[groupSkuStr];
                                    currentSkuCounts[groupSkuStr] -= numberOfSkusToUpdate;
                                    break;
                                }

                            }
                        }
                    }
                }
            }
                    
        }
    }
}


