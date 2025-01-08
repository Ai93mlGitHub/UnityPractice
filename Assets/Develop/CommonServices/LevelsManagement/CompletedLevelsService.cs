using Assets.Develop.CommonServices.DataManagment.DataProviders;
using System.Collections.Generic;

namespace Assets.Develop.CommonServices.LevelsManagement
{
    public class CompletedLevelsService : IDataReader<PlayerData>, IDataWriter<PlayerData>
    {
        private List<int> _completedLevels;

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
            data.CompletedLevels.Clear();
            data.CompletedLevels.AddRange(_completedLevels);
        }
    }
}