using System;
using Game.Views.Planets;
using Modules.Money;
using Modules.Planets;
using Zenject;

namespace Game.Presenters.Planets
{
    public class PlanetPopupPresenter
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
            Subscribe();
            Invalidate();
            _view.SetVisible(true);
        }

        private void Subscribe()
        {
            _view.OnClose += CloseHandler;
            _view.OnUpgrade += UpgradeHandler;
            _moneyStorage.OnMoneyChanged += MoneyChangedHandler;
        }

        private void Unsubscribe()
        {
            _view.OnClose -= CloseHandler;
            _view.OnUpgrade -= UpgradeHandler;
            _moneyStorage.OnMoneyChanged -= MoneyChangedHandler;
        }

        private void CloseHandler()
        {
            _view.SetVisible(false);
            Unsubscribe();
        }

        private void UpgradeHandler()
        {
            if (!_model.CanUpgrade) return;
            _model.Upgrade();
            Invalidate();
        }

        private void Invalidate()
        {
            _view.SetIcon(_model.GetIcon(_model.IsUnlocked));
            _view.SetTitle(_model.Name);
            _view.SetPopulation($"Population: {_model.Population}");
            _view.SetIncome($"Income: {_model.MinuteIncome}/sec");
            _view.SetLevel($"Level: {_model.Level}/{_model.MaxLevel}");
            _view.SetUpgradeVisible(!_model.IsMaxLevel);
            _view.SetUpgradeEnabled(_model.CanUpgrade);
            _view.SetUpgradePrice(_model.Price.ToString());
        }

        private void MoneyChangedHandler(int newValue, int prevValue) => Invalidate();
    }
}