using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class EnemyView : MonoBehaviour
{
    GameObject player;
    [SerializeField] GameObject self;
    [SerializeField] bool DebugMode = false;
    [Range(0f, 360f)][SerializeField] float ViewAngle = 0f;
    [SerializeField] float ViewRadius = 1f;
    [SerializeField] float ActionRange = 10f;
    [SerializeField] LayerMask TargetMask;
    [SerializeField] LayerMask ObstacleMask;
    [SerializeField] GameObject EnemySpawn;
    Boolean onBound = true;
    float min = 99999999;
    float spawnDistance = 0;
    Animator anim;

    List<Collider> hitTargetList = new List<Collider>();
    RaycastHit hit;

    private void Awake()
    {
     anim = GetComponentInChildren<Animator>();   
    }

    private void Start()
    {
        
    }
    Vector3 AngleToDir(float angle)
    {
        float radian = (angle) * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0f, Mathf.Cos(radian));
    }

    private void OnDrawGizmos()
    {
        if (!DebugMode) return;
        Vector3 myPos = transform.position + Vector3.up * 0.5f;
        Gizmos.DrawWireSphere(myPos, ViewRadius);

        float lookingAngle = transform.eulerAngles.y;  //ĳ���Ͱ� �ٶ󺸴� ������ ����
        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + ViewAngle * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - ViewAngle * 0.5f);
        Vector3 lookDir = AngleToDir(lookingAngle);

        Vector3 spawnPoint = EnemySpawn.transform.position;

        Debug.DrawRay(myPos, rightDir * ViewRadius, Color.blue);
        Debug.DrawRay(myPos, leftDir * ViewRadius, Color.blue);
        Debug.DrawRay(myPos, lookDir * ViewRadius, Color.cyan);

        hitTargetList.Clear();
        Collider[] Targets = Physics.OverlapSphere(myPos, ViewRadius, TargetMask);
        
  
        spawnDistance = Vector3.Distance(myPos, spawnPoint);
        Collider closeEnemy = null ;
      
        foreach (Collider EnemyColli in Targets)
        {
            //gameObject.GetComponent<Enemy>().enabled = false;
            //gameObject.GetComponent<EnemyIdle>().enabled = true;
            //enemy.target = null;
            Vector3 targetPos = EnemyColli.transform.position;
            Vector3 targetDir = (targetPos - myPos).normalized;
        float distance = Vector3.Distance(myPos, targetPos);

            if(min> distance) {
                min = distance;
                closeEnemy= EnemyColli;
            }
         //  Debug.Log(EnemyColli+" "+enemy.target);
            
            float targetAngle = Mathf.Acos(Vector3.Dot(lookDir, targetDir)) * Mathf.Rad2Deg;
            if (targetAngle <= ViewAngle * 0.5f && !Physics.Raycast(myPos, targetDir, ViewRadius, ObstacleMask))
            {
                hitTargetList.Add(EnemyColli);
               
                if (DebugMode) Debug.DrawLine(myPos, targetPos, Color.red);
            gameObject.GetComponent<EnemyIdle>().enabled = false;
            gameObject.GetComponent<Enemy>().enabled = true;

            }

        }
                            

      
        if (onBound == true&&closeEnemy!=null) {
            self.GetComponent<Enemy>().target = closeEnemy.transform;  
        }
        if (spawnDistance < 5&&onBound==false)
        {
            Debug.Log("왓다!!");
            onBound = true;
            anim.SetBool("isRun", false);
            anim.SetBool("isWalk", false);
            self.GetComponent<Enemy>().target = null;
            gameObject.GetComponent<Enemy>().enabled = false;
            gameObject.GetComponent<EnemyIdle>().enabled = true;
        }
        if (spawnDistance >ActionRange )//몬스터 행동범위
        {
            onBound = false;
            min = 9999999;
            hitTargetList.Clear();
            closeEnemy = null;
            self.GetComponent<Enemy>().target = EnemySpawn.transform;
            
        }
        
        
    }
}
