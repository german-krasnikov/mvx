using System;
using Game.Views.Planets;
using Modules.Planets;
using Zenject;

namespace Game.Presenters.Planets
{
    public class PlanetPresenter : IInitializable, IDisposable
    {
        private PlanetView _view;
        private Planet _model;

        public PlanetPresenter(Planet model, PlanetView view)
        {
            _model = model;
            _view = view;
        }

        void IInitializable.Initialize()
        {
            _view.OnClick += ClickHandler;
            _model.OnIncomeReady += IncomeReadyHandler;
            _model.OnIncomeTimeChanged += IncomeTimeChangedHandler;
            Invalidate();
        }

        void IDisposable.Dispose()
        {
            _view.OnClick -= ClickHandler;
        }

        private void Invalidate()
        {
            _view.SetIcon(_model.GetIcon(_model.IsUnlocked));
            _view.SetLock(!_model.IsUnlocked);
            _view.SetPrice(_model.Price.ToString());
            //view.SetProgress(planet.planet.IncomeProgress);
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
            }
        }

        private void IncomeReadyHandler(bool value)
        {
            
        }

        private void IncomeTimeChangedHandler(float time)
        {
            _view.SetProgress(time.ToString("00:00"), _model.IncomeProgress);
        }
    }
}