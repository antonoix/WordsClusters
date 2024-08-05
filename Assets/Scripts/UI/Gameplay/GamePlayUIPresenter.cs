using System.Collections;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Gameplay.LevelRefereeing;
using Infrastructure.Services.Input;
using Internal.Scripts.AssetManagement;
using Internal.Scripts.Infrastructure.Services.Sound;
using ModestTree;
using Plugins.Antonoix.UISystem.Base;
using UnityEngine;

namespace GameplayUI.GamePlay
{
    public class GamePlayUIPresenter : BaseUIPresenter<GamePlayUIView, GamePlayUIModel>
    {
        private readonly GameplayUIFactory _factory;
        private readonly ISoundsService _soundsService;
        private readonly IInputService _inputService;
        private readonly ILevelReferee _levelReferee;

        private WordRawView _selectedWord;
        private ClusterView _selectedCluster;
        private bool _isScrollBarDragging;

        private CancellationTokenSource _dragCancellation;
        
        public override string UIPrefabAddressablesName => "GamePlayUI";

        public GamePlayUIPresenter(
            GameplayUIFactory factory,
            ISoundsService soundsService,
            IInputService inputService,
            ILevelReferee levelReferee)
        {
            _factory = factory;
            _soundsService = soundsService;
            _inputService = inputService;
            _levelReferee = levelReferee;
        }

        public override async void Show()
        {
            base.Show();

            await CreateWords();
            await CreateClusters();

            _view.OnPointerEnteredWord += HandlePointerOnWord;
            _view.OnPointerEnteredCluster += HandlePointerOnCluster;
            _view.OnTipClicked += HandleTipClicked;
            _view.ScrollBar.OnHovered += HandlePointerOnScrollbar;
            
            _inputService.OnPointerClicked += HandlePointerClicked;
            _inputService.OnPointerUnclicked += HandlePointerUnclicked;
        }

        private async UniTask CreateWords()
        {
            var wordsViews = await _factory.CreateWords(_model.GetWords(), _view.WordsRoot);
            _view.AddWords(wordsViews);
        }

        private async UniTask CreateClusters()
        {
            var clustersViews = await _factory.CreateClusters(_model.GetClusters(), _view.ClustersRoot);
            _view.AddClusters(clustersViews);
        }

        public override void Hide()
        {
            _view.OnPointerEnteredWord -= HandlePointerOnWord;
            _view.OnPointerEnteredCluster -= HandlePointerOnCluster;
            _view.OnTipClicked -= HandleTipClicked;
            _view.ScrollBar.OnHovered -= HandlePointerOnScrollbar;
            
            _inputService.OnPointerClicked -= HandlePointerClicked;
            _inputService.OnPointerUnclicked -= HandlePointerUnclicked;
            
            base.Hide();
        }

        private void HandlePointerClicked()
        {
            if (_isScrollBarDragging)
                return;
            
            if (_selectedCluster != null)
            {
                _view.ActivateClustersScroll(false);
            }

            _dragCancellation = new CancellationTokenSource();
            if (_selectedCluster != null)
            {
                _view.SetClusterParent(_selectedCluster, false);
            }
            DragCluster();
        }

        private void HandlePointerUnclicked()
        {
            _view.ActivateClustersScroll(true);
            
            _dragCancellation?.Cancel();
            
            if (_selectedCluster == null) 
                return;
            
            if (!CheckIfWordSolved())
            {
                _view.SetClusterParent(_selectedCluster, true);
            }
        }

        private async UniTaskVoid DragCluster()
        {
            while (!_dragCancellation.IsCancellationRequested)
            {
                if (_selectedCluster != null)
                {
                    _selectedCluster.transform.position = Input.mousePosition;
                }
                
                await UniTask.Yield(PlayerLoopTiming.Update);
            }
        }

        private void HandlePointerOnWord(WordRawView word, bool isEnter)
        {
            if (isEnter)
            {
                _selectedWord = word;
            }
            else if(_selectedWord == word)
            {
                _selectedWord = null;
            }
        }

        private void HandlePointerOnScrollbar(bool onScrollBar)
        {
            _isScrollBarDragging = onScrollBar;
        }

        private bool CheckIfWordSolved()
        {
            if (_selectedCluster == null || _selectedWord == null) 
                return false;

            if (_selectedWord.WordConfig.Cluster.ToUpper() != _selectedCluster.Cluster.ToUpper()) 
                return false;
            
            _soundsService.PlaySound(SoundType.ButtonClick);
            _levelReferee.HandleWordSolved(_selectedWord.WordConfig);
            _view.SetSolved(_selectedWord, _selectedCluster);
            _selectedWord.OnPointerEntered -= HandlePointerOnWord;
            return true;
        }

        private void HandlePointerOnCluster(ClusterView cluster, bool isEnter)
        {
            if (isEnter)
            {
                _selectedCluster = cluster;
            }
            else if(_selectedCluster == cluster)
            {
                _selectedCluster = null;
            }
        }

        private void HandleTipClicked()
        {
            var word = _view.WordRaws.FirstOrDefault(x => !x.IsSolved);
            var isSolved = word.SolveOneLetter();

            if (isSolved)
            {
                _levelReferee.HandleWordSolved(word.WordConfig);
            }
        }
    }
}