using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public virtual void ApplyEffect(){

    }

    protected void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Colliding with " + other.gameObject.name);
        if(other.gameObject.tag == "Paddle"){
            ApplyEffect();
        }
    }

    protected void OnBecameInvisible() {
        Destroy(gameObject);
    }

}
