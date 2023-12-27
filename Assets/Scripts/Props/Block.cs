using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField]
    public Block_SO blockData;
    SpriteRenderer spriteRenderer;
    GameController gameController;
    int hits;
    
    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameController = FindObjectOfType<GameController>();
        hits = blockData.hits;
    }

    public void OnHit(){
        hits -= 1;
        if (hits <= 0){
            Instantiate(blockData.BlockExplosionPrefab,transform.position,Quaternion.identity);
            ReturnToPool();
            // Destroy(gameObject);
            return;
        }
        ChangeColor();
        gameController.audioController.PlayClip(blockData.OnBlockHit);
    }

    public void ChangeColor(){
        spriteRenderer.color = new Color(
            spriteRenderer.color.r + blockData.RGBModifiers.R,
            spriteRenderer.color.g + blockData.RGBModifiers.G,
            spriteRenderer.color.b + blockData.RGBModifiers.B
        );
    }

    public void SpawnPOW(){
        var pow = Instantiate(gameController.PowerUpsPrefabs[Random.Range(0,gameController.PowerUpsPrefabs.Count)], new Vector3(transform.position.x, transform.position.y -1, 0), Quaternion.identity);
        Rigidbody2D rb = pow.GetComponent<Rigidbody2D>();
        rb.AddForce(Vector2.down * 100);
    }
    
    void ReturnToPool() {
        gameController.AddScore(blockData.score);
        gameController.audioController.PlayClip(blockData.OnBlockDestroyed);
        BlockController.instance.ReturnBlockToProperPool(gameObject);
        BlockController.instance.CheckLevelCleared();
    }
}
