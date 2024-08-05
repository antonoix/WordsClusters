using System;
using System.Collections.Generic;
using Infrastructure.Services.LevelService;
using Internal.Scripts.Infrastructure.Services.PlayerProgressService;
using Internal.Scripts.Infrastructure.Services.SaveLoad;

namespace Gameplay.LevelRefereeing
{
    public class LevelReferee : ILevelReferee
    {
        private readonly ILevelService _levelService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IPersistentProgressService _progressService;
        private readonly Queue<string> _solvedWords = new();
        
        private LevelConfig _levelConfig;

        public Queue<string> SolvedWords => _solvedWords;

        public event Action OnLevelPassed; 

        public LevelReferee(ILevelService levelService, ISaveLoadService saveLoadService,
            IPersistentProgressService progressService)
        {
            _levelService = levelService;
            _saveLoadService = saveLoadService;
            _progressService = progressService;
        }
        
        public void StartLevel()
        {
            _levelConfig = _levelService.GetActualLevel();

            foreach (var solvedWord in _progressService.PlayerProgress.CurrentLevelSolvedWords)
            {
                _solvedWords.Enqueue(solvedWord.MainWord);
            }
        }

        public void HandleWordSolved(WordConfig word)
        {
            if (_solvedWords.Contains(word.MainWord))
                return;
            
            _solvedWords.Enqueue(word.MainWord);
            _progressService.PlayerProgress.CurrentLevelSolvedWords.Add(word);
            _saveLoadService.SaveProgress();

            if (_solvedWords.Count == _levelConfig.Words.Length)
            {
                OnLevelPassed?.Invoke();
                HandleLevelPassed();
            }
        }

        private void HandleLevelPassed()
        {
            _levelService.PassCurrentLevel();
            _saveLoadService.SaveProgress();
        }
    }
}