using GameplayUI.GamePlay;
using GameplayUI.WinScreen;
using UnityEngine;
using Zenject;

namespace GameplayUI
{
    [CreateAssetMenu(fileName = nameof(UIExtrasInstaller), menuName = "Installers/" + nameof(UIExtrasInstaller))]
    public class UIExtrasInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private GamePlayUIConfig _gamePlayUIConfig;
        [SerializeField] private WinUIConfig _winUIConfig;

        public override void InstallBindings()
        {
            BindUIConfigs();
            BindUIFactories();
        }

        private void BindUIConfigs()
        {
            Container.BindInstance(_gamePlayUIConfig).AsSingle();
            Container.BindInstance(_winUIConfig).AsSingle();
        }

        private void BindUIFactories()
        {
            Container.BindInterfacesAndSelfTo<GameplayUIFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<WinUIFactory>().AsSingle();
        }
    }
}