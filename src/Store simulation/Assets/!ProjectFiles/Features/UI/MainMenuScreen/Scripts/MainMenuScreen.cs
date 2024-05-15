using UI.Scripts.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenuScreen.Scripts
{
    public class MainMenuScreen : BaseScreen
    {
        [field: SerializeField] public Button RunSimulationButton { get; private set; }
        [field: SerializeField] public Button StatisticsButton { get; private set; }
        [field: SerializeField] public Button ExitButton { get; private set; }
    }
}