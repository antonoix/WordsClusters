using System;
using Internal.Scripts.Infrastructure.Services.PlayerProgressService;
using UnityEngine;

namespace Infrastructure.Services.LevelService
{
    public class LevelService : ILevelService
    {
        private const int MIN_LEVEL_INDEX = 0;
        private const int MAX_LEVEL_INDEX = 0;
        
        private readonly IPersistentProgressService _progressService;
        private readonly LevelServiceConfig _config;

        public LevelService(IPersistentProgressService progressService, LevelServiceConfig config)
        {
            _progressService = progressService;
            _config = config;
        }

        public LevelConfig GetActualLevel()
        {
            int currentLevelIndex 
                = Math.Clamp(_progressService.PlayerProgress.LastPassedLevelIndex, MIN_LEVEL_INDEX, MAX_LEVEL_INDEX);

            if (_config.AllLevels == null || _config.AllLevels.Length == 0)
            {
                throw new Exception($"Please check levels configs. It should not be empty");
            }

            if (currentLevelIndex >= _config.AllLevels.Length)
            {
                Debug.LogError($"Current level index is out of Level Configs array");
                return _config.AllLevels[0];
            }
            
            return _config.AllLevels[currentLevelIndex];
        }

        public void PassCurrentLevel()
        {
            _progressService.PlayerProgress.IncreasePassedLevelIndex();
        }
    }
}