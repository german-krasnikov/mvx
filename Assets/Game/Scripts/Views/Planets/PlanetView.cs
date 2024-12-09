using TMPro;
using UnityEngine;

namespace Game.Views.Planets
{
    public class PlanetView : MonoBehaviour
    {
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

        public void SetLock(bool isLocked)
        {
            _lock.SetActive(false);
        }

        public void SetPrice(string price)
        {
            _priceText.text = price;
        }
        
        public void SetProgress(string time, float progress) => _progress.SetProgress(time, progress);
    }
}