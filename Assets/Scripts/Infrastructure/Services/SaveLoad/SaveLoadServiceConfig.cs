using UnityEngine;

namespace Internal.Scripts.Infrastructure.Services.SaveLoad
{
    [CreateAssetMenu(fileName = nameof(SaveLoadServiceConfig), menuName = "Configs/" + nameof(SaveLoadServiceConfig))]
    public class SaveLoadServiceConfig : ScriptableObject
    {
        [field: SerializeField] public float SaveInterval { get; private set; } = 10;
        [field: SerializeField] public AutoSaver AutoSaverPrefab { get; private set; }
    }
}