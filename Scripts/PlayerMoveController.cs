using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMoveController : MonoBehaviour
{
    Rigidbody rb;
    Animator anit;

    [SerializeField]
    float speed = 5;

    Vector3 movement;

    int floorMask;
    int spdLv = 1;
    Text spdUI;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anit = GetComponent<Animator>();
        floorMask = LayerMask.GetMask("Floor");
        spdUI = GameObject.Find("SpdUI").GetComponentInChildren<Text>();
    }

    private void Update()
    {
       
    }

    private void FixedUpdate()
    {
        if (!GameManager.gm.START || GameManager.gm.END) return;

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Move(h, v);
        Turning();
        Animation(h, v);
    }

    void Move(float h, float v)
    {
        movement.Set(h, 0, v);

        movement = movement.normalized * speed * Time.deltaTime;

        rb.MovePosition(transform.position + movement);
    }

    void Turning()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);                //카메라에서 광선 매개변수의 위치로 쏜다.

        RaycastHit hit;                                                             //광선이 닿은 물체를 담아둘 변수

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, floorMask))               //광선에 mask에 해당하는 물체가 맞으면 true 아니면 false; Physics.Raycast(광선,맞은 물체, 광선의 거리, 마스크)
        {
            Vector3 dir = hit.point - transform.position;                           //바라볼 방향을 구한다

            dir.y = 0;

            Quaternion rot = Quaternion.LookRotation(dir);                          //회전값을 변수에 대입

            rb.MoveRotation(rot);
        }
    }
    void Animation(float h, float v)
    {
        bool isMove = h != 0f || v != 0f;

        anit.SetBool("isMove", isMove);
    }

    public void GetSpeedUpItem()
    {
        if (spdLv < 6) spdLv++;
        else return;

        speed = DataManager.dm.GetDataSpeed(spdLv-1);
        spdUI.text = "Lv." + spdLv.ToString();
    }
}
