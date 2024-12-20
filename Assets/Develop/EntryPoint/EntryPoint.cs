using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Develop.DI;
using Assets.Develop.CommonServices.AssetsManagment;
using Assets.Develop.CommonServices.CoroutinePerformer;
using Assets.Develop.CommonServices.LoadingScreen;
using Assets.Develop.CommonServices.SceneManagment;
using Assets.Develop.CommonServices.DataManagment;
using Assets.Develop.CommonServices.DataManagment.DataProviders;

namespace Assets.Develop.EntryPoint
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private Bootstrap _gameBootstrap;

        private void Awake()
        {
            SetupAppSettings();

            DIContainer projectContainer = new DIContainer();


            //регистрации
            RegisterResourcesAsseetLoader(projectContainer);
            RegisterCoroutinePerformer(projectContainer);

            RegisterLoadingCourtain(projectContainer);
            RegisterSceneLoader(projectContainer);
            RegisterSceneSwitcher(projectContainer);

            RegisterSaveLoadService(projectContainer);
            RegisterPlayerDataProvider(projectContainer);


            //
            projectContainer.Resolve<ICoroutinePerformer>().StartPerform(_gameBootstrap.Run(projectContainer));
        }

        private void SetupAppSettings()
        {
            QualitySettings.vSyncCount = 0;
            Application.targetFrameRate = 144;
        }

        private void RegisterPlayerDataProvider(DIContainer container)
            => container.RegisterAsSingle(c => new PlayerDataProvider(c.Resolve<ISaveLoadService>()));

        private void RegisterSaveLoadService(DIContainer container)
            => container.RegisterAsSingle<ISaveLoadService>(c
                => new SaveLoadService(new JsonSerializer(), new LocalDataRepository())); 


        private void RegisterSceneSwitcher(DIContainer container) 
            => container.RegisterAsSingle(c 
                => new SceneSwitcher(
                    c.Resolve<ICoroutinePerformer>(),
                    c.Resolve<ILoadingCurtain>(),
                    c.Resolve<ISceneLoader>(),
                    c));

        private void RegisterResourcesAsseetLoader(DIContainer container) => 
            container.RegisterAsSingle(c => new ResourcesAssetLoader());

        private void RegisterCoroutinePerformer(DIContainer container) =>
            container.RegisterAsSingle<ICoroutinePerformer>(c => 
                {
                    ResourcesAssetLoader resourcesAssetLoader = c.Resolve<ResourcesAssetLoader>();

                    CoroutinePerformer coroutinePerformer = resourcesAssetLoader
                    .LoadResource<CoroutinePerformer>(InfrastructureAssetPaths.CoroutinePerformerPath);

                    return Instantiate(coroutinePerformer);
                });

        private void RegisterLoadingCourtain(DIContainer container)
        { 
            container.RegisterAsSingle<ILoadingCurtain>(c =>
            {
                ResourcesAssetLoader resourcesAssetLoader = c.Resolve<ResourcesAssetLoader>();

                StandartLoadingCurtain standartLoadingCurtain = resourcesAssetLoader
                .LoadResource<StandartLoadingCurtain>(InfrastructureAssetPaths.LoadingCurtainPath);

                return Instantiate(standartLoadingCurtain);
            });
        }

        private void RegisterSceneLoader(DIContainer container) =>
            container.RegisterAsSingle<ISceneLoader>(c => new SceneLoader());


    }
}