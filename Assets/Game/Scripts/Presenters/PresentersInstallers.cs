using System;
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
            Container.BindFactory<Planet, PlanetView, PlanetPresenter, PlanetPresenterFactory>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlanetPresenter>().FromMethodMultiple(CreatePlanetPresenters).AsTransient();
        }

        private PlanetPresenter[] CreatePlanetPresenters(InjectContext context)
        {
            var factory = context.Container.Resolve<PlanetPresenterFactory>();
            var planets = context.Container.Resolve<Planet[]>();
            var views = context.Container.Resolve<PlanetView[]>();
            var presenters = new PlanetPresenter[views.Length];

            for (int i = 0; i < views.Length; i++)
            {
                presenters[i] = factory.Create(planets[i], views[i]);
            }

            return presenters;
        }

        private class PlanetPresenterFactory : PlaceholderFactory<Planet, PlanetView, PlanetPresenter>
        {
        }
    }
}