using EndlessRunner.UI;
using UnityEngine;

namespace EndlessRunner.Data
{
    [CreateAssetMenu(menuName = "Game Data/ UI Data", fileName = "UI Data")]
    public class UIData : ScriptableObject
    {
        [SerializeField] private UIMainMenu uiMainMenuPrefab;
        [SerializeField] private UIHUD uiHUDPrefab;
        [SerializeField] private UIGameOverMenu uiGameOverMenuPrefab;

        public UIMainMenu UIMainMenuPrefab => uiMainMenuPrefab;
        public UIHUD UIHUDPrefab => uiHUDPrefab;
        public UIGameOverMenu UIGameOverMenuPrefab => uiGameOverMenuPrefab;
    }
}