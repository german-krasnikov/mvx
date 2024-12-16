using Game.Views.Planets;
using UnityEngine;
using Zenject;

namespace Game.Views
{
    public sealed class ViewsInstaller : MonoInstaller
    {
        [SerializeField]
        private PlanetView[] _planetViews;
        [SerializeField]
        private MoneyView _moneyView;
        [SerializeField]
        private PlanetPopupView _planetPopupView;
        [SerializeField]
        private GameObject _coinVfxPrefab;
        [SerializeField]
        private GameObject _coinVfxParent;

        public override void InstallBindings()
        {
            Container.Bind<PlanetView[]>().FromInstance(_planetViews).AsSingle().NonLazy();
            Container.Bind<MoneyView>().FromInstance(_moneyView).AsSingle().NonLazy();
            Container.Bind<PlanetPopupView>().FromInstance(_planetPopupView).AsSingle().NonLazy();
            Container.BindMemoryPool<Transform, CoinVFXPool>()
                .WithInitialSize(2) 
                .FromComponentInNewPrefab(_coinVfxPrefab)
                .UnderTransform(_coinVfxParent.transform);
        }
    }
}