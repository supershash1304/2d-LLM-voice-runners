using EndlessRunner.Data;
using UnityEngine;
using System.Collections.Generic;

namespace EndlessRunner.UI
{
    public class UIHUDController
    {
        private UIData uiData;
        private Canvas uiCanvas;
        private UIManager uiManager;

        private UIHUD uiHUD;

        public UIHUDController(UIData uiData, Canvas uiCanvas, UIManager uiManager)
        {
            this.uiData = uiData;
            this.uiCanvas = uiCanvas;
            this.uiManager = uiManager;
        }

        public void InitializeController()
        {
            uiHUD = GameObject.Instantiate<UIHUD>(uiData.UIHUDPrefab, uiCanvas.transform);
        }
        public void QuitGame()
        {
            Debug.Log("[UI] Quit pressed");

            Application.Quit();

        #if UNITY_EDITOR
         UnityEditor.EditorApplication.isPlaying = false;
        #endif
        }


        public void ShowUI() => uiHUD.ShowUI();
        public void HideUI() => uiHUD.HideUI();
        public void OnScoreUpdated(int playerScore) => uiHUD.OnScoreUpdated(playerScore);
    }
}