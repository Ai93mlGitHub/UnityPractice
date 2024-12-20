namespace Assets.Develop.CommonServices.DataManagment.DataProviders
{
    public class PlayerDataProvider : DataProvider<PlayerData>
    {
        public PlayerDataProvider(ISaveLoadService saveLoadService) : base(saveLoadService)
        {

        }

        protected override PlayerData GetOriginData()
        {
            return new PlayerData()
            {
                Money = 0,
                CompletedLevels = new()
            };
        }
    }

}