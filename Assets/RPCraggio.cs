using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class RPCraggio : MonoBehaviourPun
{
    public GameObject XRray;
    public MeasureRPC measurements;



    [PunRPC]
    public void Ray(bool active)
    {
        //in base al valore di "value" il puntatore laser viene attivato o disattivato 
        XRray.gameObject.SetActive(active);
    }

    bool activeRay = false;
    private void Update()
    {
        Debug.Log(activeRay);
        /*if (!photonView.IsMine)
        {
            return;
        }
       
        if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Fray");
            photonView.RPC("Ray", RpcTarget.AllBuffered, true);
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            photonView.RPC("Ray", RpcTarget.AllBuffered, false);
        }

        if (measurements.enabled == true)
        {
           
            photonView.RPC("Ray", RpcTarget.AllBuffered, true);
        }*/
    }
    
    public void RayOn()
    {
        
        if (!photonView.IsMine)
        {

            return;
        }
        else{
                photonView.RPC("Ray", RpcTarget.AllBuffered, true);
        }
    }


    public void RayCall(bool value)
    {
        
        if (!photonView.IsMine)
        {

            return;
        }
        else
        {
            //viene invocata la funzione Ray() che effettua la remote procedure call per attivare o disattivare il puntatore laser
            photonView.RPC("Ray", RpcTarget.AllBuffered, value);
        }
    }


    public void RayOff()
    {
       
        if (!photonView.IsMine)
        {

            return;
        }
        else
        {
            photonView.RPC("Ray", RpcTarget.AllBuffered, false);
        }
    }
}


