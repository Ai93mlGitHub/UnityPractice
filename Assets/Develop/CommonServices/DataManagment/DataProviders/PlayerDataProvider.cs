using Assets.Develop.CommonServices.ConfigsManagement;
using Assets.Develop.CommonServices.Wallet;
using System;
using System.Collections.Generic;

namespace Assets.Develop.CommonServices.DataManagment.DataProviders
{
    public class PlayerDataProvider : DataProvider<PlayerData>
    {
        private ConfigsProviderService _configsProviderService;

        public PlayerDataProvider(
            ISaveLoadService saveLoadService,
            ConfigsProviderService configsProviderService) : base(saveLoadService)
        {
            _configsProviderService = configsProviderService;
        }

        protected override PlayerData GetOriginData()
        {
            return new PlayerData()
            {
                WalletData = InitWalletData(),
                CompletedLevels = new()
            };

        }

        private Dictionary<CurrencyTypes, int> InitWalletData()
        {
             Dictionary<CurrencyTypes, int> walletData = new();

            foreach (CurrencyTypes currencyType in Enum.GetValues(typeof(CurrencyTypes)))
                walletData.Add(currencyType, _configsProviderService.StartWalletConfig.GetStartValueFor(currencyType));

            return walletData;
        }
    }

}