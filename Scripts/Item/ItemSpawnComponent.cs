using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnComponent : MonoBehaviour
{
    [SerializeField]
    float minX = 0;
    [SerializeField]
    float maxX = 0;
    [SerializeField]
    float minZ = 0;
    [SerializeField]
    float maxZ = 0;

    [SerializeField]
    GameObject[] hpItem;
    [SerializeField]
    GameObject[] statItem;
    [SerializeField]
    GameObject[] activeItem;

    Vector3 pos;
    Ray ray;
    RaycastHit hit;

    float spawnRate = 7;
    float time = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.gm.WAVESTART && GameManager.gm.END) return;

        time += Time.deltaTime;

        if (time >= spawnRate)
        {
            SpawnItem();
        }
    }

    void SpawnItem()
    {
        GameObject item = null;

        int idx = Random.Range(0, 3);

        if (idx == 0)
        {
            int num = Random.Range(0, hpItem.Length);
            item = hpItem[num];
        }
        else if(idx == 1)
        {
            int num = Random.Range(0, statItem.Length);
            item = statItem[num];
        }
        else if (idx == 2)
        {
            int num = Random.Range(0, activeItem.Length);
            item = activeItem[num];
        }
        
        Instantiate(item, SetPosition(), Quaternion.identity);
        time = 0;
    }

    Vector3 SetPosition()
    {
        while (true)
        {
            pos = new Vector3(Random.Range(minX, maxX), 10, Random.Range(minZ, maxZ));

            ray.origin = pos;
            ray.direction = Vector3.down;

            if(Physics.Raycast(ray,out hit, Mathf.Infinity))
            {
                if (hit.transform.CompareTag("Floor")) break;
                else continue;
            }
        }

        return hit.point;
    }
}
