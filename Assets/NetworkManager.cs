using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;

using Photon.Voice.Unity;

public class NetworkManager : MonoBehaviourPunCallbacks
{

    public static int _pcount = 0;

    public GameObject playerPrefab;

    private string[] players = {"pide", "elisa"};

    void Start()
    {
        Connect();
    }

    void Update() {

    }

    private void Connect() {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() {
        Debug.Log("NetworkManager: Connected to master server");
        PhotonNetwork.NickName = players[_pcount];
        _pcount++;
        PhotonNetwork.JoinRandomRoom();
    }

    
    public override void OnJoinRandomFailed(short returnCode, string message) {
        PhotonNetwork.CreateRoom("Room");
    }

    public override void OnJoinedRoom() {
        Debug.Log("NetworkManager: room joined");
        Vector3 position = new Vector3(0, 2, 0);
        PhotonNetwork.Instantiate(playerPrefab.name, position, Quaternion.identity, 0);
    }

    public override void OnPlayerEnteredRoom(Player other) {
        Debug.LogFormat("GameManager: {0} entered the room.", other.NickName);
    } 
}
