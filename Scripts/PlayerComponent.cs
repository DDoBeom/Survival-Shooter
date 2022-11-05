using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerComponent : MonoBehaviour
{
    [SerializeField]
    int hp = 100;                       //현재 hp
    [SerializeField]
    int maxHp = 100;                    //최대 hp
    int limitMaxHp = 200;               //최대 늘어날 수 있는 maxHp양
    GameObject hpSlider;
    [SerializeField]
    GameObject dmgText;
    [SerializeField]
    float dmgTextYPos = 1.5f;

    bool isDead = false;

    Animator anit;
    [SerializeField]
    Material[] materials;

    SkinnedMeshRenderer smr;

    // Start is called before the first frame update
    void Start()
    {
        anit = GetComponent<Animator>();
        smr = GetComponentInChildren<SkinnedMeshRenderer>();
        hpSlider = GameObject.Find("HpSlider");

        SetHp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int _dmg)
    {
        if (isDead) return;

        hp -= _dmg;
        hpSlider.GetComponent<Slider>().value = hp;
        hpSlider.GetComponentInChildren<Text>().text = "HP" + hp.ToString();
        GameObject temp = Instantiate(dmgText, new Vector3(transform.position.x, transform.position.y + dmgTextYPos, transform.position.z), Quaternion.identity);
        temp.GetComponentInChildren<TextMesh>().text = "-" + _dmg.ToString();
        Destroy(temp, 0.3f);

        StartCoroutine(ChangeMaterial());

        if (hp <= 0)
        {
            Death();
        }
    }

    void Death()
    {
        isDead = true;
        anit.SetTrigger("Death");
        GameManager.gm.GameOver();
    }

    IEnumerator ChangeMaterial()
    {
        smr.material = materials[1];

        yield return new WaitForSeconds(0.1f);

        smr.material = materials[0];
    }

    void SetHp()
    {
        hp = maxHp;

        hpSlider.GetComponent<Slider>().maxValue = hp;
        hpSlider.GetComponent<Slider>().value = hp;
        hpSlider.GetComponentInChildren<Text>().text = "HP" + hp.ToString();
    }

    public void GetHpItem(int _hp)
    {
        hp += _hp;
        StartCoroutine(PrintHpText("+", _hp));

        if (hp > maxHp) hp = maxHp;

        hpSlider.GetComponent<Slider>().value = hp;
    }

    public void GetMaxHpItem()
    {
        maxHp += 5;
        hpSlider.GetComponent<Slider>().maxValue = maxHp;
        StartCoroutine(PrintHpText("MAX HP+", 5));

        if (maxHp >= limitMaxHp) maxHp = limitMaxHp;
    }

    IEnumerator PrintHpText(string text, int _hp)
    {
        hpSlider.GetComponentInChildren<Text>().text = text + _hp.ToString();

        yield return new WaitForSeconds(1);

        hpSlider.GetComponentInChildren<Text>().text = "HP" + hp.ToString();
    }
}
