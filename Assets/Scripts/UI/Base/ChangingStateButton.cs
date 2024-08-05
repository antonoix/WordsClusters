using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameplayUI.Base
{
    public class ChangingStateButton : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _mainImage;
        [SerializeField] private Sprite _enabledSprite;
        [SerializeField] private Sprite _disabledSprite;
        
        public bool IsEnabledState { get; private set; }
        
        public event Action<bool> OnClicked;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            SetState(!IsEnabledState);
            
            OnClicked?.Invoke(IsEnabledState);
        }

        public void SetState(bool isEnabledState)
        {
            IsEnabledState = isEnabledState;

            _mainImage.sprite = IsEnabledState ? _enabledSprite : _disabledSprite;
        }
    }
}