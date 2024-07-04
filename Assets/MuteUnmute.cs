using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Voice.PUN;
using Photon.Pun;
using TMPro;
using Photon.Voice.Unity;
using static Unity.VisualScripting.Member;
using System.Diagnostics;

public class MuteUnmute : MonoBehaviourPunCallbacks
{
    public TMP_Text username;
    public bool isMutedR;
    [SerializeField]
    private TMP_Text btnMsg;
    public void Mute()
    {
        GameObject[] Giocatori = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < Giocatori.Length; i++)
        {
            if (Giocatori[i].GetComponent<PhotonView>().Owner.NickName == username.text)
            {
                UnityEngine.Debug.Log("Muting player: " + username.text);
                isMutedR = Giocatori[i].GetComponent<PlayerController>().mutePlayer(isMutedR);
                if (isMutedR)
                {
                    btnMsg.text = "Unmute";
                }
                else
                {
                    btnMsg.text = "Mute";
                }
            }
        }
    }
    
}
