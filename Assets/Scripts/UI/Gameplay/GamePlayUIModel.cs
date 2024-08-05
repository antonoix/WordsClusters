using System.Collections.Generic;
using System.Linq;
using Extensions;
using Infrastructure.Services.LevelService;
using Plugins.Antonoix.UISystem.Base;

namespace GameplayUI.GamePlay
{
    public class GamePlayUIModel : BaseUIModel
    {
        private readonly ILevelService _levelService;
        private LevelConfig _currentLevelConfig;

        public GamePlayUIModel(ILevelService levelService)
        {
            _levelService = levelService;
        }

        public override void Initialize()
        {
            _currentLevelConfig = _levelService.GetActualLevel();
            base.Initialize();
        }

        public WordConfig[] GetWords()
        {
            return _currentLevelConfig.Words;
        }

        public List<string> GetClusters()
        {
            List<string> result = _currentLevelConfig.TrashClusters.ToList();
            result.AddRange(_currentLevelConfig.Words.Select(wordConfig => wordConfig.Cluster));
            result.Shuffle();
            
            return result;
        }
    }
}