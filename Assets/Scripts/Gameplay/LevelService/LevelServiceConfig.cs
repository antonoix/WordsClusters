using UnityEngine;

namespace Infrastructure.Services.LevelService
{
    [CreateAssetMenu(fileName = (nameof(LevelServiceConfig)), menuName = "Configs/" + nameof(LevelServiceConfig))]
    public class LevelServiceConfig : ScriptableObject
    {
        [SerializeField] private LevelConfig[] _allLevels;

        public LevelConfig[] AllLevels
        {
            get => _allLevels;
#if UNITY_EDITOR
            set => _allLevels = value;
#endif
        }
        
        
        [ContextMenu("Validate")]
        private void Validate()
        {
            if (_allLevels == null || _allLevels.Length == 0)
                return;

            bool isCorrect = true;
            
            foreach (var config in _allLevels)
            {
                foreach (var word in config.Words)
                {
                    if (!word.MainWord.Contains(word.Cluster))
                    {
                        isCorrect = false;
                        Debug.LogError($"Cluster should be a part of the world: {word.MainWord}");
                    }
                }
            }
            
            if (isCorrect)
                Debug.Log($"{nameof(LevelServiceConfig)} is correct!");
        }
    }
}