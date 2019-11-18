using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{


    private void Awake()
    {
        transform.tag = "Enemy";
    }

    public void Damage(int amount)
    {
        Debug.Log("Hit enemy for " + amount);
    }
}
