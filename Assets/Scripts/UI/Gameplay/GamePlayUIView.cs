using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameplayUI.Base;
using Infrastructure.Services.LevelService;
using Plugins.Antonoix.UISystem.Base;
using UnityEngine;
using UnityEngine.UI;

namespace GameplayUI.GamePlay
{
    public class GamePlayUIView : BaseUIView
    {
        [SerializeField] private ScrollRect _clusterScroll;
        [SerializeField] private Button _tipButton;
        [field: SerializeField] public Transform WordsRoot { get; private set; }
        [field: SerializeField] public Transform ClustersRoot { get; private set; }
        [field: SerializeField] public PointerHovering ScrollBar { get; private set; }

        private readonly List<WordRawView> _wordRaws = new();
        private readonly List<ClusterView> _clusters = new();

        public List<WordRawView> WordRaws => _wordRaws;

        public event Action<WordRawView, bool> OnPointerEnteredWord;
        public event Action<ClusterView, bool> OnPointerEnteredCluster;
        public event Action OnTipClicked;

        public void AddWords(List<WordRawView> wordsViews)
        {
            foreach (var wordRawView in wordsViews)
            {
                wordRawView.OnPointerEntered += HandleWordPointerEnter;
                _wordRaws.Add(wordRawView);    
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(WordsRoot as RectTransform);
        }

        public void AddClusters(List<ClusterView> clustersViews)
        {
            foreach (var clusterView in clustersViews)
            {
                clusterView.OnPointerEntered += HandleClusterPointerEnter;
                _clusters.Add(clusterView);    
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(ClustersRoot as RectTransform);
        }

        public void ActivateClustersScroll(bool isActive) 
            => _clusterScroll.enabled = isActive;

        public override UniTask Show()
        {
            _tipButton.onClick.AddListener(HandleTipClicked);
            
            return base.Show();
        }

        public override UniTask Hide()
        {
            foreach (var wordView in _wordRaws)
            {
                wordView.OnPointerEntered -= HandleWordPointerEnter;
                Destroy(wordView.gameObject);
            }

            foreach (var clusterView in _clusters)
            {
                clusterView.OnPointerEntered -= HandleClusterPointerEnter;
                Destroy(clusterView.gameObject);
            }
            
            _tipButton.onClick.RemoveListener(HandleTipClicked);
            
            _wordRaws.Clear();
            _clusters.Clear();
            
            return base.Hide();
        }

        public void SetClusterParent(ClusterView clusterView, bool isInRoot)
        {
            clusterView.SetParent(isInRoot ? ClustersRoot : transform, isInRoot);
            if (isInRoot)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(ClustersRoot as RectTransform);
            }
        }

        public void SetSolved(WordRawView selectedWord, ClusterView selectedCluster)
        {
            selectedWord.SetSolved();
            _clusters.Remove(selectedCluster);
            Destroy(selectedCluster.gameObject);
        }

        private void HandleTipClicked()
        {
            OnTipClicked?.Invoke();
        }

        private void HandleWordPointerEnter(WordRawView word, bool isEnter)
        {
            OnPointerEnteredWord?.Invoke(word, isEnter);
        }

        private void HandleClusterPointerEnter(ClusterView cluster, bool isEnter)
        {
            OnPointerEnteredCluster?.Invoke(cluster, isEnter);
        }
    }
}