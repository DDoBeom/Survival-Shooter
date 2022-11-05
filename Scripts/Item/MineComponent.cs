using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineComponent : MonoBehaviour
{
    [SerializeField]
    GameObject fx;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Instantiate(fx, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
