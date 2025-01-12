using Assets.Develop.CommonServices.DataManagment.DataProviders;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Develop.CommonServices.LevelsManagement
{
    public class CompletedLevelsService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private List<int> _completedLevels = new();

        public CompletedLevelsService(PlayerDataProvider playerDataProvider)
        {
            playerDataProvider.RegisterReader(this);
            playerDataProvider.RegisterWriter(this);
        }

        public bool IsLevelCompleted(int levelNumber) => _completedLevels.Contains(levelNumber);

        public bool TryAddLevelToCompleted(int levelNumber)
        {
            if (IsLevelCompleted(levelNumber))
                return false;

            _completedLevels.Add(levelNumber);            
            return true;
        }
        public void ReadFrom(PlayerData data)
        {
            _completedLevels.Clear();
            _completedLevels.AddRange(data.CompletedLevels);
        }

        public void WriteTo(PlayerData data)
        {
            if (data.CompletedLevels == null)
            {
                Debug.LogError("CompletedLevels in PlayerData is null. Initializing with an empty list.");
                data.CompletedLevels = new List<int>();
            }

            if (_completedLevels == null)
            {
                Debug.LogError("_completedLevels is null. Ensure it's properly initialized.");
                return; // Либо выполните подходящую логику, если _completedLevels пустой
            }

            data.CompletedLevels.Clear();
            data.CompletedLevels.AddRange(_completedLevels);
        }
    }
}