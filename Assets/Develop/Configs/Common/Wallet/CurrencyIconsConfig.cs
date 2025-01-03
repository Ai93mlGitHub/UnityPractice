using Assets.Develop.CommonServices.Wallet;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Develop.Configs.Common.Wallet
{
    [CreateAssetMenu(menuName = "Configs/Common/Wallet/NewCurrencyIconsConfig", fileName = "CurrencyIconsConfig")]

    public class CurrencyIconsConfig : ScriptableObject
    {
        [SerializeField] private List<CurrensyIconConfig> _configs;

        private void OnValidate()
        {
            //����� ������� ��������
        }

        public Sprite GetSpriteFor(CurrencyTypes type)
            => _configs.First(config => config.CurrencyType == type).Sprite;

        [Serializable]
        private class CurrensyIconConfig
        {
            [field: SerializeField] public CurrencyTypes CurrencyType { get; private set; }
            [field: SerializeField] public Sprite Sprite { get; private set; }
        }
    }
}