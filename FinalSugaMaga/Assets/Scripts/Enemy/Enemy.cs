using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public int maxHealth;
    public int curHealth;
    public Transform target;
    public bool isChase;
    public SphereCollider meleeArea; 
    public bool isAttack;
    public bool takeDamage;
    
    Rigidbody rigid;
    CapsuleCollider capCollider;
    Material mat;
    NavMeshAgent nav;
    MeshRenderer[] meshs;
    Animator anim;
    GameObject eAttack;
    
 
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        capCollider = GetComponent<CapsuleCollider>();
      //  mat = GetComponentInChildren<MeshRenderer>().material;
       nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        meshs = GetComponentsInChildren<MeshRenderer>();
        
        ChaseStart();
    }

    void ChaseStart()
    {
        isChase = true;
        anim.SetBool("isWalk", false);
        nav.speed = 7f;
        //anim.SetBool("isRun", true);
    }

    private void Update()
    {
        if (nav.enabled)
        {
            nav.SetDestination(target.position);
            nav.isStopped = !isChase;
            anim.SetBool("isRun", true);
        }
    }
        IEnumerator OnTriggerStay (Collider other)//�÷��̾ ���ݽ� ����
    {
        if(other.tag == "Plaayer")//�÷��̾� �������� �ٲٱ�
        {
            //  curHealth -= 10;

            if (curHealth > 0) {
                Vector3 reactVec = transform.position-other.transform.position;
                yield return StartCoroutine(OnDamage(reactVec));
            }
        }
        /*else if(other.tag == "Bullet")
        {
            Bullet bullet = other.GetComponent<Bullet>();
            curHealth -= bullet.damage;
            Vector3 reactVec = transform.position - other.transform.position;
            Destroy(other.gameObject);

            StartCoroutine(OnDamage(reactVec));
        }*/
    }
    void FreezeRotation()
    {
        if (isChase) { 
            rigid.velocity = Vector3.zero;
            rigid.angularVelocity = Vector3.zero;
        }
    }
    private void FixedUpdate()
    {
        Targeting();
        FreezeRotation();
    }

    void Targeting()
    {

        float targetRadius = 5f;
        float targetRange = 10f;

        RaycastHit[] rayHits =
            Physics.SphereCastAll(transform.position, targetRadius, 
            transform.forward, targetRange, LayerMask.GetMask("Player"));
             if(rayHits.Length > 0 && !isAttack&&curHealth>0 && !takeDamage) {

            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        isChase = false;
        isAttack = true;
        anim.SetBool("isAttack", true);

        yield return new WaitForSeconds(.4f);
        meleeArea.enabled= true;
            
        yield return new WaitForSeconds(.2f);
        meleeArea.enabled = false;

        isChase = true;
        isAttack = false;
        anim.SetBool("isAttack", false);
    }

    IEnumerator OnDamage(Vector3 reactVec)
    {
        //    mat.color = Color.red;

        //if (curHealth > 0){
        anim.SetBool("takeDamage", true);
        curHealth -= 10;
        //    Debug.Log("���� �ǰ�");
        isAttack = false;
        isChase = false;
        takeDamage = true;
        nav.enabled= false;
       
        yield return new WaitForSeconds(1f);
        anim.SetBool("takeDamage", false);
        takeDamage = false;
        nav.enabled= true;
        isChase = true;
        //}
        //else
        if (curHealth<= 0) 
        {
     //       mat.color = Color.gray;
            gameObject.layer = 10;
            isChase = false;
            isAttack = false;
            nav.enabled = false;
            anim.SetTrigger ("doDie");
            Destroy(gameObject, 4);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        eAttack = transform.Find("EnemyAttack").gameObject;
        Debug.Log(eAttack.GetComponent<SphereCollider>().radius);

    }

    // Update is called once per frame

}
