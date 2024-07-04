using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;
using Photon.Realtime;

public class Object_Destroyer : MonoBehaviourPunCallbacks
{
    public XRRayInteractor XRRayInteractor;

    public GameObject Bandierina;

    public GameObject cosaColpisco;

    public InputAction action = null;

    public UnityEvent OnPress = new UnityEvent();

    public string nomePannello;

    private bool var1;
    // Update is called once per frame


    void Update()
    { 
        if (PhotonNetwork.IsMasterClient)
        {
            XRRayInteractor = GameObject.Find("LeftHand Controller").GetComponent<XRRayInteractor>();
            RaycastHit ray;
            var1 = XRRayInteractor.enabled;
            if (XRRayInteractor.TryGetCurrent3DRaycastHit(out ray))
            {
                cosaColpisco = ray.collider.gameObject;
                if (ray.collider.gameObject == Bandierina)
                {

                    if (var1)
                    {
                        action.Enable();

                    }
                    else
                    {
                        action.Disable();
                    }
                    action.started += Pressed;

                }
                if (ray.collider.gameObject != Bandierina)
                {
                    action.Disable();
                }
            }
        }
    }

    private void Pressed(InputAction.CallbackContext context)
    {
        OnPress.Invoke();
    }

}
