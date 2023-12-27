using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Paddle : MonoBehaviour
{
    public float defaultSpeed;
    public bool isShooting;
    float startingWidth = 2.0f;
    float startingHeight = 0.32f;
    
    private void Start() {
        ResetToDefault();
    }

    void ResetToDefault(){
        isShooting = false;
        gameObject.GetComponent<SpriteRenderer>().size = new Vector2(startingWidth,startingHeight);
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(startingWidth,startingHeight);
    }

    public void MovePaddle(float moveX){
        transform.position += Vector3.right * moveX * defaultSpeed * Time.deltaTime;
    }
}
