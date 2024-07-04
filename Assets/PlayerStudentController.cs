using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.XR;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;
using Photon.Voice.PUN;
using Photon.Voice.Unity;

using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerStudentController : MonoBehaviourPunCallbacks, IPunObservable
{

    //Critical: used by GameManager to keep track of
    //the spawned instances
    public static GameObject localPlayerInstance;

    public Transform head;
    

    public Image speakerImage;
    public Image teacherImage;

    public Canvas playerNameCanvas;

    private GameObject XROrigin;
    private GameObject XRHead;


    //private GameObject XRray;

    private PhotonVoiceView photonVoiceView;
    public Recorder recorder;
    public Speaker speaker;

    public PhotonView PV;

    private bool isTeacher;

    private float playerNameOffset = 0.5f;

    void Awake() {

        //Get photon voice view instance
        photonVoiceView = GetComponent<PhotonVoiceView>();
        recorder = gameObject.GetComponentInChildren<Recorder>();
        recorder.RecordingEnabled = photonView.IsMine;
        speaker = gameObject.GetComponentInChildren<Speaker>();


        if(photonView.IsMine){
            //Critical: teniamo traccia delle istanze dei giocatori gia spawnati
            localPlayerInstance = this.gameObject;

            speakerImage.enabled = false;
        }

        string nickname = photonView.Owner.NickName;
        playerNameCanvas.transform.Find("Name Panel/Player Name").GetComponent<TMP_Text>().text = nickname;

        DontDestroyOnLoad(this.gameObject);

        //Devo posizionare XR Origin dove è stato spawnato l'avatar
        //FindAndBindXRComponents();
    }



    void Update() {

        //Critical: Quando si carica una nuova scena bisogna prima di tutto assicurarsi
        //di avere i riferimenti ai componenti dell'XR Origin, visto che questo
        //Cambia da una scena all'altra.
        if(XRHead == null ) {
            FindAndBindXRComponents();
        }

        if(photonView.IsMine) {
            mapPosition(head, XRHead.transform);
            //mapPosition(leftRay, XRray.transform);
            
            
            if(playerNameCanvas != null)
                MapPosition(playerNameCanvas.transform, head, 0, playerNameOffset, 0);
        }

        //E' una cosa atroce, ma per qualche motivo la soglia continua a tornare a zero.
        recorder.VoiceDetectionThreshold = 0.02f;

    }


    public bool mutePlayer(bool IsMuted)
    {
        PV.RPC("mutePlayerRPC", RpcTarget.AllBuffered);
        return !IsMuted;
    }

    //Questo metodo è fatto per essere chiamato da un altro componente, per stabilire
    //se questa istanza sarà teacher oppure no.
    //purtroppo non basta vedere chi è il masterclient, infatti se il teacher dovesse
    //lasciare la stanza per qualsiasi motivo uno studente diverrebbe il master client.
    public void SetTeacher(bool isTeacher) {
        this.isTeacher = isTeacher;
    }

    public bool IsTeacher() {
        return isTeacher;
    }

    // Assigns to the prefab components (head, left and right hands) the position and the rotation of the XROrigin
    void mapPosition(Transform target, Transform rigTransform) {
        target.position = rigTransform.position;
        target.rotation = rigTransform.rotation;
        //target.Rotate(-90, 0, 0);
    }
    void mapPosition2(Transform target, Transform rigTransform)
    {
        target.position = rigTransform.position + new Vector3(0, 2, 0);
        target.rotation = rigTransform.rotation;
    }
    void mapPosition1(Transform target, Transform rigTransform)
    {
        target.position = rigTransform.position - new Vector3(0, 2f, 0);
        target.rotation = rigTransform.rotation;
        target.Rotate(-90, 0, 0);   
    }
    /*Metodo che mappa la posizione di target in quella di other, con un eventuale offset sulla posizione*/
    void MapPosition(Transform target, Transform other, float offsetX = 0f, float offsetY = 0f, float offsetZ = 0f) {

        target.position = other.position + new Vector3(offsetX, offsetY, offsetZ);
        /*if(other.gameObject == XRrightHand || other.gameObject == XRleftHand) {
            target.rotation = other.rotation;
            target.Rotate(-90, 0, 0);
        }*/
        
            target.rotation = other.rotation;
       
    }

    private void FindAndBindXRComponents() {
        XROrigin = GameObject.Find("XR Origin");
        if (photonView.IsMine)
        {
            mapPosition(XROrigin.transform, localPlayerInstance.transform);
        }
        XRHead = GameObject.Find("Main Camera");
       
        //XRray = GameObject.Find("Ray Interactor");
    }

    #region IPunObservable implementation
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if(stream.IsWriting) {
            bool IsRecording = photonVoiceView.IsRecording;
            speakerImage.enabled = IsRecording;
            stream.SendNext(IsRecording);
            stream.SendNext(isTeacher);
        }
        else {
            speakerImage.enabled = (bool) stream.ReceiveNext();
            teacherImage.enabled = (bool) stream.ReceiveNext();
        }
    }
    #endregion

    [PunRPC]
    public void mutePlayerRPC()
    {
        AudioSource source = speaker.GetComponent<AudioSource>();
        //isMuted = false;
        if (source != null)
        {
            Debug.Log("[PlayerController: mutePlayer] - AudioSource found.");
            source.mute = !source.mute;
            //isMuted = source.mute;
        }
        else
        {
            Debug.LogError("[PlayerController: mutePlayer] - AudioSource not found!");
        };
    }
}
