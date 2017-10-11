using System;
using System.Globalization;
using System.Linq;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Tax;
using Nop.Core.Infrastructure;
using Nop.Services.Directory;
using Nop.Services.Localization;
using Nop.Plugin.Misc.CoinService;

namespace Nop.Services.Catalog
{
    /// <summary>
    /// Price formatter
    /// </summary>
    public partial class BitcoinPriceFormatter : PriceFormatter
    {
        #region Fields

        private readonly IWorkContext _workContext;
        private readonly ICurrencyService _currencyService;
        private readonly ILocalizationService _localizationService;
        private readonly TaxSettings _taxSettings;
        private readonly CurrencySettings _currencySettings;

        #endregion

        #region Constructors

        public BitcoinPriceFormatter (IWorkContext workContext,
            ICurrencyService currencyService,
            ILocalizationService localizationService,
            TaxSettings taxSettings,
            CurrencySettings currencySettings): base(workContext, currencyService,localizationService,taxSettings,currencySettings)
        {
            this._workContext = workContext;
            this._currencyService = currencyService;
            this._localizationService = localizationService;
            this._taxSettings = taxSettings;
            this._currencySettings = currencySettings;
        }

        #endregion

        /// <summary>
        /// Formats the price
        /// </summary>
        /// <param name="price">Price</param>
        /// <param name="showCurrency">A value indicating whether to show a currency</param>
        /// <param name="targetCurrency">Target currency</param>
        /// <param name="language">Language</param>
        /// <param name="priceIncludesTax">A value indicating whether price includes tax</param>
        /// <param name="showTax">A value indicating whether to show tax suffix</param>
        /// <returns>Price</returns>
        public override string FormatPrice(decimal price, bool showCurrency, 
            Currency targetCurrency, Language language, bool priceIncludesTax, bool showTax)
        {
            //START PATCH
            var coinServiceSettings = EngineContext.Current.ContainerManager.Resolve<CoinServiceSettings>();
            string[] Cryptos;

            if (coinServiceSettings.SupportedCoins != null)
            {
                Cryptos = coinServiceSettings.SupportedCoins.Split(',');
            }
            else
            {
                Cryptos = new string[0];
            }

            if (!Cryptos.Contains(targetCurrency.CurrencyCode) )
            {
                //we should round it no matter of "ShoppingCartSettings.RoundPricesDuringCalculation" setting
                price = RoundingHelper.RoundPrice(price);
            }       
            //END PATCH

            string currencyString = GetCurrencyString(price, showCurrency, targetCurrency);
            if (showTax)
            {
                //show tax suffix
                string formatStr;
                if (priceIncludesTax)
                {
                    formatStr = _localizationService.GetResource("Products.InclTaxSuffix", language.Id, false);
                    if (String.IsNullOrEmpty(formatStr))
                        formatStr = "{0} incl tax";
                }
                else
                {
                    formatStr = _localizationService.GetResource("Products.ExclTaxSuffix", language.Id, false);
                    if (String.IsNullOrEmpty(formatStr))
                        formatStr = "{0} excl tax";
                }
                return string.Format(formatStr, currencyString);
            }
            
            return currencyString;
        }
    }
}
