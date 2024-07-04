using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class provaRPCraggio : MonoBehaviourPun
{
    public GameObject XRray;

    [PunRPC]
    public void Ray(bool active)
    {
        XRray.gameObject.SetActive(active);
      

    }

    private void Update()
    {
        if (!photonView.IsMine)
        {
            Debug.Log("Fraywdwa");

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
    }
}