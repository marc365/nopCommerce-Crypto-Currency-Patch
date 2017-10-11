using Nop.Core.Domain.Directory;
using Nop.Core.Plugins;
using Nop.Services.Configuration;
using Nop.Services.Directory;
using System;

namespace Nop.Plugin.Misc.CoinService
{
    class CoinServicePlugin : BasePlugin
    {
        private readonly ICurrencyService _currencyService;
        private readonly ISettingService _settingService;

        public CoinServicePlugin(ICurrencyService currencyService, ISettingService settingService)
        {
            this._currencyService = currencyService;
            this._settingService = settingService;
        }

        /// <summary>
        /// Install plugin
        /// </summary>
        public override void Install()
        {
            //settings
            var settings = new CoinServiceSettings
            {
                SupportedCoins = "BTC,LTC,ETH,XMR,ZEC,DOGE,SIGT"
            };

            _settingService.SaveSetting(settings);

            //add currencies (the name and the code are used to identify the currency) 
            if (_currencyService.GetCurrencyByCode("BTC") == null)
            {
                Currency coin = new Currency()
                {
                    Name = "Bitcoin",
                    CurrencyCode = "BTC",
                    Rate = CoinMarketCapHelper.GetCoinMarketCapRate("BTC"),
                    Published = true,
                    CreatedOnUtc = DateTime.UtcNow,
                    UpdatedOnUtc = DateTime.UtcNow,
                    RoundingType = RoundingType.Rounding001,
                    CustomFormatting = "N8"
                };

                _currencyService.InsertCurrency(coin);
            }

            if (_currencyService.GetCurrencyByCode("LTC") == null)
            {
                Currency coin = new Currency()
                {
                    Name = "Litecoin",
                    CurrencyCode = "LTC",
                    Rate = CoinMarketCapHelper.GetCoinMarketCapRate("LTC"),
                    Published = true,
                    CreatedOnUtc = DateTime.UtcNow,
                    UpdatedOnUtc = DateTime.UtcNow,
                    RoundingType = RoundingType.Rounding001,
                    CustomFormatting = "N8"
                };

                _currencyService.InsertCurrency(coin);
            }

            if (_currencyService.GetCurrencyByCode("ETH") == null)
            {
                Currency coin = new Currency()
                {
                    Name = "Ethereum",
                    CurrencyCode = "ETH",
                    Rate = CoinMarketCapHelper.GetCoinMarketCapRate("ETH"),
                    Published = true,
                    CreatedOnUtc = DateTime.UtcNow,
                    UpdatedOnUtc = DateTime.UtcNow,
                    RoundingType = RoundingType.Rounding001,
                    CustomFormatting = "N8"
                };

                _currencyService.InsertCurrency(coin);
            }

            if (_currencyService.GetCurrencyByCode("XMR") == null)
            {
                Currency coin = new Currency()
                {
                    Name = "Monero",
                    CurrencyCode = "XMR",
                    Rate = CoinMarketCapHelper.GetCoinMarketCapRate("XMR"),
                    Published = true,
                    CreatedOnUtc = DateTime.UtcNow,
                    UpdatedOnUtc = DateTime.UtcNow,
                    RoundingType = RoundingType.Rounding001,
                    CustomFormatting = "N8"
                };

                _currencyService.InsertCurrency(coin);
            }

            if (_currencyService.GetCurrencyByCode("ZEC") == null)
            {
                Currency coin = new Currency()
                {
                    Name = "Zcash",
                    CurrencyCode = "ZEC",
                    Rate = CoinMarketCapHelper.GetCoinMarketCapRate("ZEC"),
                    Published = true,
                    CreatedOnUtc = DateTime.UtcNow,
                    UpdatedOnUtc = DateTime.UtcNow,
                    RoundingType = RoundingType.Rounding001,
                    CustomFormatting = "N8"
                };

                _currencyService.InsertCurrency(coin);
            }

            if (_currencyService.GetCurrencyByCode("DOGE") == null)
            {
                Currency coin = new Currency()
                {
                    Name = "Dogecoin",
                    CurrencyCode = "DOGE",
                    Rate = CoinMarketCapHelper.GetCoinMarketCapRate("DOGE"),
                    Published = true,
                    CreatedOnUtc = DateTime.UtcNow,
                    UpdatedOnUtc = DateTime.UtcNow,
                    RoundingType = RoundingType.Rounding001,
                    CustomFormatting = "N8"
                };

                _currencyService.InsertCurrency(coin);
            }

            if (_currencyService.GetCurrencyByCode("SIGT") == null)
            {
                Currency coin = new Currency()
                {
                    Name = "Signatum",
                    CurrencyCode = "SIGT",
                    Rate = CoinMarketCapHelper.GetCoinMarketCapRate("SIGT"),
                    Published = true,
                    CreatedOnUtc = DateTime.UtcNow,
                    UpdatedOnUtc = DateTime.UtcNow,
                    RoundingType = RoundingType.Rounding001,
                    CustomFormatting = "N8"
                };

                _currencyService.InsertCurrency(coin);
            }

            base.Install();
        }

        /// <summary>
        /// Uninstall plugin
        /// </summary>
        public override void Uninstall()
        {
            //settings
            _settingService.DeleteSetting<CoinServiceSettings>();

            //remove currencies
            if (_currencyService.GetCurrencyByCode("BTC") != null)
            {
                _currencyService.DeleteCurrency(_currencyService.GetCurrencyByCode("BTC"));
            }
            if (_currencyService.GetCurrencyByCode("LTC") != null)
            {
                _currencyService.DeleteCurrency(_currencyService.GetCurrencyByCode("LTC"));
            }
            if (_currencyService.GetCurrencyByCode("ETH") != null)
            {
                _currencyService.DeleteCurrency(_currencyService.GetCurrencyByCode("ETH"));
            }
            if (_currencyService.GetCurrencyByCode("XMR") != null)
            {
                _currencyService.DeleteCurrency(_currencyService.GetCurrencyByCode("XMR"));
            }
            if (_currencyService.GetCurrencyByCode("ZEC") != null)
            {
                _currencyService.DeleteCurrency(_currencyService.GetCurrencyByCode("ZEC"));
            }
            if (_currencyService.GetCurrencyByCode("DOGE") != null)
            {
                _currencyService.DeleteCurrency(_currencyService.GetCurrencyByCode("DOGE"));
            }
            if (_currencyService.GetCurrencyByCode("SIGT") != null)
            {
                _currencyService.DeleteCurrency(_currencyService.GetCurrencyByCode("SIGT"));
            }
            base.Uninstall();
        }

    }
}
