using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public float HP = 100f;
    public bool SafeZone = true;
    public Text HPText;
    public Text ZoneText;
    private float currentVelocity = 0;

    void Update()
    {
        if (!SafeZone)
        {
            if (HP > 0)
            {
                HP = Mathf.SmoothDamp(HP, 0, ref currentVelocity, 10);
            }
        }
        HPText.text = "HP: " + Mathf.RoundToInt(HP).ToString();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Zone")
        {
            SafeZone = true;
            ZoneText.text = "In the safe area";
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Zone")
        {
            SafeZone = false;
            ZoneText.text = "Not in safe area";
        }
    }
}