using Cysharp.Threading.Tasks;
using Internal.Scripts.AssetManagement;
using Internal.Scripts.Infrastructure.Constants;
using Internal.Scripts.Infrastructure.GameStatesMachine;
using Internal.Scripts.Infrastructure.Services.PlayerProgressService;
using Internal.Scripts.Infrastructure.Services.SaveLoad;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Internal.Scripts.Infrastructure.CompositionRoot
{
    public class GameBootstrap : MonoBehaviour
    {
        private IGameStatesMachine _gameStatesMachine;
        private IPersistentProgressService _persistentProgressService;
        private ISaveLoadService _saveLoadService;
        private IAssetsProvider _assetsProvider;

        [Inject]
        private void Construct(IGameStatesMachine gameStatesMachine,
            IPersistentProgressService persistentProgressService,
            ISaveLoadService saveLoadService,
            IAssetsProvider assetsProvider)
        {
            _gameStatesMachine = gameStatesMachine;
            _persistentProgressService = persistentProgressService;
            _saveLoadService = saveLoadService;
            _assetsProvider = assetsProvider;
        }
    
        private async void Start()
        {
            _gameStatesMachine.Enter();
        }
    }
}
