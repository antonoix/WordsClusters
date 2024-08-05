using UnityEngine;

namespace Infrastructure.Services.Camera
{
    [CreateAssetMenu(fileName = nameof(CameraServiceConfig), menuName = "Configs/" + nameof(CameraServiceConfig))]
    public class CameraServiceConfig : ScriptableObject
    {
        [field: SerializeField] public UnityEngine.Camera CameraViewPrefab { get; private set; }
    }
}