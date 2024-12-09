using Game.Presenters.Planets;
using Game.Views.Planets;
using Modules.Planets;
using UnityEngine;
using Zenject;

namespace Game.Presenters
{
    [CreateAssetMenu(
        fileName = "PresentersInstallers",
        menuName = "Zenject/New PresentersInstallers"
    )]
    public sealed class PresentersInstallers : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindFactory<Planet, PlanetView, PlanetPresenter, PlanetPresenter.Factory>()
                .AsSingle();
            Container.Bind<PlanetPresenter[]>().FromMethod(CreatePlanetPresenters).AsSingle().NonLazy();
        }

        private PlanetPresenter[] CreatePlanetPresenters(InjectContext context)
        {
            var factory = context.Container.Resolve<PlanetPresenter.Factory>();
            var planets = context.Container.Resolve<Planet[]>();
            var views = context.Container.Resolve<PlanetView[]>();
            var presenters = new PlanetPresenter[views.Length];

            for (int i = 0; i < views.Length; i++)
            {
                presenters[i] = factory.Create(planets[i], views[i]);
            }

            return presenters;
        }
    }
}