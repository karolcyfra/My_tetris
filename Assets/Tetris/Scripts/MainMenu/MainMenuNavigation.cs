using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuNavigation : MonoBehaviour
{
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button exitGameButton;
    [SerializeField] private Button twoPlayersButton;

    void Start()
    {
        startGameButton.onClick.AddListener(StartGame);
        exitGameButton.onClick.AddListener(ExitGame);
        twoPlayersButton.onClick.AddListener(StartTwoPlayersMode);
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Tetris");
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void StartTwoPlayersMode()
    {
        SceneManager.LoadScene("Two Players Tetris");
    }
}
