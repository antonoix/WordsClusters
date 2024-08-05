using System;
using System.Collections.Generic;
using Infrastructure.Services.LevelService;

namespace Gameplay.LevelRefereeing
{
    public interface ILevelReferee
    {
        event Action OnLevelPassed;
        Queue<string> SolvedWords { get; }
        void StartLevel();
        void HandleWordSolved(WordConfig word);
        void HandleLevelPassed();
    }
}