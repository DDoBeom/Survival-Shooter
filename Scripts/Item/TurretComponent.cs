using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretComponent : MonoBehaviour
{
    GameObject targetEnemy;
    Transform turretHead;
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    Transform shootPoint;

    [SerializeField]
    float desttime = 10;

    [SerializeField]
    float atkRate = 0.3f;
    float time = 0;
    bool isCanAttack = true;
    [SerializeField]
    float rotSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        turretHead = transform.GetChild(0).GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        if(targetEnemy) Rotate();

        if (!isCanAttack)
        {
            time += Time.deltaTime;
            if(time >= atkRate)
            {
                isCanAttack = true;
                time = 0;
            }
        }
        else
        {
            if (targetEnemy)
            {
                if (!targetEnemy.GetComponent<EnemyComponent>().Dead)
                {
                    Shoot();
                }
                else targetEnemy = null;
            }     
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (targetEnemy == null) targetEnemy = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (targetEnemy == other.gameObject) targetEnemy = null;
        }
    }

    void Rotate()
    {
        Vector3 dir = targetEnemy.transform.position - turretHead.position;
        Quaternion q = Quaternion.LookRotation(dir);
        turretHead.rotation = Quaternion.Slerp(turretHead.rotation, q, Time.deltaTime * rotSpeed);
    }

    void Shoot()
    {
        GameObject temp = Instantiate(bullet, shootPoint.position, shootPoint.rotation);
        temp.GetComponent<MissileComponent>().Move(shootPoint.forward);
        isCanAttack = false;
    }
}
