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
    public float initialScalarVelocity = 5.0f;
    float scalarVelocity;
    public bool isInStartPosition {get; private set;}
    public float startingPositionYOffset = 0.5f;
    public float scalarVelocityIncrement = 0.1f;
    float vx;
    float vy;

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
            transform.Translate(Velocity * Time.deltaTime);
        }
    }

    void OnBecameInvisible() {
        Invoke("SpawnBall",1.0f);
        gameController.RemoveLife();
    }

    public void SpawnBall(){
        isInStartPosition = true;
        transform.position = new Vector3(paddle.transform.position.x, paddle.transform.position.y + startingPositionYOffset,paddle.transform.position.z);
    }

    public void PlayBall(){
        isInStartPosition = false;
        scalarVelocity = initialScalarVelocity;

        float startingDegAngle = Random.Range(45f,145f);
        vx = initialScalarVelocity * Mathf.Cos(Mathf.Deg2Rad * startingDegAngle);
        vy = initialScalarVelocity * Mathf.Sin(Mathf.Deg2Rad * startingDegAngle);
        Velocity = new Vector2(vx,vy);
    }

    public void IncreaseVelocity(){
        scalarVelocity += scalarVelocityIncrement;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Paddle"){
            float relativeCollisionX = other.contacts[0].point.x - other.transform.position.x;
            float maxPaddleX = other.collider.bounds.max.x - other.transform.position.x;
            // need to adjust/normalize values (?)
            vy = scalarVelocity * ((maxPaddleX-Mathf.Abs(relativeCollisionX))/maxPaddleX);
            vx = Mathf.Sqrt(Mathf.Pow(scalarVelocity,2) - Mathf.Pow(vy,2)) * Mathf.Sign(relativeCollisionX);
            Velocity = new Vector2(vx,vy);
            gameController.audioController.PlayClip(OnPaddleHit);
        } else {
            if(other.gameObject.tag == "Wall"){
                IncreaseVelocity();
                gameController.audioController.PlayClip(OnWallHit);
            } else if (other.gameObject.tag == "Block"){
                other.gameObject.GetComponent<Block>().OnHit();
            }
            Velocity = Vector2.Reflect(Velocity, other.contacts[0].normal);
        }
    }

}
