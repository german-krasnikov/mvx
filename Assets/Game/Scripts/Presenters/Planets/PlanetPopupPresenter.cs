using System;
using Game.Views.Planets;
using Modules.Planets;
using UnityEngine;
using Zenject;

namespace Game.Presenters.Planets
{
    public class PlanetPopupPresenter : IInitializable, IDisposable
    {
        public class Factory : PlaceholderFactory<PlanetPopupView, PlanetPopupPresenter>
        {
        }

        private readonly PlanetPopupView _view;
        private Planet _model;

        public PlanetPopupPresenter(PlanetPopupView view)
        {
            _view = view;
        }

        public void Show(Planet model)
        {
            _model = model;
            Invalidate();
            _view.SetVisible(true);
        }

        void IInitializable.Initialize()
        {
            _view.OnClose += CloseHandler;
        }

        void IDisposable.Dispose()
        {
            _view.OnClose -= CloseHandler;
        }

        private void CloseHandler()
        {
            _view.SetVisible(false);
        }

        private void Invalidate()
        {
            _view.SetIcon(_model.GetIcon(_model.IsUnlocked));
            _view.SetTitle(_model.Name);
            _view.SetPopulation($"Population: {_model.Population}");
            _view.SetIncome($"Income: {_model.MinuteIncome}/sec");
            _view.SetLevel($"Level: {_model.Level}/{_model.MaxLevel}");
        }
    }
}