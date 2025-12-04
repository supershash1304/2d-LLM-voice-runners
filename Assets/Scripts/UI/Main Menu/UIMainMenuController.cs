using EndlessRunner.Data;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

namespace EndlessRunner.UI
{
    public class UIMainMenuController
    {
        private UIData uiData;
        private Canvas uiCanvas;
        private UIManager uiManager;

        private UIMainMenu uiMainMenu;

        public UIMainMenuController(UIData uiData, Canvas uiCanvas, UIManager uiManager)
        {
            this.uiData = uiData;
            this.uiCanvas = uiCanvas;
            this.uiManager = uiManager;
        }

        public void InitializeController()
        {
            uiMainMenu = GameObject.Instantiate<UIMainMenu>(uiData.UIMainMenuPrefab, uiCanvas.transform);
            uiMainMenu.SetController(this);
        }

        public void ShowUI() => uiMainMenu.ShowUI();
        public void HideUI() => uiMainMenu.HideUI();
        public void OnStartButtonClicked() => uiManager.OnStartButtonClicked();
    }
}