using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveComponent : MonoBehaviour
{
    [SerializeField]
    int dmg = 30;
    [SerializeField]
    float destTime = 0.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyComponent>().TakeDamage(dmg, false);
            StartCoroutine(OffCollider());
            Destroy(gameObject, destTime);
        }
    }

    IEnumerator OffCollider()
    {
        yield return new WaitForSeconds(0.3f);
        GetComponent<SphereCollider>().enabled = false;

    }
}
