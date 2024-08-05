using System;
using System.Collections.Generic;
using Infrastructure.Services.LevelService;
using UnityEngine;

namespace Internal.Scripts.Infrastructure.Services.PlayerProgressService
{
    [Serializable]
    public class PlayerProgress
    {
        [SerializeField] private int _lastPassedLevelIndex;
        [SerializeField] private List<WordConfig> _currentLevelSolvedWords;

        public int LastPassedLevelIndex => _lastPassedLevelIndex;
        public List<WordConfig> CurrentLevelSolvedWords => _currentLevelSolvedWords;

        // need for SaveSystem
        public PlayerProgress()
        {
            _lastPassedLevelIndex = -1;
            _currentLevelSolvedWords = new();
        }

        public void IncreasePassedLevelIndex()
        {
            _currentLevelSolvedWords.Clear();
            _lastPassedLevelIndex++;
        }

        public void AddSolvedWord(WordConfig word)
        {
            _currentLevelSolvedWords.Add(word);    
        }
    }
}