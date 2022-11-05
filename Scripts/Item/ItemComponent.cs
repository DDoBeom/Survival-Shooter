using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemComponent : MonoBehaviour
{
    [SerializeField]
    float dTime = 10;

    float rot = 0;
    float rotSpeed = 150;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, dTime);
    }

    // Update is called once per frame
    void Update()
    {
        RotationItem();
    }

    void RotationItem()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, rot += Time.deltaTime * rotSpeed, transform.rotation.z);
    }
}
