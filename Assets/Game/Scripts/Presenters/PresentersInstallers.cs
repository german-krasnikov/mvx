using System;
using Game.Presenters.Money;
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
            Container.BindFactory<Planet, PlanetView, PlanetPresenter, PlanetPresenter.Factory>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlanetPresenter>().FromMethodMultiple(CreatePlanetPresenters).AsTransient();
            Container.BindFactory<MoneyView, MoneyPresenter, MoneyPresenter.Factory>().AsSingle();
            Container.BindInterfacesAndSelfTo<MoneyPresenter>().FromMethod(CreateMoneyPresenter).AsSingle();
            Container.BindFactory<PlanetPopupView, PlanetPopupPresenter, PlanetPopupPresenter.Factory>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlanetPopupPresenter>().FromMethod(CreatePlanetPopupPresenter).AsTransient();
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

        private MoneyPresenter CreateMoneyPresenter(InjectContext context)
        {
            var factory = context.Container.Resolve<MoneyPresenter.Factory>();
            var view = context.Container.Resolve<MoneyView>();
            var presenter = factory.Create(view);
            return presenter;
        }

        private PlanetPopupPresenter CreatePlanetPopupPresenter(InjectContext context)
        {
            var factory = context.Container.Resolve<PlanetPopupPresenter.Factory>();
            var view = context.Container.Resolve<PlanetPopupView>();
            var presenter = factory.Create(view);
            return presenter;
        }
    }
}