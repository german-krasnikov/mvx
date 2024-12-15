using System;
using Modules.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views.Planets
{
    public class PlanetView : MonoBehaviour
    {
        public event Action OnClick;
        public event Action OnHold;

        [SerializeField]
        private Image _icon;
        [SerializeField]
        private GameObject _lock;
        [SerializeField]
        private GameObject _coin;
        [SerializeField]
        private GameObject _priceContainer;
        [SerializeField]
        private TMP_Text _priceText;
        [SerializeField]
        private PlanetProgressBarView _progress;
        [SerializeField]
        private SmartButton _button;

        public void SetIcon(Sprite icon) => _icon.sprite = icon;

        public void SetLock(bool isLocked)
        {
            _lock.SetActive(isLocked);
            SetCoinVisible(!isLocked);
            SetPriceVisible(!isLocked);
        }

        public void SetCoinVisible(bool isVisible) => _coin.SetActive(isVisible);

        public void SetPriceVisible(bool isVisible) => _priceContainer.SetActive(isVisible);

        public void SetPrice(string price) => _priceText.text = price;

        public void SetProgress(string time, float progress) => _progress.SetProgress(time, progress);
        public void SetProgressVisible(bool isVisible) => _progress.gameObject.SetActive(isVisible);

        private void OnEnable()
        {
            _button.OnClick += ClickHandler;
            _button.OnHold += HoldHandler;
        }

        private void OnDisable()
        {
            _button.OnClick -= ClickHandler;
            _button.OnHold -= HoldHandler;
        }

        private void ClickHandler() => OnClick?.Invoke();
        private void HoldHandler() => OnHold?.Invoke();
    }
}