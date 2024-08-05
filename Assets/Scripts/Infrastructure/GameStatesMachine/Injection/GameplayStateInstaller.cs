using Internal.Scripts.Infrastructure.GameStatesMachine.States;
using UnityEngine;
using Zenject;

namespace Internal.Scripts.Infrastructure.Injection
{
    [CreateAssetMenu(fileName = nameof(GameplayStateInstaller), menuName = "Installers/" + nameof(GameplayStateInstaller))]
    public class GameplayStateInstaller : ScriptableObjectInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<GamePlayState>().AsSingle();
        }
    }
}