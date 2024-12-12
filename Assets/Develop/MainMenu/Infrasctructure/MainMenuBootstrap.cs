using Assets.Develop.CommonServices.SceneManagment;
using Assets.Develop.DI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBootstrap : MonoBehaviour
{
    private DIContainer _container;

    public IEnumerator Run(DIContainer container, MainMenuInputArgs mainMenuInputArgs)
    {
        _container = container;

        ProcessRegistrations();

        yield return new WaitForSeconds(3f);
    }

    private void ProcessRegistrations()
    {
        //����� ��� ����������� �������� 
    }

    private void Start()
    {
        Debug.Log("����� ���� ���������");
    }

    private void Update()
    {
            
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _container.Resolve<SceneSwitcher>().ProseccSwitchSceneFor(new OutputMainMenuArgs(new GameplayInputArgs(2)));
            
            Debug.Log("��������� �� ���� � ����");
        }
    }
}
