using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCritical : ItemComponent
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponentInChildren<ShootComponent>().GetCriticalItem();
            Destroy(gameObject);
        }
    }
}
