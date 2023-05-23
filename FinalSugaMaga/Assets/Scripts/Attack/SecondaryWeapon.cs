using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondaryWeapon : MonoBehaviour
{
    public BoxCollider meleeArea;
    public GameObject firstWeapon;
    public int equipType;
    public int equipIdx; // ���� �ڵ� �̸� �Է�
    public string equipName;
    public float equipDamege;
    // Start is called before the first frame update
    public float equipSpeed;
    public string skillName;
    public float skillDamage;
    public float skillCooltime;

    Weapon weapon;
    // Start is called before the first frame update
    private void Start()
    {
        //equipWeapon(equipIdx);
        weapon = firstWeapon.GetComponent<Weapon>();
        getWeaponDatabase(transform);

    }
    public void getWeaponDatabase(Transform self)
    {

        meleeArea = self.transform.Find("collider").GetComponent<BoxCollider>();
        equipType = weapon.equipType;
        equipIdx = weapon.equipIdx;
        equipName = weapon.equipName;
        equipSpeed = weapon.equipSpeed;
        equipDamege = weapon.equipDamage;
        skillName = weapon.skillName;
        skillDamage = weapon.skillDamage;
        skillCooltime = weapon.skillCooltime;
    }


}
