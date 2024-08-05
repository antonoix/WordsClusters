using System;
using Gameplay.LevelRefereeing;
using Internal.Scripts.AssetManagement;
using Internal.Scripts.Infrastructure.Services.Sound;
using Plugins.Antonoix.UISystem.Base;
using UnityEngine;

namespace GameplayUI.WinScreen
{
    public class WinUIPresenter : BaseUIPresenter<WinUIView, WinUIModel>
    {
        private readonly ILevelReferee _levelReferee;
        private readonly WinUIFactory _winUIFactory;
        private readonly ISoundsService _soundsService;

        public override string UIPrefabAddressablesName => "WinUI";

        public event Action OnMenuButtonClicked; 

        public WinUIPresenter(ILevelReferee levelReferee,
            WinUIFactory winUIFactory,
            ISoundsService soundsService)
        {
            _levelReferee = levelReferee;
            _winUIFactory = winUIFactory;
            _soundsService = soundsService;
        }

        public override async void Show()
        {
            _soundsService.PlaySound(SoundType.Win);
            
            while (_levelReferee.SolvedWords.Count > 0)
            {
                var word = _levelReferee.SolvedWords.Dequeue();
                var wordView = await _winUIFactory.CreateSolvedWord(_view.SolvesWordsView, word);
                _view.AddWord(wordView);
            }

            _view.OnMenuButtonClicked += HandleMenuButtonClicked;
            
            base.Show();
        }

        public override void Hide()
        {
            _view.OnMenuButtonClicked -= HandleMenuButtonClicked;
            
            base.Hide();
        }

        private void HandleMenuButtonClicked()
        {
            OnMenuButtonClicked?.Invoke();
        }
    }
}