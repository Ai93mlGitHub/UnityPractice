using System.Collections;
using System.Collections.Generic;

namespace Assets.Develop.CommonServices.LoadingScreen
{
    public interface ILoadingCurtain
    {
        bool IsShow { get; }

        void Show();

        void Hide();
    }
}