using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Develop.DI;
using Assets.Develop.CommonServices.LoadingScreen;
using Assets.Develop.CommonServices.SceneManagment;

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
            yield return new WaitForSeconds(1.5f); //�������� ������ ������������

            Debug.Log("����� ������������� ��������");

            loadingCurtain.Hide();

            sceneSwitcher.ProseccSwitchSceneFor(new OutputBootstrapArgs(new MainMenuInputArgs()));
        }
    }
}