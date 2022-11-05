using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHp : ItemComponent
{
    [SerializeField]
    int hp = 5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerComponent>().GetHpItem(hp);
            Destroy(gameObject);
        }
    }
}
