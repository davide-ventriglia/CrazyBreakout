using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Ball : MonoBehaviour
{
    GameObject lastHitObject;
    CircleCollider2D circleCollider;
    Paddle paddle;
    GameController gameController;
    public AudioClip OnPaddleHit;
    public AudioClip OnWallHit;
    public Vector2 Velocity;
    public bool isInStartPosition {get; private set;}

    void Awake() {
        circleCollider = GetComponent<CircleCollider2D>();
        gameController = FindObjectOfType<GameController>();
        paddle = FindObjectOfType<Paddle>();
        isInStartPosition = true;
    }

    void Update()
    {
        if(isInStartPosition){
            SpawnBall();
        }
        else{
            // managing ball collisions with raycast
            // there is a bug if hit a wall
            transform.Translate(Velocity * Time.deltaTime);
            RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, circleCollider.radius, Velocity, (Velocity*Time.deltaTime).magnitude);
            foreach(RaycastHit2D hit in hits){
                if(hit.collider != circleCollider && lastHitObject != hit.transform.gameObject){
                    lastHitObject = hit.transform.gameObject;
                    Velocity = Vector2.Reflect(Velocity, hit.normal);

                    if(hit.transform.GetComponent<Paddle>()){
                        Velocity.y = Mathf.Abs(Velocity.y);
                        gameController.audioController.PlayClip(OnPaddleHit);
                    }

                    if(hit.transform.GetComponent<Block>()){
                        hit.transform.GetComponent<Block>().OnHit();
                    }

                    if(hit.transform.tag == "Wall"){
                        gameController.audioController.PlayClip(OnWallHit);
                    }
                }
            }

            // managing ball collision
        }
    }

    void OnBecameInvisible() {
        Invoke("SpawnBall",1.0f);
        gameController.RemoveLife();
    }

    public void SpawnBall(){
        isInStartPosition = true;
        transform.position = new Vector3(paddle.transform.position.x, paddle.transform.position.y+0.5f,0);
    }

    public void PlayBall(){
        isInStartPosition = false;
    }

}
