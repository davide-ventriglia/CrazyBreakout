using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPOW : PowerUp
{
    override public void ApplyEffect(){
        Paddle paddle = FindObjectOfType<Paddle>();
        paddle.isShooting = true;
        Destroy(gameObject);
    }
}
