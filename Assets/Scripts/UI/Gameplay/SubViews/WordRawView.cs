using System;
using System.Collections.Generic;
using System.Linq;
using GameplayUI.Base;
using Infrastructure.Services.LevelService;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameplayUI.GamePlay
{
    public class WordRawView : HoverAnimatedElement
    {
        [SerializeField] private Transform _lettersRoot;
        [SerializeField] private LetterView _letterPrefab;
        private bool _isHovering;
        private List<LetterView> _letterViews = new();
        
        public WordConfig WordConfig { get; private set; }
        public bool IsSolved { get; private set; }
        
        public event Action<WordRawView, bool> OnPointerEntered; 
        
        public void Initialize(WordConfig wordConfig)
        {
            WordConfig = wordConfig;
            
            var word = wordConfig.MainWord.ToUpper();
            var cluster = wordConfig.Cluster.ToUpper();
            
            var startHiddenIndex = word.IndexOf(cluster);
            var endHiddenIndex = startHiddenIndex + cluster.Length - 1;
            
            for(int i = 0; i < word.Length; i++)
            {
                bool letterIsHidden = i >= startHiddenIndex && i <= endHiddenIndex;
                var letterView = Instantiate(_letterPrefab, _lettersRoot);
                letterView.Initialize(word[i], letterIsHidden);
                
                _letterViews.Add(letterView);
            }
        }

        public void SetSolved()
        {
            IsSolved = true;
            foreach (var letter in _letterViews)
            {
                letter.Open();
            }
        }

        public bool SolveOneLetter()
        {
            var letters = _letterViews.Where(x => x.IsHidden).ToList();
            var randomLetter = letters[Random.Range(0, letters.Count)];
            randomLetter.OpenByTip();

            if (letters.Count == 1)
                IsSolved = true;

            return IsSolved;
        }

        private void Update()
        {
            bool isHover =
                RectTransformUtility.RectangleContainsScreenPoint(gameObject.GetComponent<RectTransform>(),
                    Input.mousePosition);

            if (isHover && !_isHovering)
            {
                Hover();
                _isHovering = true;
                OnPointerEntered?.Invoke(this, true);
            }
            else if (!isHover && _isHovering)
            {
                UnHover();
                _isHovering = false;
                OnPointerEntered?.Invoke(this, false);
            }
        }
    }
}