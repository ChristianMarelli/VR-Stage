using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Voice.Unity;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class NetworkAudioManager : MonoBehaviourPunCallbacks
{

    // se si vuole dare la possibilità agli utenti di creare propri gruppi vocali, il metodo seguente potrebbe impedire questa possibilità
    public bool muteAllSpeakers() {
        // Non posso usare il fatto che la funzione viene eseguita da tutti perché in realtà è solo il masterClient che può mutare tutti

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players != null && players.Length > 0) {
            for (int i = 0; i < players.Length; i++) {
                Speaker speaker = players[i].GetComponentInChildren<Speaker>();
                if (speaker != null) {
                    AudioSource source = speaker.GetComponent<AudioSource>();
                    if (source != null) {
                        source.mute = !source.mute;
                        return source.mute;
                    } else {
                        Debug.LogWarning("[NetworkAudioManager - muteAllSpeakers]: AudioSource not found");
                    }
                }
            }
        }
        return false;
    }

}
