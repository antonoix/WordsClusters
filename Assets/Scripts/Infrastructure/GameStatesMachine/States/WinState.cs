using Cysharp.Threading.Tasks;
using GameplayUI.WinScreen;
using Internal.Scripts.Infrastructure.Constants;
using Plugins.Antonoix.UISystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Internal.Scripts.Infrastructure.GameStatesMachine.States
{
    public class WinState : IGameState, IInitializable, ILateDisposable
    {
        private readonly IGameStatesMachine _gameStatesMachine;
        private readonly IUiService _uiService;

        private WinUIPresenter _winUIPresenter;

        public WinState(IGameStatesMachine gameStatesMachine, IUiService uiService)
        {
            _gameStatesMachine = gameStatesMachine;
            _uiService = uiService;
        }

        void IInitializable.Initialize()
        {
            _gameStatesMachine.RegisterState<WinState>(this);
        }

        void ILateDisposable.LateDispose()
        {
            _gameStatesMachine.UnRegisterState<WinState>();
        }

        public async void Enter()
        {
            _winUIPresenter = await _uiService.GetPresenter<WinUIPresenter>();
            _winUIPresenter.OnMenuButtonClicked += GoToMenu;
            
            _winUIPresenter.Show();
        }

        public void Exit()
        {
            _winUIPresenter.OnMenuButtonClicked -= GoToMenu;
            
            _winUIPresenter.Hide();
        }

        private async void GoToMenu()
        {
            AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(ScenesNames.MENU_SCENE_NAME);

            while (!loadSceneAsync.isDone)
                await UniTask.Yield();
            
            _gameStatesMachine.SetState<MenuState>();
        }
    }
}