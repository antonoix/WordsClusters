using UnityEngine;
using Zenject;

namespace Infrastructure.Services.Camera
{
    [CreateAssetMenu(fileName = nameof(CameraServiceInstaller), menuName = "Installers/" + nameof(CameraServiceInstaller))]
    public class CameraServiceInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private CameraServiceConfig _config;

        public override void InstallBindings()
        {
            Container.BindInstance(_config).AsSingle();
            Container.BindInterfacesAndSelfTo<CameraService>().AsSingle();
        }
    }
}