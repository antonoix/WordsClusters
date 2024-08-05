using System;
using Internal.Scripts.Infrastructure.Services.PlayerProgressService;
using UnityEngine;
using Zenject;

namespace Internal.Scripts.Infrastructure.Services.SaveLoad
{
    public class SaveLoadService : ISaveLoadService, IInitializable, ILateDisposable
    {
        private const string PLAYER_PROGRESS_KEY = "PlayerProgress";

        private readonly IPersistentProgressService _persistentProgressService;
        private readonly SaveLoadServiceConfig _config;
        private bool isInitialized;

        
        public SaveLoadService(IPersistentProgressService persistentProgressService, SaveLoadServiceConfig config)
        {
            _persistentProgressService = persistentProgressService;
            _config = config;
        }

        public void Initialize()
        {
            if (isInitialized)
                return;

            isInitialized = true;
            ES3.Init();

            var saver = GameObject.Instantiate(_config.AutoSaverPrefab);
            GameObject.DontDestroyOnLoad(saver);
            saver.StartSaving(this, _config.SaveInterval);
        }
        
        public void LateDispose()
        {
            SaveProgress();
        }
        
        public void SaveProgress()
        {
            try
            {
                ES3.Save(PLAYER_PROGRESS_KEY, _persistentProgressService.PlayerProgress);
                
                PlayerProgress playerProgress = LoadProgress();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw new ArgumentException(e.ToString());
            }
        }

        public PlayerProgress LoadProgress()
        {
            PlayerProgress playerProgress = ES3.KeyExists(PLAYER_PROGRESS_KEY) 
                ? ES3.Load<PlayerProgress>(PLAYER_PROGRESS_KEY) 
                : null;
            return playerProgress;
        }

        public void ClearProgress()
        {
            ES3.DeleteKey(PLAYER_PROGRESS_KEY);
        }
    }
}