using DG.Tweening;
using UnityEngine;

namespace GameplayUI.Base
{
    public abstract class HoverAnimatedElement : MonoBehaviour
    {
        private Sequence _hoverAnim;

        protected void Hover()
        {
            _hoverAnim?.Kill();
            _hoverAnim = DOTween.Sequence()
                .Append(transform.DOScale(Vector3.one * 1.07f, 0.2f))
                .SetEase(Ease.OutSine);
        }

        protected void UnHover()
        {
            _hoverAnim?.Kill();
            _hoverAnim = DOTween.Sequence()
                .Append(transform.DOScale(Vector3.one, 0.2f))
                .SetEase(Ease.OutSine);
        }
    }
}