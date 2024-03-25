using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextBlockScreen : MonoBehaviour
{
    private Dictionary<BlockBehaviour.Type, Sprite> blocksSpritesDictionary = new Dictionary<BlockBehaviour.Type, Sprite>();

    [SerializeField]
    private Sprite pinkBlock, greenBlock, blueBlock, orangeBlock, yellowBlock, cyanBlock, purpleBlock, bomb, filler;
    [SerializeField]
    private Image nextBlockImage;

    public void CreateDictionary()
    {
        blocksSpritesDictionary.Add(BlockBehaviour.Type.PinkBlock, pinkBlock);
        blocksSpritesDictionary.Add(BlockBehaviour.Type.GreenBlock, greenBlock);
        blocksSpritesDictionary.Add(BlockBehaviour.Type.BlueBlock, blueBlock);
        blocksSpritesDictionary.Add(BlockBehaviour.Type.OrangeBlock, orangeBlock);
        blocksSpritesDictionary.Add(BlockBehaviour.Type.YellowBlock, yellowBlock);
        blocksSpritesDictionary.Add(BlockBehaviour.Type.CyanBlock, cyanBlock);
        blocksSpritesDictionary.Add(BlockBehaviour.Type.PurpleBlock, purpleBlock);
        blocksSpritesDictionary.Add(BlockBehaviour.Type.Bomb, bomb);
        blocksSpritesDictionary.Add(BlockBehaviour.Type.Filler, filler);
    }



    public void UpdateNextBlock(BlockBehaviour.Type type)
    {
        nextBlockImage.sprite = blocksSpritesDictionary[type];
    }
}
