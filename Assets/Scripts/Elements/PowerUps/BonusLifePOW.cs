using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusLifePOW : PowerUp
{
    override public void ApplyEffect(){
        var gc = FindObjectOfType<GameController>();
        gc.AddLife();
        Destroy(gameObject);
    }
}
