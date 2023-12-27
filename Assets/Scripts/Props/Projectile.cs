using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{   
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Block"){
            Destroy(other.gameObject);
            Destroy(gameObject);
        } else if(other.gameObject.tag == "Wall"){
            Destroy(gameObject);
        }
    }

    void OnBecameInvisible() {
        Destroy(gameObject);
    }
}
