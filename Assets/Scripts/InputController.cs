using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    GameController gameController;
    BlockController blockController;
    Paddle paddle;
    Ball ball;
    float moveX;
    void Awake(){
        gameController = FindObjectOfType<GameController>();
        blockController = FindObjectOfType<BlockController>();
        paddle = FindObjectOfType<Paddle>();
        ball = FindObjectOfType<Ball>();
    }
    void Update()
    {
        // P: Pause the game
        if(!gameController.isGamePaused && Input.GetKeyDown(KeyCode.P)){
            gameController.PauseGame();
        } else if (gameController.isGamePaused && Input.GetKeyDown(KeyCode.P)){
            gameController.UnpauseGame();
        }

        // Get X Axis Input
        if(!gameController.isGamePaused && Input.GetAxisRaw("Horizontal")!=0){
            // Raw because is discrete, not continous
            moveX = Input.GetAxisRaw("Horizontal");
            paddle.MovePaddle(moveX);
        }

        // SPACE: play the ball
        if(ball.isInStartPosition && Input.GetKeyDown(KeyCode.Space)){
            ball.PlayBall();
            ball.Velocity = new Vector2(Random.Range(-2.0f,2.0f),Random.Range(3.0f,5.0f));
        }

        // TEST COMMANDS
        // if(!gameController.isGamePaused && Input.GetKeyDown(KeyCode.X)){
        //     blockController.EraseLevel();
        // }
        // if(!gameController.isGamePaused && Input.GetKeyDown(KeyCode.R)){
        //     blockController.DrawLevel(1);
        // }

    }
}
