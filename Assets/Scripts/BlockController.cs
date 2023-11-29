using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using Mono.Cecil;

public class BlockController : MonoBehaviour
{
    GameController gameController;
    public GameObject RedBlockPrefab;
    public GameObject GreenBlockPrefab;
    public GameObject YellowBlockPrefab;
    public GameObject BlockPrefab;
    // replace with min and max
    float minX = -7f;
    float maxX = 7f;
    float minY = 2f;
    float maxY = 0.08f;
    float[] xCoordinates = new[] {-7f,-6f,-5f,-4f,-3f,-2f,-1f,0f,1f,2f,3f,4f,5f,6f,7f};
    float[] yCoordinates = new[] {2f,1.68f,1.36f,1.04f,0.72f,0.40f};

    private void Awake() {
        gameController = FindObjectOfType<GameController>();
    }
    private void Update() {
        if(gameController.Score>0 && transform.childCount <= 0){
            gameController.PlayNextLevel();
        }
    }
    public void DrawLevel(int level){
        EraseLevel();
        string levelText = Resources.Load("Level"+level.ToString()).ToString();
        float i = minX;
        float j = minY;
        foreach(char current in levelText){
            switch(current){
                case 'R': Instantiate(RedBlockPrefab, new Vector3(i, j, 0), Quaternion.identity, gameObject.transform); break;
                case 'G': Instantiate(GreenBlockPrefab, new Vector3(i, j, 0), Quaternion.identity, gameObject.transform); break;
                case 'Y': Instantiate(YellowBlockPrefab, new Vector3(i, j, 0), Quaternion.identity, gameObject.transform); break;
                case 'B': Instantiate(BlockPrefab, new Vector3(i, j, 0), Quaternion.identity, gameObject.transform); break;
                case '-': break;
                case '\n': j-=0.32f; i=minX; break;
                default: break;
            }
            i++;
        }
    }

    public void EraseLevel(){
        foreach(Transform child in gameObject.transform){
            Destroy(child.gameObject);
        }
    }
}
