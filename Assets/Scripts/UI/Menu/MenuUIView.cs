using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using GameplayUI.Base;
using GameplayUI.WinScreen.SubViews;
using Internal.Scripts.Infrastructure.Services.Sound;
using Plugins.Antonoix.UISystem.Base;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GameplayUI.MenuUI
{
    public class MenuUIView : BaseUIView
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _settingsCloseButton;
        [SerializeField] private ChangingStateButton _soundsButton;
        [SerializeField] private GameObject _settingsPanel;

        private ISoundsService _soundsService;

        public event Action OnStartClick;
        public event Action<bool> OnSoundsButtonClicked;

        [Inject]
        private void Construct(ISoundsService soundsService)
        {
            _soundsService = soundsService;
        }
        
        public override UniTask Show()
        {
            _startButton.onClick.AddListener(HandleStartClick);
            _settingsButton.onClick.AddListener(HandleOpenSettingsClick);
            _settingsCloseButton.onClick.AddListener(HandleCloseSettingsClick);
            _soundsButton.OnClicked += HandleSoundButtonClicked;
            
            return base.Show();
        }

        public override UniTask Hide()
        {
            _startButton.onClick.RemoveListener(HandleStartClick);
            _settingsButton.onClick.RemoveListener(HandleOpenSettingsClick);
            _settingsCloseButton.onClick.RemoveListener(HandleCloseSettingsClick);
            _soundsButton.OnClicked -= HandleSoundButtonClicked;
            
            return base.Hide();
        }

        private void HandleStartClick()
        {
            _soundsService.PlaySound(SoundType.ButtonClick);
            OnStartClick?.Invoke();
        }

        private void HandleOpenSettingsClick()
        {
            _soundsService.PlaySound(SoundType.ButtonClick);
            _settingsPanel.SetActive(true);
        }

        private void HandleCloseSettingsClick()
        {
            _soundsService.PlaySound(SoundType.ButtonClick);
            _settingsPanel.SetActive(false);
        }

        private void HandleSoundButtonClicked(bool soundEnabled)
        {
            OnSoundsButtonClicked?.Invoke(soundEnabled);
        }

        public void SetSoundsButtonState(bool isEnabled)
        {
            _soundsButton.SetState(isEnabled);
        }
    }
}