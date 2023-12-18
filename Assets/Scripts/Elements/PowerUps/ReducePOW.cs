using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReducePOW : PowerUp
{
    override public void ApplyEffect(){
        Paddle paddle = FindObjectOfType<Paddle>();
        SpriteRenderer sr = paddle.GetComponent<SpriteRenderer>();
        BoxCollider2D bc = paddle.GetComponent<BoxCollider2D>();
        sr.size -= new Vector2(0.5f,0f);
        bc.size -= new Vector2(0.5f,0f);
        Destroy(gameObject);
    }
}
