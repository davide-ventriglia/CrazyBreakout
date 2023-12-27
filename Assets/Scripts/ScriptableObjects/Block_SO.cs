using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

[CreateAssetMenu(fileName = "Block", menuName = "Elements/Block")]
public class Block_SO : PoolableObject_SO
{
    [SerializeField]
    int m_score = 100, m_hits = 1;
    public int score => m_score;
    public int hits => m_hits;
    public GameObject BlockExplosionPrefab;
    public AudioClip OnBlockDestroyed;
    public AudioClip OnBlockHit;
    [SerializeField]
    RGBRepresentation m_RGBModifiers;
    public RGBRepresentation RGBModifiers => m_RGBModifiers;
}
