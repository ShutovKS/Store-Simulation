using System;
using UI.Scripts.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenuScreen.Scripts
{
    public class MainMenuScreen : BaseScreen
    {
        [field: SerializeField] public Button StartGameButton { get; private set; }
    }
}