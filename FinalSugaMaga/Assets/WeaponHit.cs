using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class WeaponHit : MonoBehaviour
{
    public GameObject UiController;

    // �Ʒ��� �� ������ �ڵ� - �¾��� �� �״� ��
    private void OnTriggerEnter(Collider other)
    {
        PlayerTest enemy = other.GetComponent<PlayerTest>();
        Debug.Log(enemy.Health);
        if (enemy.Health <= 0)
        {
            Debug.Log("��� ����");
            UiController.GetComponent<UIController>().KillCntUp();
        }
    }




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
