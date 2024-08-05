using Internal.Scripts.Infrastructure.GameStatesMachine.States;
using UnityEngine;
using Zenject;

namespace Internal.Scripts.Infrastructure.Injection
{
    [CreateAssetMenu(fileName = nameof(BootstrapStateInstaller), menuName = "Installers/" + nameof(BootstrapStateInstaller))]
    public class BootstrapStateInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<BootstrapState>().AsSingle();
        }
    }
}