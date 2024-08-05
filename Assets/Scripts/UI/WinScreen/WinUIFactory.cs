using Cysharp.Threading.Tasks;
using GameplayUI.WinScreen.SubViews;
using Internal.Scripts.AssetManagement;
using UnityEngine;

namespace GameplayUI.WinScreen
{
    public class WinUIFactory
    {
        private readonly IAssetsProvider _assetsProvider;
        private readonly WinUIConfig _winUIConfig;

        public WinUIFactory(IAssetsProvider assetsProvider, WinUIConfig winUIConfig)
        {
            _assetsProvider = assetsProvider;
            _winUIConfig = winUIConfig;
        }
        
        public async UniTask<SolvedWordView> CreateSolvedWord(Transform root, string word)
        {
            var wordPrefab = await _assetsProvider
                .LoadAsync<GameObject>(_winUIConfig.SolvedWordPrefabLabel.labelString);

            var wordView = GameObject.Instantiate(wordPrefab, root).GetComponent<SolvedWordView>();
            wordView.Initialize(word);
            return wordView;
        }
    }
}