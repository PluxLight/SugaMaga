using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyIdle : MonoBehaviour
{


    Rigidbody rigid;
    NavMeshAgent agent;
    Animator anim;

    public Transform[] arrWaypoint;
    //�������� �迭�� �־���

    private Vector3 destination;
    private Coroutine moveStop;

    // ���� 1ȸ�� ����
    void Start()
    {
        this.rigid = this.GetComponent<Rigidbody>();
        this.anim = this.GetComponent<Animator>();

        this.agent = this.GetComponent<NavMeshAgent>();
        Debug.Log("enemyidle ����");
        //agent�� AI�� ������
        anim.SetBool("isRun", false);
        Invoke("AiMove", 2);
    }

    // ��ũ��Ʈ�� Enable �� �� ���� ����
    private void OnEnable()
    {
        anim.SetBool("isRun", false);
        Invoke("AiMove", 2);
    }

    private void AiMove()
    {
        int random = Random.Range(0, arrWaypoint.Length);
        Debug.LogFormat("random : {0}", random);
        // �������� ������ ����

        for (int i = 0; i < arrWaypoint.Length; i++)
        {
            if (i == random)
            {
                this.destination = this.arrWaypoint[i].position;
                // ������ �������� ���

                if (this.moveStop == null)
                {
                    Debug.Log("�ڷ�ƾ����");
                    this.moveStop = this.StartCoroutine(this.crAiMove());
                    // �������� AI�� �Ÿ��� ����ϴ� �޼��� --> �ִϸ��̼� ������ ����
                }

                this.agent.SetDestination(this.destination);
                // AI���� �����̷� �̵������ ����
                break;
            }
        }
    }

    IEnumerator crAiMove()
    {
        while (true)
        {
            this.anim.SetBool("isWalk",true);
 
            var dis = Vector3.Distance(this.transform.position, this.destination);
            //�������� AI������ �Ÿ� ���
            if (dis <= 0.2f)
            {
                Debug.Log("������ ����");
                this.anim.SetBool("isWalk",false);
                //�����ϸ� �ִϸ��̼� �ٲ�
                if (this.moveStop != null)
                {
                    this.StopCoroutine(this.moveStop);
                    this.moveStop = null;
                    Invoke("AiMove", 1.5f);
                    //1.5�ʵ� �ٸ� ������ �̵���Ŵ
                    break;
                }
            }
            yield return null;
        }
    }
}
    


