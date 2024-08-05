using Internal.Scripts.Infrastructure.GameStatesMachine.States;
using UnityEngine;
using Zenject;

namespace Internal.Scripts.Infrastructure.Injection
{
    [CreateAssetMenu(fileName = nameof(MenuStateInstaller), menuName = "Installers/" + nameof(MenuStateInstaller))]
    public class MenuStateInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<MenuState>().AsSingle();
        }
    }
}