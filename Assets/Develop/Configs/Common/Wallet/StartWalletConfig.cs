using Assets.Develop.CommonServices.Wallet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Develop.Configs.Common.Wallet
{
    [CreateAssetMenu(menuName = "Configs/Common/Wallet/NewStartWalletConfig", fileName = "StarWalletConfig")]
    public class StartWalletConfig : ScriptableObject
    {
        [SerializeField] private List<CurrencyConfig> _values;

        private void OnValidate()
        {
            //На буду щее для проверок настройки конфигов
            //Все ли элементы энама указаны
            //Проверка на дубликаты
        }

        public int GetStartValueFor(CurrencyTypes currencyType)
            => _values.First(config => config.Type == currencyType).Value;

        [Serializable]
        private class CurrencyConfig
        {
            [field: SerializeField] public CurrencyTypes Type { get; private set; }

            [field: SerializeField] public int Value { get; private set; }
        }
    }
}