using Assets.Develop.CommonServices.AssetsManagment;
using Assets.Develop.CommonServices.DataManagment;
using Assets.Develop.CommonServices.DataManagment.DataProviders;
using Assets.Develop.CommonServices.SceneManagment;
using Assets.Develop.CommonServices.Wallet;
using Assets.Develop.CommonUI.Wallet;
using Assets.Develop.DI;
using Assets.Develop.MainMenu.UI;
using System.Collections;
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
        _container.RegisterAsSingle(c => new WalletPresenterFactory(c));

        //метод для регистрации сервисов 
        _container.RegisterAsSingle(c =>
        {
            MainMenuUIRoot mainMenuUIRootPrefab = c.Resolve<ResourcesAssetLoader>().LoadResource<MainMenuUIRoot>("MainMenu/UI/MainMenuUIRoot");
            return Instantiate(mainMenuUIRootPrefab);
        }).NonLazy();

        _container
            .RegisterAsSingle(c => c.Resolve<WalletPresenterFactory>()
            .CreateCurrencyPresenter(c.Resolve<MainMenuUIRoot>()._currencyView, CurrencyTypes.Gold))
            .NonLazy();

        _container.Initialize();
    }

    private CurrencyPresenter _currencyPresenter;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            _currencyPresenter?.Dispose();
            MainMenuUIRoot mainMenuUIRoot = _container.Resolve<MainMenuUIRoot>();
            _currencyPresenter = _container.Resolve<WalletPresenterFactory>().CreateCurrencyPresenter(mainMenuUIRoot._currencyView, CurrencyTypes.Gold);
            _currencyPresenter.Initialize();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            _currencyPresenter?.Dispose ();
            MainMenuUIRoot mainMenuUIRoot = _container.Resolve<MainMenuUIRoot>();
            _currencyPresenter = _container.Resolve<WalletPresenterFactory>().CreateCurrencyPresenter(mainMenuUIRoot._currencyView, CurrencyTypes.Diamond);
            _currencyPresenter.Initialize();
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            _container.Resolve<SceneSwitcher>().ProseccSwitchSceneFor(new OutputMainMenuArgs(new GameplayInputArgs(2)));
            
            Debug.Log("Переходим из меню в игру");
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            WalletService wallet = _container.Resolve<WalletService>();
            wallet.Add(CurrencyTypes.Gold, 100);
            Debug.Log($"Gold: {wallet.GetCurrency(CurrencyTypes.Gold).Value}");
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            _container.Resolve<PlayerDataProvider>().Save();
        }
    }
}
