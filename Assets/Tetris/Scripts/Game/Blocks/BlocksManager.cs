using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksManager : MonoBehaviour
{
    public Action<BlockBehaviour.Type> onNextBlockSelected;
    public Action onLineDestroyed;
    public Action<Transform> onBlockFallen;
    public Action onGameOver;
    public Controls controls;

    public GameData gameData;
    public StartingConfig startingConfig;

    private BlockBehaviour nextBlock;


    [SerializeField] private Transform spawnPoint;
    [SerializeField] private BlockBehaviour[] blocksPrefabs;
    [SerializeField] private Transform leftDownCorner;
    [SerializeField] private Transform rightUpCorner;
    [SerializeField] private SquaresGridManager squaresGridManager;

    void Awake()
    {
        squaresGridManager.onLineDestroyed += OnLineDestroyed;
    }

    public void Init()
    {
        squaresGridManager.CreateSquaresGrid();
        squaresGridManager.startingConfig = startingConfig;
    }


    public void ChooseAndDisplayNextBlock()
    {
        nextBlock = blocksPrefabs[UnityEngine.Random.Range(0, blocksPrefabs.Length)];
        onNextBlockSelected?.Invoke(nextBlock.type);     
    }

    public void SpawnBlock()
    {
        nextBlock.fallingSpeed = gameData.fallingSpeed;
        nextBlock.downKeyAcceleration = startingConfig.downKeyAcceleration;

        nextBlock.leftDownCorner = leftDownCorner;
        nextBlock.rightUpCorner = rightUpCorner;

        BlockBehaviour fallingBlock = Instantiate(nextBlock, spawnPoint.position, Quaternion.identity, squaresGridManager.transform);

        fallingBlock.controls = controls;
        fallingBlock.squaresDetector = squaresGridManager.squaresDetector;
        fallingBlock.onFallen += OnBlockFallen;
        fallingBlock.onOverGameArea += GameOver;

        ChooseAndDisplayNextBlock();
    }

    public void GameOver()
    {
        onGameOver?.Invoke();
    }

    private void OnBlockFallen(BlockBehaviour block)
    {
        squaresGridManager.AddToGrid(block);
        SpawnBlock();
        squaresGridManager.DestroyFilledLines();
    }

    private void OnLineDestroyed()
    {
        onLineDestroyed?.Invoke();
    }
}
