using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameplayUI.WinScreen
{
    [CreateAssetMenu(fileName = nameof(WinUIConfig), menuName = "Configs/" + nameof(WinUIConfig))]
    public class WinUIConfig : ScriptableObject
    {
        [field: SerializeField] public AssetLabelReference SolvedWordPrefabLabel { get; private set; }
    }
}