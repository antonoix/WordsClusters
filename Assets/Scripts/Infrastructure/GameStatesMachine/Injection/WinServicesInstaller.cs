using Internal.Scripts.Infrastructure.GameStatesMachine.States;
using UnityEngine;
using Zenject;

namespace Internal.Scripts.Infrastructure.Injection
{
    [CreateAssetMenu(fileName = nameof(WinServicesInstaller), menuName = "Installers/" + nameof(WinServicesInstaller))]
    public class WinServicesInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<WinState>().AsSingle();
        }
    }
}