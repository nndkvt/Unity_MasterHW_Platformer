using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IsDestroyable : MonoBehaviour, IHaveHealth
{
    public int health { get ; set ; }

    public void ReceiveDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        health = 1;
    }
}
