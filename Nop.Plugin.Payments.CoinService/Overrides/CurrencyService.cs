using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Directory;
using Nop.Core.Infrastructure;
using Nop.Core.Plugins;
using Nop.Plugin.Misc.CoinService;
using Nop.Services.Events;
using Nop.Services.Stores;
using System;
using System.Linq;

namespace Nop.Services.Directory
{
    /// <summary>
    /// Currency service
    /// </summary>
    public partial class BitcoinCurrencyService : CurrencyService
    {
        #region Constants

        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : currency ID
        /// </remarks>
        private const string CURRENCIES_BY_ID_KEY = "Nop.currency.id-{0}";
        /// <summary>
        /// Key for caching
        /// </summary>
        /// <remarks>
        /// {0} : show hidden records?
        /// </remarks>
        private const string CURRENCIES_ALL_KEY = "Nop.currency.all-{0}";
        /// <summary>
        /// Key pattern to clear cache
        /// </summary>
        private const string CURRENCIES_PATTERN_KEY = "Nop.currency.";

        #endregion

        #region Fields

        private readonly IRepository<Currency> _currencyRepository;
        private readonly IStoreMappingService _storeMappingService;
        private readonly ICacheManager _cacheManager;
        private readonly CurrencySettings _currencySettings;
        private readonly IPluginFinder _pluginFinder;
        private readonly IEventPublisher _eventPublisher;

        #endregion

        #region Constructor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="cacheManager">Cache manager</param>
        /// <param name="currencyRepository">Currency repository</param>
        /// <param name="storeMappingService">Store mapping service</param>
        /// <param name="currencySettings">Currency settings</param>
        /// <param name="pluginFinder">Plugin finder</param>
        /// <param name="eventPublisher">Event published</param>
        public BitcoinCurrencyService(ICacheManager cacheManager,
            IRepository<Currency> currencyRepository,
            IStoreMappingService storeMappingService,
            CurrencySettings currencySettings,
            IPluginFinder pluginFinder,
            IEventPublisher eventPublisher): base(cacheManager, currencyRepository, storeMappingService, currencySettings, pluginFinder, eventPublisher)
        {
            this._cacheManager = cacheManager;
            this._currencyRepository = currencyRepository;
            this._storeMappingService = storeMappingService;
            this._currencySettings = currencySettings;
            this._pluginFinder = pluginFinder;
            this._eventPublisher = eventPublisher;
        }

        #endregion

        /// <summary>
        /// Converts from primary exchange rate currency
        /// </summary>
        /// <param name="amount">Amount</param>
        /// <param name="targetCurrencyCode">Target currency code</param>
        /// <returns>Converted value</returns>
        public override decimal ConvertFromPrimaryExchangeRateCurrency(decimal amount, Currency targetCurrencyCode)
        {
            if (targetCurrencyCode == null)
                throw new ArgumentNullException("targetCurrencyCode");

            var primaryExchangeRateCurrency = GetCurrencyById(_currencySettings.PrimaryExchangeRateCurrencyId);
            if (primaryExchangeRateCurrency == null)
                throw new Exception("Primary exchange rate currency cannot be loaded");

            decimal result = amount;
            if (result != decimal.Zero && targetCurrencyCode.Id != primaryExchangeRateCurrency.Id)
            {
                decimal exchangeRate = targetCurrencyCode.Rate;

                //START PATCH

                var coinServiceSettings = EngineContext.Current.ContainerManager.Resolve<CoinServiceSettings>();
                string[] Cryptos;

                if (coinServiceSettings.SupportedCoins != null)
                    Cryptos = coinServiceSettings.SupportedCoins.Split(',');
                else
                    Cryptos = new string[0];

                if (Cryptos.Contains(targetCurrencyCode.CurrencyCode))
                    exchangeRate = CoinMarketCapHelper.GetCoinMarketCapRate(targetCurrencyCode.CurrencyCode);
                else
                     exchangeRate = targetCurrencyCode.Rate;

                //END PATCH

                if (exchangeRate == decimal.Zero)
                    throw new NopException(string.Format("Exchange rate not found for currency [{0}]", targetCurrencyCode.Name));
                result = result * exchangeRate;
            }
            return result;
        }

    }
}