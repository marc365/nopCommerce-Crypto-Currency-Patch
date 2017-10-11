using Nop.Core.Configuration;

namespace Nop.Plugin.Misc.CoinService
{
    public class CoinServiceSettings : ISettings
    {
        ///// <summary>
        ///// Gets or sets the list of cryptoi currency codes
        ///// </summary>
        public string SupportedCoins { get; set; }
    }
}
