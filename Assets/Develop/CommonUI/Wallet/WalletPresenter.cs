using Assets.Develop.CommonServices.Wallet;
using Assets.Develop.Configs.Common.Wallet;
using Assets.Develop.DI;
using System;
using System.Collections.Generic;

namespace Assets.Develop.CommonUI.Wallet
{
    public class WalletPresenter : IInitializable, IDisposable
    {
        //MVP

        //Model
        private WalletService _walletService;
        private WalletPresenterFactory _factory;

        private List<CurrencyPresenter> _currencyPresenters = new();

        //Viewer
        private IconWithTextListView _view;

        public WalletPresenter(WalletService walletService, IconWithTextListView view, WalletPresenterFactory factory)
        {
            _walletService = walletService;
            _view = view;
            _factory = factory;
        }

        public void Initialize()
        {
            foreach (CurrencyTypes currencyType in _walletService.AvailableCurrencies)
            {
                IconWithText currencyView = _view.SpawnElement();

                CurrencyPresenter currencyPresenter = _factory.CreateCurrencyPresenter(currencyView, currencyType);

                currencyPresenter.Initialize();
                _currencyPresenters.Add(currencyPresenter);
            }
        }

        public void Dispose()
        {
            foreach (CurrencyPresenter currencyPresenter in _currencyPresenters)
            {
                _view.Remove(currencyPresenter.View);
                currencyPresenter.Dispose();
            }

            _currencyPresenters.Clear();
        }

    }
}