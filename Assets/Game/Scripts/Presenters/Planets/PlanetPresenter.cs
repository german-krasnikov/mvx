using System;
using Game.Views.Planets;
using Modules.Planets;
using UnityEngine;
using Zenject;

namespace Game.Presenters.Planets
{
    public class PlanetPresenter : IInitializable, IDisposable
    {
        public class Factory : PlaceholderFactory<Planet, PlanetView, PlanetPresenter>
        {
        }

        public static event Action<PlanetPresenter> OnGathered;

        private readonly PlanetView _view;
        private readonly Planet _model;
        private readonly PlanetPopupPresenter _planetPopupPresenter;

        public PlanetPresenter(Planet model, PlanetView view, PlanetPopupPresenter planetPopupPresenter)
        {
            _model = model;
            _view = view;
            _planetPopupPresenter = planetPopupPresenter;
        }

        void IInitializable.Initialize()
        {
            _view.OnClick += ClickHandler;
            _view.OnHold += HoldHandler;
            _model.OnIncomeReady += IncomeReadyHandler;
            _model.OnIncomeTimeChanged += IncomeTimeChangedHandler;
            Invalidate();
        }

        void IDisposable.Dispose()
        {
            _view.OnClick -= ClickHandler;
            _view.OnHold -= HoldHandler;
            _model.OnIncomeReady -= IncomeReadyHandler;
            _model.OnIncomeTimeChanged -= IncomeTimeChangedHandler;
        }
        
        public Vector2 GetCoinVfxSpawnPoint() => _view.GetCoinVfxSpawnPoint();

        private void Invalidate()
        {
            _view.SetIcon(_model.GetIcon(_model.IsUnlocked));
            _view.SetLock(!_model.IsUnlocked);
            _view.SetPrice(_model.Price.ToString());
            _view.SetCoinVisible(false);
            _view.SetPriceVisible(!_model.IsUnlocked);
            _view.SetProgressVisible(_model.IsUnlocked);
        }

        private void ClickHandler()
        {
            if (!_model.IsUnlocked && _model.CanUnlock)
            {
                _model.Unlock();
                Invalidate();
            }

            if (_model.IsUnlocked && _model.IsIncomeReady)
            {
                _model.GatherIncome();
                OnGathered?.Invoke(this);
            }
        }

        private void HoldHandler()
        {
            if (_model.IsUnlocked)
            {
                _planetPopupPresenter.Show(_model);
            }
        }

        private void IncomeReadyHandler(bool value)
        {
            _view.SetProgressVisible(!value);
            _view.SetCoinVisible(value);
        }

        private void IncomeTimeChangedHandler(float time)
        {
            _view.SetProgress(time.ToString("00:00"), _model.IncomeProgress);
        }
        
       
    }
}