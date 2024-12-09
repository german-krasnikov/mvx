using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Views.Planets
{
    public class PlanetProgressBarView : MonoBehaviour
    {
        [SerializeField]
        private Image _progress;
        [SerializeField]
        private TMP_Text _timeText;
        
        public void SetProgress(string time, float progress)
        {
            _timeText.text = time;
            _progress.fillAmount = progress;
        }
    }
}