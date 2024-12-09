using Game.Views.Planets;
using UnityEngine;
using Zenject;

namespace Game.Views
{
    public sealed class ViewsInstaller : MonoInstaller
    {
        [SerializeField]
        private PlanetView[] _planetViews;

        public override void InstallBindings()
        {
            Container.Bind<PlanetView[]>().FromInstance(_planetViews).AsSingle().NonLazy();
        }
    }
}