using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

using Photon.Pun;

using Photon.Voice.PUN;
using Photon.Voice.Unity;

public class AudioTestPlayerController : MonoBehaviourPun, IPunObservable
{
    [SerializeField]
    public Image speakerImage;

    private PhotonVoiceView photonVoiceView;
    private Recorder recorder;

    void Start() {
        photonVoiceView = gameObject.GetComponent<PhotonVoiceView>();
        recorder = gameObject.GetComponentInChildren<Recorder>();

        recorder.RecordingEnabled = photonView.IsMine;

        if(photonView.IsMine)
            speakerImage.enabled = false;
    }

    void Update() {

        if(Input.GetKeyDown(KeyCode.S)) {
            if(photonView.IsMine)
                this.transform.position += new Vector3(0, -1, 0);
        }
        if(Input.GetKeyDown(KeyCode.W)) {
            if(photonView.IsMine)
                this.transform.position += new Vector3(0, 1, 0);
        }
        //recorder.VoiceDetectionThreshold = 0.02f;

        Debug.Log("Using device " + recorder.MicrophoneDevice + "for " + PhotonNetwork.LocalPlayer.UserId);
    }

    #region IPunObservable implementation
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if(stream.IsWriting && photonVoiceView != null) {
            bool IsRecording = photonVoiceView.IsRecording;
            this.speakerImage.enabled = IsRecording;
            stream.SendNext(IsRecording);
        }
        else {
            if(stream.IsReading){
                speakerImage.enabled = (bool) stream.ReceiveNext();
            }
        }
    }
    #endregion
}
