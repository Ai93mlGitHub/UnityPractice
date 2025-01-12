using System.Collections;
using UnityEngine;
using Assets.Develop.DI;
using Assets.Develop.CommonServices.LoadingScreen;
using Assets.Develop.CommonServices.SceneManagment;
using Assets.Develop.CommonServices.DataManagment.DataProviders;
using Assets.Develop.CommonServices.ConfigsManagement;

namespace Assets.Develop.EntryPoint
{
    public class Bootstrap : MonoBehaviour
    {
        public IEnumerator Run(DIContainer container)
        {
            ILoadingCurtain loadingCurtain = container.Resolve<ILoadingCurtain>();

            SceneSwitcher sceneSwitcher = container.Resolve<SceneSwitcher>();   

            loadingCurtain.Show();

            Debug.Log("������ ������������� ��������");

            container.Resolve<ConfigsProviderService>().LoadAll();
            container.Resolve<PlayerDataProvider>().Load();

            Debug.Log("����������� ���� ���������");

            yield return new WaitForSeconds(1.5f); //�������� ������ ������������

            Debug.Log("����� ������������� ��������");

            loadingCurtain.Hide();

            sceneSwitcher.ProseccSwitchSceneFor(new OutputBootstrapArgs(new GameplayInputArgs(1)));
        }
    }
}