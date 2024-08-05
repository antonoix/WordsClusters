using UnityEngine;
using Zenject;

namespace Infrastructure.Services.LevelService
{
    [CreateAssetMenu(fileName = nameof(LevelServiceInstaller), menuName = "Installers/" + nameof(LevelServiceInstaller))]
    public class LevelServiceInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private LevelServiceConfig _config;

        public override void InstallBindings()
        {
            Container.BindInstance(_config).AsSingle();
            Container.BindInterfacesTo<LevelService>().AsSingle();
        }
    }
}