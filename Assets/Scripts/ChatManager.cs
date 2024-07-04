using ExitGames.Client.Photon;
using Photon.Chat;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class ChatManager : MonoBehaviour, IChatClientListener
{

    #region Setup

    [SerializeField] GameObject joinChatButton;
    ChatClient chatClient;
    bool isConnected;
    [SerializeField] string username;
    public GameManager gameManager;

    public GameObject notifica;
    public GameObject chatButton;


    public void Start()
    {
        username = gameManager.nome;
        isConnected = true;
        chatClient = new ChatClient(this);
        //chatClient.ChatRegion = "US";
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(username));
        Debug.Log("Connenting");
        timer = 0f;
    }
    public void ChatConnectOnClick()
    {
        username = gameManager.nome;
        isConnected = true;
        chatClient = new ChatClient(this);
        //chatClient.ChatRegion = "US";
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, PhotonNetwork.AppVersion, new AuthenticationValues(username));
        Debug.Log("Connenting");
        timer = 0f;
    }

    #endregion Setup

    #region General

    [SerializeField] GameObject chatPanel;
    string request = "";    
    string currentChat;
    float timer = 0f;
    float count = 30f;
    [SerializeField] TMP_InputField chatField;
    [SerializeField] Text chatDisplay;
    [SerializeField] GameObject chatBackground;
    //[SerializeField] GameObject openChat;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        /*if (timer > count)
        {
            chatBackground.SetActive(false);
            //openChat.SetActive(true);
            timer = 0f;
        }*/
        if (isConnected)
        {
            chatClient.Service();
        }

        if (chatField.text != "" && Input.GetKey(KeyCode.Return))
        {
            SubmitPublicChatOnClick();
        }
    }

    #endregion General

    #region PublicChat

    public void SubmitPublicChatOnClick()
    {
            timer = 0f;
            chatClient.PublishMessage("RegionChannel", currentChat);
            chatField.text = "";
            currentChat = "";
            chatBackground.SetActive(true);
            //openChat.SetActive(false);
    }

    public void SubmitPublicRequestTalk()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            timer = 0f;
            request = "richiede di parlare";
            chatClient.PublishMessage("RegionChannel", request);
            request = "";
            chatBackground.SetActive(true);
            //openChat.SetActive(false);
        }    
    }
    public void TypeChatOnValueChange()
    {
        currentChat = chatField.text;
    }

    #endregion PublicChat

    #region Callbacks

    public void DebugReturn(DebugLevel level, string message)
    {
        //throw new System.NotImplementedException();
    }

    public void OnChatStateChange(ChatState state)
    {
        if (state == ChatState.Uninitialized)
        {
            isConnected = false;
            joinChatButton.SetActive(true);
            chatPanel.SetActive(false);
        }
    }

    public void OnConnected()
    {
        Debug.Log("Connected");
        //joinChatButton.SetActive(false);
        chatClient.Subscribe(new string[] { "RegionChannel" });
    }

    public void OnDisconnected()
    {
        isConnected = false;
        joinChatButton.SetActive(true);
        chatPanel.SetActive(false);
    }
    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        //comparsa notifica
        if (chatButton.gameObject.activeSelf)
        {
            notifica.gameObject.SetActive(true);

        }
        timer = 0f;
        string msgs = "";
        for (int i = 0; i < senders.Length; i++)
        {
            msgs = string.Format("{0}: {1}", senders[i], messages[i]);

            chatDisplay.text += "\n" + msgs;

            Debug.Log(msgs);
        }
        //chatBackground.SetActive(true);
        //openChat.SetActive(false);
    }

    public void OnPrivateMessage(string sender, object message, string channelName)
    {
        string msgs = "";

        msgs = string.Format("(Private) {0}: {1}", sender, message);

        chatDisplay.text += "\n " + msgs;

        Debug.Log(msgs);

    }

    public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
    {
        throw new System.NotImplementedException();
    }

    public void OnSubscribed(string[] channels, bool[] results)
    {
        chatPanel.SetActive(true);
    }

    public void OnUnsubscribed(string[] channels)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserSubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        throw new System.NotImplementedException();
    }
    #endregion Callbacks
    public void Azzeratimer()
    {
        timer = 0f;
    }
}

