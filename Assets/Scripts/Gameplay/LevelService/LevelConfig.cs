using System;
using UnityEngine;

namespace Infrastructure.Services.LevelService
{
    [Serializable]
    public class LevelConfig
    {
        [field: SerializeField] public WordConfig[] Words { get; set; }
        [field: SerializeField] public string[] TrashClusters { get; set; }
    }

    [Serializable]
    public class WordConfig
    {
        [field: SerializeField] public string MainWord { get; set; }
        [field: SerializeField] public string Cluster { get; set; }
    }
}