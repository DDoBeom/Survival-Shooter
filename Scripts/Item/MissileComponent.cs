using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileComponent : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField]
    float speed = 3;
    [SerializeField]
    GameObject fx;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Move(Vector3 dir)
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity= dir * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //other.GetComponent<EnemyComponent>().TakeDamage(atk, false);
            Instantiate(fx, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
