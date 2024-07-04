using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Voice.Unity;
using Photon.Voice.PUN;


public class VoiceManager : MonoBehaviour
{
    private PunVoiceClient punVoiceClient;
    private Recorder recorder;

    void Awake() {
        punVoiceClient = gameObject.GetComponent<PunVoiceClient>();
        recorder = gameObject.GetComponent<Recorder>();

        recorder.VoiceDetection = true;
        recorder.VoiceDetectionDelayMs = 100;
        recorder.VoiceDetectionThreshold = 0.02f;
    }

    void Update() {
        if (recorder.VoiceDetectionThreshold == 0) {
            recorder.VoiceDetectionThreshold = 0.02f;
        }
    }
}
