using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBlock : Block
{
    override public void ChangeColor(){
        float m_red = DecreaseColorComponent(spriteRenderer.color.r);
        float m_green= IncreaseColorComponent(spriteRenderer.color.g);
        float m_blue= IncreaseColorComponent(spriteRenderer.color.b);
        spriteRenderer.color = new Color(m_red,m_green,m_blue);
    }
}