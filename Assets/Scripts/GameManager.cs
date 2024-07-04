using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviourPunCallbacks
{
    readonly string SOLAR_SYSTEM = "SolarSystem";
    public GameObject playerPrefab;
    public GameObject playerPrefabStudent;
    public GameObject panelPrefab1;
    public GameObject panelPrefab2;
    public GameObject panelPrefab3;
    public GameObject newPanel1;
    public GameObject newPanel2;
    public GameObject newPanel3;
    public GameObject MeasurementBTN;
    public GameObject PlacemarkBTN;
    public Object_Spawner obj_spawner;
    public UIManager uIManager;
    private GameObject newPlayer;
    private RPCraggio ray;
    public string nome;
    public GameObject userButton;
    public void Start() {
        // listener che rilevano se l'utente ha premuto il tasto per attivare la feature "Placemark" o "Measuremens"
        // Se l'utente preme uno dei due tasti, viene invocata la funzione ClickRay()
        MeasurementBTN.GetComponent<Button>().onClick.AddListener(delegate { ClickRay(true); });
        PlacemarkBTN.GetComponent<Button>().onClick.AddListener(delegate { ClickRay(true); });
        if (playerPrefab == null) {
            Debug.LogError("GameManager: Player prefab is null! Set it in the inspector.");
        }
        else {
            if(PlayerController.localPlayerInstance == null) {
                Debug.Log("GameManager: Instantiating player");
                Vector3 spawnPos;
                if (PhotonNetwork.CurrentRoom.Name == SOLAR_SYSTEM) {
                    spawnPos = GenerateRandomPositionOnXAxis(3f, 0, -25);
                } else {
                    spawnPos = GenerateRandomPosition(1.5f, 1.8f, -34f);

                }

                GameObject newPlayer;
                if (PhotonNetwork.IsMasterClient)
                    {
                         newPlayer = PhotonNetwork.Instantiate(playerPrefab.name, spawnPos, Quaternion.identity, 0);
                    }else {
                         newPlayer = PhotonNetwork.Instantiate(playerPrefabStudent.name, spawnPos, Quaternion.identity, 0);
                         userButton.SetActive(false);
                    }
                //assegna alla variabile ray il componente "RPCraggio" appartenente a newPlayer
                //RPCraggio è lo script che rende il puntatore laser visibile online
                ray = newPlayer.GetComponent<RPCraggio>();
                if (panelPrefab1 && panelPrefab2 != null)
                {
                    if (PhotonNetwork.IsMasterClient)
                    {
                        newPanel1 = PhotonNetwork.Instantiate(panelPrefab1.name, panelPrefab1.transform.position, panelPrefab1.transform.rotation, 0);
                        newPanel2 = PhotonNetwork.Instantiate(panelPrefab2.name, panelPrefab2.transform.position, panelPrefab2.transform.rotation, 0);
                        newPanel3 = PhotonNetwork.Instantiate(panelPrefab3.name, panelPrefab3.transform.position, panelPrefab3.transform.rotation, 0);
                        obj_spawner.Objects_where_spawn.Add(newPanel1);
                        obj_spawner.Objects_where_spawn.Add(newPanel2);
                        obj_spawner.Objects_where_spawn.Add(newPanel2);
                    }
                }
                Transform netPlayerCanvas = newPlayer.transform.Find("Head/NameCanvasContainer/Canvas/Name Panel/Player Name");
                nome = netPlayerCanvas.GetComponent<TMP_Text>().text;
                GameObject authHelper = GameObject.Find("AuthHelper");
                if (PhotonNetwork.CurrentRoom.PlayerCount <= 1) {
                    //Se si è i primi ad entrare, allora si è il docente
                    newPlayer.GetComponent<PlayerController>().SetTeacher(true);
                }
                //Caso in cui il docente si fosse scollegato
                // la presenza del gameobject authHelper ci informa che il docente si è ricollegato
                // allora si rende master client e si distrugge authHelper.
                else if (authHelper != null) {
                    newPlayer.GetComponent<PlayerController>().SetTeacher(true);
                    PhotonNetwork.SetMasterClient(newPlayer.GetComponent<PhotonView>().Controller);
                    Destroy(authHelper);
                }
                else {
                    newPlayer.GetComponent<PlayerController>().SetTeacher(false);
                }
            }
            else {
                Debug.Log("GameManager: ignoring spawn for player");
            }
            
        }

    }

    void Update()
    {

    }

    //invoca la funzione RayCall() presente all'interno dello script RPCraggio
    public void ClickRay(bool value)
    {
        ray.RayCall(value);
    }

    public void DisconnectPlayer()
    {

        StartCoroutine(DisconnectAndLoad());

    }

    //effettua il log-out
    IEnumerator DisconnectAndLoad()
    {
        /*;
        while(PhotonNetwork.InRoom){
            yield return null;
        }
        SceneManager.LoadScene("TeacherLobby");*/
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
        {

            yield return null;
        }
        Debug.Log("SERVER");

        // PhotonNetwork.ConnectUsingSettings();
        //PhotonNetwork.GameVersion = gameVersion;
        SceneManager.LoadScene("StartScene");



    }

    #region private methods

    private Vector3 GenerateRandomPosition(float limitX, float Y, float limitZ) {
        return new Vector3(Random.Range(limitX-2, limitX+2), Y, Random.Range(limitZ-2, limitZ+2));
    }

    private Vector3 GenerateRandomPositionOnXAxis(float limitX, float Y, float Z) {
        return new Vector3(Random.Range(-limitX, limitX), Y, Z);
    }



    #endregion

    #region PUN Callbacks

    //Critical: PhotonNetwork.AutomaticallySyncScene must be true.
    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("GameManager: {0} entered the room.", other.NickName);
        // Check on uIManager needed because in SolarSystemRoom there isn't the canvas and it caused a NullPointerException
        if (PhotonNetwork.IsMasterClient && uIManager != null)
        {
            Debug.Log($"pippo:{other.NickName}");
            uIManager.PlayerJoinedMessage(other.NickName);
        }
    }


    //Critical: PhotonNetwork.AutomaticallySyncScene must be true.
    public override void OnPlayerLeftRoom(Player other) {
        Debug.LogFormat("GameManager: {0} left the room.", other.NickName);
        if (uIManager != null)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                uIManager.PlayerLeftMessage(other.NickName);
            }
        }
    }

    #endregion
}
