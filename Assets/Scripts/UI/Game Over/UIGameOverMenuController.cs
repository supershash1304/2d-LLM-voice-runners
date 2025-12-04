using EndlessRunner.Data;
using EndlessRunner.UI;
using UnityEngine;
using System.Collections.Generic;

public class UIGameOverMenuController
{
    private UIData uiData;
    private Canvas uiCanvas;
    private UIManager uiManager;

    private UIGameOverMenu uiGameOverMenu;

    public UIGameOverMenuController(UIData uiData, Canvas uiCanvas, UIManager uiManager)
    {
        this.uiData = uiData;
        this.uiCanvas = uiCanvas;
        this.uiManager = uiManager;
    }

    public void InitializeController()
    {
        uiGameOverMenu = GameObject.Instantiate<UIGameOverMenu>(uiData.UIGameOverMenuPrefab, uiCanvas.transform);
        uiGameOverMenu.SetController(this);
    }

    public void ShowUI() => uiGameOverMenu.ShowUI();
    public void HideUI() => uiGameOverMenu.HideUI();
    public void OnRestartGame() => uiManager.OnRestartGame();
    public void OnQuitGame() => uiManager.OnQuitGame();
    public void OnGameOver(int finalScore, int highScore)
    {
        uiGameOverMenu.OnGameOver(finalScore, highScore);
    }



}
