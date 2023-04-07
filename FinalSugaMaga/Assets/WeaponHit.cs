using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class WeaponHit : MonoBehaviour
{
    public GameObject UiController;

    // 아래로 다 박해준 코드 - 맞았을 때 죽는 것
    private void OnTriggerEnter(Collider other)
    {
        PlayerTest enemy = other.GetComponent<PlayerTest>();
        Debug.Log(enemy.Health);
        if (enemy.Health <= 0)
        {
            Debug.Log("상대 제압");
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
