namespace Assets.Develop.CommonServices.LoadingScreen
{
    public interface ILoadingCurtain
    {
        bool IsShow { get; }

        void Show();

        void Hide();
    }
}