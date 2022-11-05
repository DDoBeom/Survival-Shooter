using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAttackSpeed : ItemComponent
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponentInChildren<ShootComponent>().GetAttackSpeedItem();
            Destroy(gameObject);
        }
    }

}
