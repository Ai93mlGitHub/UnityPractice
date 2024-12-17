using Assets.Develop.CommonServices.DataManagment;
using Assets.Develop.CommonServices.DataManagment.DataProviders;
using Assets.Develop.CommonServices.SceneManagment;
using Assets.Develop.DI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayBootstrap : MonoBehaviour
{
    private DIContainer _container;

    public IEnumerator Run(DIContainer container, GameplayInputArgs gameplayInputArgs)
    {
        Debug.Log("Начинаем запускать игровую сцену ");
        
        _container = container;

        ProcessRegistrations();

        yield return new WaitForSeconds(3f);
    }

    private void ProcessRegistrations()
    {
        //метод для регистрации сервисов 
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Переходим из игры в меню");

            _container.Resolve<SceneSwitcher>().ProseccSwitchSceneFor(new OutputGameplayArgs(new MainMenuInputArgs()));
        }

    }
}
