using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;
using VivoxUnity;
using UnityEngine.SceneManagement;
using Unity.Services.Vivox;


public class VivoxManager : MonoBehaviour
{
    public Vivox vivox = new Vivox();
    public ChatManager cm;

    public delegate void ChannelTextMessageChangedHandler(string sender, IChannelTextMessage channelTextMessage);
    public event ChannelTextMessageChangedHandler OnTextMessageLogReceivedEvent;


    private void Awake()
    {
        vivox.client = new Client();
        vivox.client.Uninitialize();
        vivox.client.Initialize();
        DontDestroyOnLoad(this.gameObject);

    }
    private void Start()
    {
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);

        }
        Debug.Log("음성채널 준비완료");
    }
    private void Update()
    {
        UnityEngine.SceneManagement.Scene scene = SceneManager.GetActiveScene();
        if (scene.name == "Lobby Scene")
        {
            cm = GameObject.Find("Canvas").GetComponent<ChatManager>();
        }

        if ((scene.name == "Game Scene"))
        {
            vivox.ChannelSession.Disconnect();
            JoinChannel("room", VivoxUnity.ChannelType.Positional);
        }
    }
    private void OnApplicationQuit()
    {
        vivox.client.Uninitialize();
    }
    public void userCallbacks(bool bind, IChannelSession session)
    {
        if (bind)
        {
            vivox.ChannelSession.Participants.AfterKeyAdded += AddUser;
            vivox.ChannelSession.Participants.BeforeKeyRemoved += LeaveUser;

        }
        else
        {
            vivox.ChannelSession.Participants.AfterKeyAdded -= AddUser;
            vivox.ChannelSession.Participants.BeforeKeyRemoved -= LeaveUser;
        }
    }
    public void ChannelCallbacks(bool bind, IChannelSession session)
    {
        if (bind)
        {
            Debug.Log("전에" + session.MessageLog);

            session.MessageLog.AfterItemAdded += ReciveMessage;
            Debug.Log(session.MessageLog);

        }
        else
        {
            session.MessageLog.AfterItemAdded -= ReciveMessage;

        }
    }
    public void AddUser(object sender, KeyEventArg<string> userData)
    {
        var temp = (VivoxUnity.IReadOnlyDictionary<string, IParticipant>)sender;

        IParticipant user = temp[userData.Key];
        /*        cm.inputChat($"{user.Account.Name}님이 채널에 접속했습니다.");
        */
    }
    public void LeaveUser(object sender, KeyEventArg<string> userData)
    {
        var temp = (VivoxUnity.IReadOnlyDictionary<string, IParticipant>)sender;

        IParticipant user = temp[userData.Key];
        /*        cm.inputChat($"{user.Account.Name}님이 채널에 떠났습니다.");
        */
    }

    public void Login(string userName)
    {
        Debug.Log(userName + "들오왔어요");
        AccountId accountid = new AccountId(vivox.issuer, userName, vivox.domain);
        vivox.LoginSession = vivox.client.GetLoginSession(accountid);
        vivox.LoginSession.BeginLogin(vivox.server, vivox.LoginSession.GetLoginToken(vivox.tokenKey, vivox.timespan),
            callback =>
            {
                try
                {
                    vivox.LoginSession.EndLogin(callback);
                    /*                    cm.inputChat("음성채널 로그인 완료");
                    */
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
            );
    }

    public void JoinChannel(string channelName, ChannelType channelType)
    {
        ChannelId channelId = new ChannelId(vivox.issuer, channelName, vivox.domain, channelType);
        vivox.ChannelSession = vivox.LoginSession.GetChannelSession(channelId);
        Debug.Log(vivox.ChannelSession);
        userCallbacks(true, vivox.ChannelSession);
        ChannelCallbacks(true, vivox.ChannelSession);
        vivox.ChannelSession.BeginConnect(true, true, true, vivox.ChannelSession.GetConnectToken(vivox.tokenKey, vivox.timespan),
            callback =>
            {
                try
                {
                    vivox.ChannelSession.EndConnect(callback);
                    Debug.Log("나 비복스" + channelName + "채널 들어왔어");
                    /*                    cm.inputChat(vivox.LoginSession.LoginSessionId.Name);
                    */
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
    }

    public void SendMessage(string str)
    {

        Debug.Log("보낼 메세지" + str);
        var channelSession = vivox.ChannelSession;
        Debug.Log("채널이름" + channelSession.Channel.Name);
        Debug.Log(channelSession.Participants.Count);
        Debug.Log("보낼 채널" + channelSession);
        channelSession.BeginSendText(str, callback =>
        {
            Debug.Log("나 실행은 돼?");
            try
            {
                Debug.Log("전");
                Debug.Log(callback);
                channelSession.EndSendText(callback);

            }
            catch (Exception e)
            {

                Debug.Log("후");
                Console.WriteLine(e);
                throw;
            }
        });
    }
    public void ReciveMessage(object sender, QueueItemAddedEventArgs<IChannelTextMessage> queueItemAddedEvent)
    {
        var name = queueItemAddedEvent.Value.Sender.Name;
        var message = queueItemAddedEvent.Value.Message;
        Debug.Log("나 이제 받을거야");
        Debug.Log(name + " : " + message);
        cm.inputChat(name + " : " + message);

    }

}

[Serializable]
public class Vivox
{
    public Client client;
    public Uri server = new Uri("https://mt1s.www.vivox.com/api2");
    public string issuer = "noneng5448-su49-dev";
    public string domain = "mt1s.vivox.com";
    public string tokenKey = "know859";
    public TimeSpan timespan = TimeSpan.FromSeconds(90);

    public ILoginSession LoginSession;
    public IChannelSession ChannelSession;

}