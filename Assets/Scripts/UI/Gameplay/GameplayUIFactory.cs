using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using GameplayUI.WinScreen;
using GameplayUI.WinScreen.SubViews;
using Infrastructure.Services.LevelService;
using Internal.Scripts.AssetManagement;
using Internal.Scripts.Infrastructure.Services.PlayerProgressService;
using UnityEngine;

namespace GameplayUI.GamePlay
{
    public class GameplayUIFactory
    {
        private readonly IAssetsProvider _assetsProvider;
        private readonly GamePlayUIConfig _config;
        private readonly IPersistentProgressService _progressService;

        public GameplayUIFactory(IAssetsProvider assetsProvider, GamePlayUIConfig config,
            IPersistentProgressService progressService)
        {
            _assetsProvider = assetsProvider;
            _config = config;
            _progressService = progressService;
        }
        
        public async UniTask<List<WordRawView>> CreateWords(WordConfig[] words, Transform root)
        {
            List<WordRawView> result = new();
            
            var wordPrefab = await _assetsProvider
                .LoadAsync<GameObject>(_config.WordPrefabLabel.labelString);

            foreach (var wordConfig in words)
            {
                var wordRaw = GameObject.Instantiate(wordPrefab, root).GetComponent<WordRawView>();
                if (wordRaw == null)
                {
                    Debug.LogError($"No {nameof(WordRawView)} component attached to prefab");
                    return result;
                }

                wordRaw.Initialize(wordConfig);
                if (_progressService.PlayerProgress.CurrentLevelSolvedWords.Any(x => x.MainWord == wordConfig.MainWord))
                { 
                    wordRaw.SetSolved();
                }
                result.Add(wordRaw);
            }

            return result;
        }

        public async UniTask<List<ClusterView>> CreateClusters(List<string> clusters, Transform root)
        {
            List<ClusterView> result = new();
            
            var clusterPrefab = await _assetsProvider
                .LoadAsync<GameObject>(_config.ClusterPrefabLabel.labelString);

            foreach (var cluster in clusters)
            {
                if (_progressService.PlayerProgress.CurrentLevelSolvedWords.Any(x => x.Cluster == cluster))
                    continue;
                
                var clusterViews = GameObject.Instantiate(clusterPrefab, root).GetComponent<ClusterView>();
                if (clusterViews == null)
                {
                    Debug.LogError($"No {nameof(WordRawView)} component attached to prefab");
                    return result;
                }

                clusterViews.Initialize(cluster);
                result.Add(clusterViews);
            }

            return result;
        }
    }
}