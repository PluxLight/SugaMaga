using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleUI : MonoBehaviour
{
    public GameObject WeaponToggle;
    public GameObject PotionToggle;
    public GameObject SettingsToggle;
    public GameObject KillAriveToggle;


    public void WeaponToggleObject()
    {
        WeaponToggle.SetActive(!WeaponToggle.activeSelf);
    }

    public void PotionToggleObject()
    {
        PotionToggle.SetActive(!PotionToggle.activeSelf);
    }
    public void SettingsToggleObject()
    {
        SettingsToggle.SetActive(!SettingsToggle.activeSelf);
    }
    public void KillAriveToggleObject()
    {
        KillAriveToggle.SetActive(!KillAriveToggle.activeSelf);
    }
}
