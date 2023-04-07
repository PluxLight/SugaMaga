using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Unity.VisualScripting;
using Unity.Services.Vivox;
using VivoxUnity;
using System;

public class ChatManager : MonoBehaviourPunCallbacks
{
    /*public GameObject m_Content;
    public TMP_InputField m_inputField;*/

    public UnityEngine.UI.Button button;
    public GameObject item;
    public Transform textPos;
    public Scrollbar textscrollbar;
    public VivoxManager vivoxManager;
    public Text message;




    PhotonView photonview;

    GameObject m_ContentText;

    string m_strUserName;
    /* private void Awake()
     {
         vivoxManager.OnTextMessageLogReceivedEvent += OnTextMessageLogReceivedEvent;
     }*/
    private void Start()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Lobby Scene")
        {
            vivoxManager = GameObject.Find("VivoxManager").GetComponent<VivoxManager>();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            MessageBtn();
        }
    }
    public void inputChat(string str)
    {
        Debug.Log(str);
        var temp = Instantiate(item, textPos);
        Debug.Log(temp);
        temp.GetComponent<Text>().text = str;
        Debug.Log(temp.GetComponent<Text>().text);

    }

    public void MessageBtn()
    {
        Debug.Log("나 지금 누르고 있니?");
        Debug.Log(message.text);
        vivoxManager.SendMessage(message.text);
    }
}