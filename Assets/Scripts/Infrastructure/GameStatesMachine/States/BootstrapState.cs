using Cysharp.Threading.Tasks;
using Internal.Scripts.AssetManagement;
using Internal.Scripts.Infrastructure.Constants;
using Internal.Scripts.Infrastructure.Services.PlayerProgressService;
using Internal.Scripts.Infrastructure.Services.SaveLoad;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Internal.Scripts.Infrastructure.GameStatesMachine.States
{
    public class BootstrapState : IGameState, IInitializable, ILateDisposable
    {
        private readonly IGameStatesMachine _gameStatesMachine;
        private readonly IPersistentProgressService _persistentProgressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IAssetsProvider _assetsProvider;

        public BootstrapState(IGameStatesMachine gameStatesMachine,
            IPersistentProgressService persistentProgressService,
            ISaveLoadService saveLoadService,
            IAssetsProvider assetsProvider)
        {
            _gameStatesMachine = gameStatesMachine;
            _persistentProgressService = persistentProgressService;
            _saveLoadService = saveLoadService;
            _assetsProvider = assetsProvider;
        }
        
        void IInitializable.Initialize()
        {
            _gameStatesMachine.RegisterState<BootstrapState>(this);
        }

        void ILateDisposable.LateDispose()
        {
            _gameStatesMachine.UnRegisterState<BootstrapState>();
        }
        
        public async void Enter()
        {
            LoadProgress();

            await _assetsProvider.Initialize();
        
            AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(ScenesNames.MENU_SCENE_NAME);

            while (!loadSceneAsync.isDone)
                await UniTask.Yield();
        
            _gameStatesMachine.SetState<MenuState>();
        }

        public void Exit()
        {
            
        }
        
        private void LoadProgress()
        {
            PlayerProgress playerProgress = _saveLoadService.LoadProgress();
            if (playerProgress == null)
            {
                Debug.Log("PlayerProgress is null, Init New Progress");
                _persistentProgressService.InitNewProgress();
                return;
            }

            Debug.Log("PlayerProgress is loaded");
            _persistentProgressService.PlayerProgress = playerProgress;
        }
    }
}