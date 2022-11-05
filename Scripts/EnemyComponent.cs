using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyComponent : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField]
    int hp = 100;
    [SerializeField]
    int atk = 5;
    [SerializeField]
    float atkRate = 0.5f;
    [SerializeField]
    int score = 0;

    bool canAttack = true;

    Animator anit;

    bool isDead = false;
    public bool Dead { get { return isDead; } }

    bool isSinking = false;

    [SerializeField]
    Material[] materials;

    SkinnedMeshRenderer smr;

    [SerializeField]
    GameObject fx;
    [SerializeField]
    GameObject dmgText;
    [SerializeField]
    float dmgTextYPos = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anit = GetComponent<Animator>();
        smr = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Sinking();
    }

    public void TakeDamage(int _dmg, bool cri)
    {
        hp -= _dmg;

        Instantiate(fx, new Vector3(transform.position.x, 0.5f, transform.position.z), Quaternion.identity);
        GameObject temp = Instantiate(dmgText, new Vector3(transform.position.x, transform.position.y + dmgTextYPos, transform.position.z), Quaternion.identity);
        if(cri == false)
        {
            temp.GetComponentInChildren<TextMesh>().text = "-" + _dmg.ToString();
        }
        else
        {
            temp.GetComponentInChildren<TextMesh>().text = "CRITICAL\n-" + _dmg.ToString();
            temp.GetComponentInChildren<TextMesh>().color = Color.yellow;
            temp.GetComponentInChildren<TextMesh>().fontSize = 70;
        }

        Destroy(temp, 0.3f);

        StartCoroutine(ChangeMaterial());

        if (hp <= 0) Death();
    }

    void Death()
    {
        isDead = true;
        rb.useGravity = false;
        GetComponent<CapsuleCollider>().enabled = false;
        anit.SetBool("isDead", isDead);
        GameManager.gm.AddScore(score);
    }

    public void StartSinking()
    {
        isSinking = true;
        Destroy(gameObject, 2);
    }

    void Sinking()
    {
        if (isSinking) transform.Translate(Vector3.down * 0.5f * Time.deltaTime);
    }

    IEnumerator ChangeMaterial()
    {
        smr.material = materials[1];

        yield return new WaitForSeconds(0.1f);

        smr.material = materials[0];
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && canAttack)
        {
            collision.gameObject.GetComponent<PlayerComponent>().TakeDamage(atk);
            StartCoroutine(AttackRate());
        }

        ResetVelocity();
    }

    IEnumerator AttackRate()
    {
        canAttack = false;

        yield return new WaitForSeconds(atkRate);

        canAttack = true;
    }

    void ResetVelocity()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    public void AddStat(int _hp, int _atk)
    {
        hp += _hp;
        atk += _atk;
    }
}
