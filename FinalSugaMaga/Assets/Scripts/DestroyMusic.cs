using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


// �κ�, �ΰ��ӿ��� EXIT �������� ����
public class DestroyMusic : MonoBehaviour
{
    GameObject MusicManager;
    public Button JoinButton;

    public void OnClickButton()
    {
        MusicManager = GameObject.Find("BackgroundMusic");
        Destroy(MusicManager);
    }
}

