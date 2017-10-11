# nopCommerce-Crypto-Currency-Patch
This patches the inner accounting functions of nopCommerce - it works like magic and enables the use of normal nopCommerce Payment Provider Plugins that talk to the Crypto Currency API's, without the patch it is not possible - anyone can now create an API driven crypto currency Payment Provider for nopCommerce.

It has been developed over time by watching how crypto graphic currencies work - it gives up to the minute pricing, it gets updated much more frequently than regular currencies, it uses its own method for this so doesn't interfere with normal store activity. There are a few currencies included and it instantly shows pricing for products in bitcoin when installed- it is designed to support all crypto graphic currencies.

Further currencies are better created in code to get the details correct.


```csharp
            //add currency
            if (_currencyService.GetCurrencyByCode("BTC") == null)
            {
                Currency bitcoin = new Currency()
                {
                    Name = "Bitcoin",
                    CurrencyCode = "BTC",
                    
                    Rate = CoinbaseHelper.GetBitcoinRate(),
                    Published = true,
                    CreatedOnUtc = DateTime.UtcNow,
                    UpdatedOnUtc = DateTime.UtcNow,
                    RoundingType = RoundingType.Rounding001,
                    CustomFormatting = "N8"
                };

                _currencyService.InsertCurrency(bitcoin);
            }
```
