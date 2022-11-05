using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpeed : ItemComponent
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerMoveController>().GetSpeedUpItem();
            Destroy(gameObject);
        }
    }
}
