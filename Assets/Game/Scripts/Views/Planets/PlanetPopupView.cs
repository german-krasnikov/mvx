using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views.Planets
{
    public class PlanetPopupView : MonoBehaviour
    {
        public event Action OnClose;

        [SerializeField]
        private Image _icon;
        [SerializeField]
        private TMP_Text _title;
        [SerializeField]
        private Button _closeButton;
        
        public void SetIcon(Sprite icon) => _icon.sprite = icon;
        public void SetTitle(string title) => _title.text = title;

        public void SetVisible(bool value) => gameObject.SetActive(value);

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(CloseButtonClickHandler);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(CloseButtonClickHandler);
        }

        private void CloseButtonClickHandler() => OnClose?.Invoke();
    }
}