using Gameplay.LevelRefereeing;
using Infrastructure.Services.Input;
using Internal.Scripts.AssetManagement;
using Internal.Scripts.Infrastructure.Services.PlayerProgressService;
using Internal.Scripts.Infrastructure.Services.SaveLoad;
using UnityEngine;
using Zenject;

namespace Internal.Scripts.Infrastructure.Injection
{
    [CreateAssetMenu(fileName = nameof(ProjectServicesInstaller), menuName = "Installers/" + nameof(ProjectServicesInstaller))]
    public class ProjectServicesInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<InputService>().AsSingle();
            Container.BindInterfacesTo<LevelReferee>().AsSingle();
            Container.BindInterfacesTo<AddressablesAssetsProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<PersistentProgressService>().AsSingle();
        }
    }
}