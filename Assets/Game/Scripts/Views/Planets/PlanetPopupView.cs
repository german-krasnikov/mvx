using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views.Planets
{
    public class PlanetPopupView : MonoBehaviour
    {
        public event Action OnClose;
        public event Action OnUpgrade;

        [SerializeField]
        private Image _icon;
        [SerializeField]
        private TMP_Text _title;
        [SerializeField]
        private TMP_Text _population;
        [SerializeField]
        private TMP_Text _level;
        [SerializeField]
        private TMP_Text _income;
        [SerializeField]
        private Button _closeButton;
        [SerializeField]
        private Button _upgradeButton;
        [SerializeField]
        private TMP_Text _upgradePrice;

        public void SetIcon(Sprite icon) => _icon.sprite = icon;
        public void SetTitle(string title) => _title.text = title;

        public void SetVisible(bool value) => gameObject.SetActive(value);

        public void SetPopulation(string population) => _population.text = population;
        public void SetLevel(string level) => _level.text = level;
        public void SetIncome(string income) => _income.text = income;

        public void SetUpgradeEnabled(bool value) => _upgradeButton.interactable = value;
        public void SetUpgradePrice(string price) => _upgradePrice.text = price;

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(CloseButtonClickHandler);
            _upgradeButton.onClick.AddListener(UpgradeButtonClickHandler);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(CloseButtonClickHandler);
            _upgradeButton.onClick.RemoveListener(UpgradeButtonClickHandler);
        }

        private void CloseButtonClickHandler() => OnClose?.Invoke();
        private void UpgradeButtonClickHandler() => OnUpgrade?.Invoke();
    }
}