using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTurret : ItemComponent
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<ItemInventory>().GetItem(Item.TURRET);
            Destroy(gameObject);
        }
    }
}
