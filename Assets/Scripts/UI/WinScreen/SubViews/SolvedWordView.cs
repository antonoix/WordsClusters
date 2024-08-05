using TMPro;
using UnityEngine;

namespace GameplayUI.WinScreen.SubViews
{
    public class SolvedWordView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        
        public void Initialize(string word)
        {
            _text.text = word;
        }
    }
}