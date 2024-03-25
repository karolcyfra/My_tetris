using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Action onClickFillerButton;
    public Action onClickBackButton;
    public Action onClickRestartButton;
    public Action onClickExitButton;

    public GameData gameData;

    [SerializeField] private GameOverScreen gameOverScreen;
    [SerializeField] private StatsScreen statsScreen;
    [SerializeField] public NextBlockScreen nextBlockScreen;

    void Start()
    {
        gameOverScreen.onClickBackToMenu += BackToMenu;
        gameOverScreen.onClickRestart += RestartScene;
        gameOverScreen.onClickExit += ExitGame;   
    }

    public void CreateUI()
    {
        UpdateStatsScreen();
        nextBlockScreen.CreateDictionary();
    }
    public void UpdateStatsScreen()
    {
        statsScreen.UpdateStats(gameData.currentLevel, gameData.destroyedLines);
    }

    public void ShowGameOverScreen()
    {
        gameOverScreen.ShowGameOverScreen();
    }

    private void BackToMenu()
    {
        onClickBackButton?.Invoke();
    }

    private void RestartScene()
    {
        onClickRestartButton?.Invoke();
    }

    private void ExitGame()
    {
        onClickExitButton?.Invoke();
    }

    public void UpdateNextBlock(BlockBehaviour.Type type)
    {
        nextBlockScreen.UpdateNextBlock(type);
    }

}
