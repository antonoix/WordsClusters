using System;
using GameplayUI.Base;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameplayUI.GamePlay
{
    public class ClusterView : HoverAnimatedElement, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private TMP_Text _text;
        
        public event Action<ClusterView, bool> OnPointerEntered; 
        
        public string Cluster { get; private set; }
        public int SiblingIndex { get; private set; }
        
        public void Initialize(string cluster)
        {
            _text.text = cluster;
            Cluster = cluster;
        }

        public void SetParent(Transform parent, bool isSettingToRoot)
        {
            if (!isSettingToRoot)
                SiblingIndex = transform.GetSiblingIndex();
            transform.SetParent(parent, true);
            if (isSettingToRoot)
                transform.SetSiblingIndex(SiblingIndex);
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            Hover();
            OnPointerEntered?.Invoke(this, true);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            UnHover();
            OnPointerEntered?.Invoke(this, false);
        }
    }
}