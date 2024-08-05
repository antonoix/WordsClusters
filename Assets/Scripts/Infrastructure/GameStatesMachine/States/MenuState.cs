using Cysharp.Threading.Tasks;
using GameplayUI.MenuUI;
using Internal.Scripts.Infrastructure.Constants;
using Plugins.Antonoix.UISystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Internal.Scripts.Infrastructure.GameStatesMachine.States
{
    public class MenuState : IGameState, IInitializable, ILateDisposable
    {
        private readonly IGameStatesMachine _gameStatesMachine;
        private readonly IUiService _uiService;
        private MenuUIPresenter _menuUIPresenter;

        public MenuState(IGameStatesMachine gameStatesMachine, IUiService uiService)
        {
            _gameStatesMachine = gameStatesMachine;
            _uiService = uiService;
        }

        void IInitializable.Initialize()
        {
            _gameStatesMachine.RegisterState<MenuState>(this);
        }

        void ILateDisposable.LateDispose()
        {
            _gameStatesMachine.UnRegisterState<MenuState>();
        }

        public async void Enter()
        {
            _menuUIPresenter = await _uiService.GetPresenter<MenuUIPresenter>();
            
            _menuUIPresenter.Show();
            _menuUIPresenter.OnStartBtnClicked += HandleStartBtnClick;
        }

        public void Exit()
        {
            _menuUIPresenter.Hide();
            
            _menuUIPresenter.OnStartBtnClicked -= HandleStartBtnClick;
        }

        private async void HandleStartBtnClick()
        {
            AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(ScenesNames.GAMEPLAY_SCENE_NAME);

            while (!loadSceneAsync.isDone)
                await UniTask.Yield();
            
            _gameStatesMachine.SetState<GamePlayState>();
        }
    }
}
