using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using Mono.Cecil;
using System.Linq;

public class BlockController : MonoSingleton<BlockController>
{
    GameController gameController;
    
    [SerializeField]
    Block_SO RedBlock;

    [SerializeField]
    Block_SO GreenBlock;

    [SerializeField]
    Block_SO YellowBlock;

    [SerializeField]
    Block_SO Block;

    public Block lastDestroyedBlock;
    // replace with min and max
    float minX = -7f;
    // float maxX = 7f;
    float minY = 2f;
    // float maxY = 0.08f;

    private void Awake() {
        gameController = FindObjectOfType<GameController>();
    }

    public void DrawLevel(int level){
        ClearLevel();
        string levelText = Resources.Load("Level"+level.ToString()).ToString();
        float i = minX;
        float j = minY;
        // foreach(char current in levelText){
        //     switch(current){
        //         case 'R': Instantiate(RedBlock.objectPrefab, new Vector3(i, j, 0), Quaternion.identity, gameObject.transform); i++; break;
        //         case 'G': Instantiate(GreenBlock.objectPrefab, new Vector3(i, j, 0), Quaternion.identity, gameObject.transform); i++; break;
        //         case 'Y': Instantiate(YellowBlock.objectPrefab, new Vector3(i, j, 0), Quaternion.identity, gameObject.transform); i++; break;
        //         case 'B': Instantiate(Block.objectPrefab, new Vector3(i, j, 0), Quaternion.identity, gameObject.transform); i++; break;
        //         case '-': i++; break;
        //         case '\n': j-=0.32f; i=minX; break;
        //         default: break;
        //     }
        // }

        foreach(char current in levelText){
            switch(current){
                case 'R': {
                    var obj = ObjectPoolManager.instance.GetPool("RedBlocksOP").GetPooledObject();
                    obj.transform.position = new Vector3(i, j, 0);
                    i++; break;
                }
                case 'G': {
                    var obj = ObjectPoolManager.instance.GetPool("GreenBlocksOP").GetPooledObject();
                    obj.transform.position = new Vector3(i, j, 0);
                    i++; break;
                }
                case 'Y': {
                    var obj = ObjectPoolManager.instance.GetPool("YellowBlocksOP").GetPooledObject();
                    obj.transform.position = new Vector3(i, j, 0);
                    i++; break;
                }
                case 'B': {
                    var obj = ObjectPoolManager.instance.GetPool("BaseBlocksOP").GetPooledObject();
                    obj.transform.position = new Vector3(i, j, 0);
                    i++; break;
                }
                case '-': i++; break;
                case '\n': j-=0.32f; i=minX; break;
                default: break;
            }
        }
    }

    public void ReturnBlockToProperPool(GameObject block){
        if(block.name.StartsWith("Red")){
            ObjectPoolManager.instance.GetPool("RedBlocksOP").ReturnPooledObject(block);
        } else if(block.name.StartsWith("Yellow")){
            ObjectPoolManager.instance.GetPool("YellowBlocksOP").ReturnPooledObject(block);
        } else if(block.name.StartsWith("Green")){
            ObjectPoolManager.instance.GetPool("GreenBlocksOP").ReturnPooledObject(block);
        } else if(block.name.StartsWith("Base")){
            ObjectPoolManager.instance.GetPool("BaseBlocksOP").ReturnPooledObject(block);
        } else {
            Debug.Log("Can't found proper pool for block: " + block.name);
        }
    }

    public void CheckLevelCleared(){
        var count = ObjectPoolManager.instance.GetActiveChildCount();
        if(count == 0){
            gameController.PlayNextLevel();
        }
    }

    public void ClearLevel(){
        var poolManager = ObjectPoolManager.instance.transform;
        for(int i=0; i < poolManager.childCount; i++){
            if(poolManager.GetChild(i).gameObject.activeInHierarchy){
                ReturnBlockToProperPool(poolManager.GetChild(i).gameObject);
            }
        }
    }
}
