using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMine : ItemComponent
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<ItemInventory>().GetItem(Item.MINE);
            Destroy(gameObject);
        }
    }
}
