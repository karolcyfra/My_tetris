using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TetrisGameManager : MonoBehaviour
{
    public Controls.NumberOfPlayer numberOfPlayer;
    public Controls controls = new Controls();
    private GameData gameData = new GameData();
    private StartingConfig startingConfig = new StartingConfig();

    [SerializeField] private UIManager uiManager;
    [SerializeField] private BlocksManager blocksManager;
    [SerializeField] GameMode gameModeA;
    [SerializeField] GameMode gameModeB;

    void Awake()
    {
        controls.SetControls(numberOfPlayer, true); //todo
        gameData.fallingSpeed = startingConfig.startingFallingSpeed;
        blocksManager.startingConfig = startingConfig;
        
        blocksManager.gameData = gameData;
        blocksManager.controls = controls;

        uiManager.gameData = gameData;
        uiManager.CreateUI();
        blocksManager.Init();

        blocksManager.onGameOver += GameOver;

        uiManager.onClickBackButton += BackToMenu;
        uiManager.onClickExitButton += ExitGame;
        uiManager.onClickRestartButton += RestartScene;

        blocksManager.onLineDestroyed += IncreamentNumberOfDestroyedLines;
        blocksManager.onNextBlockSelected += OnNextBlockSelected;

        blocksManager.ChooseAndDisplayNextBlock();
        blocksManager.SpawnBlock();    
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            SetGameMode(gameModeA);
        }
        else if(Input.GetKeyDown(KeyCode.Keypad2))
        {
            SetGameMode(gameModeB);
        }
    }
    private void SetGameMode(GameMode gameMode)
    {
        startingConfig.bombRadius = gameMode.bombRadius;
        startingConfig.startingFallingSpeed = gameMode.startingFallingSpeed;
        startingConfig.difficultyGrowing = gameMode.difficultyGrowing;
        startingConfig.nextLevelRequirement = gameMode.nextLevelRequirement;
        startingConfig.downKeyAcceleration = gameMode.downKeyAcceleration;

        gameData.fallingSpeed = gameMode.startingFallingSpeed;
    }

    private void OnNextBlockSelected(BlockBehaviour.Type type)
    {
        uiManager.UpdateNextBlock(type);
    }
    private void GameOver()
    {
        uiManager.ShowGameOverScreen();
    }

    private void BackToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    private void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ExitGame()
    {
        Application.Quit();
    }

    private void IncreamentNumberOfDestroyedLines()
    {
        gameData.destroyedLines++;
        int calculatedLevel = (int)(gameData.destroyedLines / startingConfig.nextLevelRequirement);
        if (calculatedLevel >= gameData.currentLevel)
        {
            gameData.currentLevel = calculatedLevel + 1;
            gameData.fallingSpeed = gameData.fallingSpeed - (gameData.fallingSpeed * startingConfig.difficultyGrowing);
        }
        uiManager.UpdateStatsScreen();
    }

}
