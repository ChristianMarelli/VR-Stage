using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
public class MeasureRPC : MonoBehaviourPun
{
    public LineRenderer linea;
    private GameObject linea1;
    private GameObject testo1;
    public TextMeshPro testo;
    private RaycastHit position;
    private RaycastHit movePosition;
    public Transform origin;
    private Transform fine;
    private float length;
    private int count = 0;
    public bool lineActive;
    public GameObject player;
    public RPCraggio XRray;
 
    [PunRPC]
    public void RayOff(bool active)
    {
        XRray.gameObject.SetActive(active);
    }

    [PunRPC]
    public void Line(bool active, Vector3 coordi, Vector3 coordf)
    {
            //Rende la linea e la misura visibili a tutti gli utenti online
            linea.gameObject.SetActive(active);
            testo.gameObject.SetActive(active);
            //viene impostata la variabile coordi come inizio della linea
            linea.SetPosition(0, coordi);
            //ogni volta che Line() viene invocata, coordf cambia.
            //In questo modo gli utenti online riescono a vedere la linea formarsi man mano che il docente effettua la misurazione
            linea.SetPosition(1, coordf);
            //imposta posizione del testo con le misure
            testo.transform.position = new Vector3(coordf.x + 9, coordf.y, coordf.z); ;
            //calcolo della lunghezza della linea
            length = (float)Math.Sqrt(((coordf.x - coordi.x) * (coordf.x - coordi.x)) + ((coordf.y - coordi.y) * (coordf.y - coordi.y)));
            testo.GetComponent<TMP_Text>().SetText(length.ToString() + "m");
    }

    [PunRPC]
    public void LineOff(bool active)
    {
        //disattiva linea e testo in modo da renderli non visibili per gli utenti
        linea.gameObject.SetActive(active);
        testo.gameObject.SetActive(active);
    }

    void Update()
    {
        RaycastHit hitStart;
        RaycastHit hitMove;
        //Lettura input da tastiera e controller 
        if (Input.GetKeyDown(KeyCode.S) | (OVRInput.Get(OVRInput.Button.One)))
        {
            Debug.Log("Inizio Linea");
            //attivazione raycast
            if (Physics.Raycast(origin.position, origin.forward, out hitStart))
            {
                //il punto indicato dall'utente viene impostato come inizio della linea
                position = hitStart;
                linea.SetPosition(0, position.point);
            }
        }
        //rileva se l'utente tiene premuto il tasto
        if (Input.GetKey(KeyCode.S) | (OVRInput.Get(OVRInput.Button.One)))
        {
             if (Physics.Raycast(origin.position, origin.forward, out hitMove))
             {
                 Vector3 coordi = new Vector3(position.point.x, position.point.y, position.point.z);
                 //la coordinata di fine della linea viene cambiata in base al movimento della mano dell'utente
                 Vector3 coordf = new Vector3(hitMove.point.x, hitMove.point.y, hitMove.point.z);
                 //viene invocata la funzione per la remote procedure call che rende la misurazione visibile online
                 //a cui vengono passate la coordinata di inizio e la coordinata di fine del metro
                 photonView.RPC("Line", RpcTarget.AllBuffered, true, coordi, coordf);
                //viene invocata la funzione per la remote procedure call che disattiva la linea
                photonView.RPC("RayOff", RpcTarget.AllBuffered, false);
             }
        }

        if (Input.GetKeyDown(KeyCode.X) | (OVRInput.Get(OVRInput.Button.Two)))
        {
            photonView.RPC("LineOff", RpcTarget.AllBuffered, false);
            lineActive = false;
        }


    }
    
 }

