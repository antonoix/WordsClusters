using Plugins.Antonoix.UISystem.Base;
using System;
using Internal.Scripts.Infrastructure.Services.Sound;

namespace GameplayUI.MenuUI
{
    public class MenuUIPresenter : BaseUIPresenter<MenuUIView, MenuUIModel>
    {
        private readonly ISoundsService _soundsService;
        public override string UIPrefabAddressablesName => "MenuUI";
        
        public event Action OnStartBtnClicked;

        public MenuUIPresenter(ISoundsService soundsService)
        {
            _soundsService = soundsService;
        }
        
        public override void Show()
        {
            _view.SetSoundsButtonState(_soundsService.IsSoundEnabled);
            
            _view.OnStartClick += HandleStartClick;
            _view.OnSoundsButtonClicked += HandleSoundsButtonClick;
            
            base.Show();
        }

        public override void Hide()
        {
            _view.OnStartClick -= HandleStartClick;
            _view.OnSoundsButtonClicked -= HandleSoundsButtonClick;
            
            base.Hide();
        }

        private void HandleStartClick()
        {
            OnStartBtnClicked?.Invoke();
        }
        
        private void HandleSoundsButtonClick(bool isEnabled)
        {
            _soundsService.EnableAudio(isEnabled);
        }
    }
}