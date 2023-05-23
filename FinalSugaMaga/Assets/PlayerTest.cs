using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public float Health=20;
    
    bool isDamage;

    MeshRenderer[] meshs;

    private void Awake()
    {
        isDamage= false;
          meshs = GetComponentsInChildren<MeshRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // ������
    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("��ų�¾Ѵ�");
        Health -= 10;
    }//�÷��̾ �ֱ�
    private void OnTriggerEnter(Collider other)
    {
        Weapon enemyWeapon = other.transform.parent.GetComponent<Weapon>();
        if (other.tag == "PlayerAttack" && enemyWeapon.meleeArea == true)
        {
            if (!isDamage)
            {
                Debug.Log(enemyWeapon.equipName);
                Health -= enemyWeapon.equipDamage;
                StartCoroutine(OnDamage());
            }
        }
    }

    IEnumerator OnDamage()
    {
        isDamage= true;
        foreach(MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.yellow;
        }
       //
       yield return new WaitForSeconds(3f);
        isDamage = false;

        foreach(MeshRenderer mesh in meshs)
        {
            mesh.material.color = Color.white;
        }

    }
    // Update is called once per frame
    void Update()
    {
    }
}
