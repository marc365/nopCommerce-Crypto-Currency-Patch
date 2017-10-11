using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Directory;
using Nop.Core.Infrastructure;
using Nop.Services.Directory;
using System;
using System.Net;

namespace Nop.Plugin.Misc.CoinService
{
    /// <summary>
    /// CoinMarketCap Extensions
    /// </summary>
    public static class CoinMarketCapHelper
    {
        private static string GetCoinMarketCapRates(ICacheManager cacheManager, ICurrencyService currencyService, string Name)
        {
            WebClient client = new WebClient();
            
            return cacheManager.Get<string>(string.Format("Nop.crypto.coinbase.exchange-rates.{0}", Name), 300000, () => {
                    try
                    {
                        return client.DownloadString(string.Format("https://api.coinmarketcap.com/v1/ticker/{0}/", Name));
                    }
                    catch (Exception exc)
                    {
                        //TODO log error and/or give customer notification
                        throw new NopException(string.Format("Coin Market Cap exchange rate api error [{0}]", exc.Message));
                        //return string.Empty;
                    }
                }
            );
        }

        public static decimal GetCoinMarketCapRate(string code)
        {
            var currencyService = EngineContext.Current.ContainerManager.Resolve<ICurrencyService>();
            var currencySettings = EngineContext.Current.ContainerManager.Resolve<CurrencySettings>();
            var cacheManager = EngineContext.Current.ContainerManager.Resolve<ICacheManager>("nop_cache_static");

            var _primaryCurrency = currencyService.GetCurrencyById(currencySettings.PrimaryExchangeRateCurrencyId);

            var cryptoCurrency = currencyService.GetCurrencyByCode(code);

            if (cryptoCurrency!= null && !string.IsNullOrEmpty(cryptoCurrency.Name))
            {
                var _client = new WebClient();

                string rates = GetCoinMarketCapRates(cacheManager, currencyService, cryptoCurrency.Name.ToLower());

                //TODO json parse
                string[] tokens = rates.Split(',');
                try
                {
                
                    return 1 / decimal.Parse(tokens[4].Split(':')[1].Replace("/", "").Replace("\"", ""));
                }
                catch
                {                   
                }
            }

            return decimal.Zero;
        }
    }
}
