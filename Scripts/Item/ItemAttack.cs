using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAttack : ItemComponent
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponentInChildren<ShootComponent>().GetAttackUpItem();
            Destroy(gameObject);
        }
    }
}
