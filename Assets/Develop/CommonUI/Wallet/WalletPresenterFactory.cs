using Assets.Develop.CommonServices.ConfigsManagement;
using Assets.Develop.CommonServices.Wallet;
using Assets.Develop.DI;

namespace Assets.Develop.CommonUI.Wallet
{
    public class WalletPresenterFactory
    {
        private WalletService _walletService;
        private ConfigsProviderService _configsProviderService;

        public WalletPresenterFactory(DIContainer container)
        {
            _walletService = container.Resolve<WalletService>();
            _configsProviderService = container.Resolve<ConfigsProviderService>();
        }

        public WalletPresenter CreateWalletPresenter(IconWithTextListView view)
            => new WalletPresenter(
                _walletService,
                view,
                this);

        public CurrencyPresenter CreateCurrencyPresenter(IconWithText view, CurrencyTypes currencyType)
            => new CurrencyPresenter(
                _walletService.GetCurrency(currencyType),
                currencyType,
                view,
                _configsProviderService.CurrencyIconsConfig);
    }
}