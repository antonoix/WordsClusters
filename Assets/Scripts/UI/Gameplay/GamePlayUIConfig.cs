using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameplayUI.GamePlay
{
    [CreateAssetMenu(fileName = nameof(GamePlayUIConfig), menuName = "Configs/" + nameof(GamePlayUIConfig))]
    public class GamePlayUIConfig : ScriptableObject
    {
        [field: SerializeField] public AssetLabelReference WordPrefabLabel { get; private set; }
        [field: SerializeField] public AssetLabelReference ClusterPrefabLabel { get; private set; }
    }
}