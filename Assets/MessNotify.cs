using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class MessNotify : MonoBehaviourPun
{

    
    public GameObject notifica;
    public GameObject chatButton;
    [PunRPC]
    public void ShowNotify(bool active)
    {
            
        if (chatButton.gameObject.activeSelf)
        {
            notifica.gameObject.SetActive(active);
            
        }

    }
    public void Notifica(bool active)
    {
            photonView.RPC("ShowNotify", RpcTarget.All, active);


    }

}
