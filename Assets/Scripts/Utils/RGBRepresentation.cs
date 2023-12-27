using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct RGBRepresentation
{
    [SerializeField]
    [Range(-1.0f,1.0f)]
    float m_R;
    public float R => m_R;
    [SerializeField]
    [Range(-1.0f,1.0f)]
    float m_G;
    public float G => m_G;
    [SerializeField]
    [Range(-1.0f,1.0f)]
    float m_B;
    public float B => m_B;
}
