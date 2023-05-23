using MiniJSON;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;


public class Weapon : MonoBehaviour
{

    public BoxCollider meleeArea;
    public ParticleSystem particleSystem;
    
    public GameObject player;
    public int equipType;
    public int equipIdx; // ���� �ڵ� �̸� �Է�
    public string equipName;
    public float equipDamage;
    // Start is called before the first frame update
    public float equipSpeed;
    public string skillName;
    public float skillDamage;
    public float skillCooltime;

    public Vector3 scale;
    PlayerController playerController;
    Vector3 pos;
    private void Update()
    {
       pos = new Vector3(particleSystem.transform.position.x, particleSystem.transform.position.y, particleSystem.transform.position.z);
    }
    private void Start()
    { 
        playerController = player.GetComponent<PlayerController>();
        PlayerMain.canMove = true;
        //equipWeapon(equipIdx);
        scale = gameObject.GetComponent<Transform>().localScale;
        
    }
    public void getWeaponDatabase(Transform self, WeaponData.WeaponTable weaponTable)
    {

        meleeArea = self.transform.Find("collider").GetComponent<BoxCollider>();
        particleSystem = self.transform.Find("skill").GetComponent<ParticleSystem>();
        
        equipType = weaponTable.equipType;
        equipIdx = weaponTable.equipItemIdx;
        equipName = weaponTable.equipName;
        equipSpeed= weaponTable.equipSpeed; 
        equipDamage = weaponTable.equipDamage;
        skillName= weaponTable.skillName;
        skillDamage= weaponTable.skillDamage;
        skillCooltime= weaponTable.skillCooltime;
    }

    public void UseSkillA()
      {
          //if (type == Type.Melee) {
          StopCoroutine("SkillA");
          StartCoroutine("SkillA"); 
          //}
      }
    public void UseNormalAttack()
    {
            StopCoroutine("NormalAttack");
            StartCoroutine("NormalAttack");
    }

    public void UseSSSkill()
    {
        PlayerMain.canMove = false;
        StopCoroutine("SSSkill");
        StartCoroutine("SSSkill");
    }
    public void UseDSSkill()
    {
        PlayerMain.canMove = false;
        playerController.usingSkill = true;
        StopCoroutine("DSSkill");
        StartCoroutine("DSSkill");
    }
    public void UseMagic()
    {
        PlayerMain.canMove = false;
        StopCoroutine("MagicA");
        StartCoroutine("MagicA");
    }
    IEnumerator NormalAttack()
    {
        
        yield return new WaitForSeconds(0.1f);
        meleeArea.enabled = true;
        //trailEffect.enabled = true;
        //2
        yield return new WaitForSeconds(0.3f);
        meleeArea.enabled = false;
        //3
        yield return new WaitForSeconds(0.3f);

    }
    public void UseTHSSkill()
    {
      
        StopCoroutine("THSSkill");
        StartCoroutine("THSSkill");

    }
    /*
    public void THSCharging()
    {
        transform.Find("skill").gameObject.SetActive(true);
        particleSystem.Play();
    }
    차지
    */
    IEnumerator SSSkill()
    {
       
        PhotonNetwork.Instantiate("candy", pos, Quaternion.identity);
        yield return new WaitForSeconds(3f);
        player.transform.Find("player").gameObject.GetComponent<Animator>().SetTrigger("SkillA");
        yield return new WaitForSeconds(10f);
        playerController.SSSkillStop();

    }

    IEnumerator DSSkill()
    {
        //particleSystem.Play();
        
        yield return new WaitForSeconds(2f);
        player.transform.Find("player").gameObject.GetComponent<Animator>().SetTrigger("Idle");
        PlayerMain.canMove = true;
        playerController.usingSkill = false;
    }
    IEnumerator THSSkill()
    {
        meleeArea.enabled = true;
        //particleSystem.Play();
        
        PhotonNetwork.Instantiate("THSSkill", pos, Quaternion.identity);
        yield return new WaitForSeconds(10f);
        playerController.THSSkillStop();
       
    

    }

    
      IEnumerator SkillA()
      {
        PlayerMain.canMove = false;
          //1
          yield return new WaitForSeconds(0.1f);
          meleeArea.enabled = true;
          //trailEffect.enabled = true;
         // particleSystem.Play();
          //2
          yield return new WaitForSeconds(0.3f);
          meleeArea.enabled = false;
          //3
          yield return new WaitForSeconds(0.3f);
        PlayerMain.canMove = true;
          //trailEffect.enabled = false;
      }

    IEnumerator MagicA()
    {
        
        PhotonNetwork.Instantiate("Fire", pos, Quaternion.identity);
        meleeArea.enabled = true;    
       //  particleSystem.Play();
        yield return new WaitForSeconds(10f);
        playerController.UseMagicStop();
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log(other.gameObject.name);
    }


}
