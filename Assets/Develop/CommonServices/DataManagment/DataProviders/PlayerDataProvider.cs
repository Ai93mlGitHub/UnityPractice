using Assets.Develop.CommonServices.ConfigsManagement;
using Assets.Develop.CommonServices.Wallet;
using System;
using System.Collections.Generic;
using UnityEngine;

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
            Debug.Log("GetOriginData: Starting to initialize PlayerData.");

            var playerData = new PlayerData()
            {
                WalletData = InitWalletData(),
                CompletedLevels = new()
            };

            Debug.Log("GetOriginData: PlayerData initialization complete.");
            return playerData;
        }

        private Dictionary<CurrencyTypes, int> InitWalletData()
        {
            Debug.Log("InitWalletData: Starting initialization of wallet data.");
            Dictionary<CurrencyTypes, int> walletData = new();

            foreach (CurrencyTypes currencyType in Enum.GetValues(typeof(CurrencyTypes)))
            {
                Debug.Log($"InitWalletData: Processing currency type {currencyType}.");

                try
                {
                    int startValue = _configsProviderService.StartWalletConfig.GetStartValueFor(currencyType);
                    walletData.Add(currencyType, startValue);
                    Debug.Log($"InitWalletData: Added currency {currencyType} with start value {startValue}.");
                }
                catch (Exception ex)
                {
                    Debug.LogError($"InitWalletData: Error processing currency type {currencyType}: {ex.Message}");
                    throw;
                }
            }

            Debug.Log("InitWalletData: Wallet data initialization complete.");
            return walletData;
        }
    }
}
