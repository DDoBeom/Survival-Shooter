using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMaxHp : ItemComponent
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerComponent>().GetMaxHpItem();
            Destroy(gameObject);
        }
    }
}
