using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnComponent : MonoBehaviour
{
    [SerializeField]
    GameObject enemy;
    [SerializeField]
    float spawnRate = 3;

    Transform[] spawnPos;

    [SerializeField]
    int addHp = 0;
    [SerializeField]
    int addAtk = 0;

    // Start is called before the first frame update
    void Start()
    {
        spawnPos = GameObject.Find("EnemySpawnPos").GetComponentsInChildren<Transform>();
        //InvokeRepeating("Spawn", spawnRate, spawnRate);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.gm.WAVESTART && !IsInvoking("Spawn") && GameManager.gm.SPAWENEMY < GameManager.gm.MAXENEMY)
        {
            InvokeRepeating("Spawn", spawnRate, spawnRate);
        }
        else
        {
            if (IsInvoking("Spawn") && GameManager.gm.SPAWENEMY == GameManager.gm.MAXENEMY)
            {
                CancelInvoke("Spawn");
                IncreaseStat();
            }
        }
    }
        
    void Spawn()
    {
        if (GameManager.gm.SPAWENEMY == GameManager.gm.MAXENEMY) return;

        int idx = Random.Range(1, spawnPos.Length);
        GameObject temp = Instantiate(enemy, spawnPos[idx].position, Quaternion.identity);
        temp.GetComponent<EnemyComponent>().AddStat(addHp, addAtk);
        temp.transform.SetParent(transform);
        GameManager.gm.AddEnemy();
    }

    void IncreaseStat()
    {
        addHp += 10;
        addAtk += 1;
    }

}
