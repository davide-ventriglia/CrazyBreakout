using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public int Hits = 1;
    public int ScoreValue = 100;
    public AudioClip OnBlockDestroyed;
    public AudioClip OnBlockHit;
    protected SpriteRenderer spriteRenderer;
    GameController gameController;
    GameObject BlockExplosionPrefab;
    protected void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameController = FindObjectOfType<GameController>();
        BlockExplosionPrefab = (GameObject)Resources.Load("BlockExplosion");
    }

    public void OnHit(){
        Hits -= 1;
        if (Hits <= 0){
            gameController.AddScore(ScoreValue);
            gameController.audioController.PlayClip(OnBlockDestroyed);
            Instantiate(BlockExplosionPrefab,transform.position,Quaternion.identity);
            Destroy(gameObject);
            return;
        }
        ChangeColor();
        gameController.audioController.PlayClip(OnBlockHit);
    }

    public virtual void ChangeColor(){

    }

    protected float IncreaseColorComponent(float _colorComponent){
        return _colorComponent < 1.0f ? _colorComponent+0.2f : _colorComponent;
    }

    protected float DecreaseColorComponent(float _colorComponent){
        return _colorComponent >= 0.2f ? _colorComponent-0.2f : _colorComponent;;
    }

}
