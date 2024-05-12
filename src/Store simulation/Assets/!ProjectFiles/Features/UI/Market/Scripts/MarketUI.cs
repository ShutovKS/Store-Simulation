using TMPro;
using UnityEngine;

namespace UI.Market.Scripts
{
    public class MarketUI : MonoBehaviour
    {
        public static MarketUI Instance { get; private set; }

        [field: SerializeField] public TextMeshProUGUI Balance { get; private set; }
        [field: SerializeField] public TextMeshProUGUI Earned { get; private set; }
        [field: SerializeField] public TextMeshProUGUI Spent { get; private set; }
        [field: SerializeField] public TextMeshProUGUI ProductCount { get; private set; }
        [field: SerializeField] public TextMeshProUGUI BuyerCount { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}