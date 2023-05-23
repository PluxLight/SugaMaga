using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class UIController : MonoBehaviour
{
    public const int STARTHEALTH = 100;
    public float currentHealth;
    public Slider healthSlider;
    public TMP_Text txtHealth;

    public static UIController instance;

    public TMP_Text txtKillCnt;
    public TMP_Text txtArriveCnt;

    public PlayerMain hp;
    // Start is called before the first frame update

    //���߿� myArriveCnt �ٲ���� �� - ������
    public int myKillCnt = 0;
    public int myArriveCnt;

    // �¸�, �й�� ������ ������ - ������
    public GameObject victoryPage;
    public GameObject defeatPage;
    public TMP_Text pageTotalLengthText;
    public TMP_Text pageMyScore;
    public TMP_Text pageKillCnt;
    public TMP_Text pageArriveCnt;

    public void Awake()
    {
      
        currentHealth = STARTHEALTH;
        healthSlider.value = currentHealth;
        txtHealth.text = string.Format("{0} / 100", currentHealth);
        instance= this;
        myArriveCnt= PhotonNetwork.PlayerList.Length;
        txtArriveCnt.text = string.Format("Alive: {0}", myArriveCnt);
    }

    public void TakeDamage(float amount)
    {
        Debug.Log("takedamage ����");
        float tempHealth = currentHealth + amount;
        Debug.Log(amount);
        if (tempHealth > 100)
        {
            tempHealth = 100;
        }

        if (tempHealth >= 0 && tempHealth <= 100)
        {
            currentHealth = tempHealth;
            healthSlider.value = currentHealth;
            txtHealth.text = string.Format("{0} / 100", currentHealth);
        }
    }

    public void HpUp(int a)
    {
        TakeDamage(a);
    }
    public void HpDown()
    {
        TakeDamage(-10);

    }

    public void Onclick()
    {
        TakeDamage(-10);
    }

    // kill�ϴ� ��� - ������
    public void KillCntUp()
    {
        Debug.Log("UIcontroller������ �ǳ���?");
        txtKillCnt.text = string.Format("Kill: {0}", myKillCnt += 1);
    }

    // �й��ϴ� ��� - ������
    public void DefeatPage()
    {
        pageKillCnt.text = string.Format("{0}", myKillCnt);
        pageMyScore.text = string.Format("{0}", myArriveCnt);
        pageArriveCnt.text = string.Format("{0", myArriveCnt);
        defeatPage.SetActive(true);
    }
    //�¸��ϴ� ���
    public void VictoryPage()
    {
        pageKillCnt.text = string.Format("{0}", myKillCnt);
        pageArriveCnt.text = string.Format("{0", myArriveCnt);
        victoryPage.SetActive(true);
    }
    // �������� �״°�� myArriveCnt�� �ϳ� ����
    [PunRPC]
    public void ArriveDown()
    {
        myArriveCnt -= 1;
        txtArriveCnt.text = string.Format("Alive: {0}", myArriveCnt);
        if (myArriveCnt == 1)
        {
            VictoryPage();
        }
    }
        

    // Update is called once per frame
    void Update()
    {
        Debug.Log("����ü��"+currentHealth);
        Debug.Log("hp hp" + hp.hp);
        // ������
        currentHealth = hp.hp;


    }
}
