using System;
using Game.Views.Planets;
using Modules.Money;
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
        private readonly MoneyStorage _moneyStorage;

        public PlanetPopupPresenter(PlanetPopupView view, MoneyStorage moneyStorage)
        {
            _view = view;
            _moneyStorage = moneyStorage;
        }

        public void Show(Planet model)
        {
            _model = model;
            _model.OnUpgraded += UpgradedHandler;
            Invalidate();
            _view.SetVisible(true);
        }

        void IInitializable.Initialize()
        {
            _view.OnClose += CloseHandler;
            _moneyStorage.OnMoneyChanged += MoneyChangedHandler;
        }

        void IDisposable.Dispose()
        {
            _view.OnClose -= CloseHandler;
            _moneyStorage.OnMoneyChanged -= MoneyChangedHandler;
            if (_model == null) return;
            _model.OnUpgraded -= UpgradedHandler;
        }

        private void UpgradedHandler(int value)
        {
            if (!_model.CanUpgrade) return;
            _model.Upgrade();
            Invalidate();
        }

        private void CloseHandler() => _view.SetVisible(false);

        private void Invalidate()
        {
            _view.SetIcon(_model.GetIcon(_model.IsUnlocked));
            _view.SetTitle(_model.Name);
            _view.SetPopulation($"Population: {_model.Population}");
            _view.SetIncome($"Income: {_model.MinuteIncome}/sec");
            _view.SetLevel($"Level: {_model.Level}/{_model.MaxLevel}");
            _view.SetUpgradeEnabled(_model.CanUpgrade);
            _view.SetUpgradePrice(_model.Price.ToString());
        }

        private void MoneyChangedHandler(int newValue, int prevValue) => Invalidate();
    }
}