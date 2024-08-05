using UnityEngine;
using Zenject;

namespace Internal.Scripts.Infrastructure.Services.SaveLoad
{
    [CreateAssetMenu(fileName = nameof(SaveLoadServiceInstaller), menuName = "Installers/" + nameof(SaveLoadServiceInstaller))]
    public class SaveLoadServiceInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private SaveLoadServiceConfig _config;
        
        public override void InstallBindings()
        {
            Container.BindInstance(_config).AsSingle();
            Container.BindInterfacesTo<SaveLoadService>().AsSingle();
        }
    }
}