using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public Action onClickRestart;
    public Action onClickExit;
    public Action onClickBackToMenu;

    public Action onShowGameOverScreen;

    [SerializeField]
    private Button exitGameButton;
    [SerializeField]
    private Button restartButton;
    [SerializeField]
    private Button backToMenuButton;

    void Start()
    {
        exitGameButton.onClick.AddListener(ExitGameClicked);
        restartButton.onClick.AddListener(RestartGameClicked);
        backToMenuButton.onClick.AddListener(BackToMenuClicked);
    }


    public void ShowGameOverScreen()
    {
        this.gameObject.SetActive(true);
        onShowGameOverScreen?.Invoke();
    }

    public void CloseGameOverScreen()
    {
        this.gameObject.SetActive(false);
    }

    private void ExitGameClicked()
    {
        onClickExit?.Invoke();
    }

    private void RestartGameClicked()
    {
        onClickRestart?.Invoke();
    }

    private void BackToMenuClicked()
    {
        onClickBackToMenu?.Invoke();
    }

    public void EnableButtons()
    {
        exitGameButton.interactable = true;
        restartButton.interactable = true;
        backToMenuButton.interactable = true;
    }
}
