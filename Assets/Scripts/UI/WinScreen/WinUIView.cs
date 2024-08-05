using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameplayUI.WinScreen.SubViews;
using Internal.Scripts.Infrastructure.Services.Sound;
using Plugins.Antonoix.UISystem.Base;
using UnityEngine;
using UnityEngine.UI;
using Zenject;


namespace GameplayUI.WinScreen
{
    public class WinUIView : BaseUIView
    {
        [SerializeField] private Button _menuButton;
        [field: SerializeField] public Transform SolvesWordsView { get; private set; }

        private readonly List<SolvedWordView> _solvedWords = new();
        private ISoundsService _soundsService;

        public event Action OnMenuButtonClicked;
        

        [Inject]
        private void Construct(ISoundsService soundsService)
        {
            _soundsService = soundsService;
        }
        
        public override UniTask Show()
        {
            _menuButton.onClick.AddListener(HandleMenuClicked);
            
            return base.Show();
        }

        public override UniTask Hide()
        {
            _menuButton.onClick.RemoveListener(HandleMenuClicked);
            
            foreach (var solvedWord in _solvedWords)
            {
                Destroy(solvedWord.gameObject);
            }
            _solvedWords.Clear();
            
            return base.Hide();
        }
        
        public void AddWord(SolvedWordView solvedWordView)
        {
            _solvedWords.Add(solvedWordView);
        }

        private void HandleMenuClicked()
        {
            _soundsService.PlaySound(SoundType.ButtonClick);
            
            OnMenuButtonClicked?.Invoke();
        }
    }
}