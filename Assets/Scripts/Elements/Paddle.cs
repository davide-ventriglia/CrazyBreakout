using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Paddle : MonoBehaviour
{
    public float MoveSpeed;

    public void MovePaddle(float moveX){
        transform.position += Vector3.right * moveX * MoveSpeed * Time.deltaTime;
    }
}
