using System;
using Game.Gameplay;
using Game.Presenters.Planets;
using Game.Views.Planets;
using Modules.Money;
using Zenject;

namespace Game.Presenters.Money
{
    public class MoneyPresenter : IInitializable, IDisposable
    {
        public class Factory : PlaceholderFactory<MoneyView, MoneyPresenter>
        {
        }

        private readonly MoneyView _view;
        private IMoneyStorage _moneyStorage;

        public MoneyPresenter(MoneyView view, IMoneyStorage moneyStorage)
        {
            _moneyStorage = moneyStorage;
            _view = view;
        }

        public void Initialize()
        {
            _moneyStorage.OnMoneyChanged += MoneyChangedHandler;
            _view.SetMoney(_moneyStorage.Money.ToString());
        }

        public void Dispose()
        {
            _moneyStorage.OnMoneyChanged -= MoneyChangedHandler;
        }

        private void MoneyChangedHandler(int newValue, int range)
        {
            _view.SetMoney(newValue.ToString());
        }
    }
}