using TMPro;
using UnityEngine;

namespace Game.Views.Planets
{
    public class MoneyView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _money;
        
        public void SetMoney(string money) => _money.text = money;
    }
}