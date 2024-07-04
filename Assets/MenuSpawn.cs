using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System;
public class MenuSpawn : MonoBehaviour
{
    public GameObject Menu;
    public GameObject XRleftHand;
    public MeasureRPC measurements;
    public Object_Spawner placemark;
    public GameManager ray;
    public GameObject textLabel;
    //public RPCraggio raggioSinistro;

    private void Update()
    {
        //lettura input da tastiera e da controller
        if (Input.GetKeyDown(KeyCode.M) || OVRInput.Get(OVRInput.Button.Three))
        {
            if (textLabel.activeSelf)
            {
                //disattiva la label con scritta la feature in uso
                textLabel.SetActive(false);
            }
            if (PhotonNetwork.IsMasterClient)
            {
                //viene attivato "Menu", questo componente contiene i pulsanti per attivare le feature "Placemark" e "Measurements"
                Menu.gameObject.SetActive(true);
                //measurements.enabled = false;
                //placemark.enabled = false;

                //se la feature attiva è "Measurements", viene disattivato il componente con lo script che gestisce la misurazione
                if (measurements.enabled == true)
                {
                    //per disattivare il puntatore laser viene invocata la funzione ClickRay() presente all'interno dello script GameManager passandogli un valore booleano false
                    ray.ClickRay(false);
                    measurements.enabled = false;
                }
                //se la feature attiva è "Placemark", viene disattivato il componente con lo script che gestisce la comparsa delle bandierine
                if (placemark.enabled == true)
                {
                    //per disattivare il puntatore laser viene invocata la funzione ClickRay() presente all'interno dello script GameManager passandogli un valore booleano false
                    ray.ClickRay(false);
                    placemark.enabled = false;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.N) || OVRInput.Get(OVRInput.Button.Two))
        {
            //disattiva il menù delle feature
            Menu.gameObject.SetActive(false);
        }
    }
}

