using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Develop.DI;
using Assets.Develop.CommonServices.LoadingScreen;
using Assets.Develop.CommonServices.SceneManagment;
using Assets.Develop.CommonServices.DataManagment.DataProviders;

namespace Assets.Develop.EntryPoint
{
    public class Bootstrap : MonoBehaviour
    {
        public IEnumerator Run(DIContainer container)
        {
            ILoadingCurtain loadingCurtain = container.Resolve<ILoadingCurtain>();

            SceneSwitcher sceneSwitcher = container.Resolve<SceneSwitcher>();   

            loadingCurtain.Show();

            Debug.Log("Начало инициализации сервисов");

            Debug.Log("До этого момента ошибки нет");
            container.Resolve<PlayerDataProvider>().Load();

            Debug.Log("Зарезолвиди дата провайдер");

            yield return new WaitForSeconds(1.5f); //имитация бурной деятельности

            Debug.Log("Конец инициализации сервисов");

            loadingCurtain.Hide();

            sceneSwitcher.ProseccSwitchSceneFor(new OutputBootstrapArgs(new GameplayInputArgs(1)));
        }
    }
}