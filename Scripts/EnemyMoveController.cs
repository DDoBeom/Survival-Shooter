using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMoveController : MonoBehaviour
{
    Transform playerPos;
    NavMeshAgent nav;
    [SerializeField]
    float speed = 2.5f;

    EnemyComponent ec;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.Find("Player").transform;
        ec = GetComponent<EnemyComponent>();
        nav = GetComponent<NavMeshAgent>();
        nav.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!ec.Dead) nav.SetDestination(playerPos.position);
        else
        {
            if (nav.enabled)
            {
                nav.enabled = false;
            }
        }
    }
}
