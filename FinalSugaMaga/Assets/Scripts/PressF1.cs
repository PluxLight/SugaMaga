using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PressF1 : MonoBehaviour
{
    public GameObject settings;
    public Button CancelButton;
    public static bool PressF1Activated = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            PressF1Activated = !PressF1Activated;

            if (settings.activeSelf == true)
            {
                EventSystem.current.SetSelectedGameObject(CancelButton.gameObject);
                CancelButton.onClick.Invoke();
            }
            else
            {
                settings.SetActive(true);
            }
        }
    }
}
