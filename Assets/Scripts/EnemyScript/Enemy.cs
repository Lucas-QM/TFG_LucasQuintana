using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string name;
    public float hp;
    public float speed;
    public float knockbackForceX;
    public float knockbackForceY;
    public float damageToGive;
    public bool shouldRespawn;
}
