using Cysharp.Threading.Tasks;
using Gameplay.LevelRefereeing;
using GameplayUI.GamePlay;
using Internal.Scripts.Infrastructure.Constants;
using Internal.Scripts.Infrastructure.Services.PlayerProgressService;
using Internal.Scripts.Infrastructure.Services.SaveLoad;
using Plugins.Antonoix.UISystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Internal.Scripts.Infrastructure.GameStatesMachine.States
{
    public class GamePlayState : IGameState, IInitializable, ILateDisposable
    {
        private readonly IGameStatesMachine _gameStatesMachine;
        private readonly IUiService _uiService;
        private readonly ILevelReferee _levelReferee;
        private readonly IPersistentProgressService _playerProgressService;
        private readonly ISaveLoadService _saveLoadService;
        private GamePlayUIPresenter _gameplayUiPresenter;

        private PlayerProgress PlayerProgress => _playerProgressService.PlayerProgress;
        
        public GamePlayState(IGameStatesMachine gameStatesMachine,
            IPersistentProgressService playerProgressService,
            ISaveLoadService saveLoadService,
            IUiService uiService,
            ILevelReferee levelReferee)
        {
            _gameStatesMachine = gameStatesMachine;
            _playerProgressService = playerProgressService;
            _saveLoadService = saveLoadService;
            _uiService = uiService;
            _levelReferee = levelReferee;
        }

        void IInitializable.Initialize()
        {
            _gameStatesMachine.RegisterState<GamePlayState>(this);
        }

        void ILateDisposable.LateDispose()
        {
            _gameStatesMachine.UnRegisterState<GamePlayState>();
        }

        public async void Enter()
        {
            _gameplayUiPresenter = await _uiService.GetPresenter<GamePlayUIPresenter>();
            
            _gameplayUiPresenter.Show();
            _levelReferee.StartLevel();
            _levelReferee.OnLevelPassed += HandleLevelPassed;
        }

        private void HandleLevelPassed()
        {
            _saveLoadService.SaveProgress();
            GoToWinState();
        }

        public void Exit()
        {
            _levelReferee.OnLevelPassed -= HandleLevelPassed;
            _gameplayUiPresenter.Hide();
        }

        private async void GoToWinState()
        {
            AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(ScenesNames.WIN_SCENE_NAME);

            while (!loadSceneAsync.isDone)
                await UniTask.Yield();
            
            _gameStatesMachine.SetState<WinState>();
        }
    }
}
