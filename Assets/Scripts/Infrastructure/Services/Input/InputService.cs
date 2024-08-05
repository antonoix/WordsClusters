using System;
using UniRx;
using Zenject;

namespace Infrastructure.Services.Input
{
    public class InputService : IInitializable, IInputService
    {
        public event Action OnPointerClicked;
        public event Action OnPointerUnclicked;

        private bool _currentlyIsClicked;
        
        public void Initialize()
        {
            Observable.EveryUpdate()
                .Where(_ => UnityEngine.Input.GetMouseButtonDown(0) || UnityEngine.Input.touchCount > 0)
                .Subscribe(HandleTouchOrClick);
            
            Observable.EveryUpdate()
                .Where(_ => UnityEngine.Input.GetMouseButtonUp(0) && UnityEngine.Input.touchCount == 0)
                .Subscribe(HandleNoClicks);
        }

        private void HandleTouchOrClick(long _)
        {
            if (!_currentlyIsClicked)
            {
                _currentlyIsClicked = true;
                OnPointerClicked?.Invoke();
            }
        }

        private void HandleNoClicks(long _)
        {
            if (_currentlyIsClicked)
            {
                OnPointerUnclicked?.Invoke();
                _currentlyIsClicked = false;
            }
        }
    }
}