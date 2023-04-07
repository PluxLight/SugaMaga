using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeadUI : MonoBehaviour
{
    public GameObject[] costumePage;
    public Button[] typeButton;

    public void ChangeType(Button clickedButton)
    {
        for(int i = 0; i < costumePage.Length; i++)
        {
            if(typeButton[i] == clickedButton)
            {
                costumePage[i].SetActive(true);
            }
            else
            {
                costumePage[i].SetActive(false);
            }
        }
    }

}
