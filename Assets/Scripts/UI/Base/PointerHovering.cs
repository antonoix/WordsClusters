using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameplayUI.Base
{
    public class PointerHovering : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action<bool> OnHovered; 
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            OnHovered?.Invoke(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnHovered?.Invoke(false);
        }
    }
}