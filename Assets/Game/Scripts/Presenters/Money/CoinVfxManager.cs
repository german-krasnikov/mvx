using System;
using System.Linq;
using DG.Tweening;
using Game.Presenters.Planets;
using Game.Views.Planets;
using Modules.Planets;
using Zenject;

namespace Game.Presenters.Money
{
    public class CoinVfxManager : IInitializable, IDisposable
    {
        private readonly CoinVFXPool _pool;
        private readonly MoneyPresenter _moneyPresenter;

        public CoinVfxManager(CoinVFXPool pool, MoneyPresenter moneyPresenter)
        {
            _pool = pool;
            _moneyPresenter = moneyPresenter;
        }

        void IInitializable.Initialize()
        {
            PlanetPresenter.OnGathered += PlanetGatheredHandler;
        }

        void IDisposable.Dispose()
        {
            PlanetPresenter.OnGathered -= PlanetGatheredHandler;
        }

        private void PlanetGatheredHandler(PlanetPresenter planetPresenter)
        {
            var from = planetPresenter.GetCoinVfxSpawnPoint();
            var coin = _pool.Spawn(from);
            var to = _moneyPresenter.GetCoinVfxTargetPoint();
            coin.DOMove(to, 0.5f).OnComplete(() => _pool.Despawn(coin));
        }
    }
}