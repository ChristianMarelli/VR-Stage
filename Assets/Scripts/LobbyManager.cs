using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    readonly string SOLAR_SYSTEM = "SolarSystem";
    string SOLAR_SYSTEM_ROOM = "SolarSystemRoom";

    ///Set in editor: prefab del bottone/UI da mostrare nella scrollview con lista delle stanze.
    ///Successivamente sarà sufficiente modificare questo per una UI più gradevole.
    [SerializeField]
    public RoomItem roomPrefab;

    ///Set in editor: content object della scrollview: dove verrano istanziati i roomPrefab.
    [SerializeField]
    public Transform contentParent;

    private List<RoomItem> cachedRoomList = new List<RoomItem>();
    
    #region private methods

    
    /*
    * Metodo che aggiorna la lista delle stanze.
    * Ogni volta che c'è un cambiamento (vedi OnRoomListUpdate)
    * si svuota e si ripopola la lista locale delle stanze disponibili.
    * infine vengono istanziati i prefab con i nomi delle stanze.
    */
    private void UpdateCachedRoomList(List<RoomInfo> roomList) {
        foreach(RoomItem item in cachedRoomList) {
            Destroy(item.gameObject);
        }
        cachedRoomList.Clear();

        foreach(RoomInfo room in roomList) {
            RoomItem newRoom = Instantiate(roomPrefab, contentParent);
            newRoom.SetRoomName(room.Name);
            newRoom.SetPlayersCount(room.PlayerCount, room.MaxPlayers);
            cachedRoomList.Add(newRoom);
        }
    }

    private IEnumerator createRoomCoroutine() {
        yield return new WaitForSeconds(2);
            createSolarSystemRoom();
            foreach (RoomItem item in cachedRoomList)
            {
                Debug.Log(item.roomName.text);
                
            }
    }

    private void createSolarSystemRoom() {
        if (cachedRoomList != null && cachedRoomList.Count > 0) {
            foreach(RoomItem room in cachedRoomList) {
                if (room.roomName.text == SOLAR_SYSTEM) {
                    return;
                }
            }
        }
        /*foreach (RoomItem item in cachedRoomList)
        {
            Debug.Log(item.roomName.text);
            createRoom(item.roomName.text, 0, 5);
        }*/

        createRoom(SOLAR_SYSTEM, 0, 5);
        createRoom("ProvaModelli", 0, 5);
        
        
    }

    private void createRoom(string name, int playerNumber, byte maxPlayers) {
        RoomItem newRoom = Instantiate(roomPrefab, contentParent);
        newRoom.SetRoomName(name);
        newRoom.SetPlayersCount(playerNumber, maxPlayers);
        cachedRoomList.Add(newRoom);
    }

    #endregion

    #region Pun callbacks

    public override void OnJoinedRoom() {
        Debug.Log(PhotonNetwork.NickName + " joined Room " + PhotonNetwork.CurrentRoom.Name);

        //Se siamo i primi a joinare, viene caricata la scena.
        //Per i prossimi player il compito è delegato a GameManager.
        if (PhotonNetwork.CurrentRoom.Name == SOLAR_SYSTEM) {
            PhotonNetwork.LoadLevel(SOLAR_SYSTEM_ROOM);
        }
        if (PhotonNetwork.CurrentRoom.Name == "ProvaModelli")
        {
            PhotonNetwork.LoadLevel("ProvaModelli");
        }
        else
        {
            //Se siamo i primi a joinare, viene caricata la scena.
            //Per i prossimi player il compito è delegato a GameManager.
            if (PhotonNetwork.CurrentRoom.PlayerCount == 1) {
                PhotonNetwork.LoadLevel("GenericRoom");
            }
        }
    }

    ///NB: quando entri in una stanza non ricevi più gli update dalla lobby!
    public override void OnRoomListUpdate(List<RoomInfo> roomList) {
        Debug.Log("LobbyManager: Change occurred in room list");
        UpdateCachedRoomList(roomList);
    }

    public override void OnJoinedLobby() {
        Debug.LogFormat("{0} joined the lobby", PhotonNetwork.NickName);
        cachedRoomList.Clear();
    }

    public override void OnLeftLobby() {
        Debug.LogFormat("{0} left the lobby", PhotonNetwork.NickName);
        cachedRoomList.Clear();
    }

    public override void OnDisconnected(DisconnectCause cause) {
        cachedRoomList.Clear();
    }

    #endregion

    #region public methods

    public void startCreateRoomCoroutine() {
        StartCoroutine(createRoomCoroutine());
    }

    #endregion
}
