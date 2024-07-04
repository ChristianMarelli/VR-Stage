using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

using TMPro;
using System;

public class UIManager : MonoBehaviourPunCallbacks
{

    public static UIManager Instance { get; private set; }

    public UIMessageItem messagePrefab;
    public Transform contentParent1;
    public GameObject Impostazioni;
    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            //Impostazioni.SetActive(true);
            UIMessageItem message = Instantiate(messagePrefab, contentParent1);
            string messageLine1 = PhotonNetwork.NickName;
            message.SetLine(messageLine1);           
        }

    }
    public void PlayerJoinedMessage(string playerName)
    {
        UIMessageItem message = Instantiate(messagePrefab, contentParent1);
        string messageLine = playerName;
        message.SetLine(messageLine);
    }

    public void PlayerLeftMessage(string playerName)
    {
        GameObject[] Messaggi = GameObject.FindGameObjectsWithTag("Message");
        for(int i = 0; i < Messaggi.Length; i++)
        {
            if (Messaggi[i].GetComponent<TMP_Text>().text == playerName) 
            {
                Destroy(Messaggi[i]);            
            }
        }
    }

    public static implicit operator GameObject(UIManager v)
    {
        throw new NotImplementedException();
    }
}
