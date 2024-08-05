using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameplayUI.GamePlay
{
    public class LetterView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _letterText;
        [SerializeField] private Image _stub;

        public bool IsHidden { get; private set; }
        
        public void Initialize(char letter, bool letterIsHidden)
        {
            IsHidden = letterIsHidden;
            
            _letterText.text = letter.ToString();
            _stub.gameObject.SetActive(letterIsHidden);
        }

        public void Open()
        {
            IsHidden = false;
            _stub.gameObject.SetActive(false);
        }
        
        public void OpenByTip()
        {
            IsHidden = false;
            _stub.color = new Color(_stub.color.r, _stub.color.g, _stub.color.b, 0.7f);
        }
    }
}