using System;
using System.Collections.Generic;

namespace Assets.Develop.CommonServices.DataManagment.DataProviders
{
    [Serializable]
    public class PlayerData : ISaveData
    {
        public int Money;
        public List<int> CompletedLevels;
    }
}