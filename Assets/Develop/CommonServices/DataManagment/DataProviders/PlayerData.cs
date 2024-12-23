using Assets.Develop.CommonServices.Wallet;
using System;
using System.Collections.Generic;

namespace Assets.Develop.CommonServices.DataManagment.DataProviders
{
    [Serializable]
    public class PlayerData : ISaveData
    {
        public Dictionary<CurrencyTypes, int> WalletData;
    }
}