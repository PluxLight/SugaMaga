using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FillAmount : MonoBehaviour
{
    public Image imgCooltime;
    public GameObject objCoolTime;
    public TMP_Text txtCoolTime;
    public bool skillState;
    public float cooltime;
    public Image imgSkill;

    // Start is called before the first frame update
    void Start()
    {
        objCoolTime.SetActive(false);
        skillState = false;
    }

    // Update is called once per frame
    void Update()
    {
     /*   if (Input.GetMouseButtonDown(1) && skillState)
        {
            skillState = false;
            objCoolTime.SetActive(true);
            imgSkill.color = new Color(1f, 1f, 1f, 80 / 255f);
            StartCoroutine(CoolTime(cooltime));
        }*/
    }
    public void CallSkillCooltime()
    {
        StartCoroutine(CoolTime(cooltime));
    }
    IEnumerator CoolTime (float delay)
    {
        delay += 1.0f;

        while (delay > 1.0f)
        {
            delay -= Time.deltaTime;
            txtCoolTime.text = string.Format("{0:0.#}", delay - 1.0f);
            imgCooltime.fillAmount = (1.0f / delay);

            yield return new WaitForFixedUpdate();
        }
        skillState = true;
        objCoolTime.SetActive(false);
        imgSkill.color = new Color(1f, 1f, 1f, 1f);
    }
}
