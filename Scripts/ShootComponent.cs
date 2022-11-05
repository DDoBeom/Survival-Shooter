using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootComponent : MonoBehaviour
{
    [SerializeField]
    int atk = 20;
    
    [SerializeField]
    float shootRate = 0.2f;    

    float shootRange = 100;
    float time = 0;
    
    int critical = 5;
    [SerializeField]
    int maxCri = 15;

    Ray shootRay;
    RaycastHit hit;
    int layerMask;

    LineRenderer shootLine;
    Coroutine cor;
    Light light;

    [SerializeField]
    float lightInten = 5;
    [SerializeField]
    float lineWidth = 0.1f;

    [SerializeField]
    GameObject fx;

    GameObject statUI;

    int atkLv = 1;
    int atkRLv = 1;
    int criLv = 1;

    AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        shootLine = GetComponent<LineRenderer>();
        light = GetComponent<Light>();
        layerMask = LayerMask.GetMask("Enemy");
        statUI = GameObject.Find("StatUI");
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.gm.START || GameManager.gm.END) return;

        time += Time.deltaTime;

        if(Input.GetButton("Fire1") && time >= shootRate)
        {
            Shoot();
        }

        EffectScaleDown();
    }

    void Shoot()
    {
        light.intensity = lightInten;
        shootLine.startWidth = lineWidth;
        light.enabled = true;
        shootLine.enabled = true;
        shootLine.SetPosition(0, transform.position);                   //라인렌더러의 라인이 시작될 위치

        shootRay.origin = transform.position;                           //광선이 시작될 위치
        shootRay.direction = transform.forward;                         //광선이 나아갈 방향

        Instantiate(fx, transform.position, Quaternion.identity);
        sound.Play();

        if(Physics.Raycast(shootRay,out hit, shootRange))
        {
            shootLine.SetPosition(1, hit.point);                        //라인렌더러 의 라인을 적이 맞은 곳까지 그려줌

            if (hit.transform.CompareTag("Enemy"))
            {
                int num = Random.Range(0, 100);

                if (critical < num)
                {
                    hit.transform.GetComponent<EnemyComponent>().TakeDamage(atk, false);
                }
                else
                {
                    hit.transform.GetComponent<EnemyComponent>().TakeDamage(atk * Random.Range(2, 4), true);           //2~3배 *Random.Range(2,4)
                }
            }
        }
        else
        {
            shootLine.SetPosition(1, shootRay.origin + shootRay.direction * shootRange);
        }

        time = 0;

        if (cor == null) cor = StartCoroutine(DisableEffect());
        else
        {
            StopCoroutine(cor);
            cor = StartCoroutine(DisableEffect());
        }
    }

    IEnumerator DisableEffect()
    {
        yield return new WaitForSeconds(0.25f);
        light.enabled = false;
        shootLine.enabled = false;
    }

    void EffectScaleDown()
    {
        if (!light.enabled) return;

        light.intensity = Mathf.Lerp(light.intensity, 0, 6f * Time.deltaTime);
        shootLine.startWidth = Mathf.Lerp(shootLine.startWidth, 0, 6f * Time.deltaTime);
    }

    public void GetAttackUpItem()
    {
        if (atkLv < 6) atkLv++;
        else return;

        atk = DataManager.dm.GetDataDamge(atkLv-1);
        statUI.transform.GetChild(0).GetComponentInChildren<Text>().text = "Lv." + atkLv.ToString();
    }

    public void GetAttackSpeedItem()
    {
        if (atkRLv < 6) atkRLv++;
        else return;

        shootRate = DataManager.dm.GetDataAttackSpeed(atkRLv-1);
        statUI.transform.GetChild(1).GetComponentInChildren<Text>().text = "Lv." + atkRLv.ToString();
    }

    public void GetCriticalItem()
    {
        critical += 2;
        if (criLv < 11) criLv++;
        statUI.transform.GetChild(2).GetComponentInChildren<Text>().text = "Lv." + criLv.ToString();

        if (critical >= maxCri) critical = maxCri;
    }
}
