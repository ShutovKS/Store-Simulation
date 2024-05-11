using TMPro;
using UnityEngine;

namespace UI.Market.Scripts
{
    public class MarketUI : MonoBehaviour
    {
        public static MarketUI Instance { get; private set; }
        
        [SerializeField] private TextMeshProUGUI balance;
        [SerializeField] private TextMeshProUGUI earned;
        [SerializeField] private TextMeshProUGUI spent;
        [SerializeField] private TextMeshProUGUI productCount;
        [SerializeField] private TextMeshProUGUI buyerCount;

        private void Awake()
        {
            Instance = this;
        }
    }
}