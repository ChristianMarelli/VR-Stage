using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

using TMPro;
using UnityEngine.UIElements;
using System.Threading;

public class TeacherLobbyManager : LobbyManager
{

    [SerializeField]
    public UnityEngine.UI.Button createRoomButton;

    [SerializeField]
    public TMP_InputField roomNameInputField;

    [SerializeField]
    private byte maxPlayersPerRoom = 2;

    [SerializeField]
    TMP_Text lobbyMessage;

    [SerializeField]
    GameObject pannelloErrore;
    float timer = 0f;
    float count = 2f;

    void Start() {
        PhotonNetwork.JoinLobby();
        createRoomButton.onClick.AddListener(HelpCreateRoom);
        lobbyMessage.text = PhotonNetwork.NickName + ", as a teacher";
        base.startCreateRoomCoroutine();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > count)
        {
            pannelloErrore.SetActive(false);
        }
    }
    #region private methods

    private void HelpCreateRoom() {
        string roomName = roomNameInputField.text;
        if (roomName != "")
        {
            PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = maxPlayersPerRoom });
        }
        else
        {
            pannelloErrore.SetActive(true);
            timer = 0f;
            Debug.Log("Nome Stanza non valido");
        }
    }

    #endregion

    #region public methods


    #endregion

}
