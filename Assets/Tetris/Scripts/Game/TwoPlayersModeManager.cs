using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TwoPlayersModeManager : MonoBehaviour
{
    [SerializeField]
    private GameOverScreen gameOverScreen1;
    [SerializeField]
    private GameOverScreen gameOverScreen2;

    bool playerOneFailed = false;
    bool playerTwoFailed = false;

    void Start()
    {
        gameOverScreen1.onShowGameOverScreen += PlayerOneFailed;
        gameOverScreen2.onShowGameOverScreen += PlayerTwoFailed;
    }

    private void PlayerOneFailed()
    {
        playerOneFailed = true;
        if(playerTwoFailed)
        {
            EnableGameOverButtons();
        }
    }
    private void PlayerTwoFailed()
    {
        playerTwoFailed = true;
        if(playerOneFailed)
        {
            EnableGameOverButtons();
        }
    }

    private void EnableGameOverButtons()
    {
        gameOverScreen1.EnableButtons();
        gameOverScreen2.EnableButtons();
    }

}
