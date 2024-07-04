using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

using TMPro;

public class StudentLobbyManager : LobbyManager
{

    [SerializeField]
    public TMP_Text lobbyMessage;

    void Start() {
        PhotonNetwork.JoinLobby();

        lobbyMessage.text = PhotonNetwork.NickName + ", as a student";
        base.startCreateRoomCoroutine();
    }

}
