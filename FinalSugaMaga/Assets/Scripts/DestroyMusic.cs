using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


// 로비, 인게임에서 EXIT 눌렀을때 실행
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

