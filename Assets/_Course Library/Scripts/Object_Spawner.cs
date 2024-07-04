
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using Photon.Pun;
using Photon.Realtime;

public class Object_Spawner : MonoBehaviourPunCallbacks
{
    [SerializeField]
    public Transform origin;

    public XRRayInteractor XRRayInteractor;
    public GameObject raggio = null;
    [Tooltip("The object that will be spawned")]
    public GameObject bandierina = null;
    private GameObject bandierina1 = null;
    [Tooltip("The transform where the object is spanwed")]
    public Transform spawnPosition = null;

    public List<GameObject> Objects_where_spawn;

    private Vector3 groundPt;

    public InputAction action = null;

    public UnityEvent OnPress = new UnityEvent();

    public GameObject cosaColpisco;
    public GameObject player;

    private int count = 0;
    public void Update()
    {
        RaycastHit res;
        bool var = XRRayInteractor.enabled;

        /*if (PhotonNetwork.IsMasterClient)
        {
            if (XRRayInteractor.TryGetCurrent3DRaycastHit(out res))
            {
                cosaColpisco = res.collider.gameObject;
                if (Objects_where_spawn.Contains(res.collider.gameObject))
                {
                    if (cosaColpisco.GetComponent<ContaBandiere>().numerobandiere == 0)
                    {
                        groundPt = res.point; // the coordinate that the ray hits    
                        if (var) // se il raggio è attivo
                        {
                            action.Enable(); // abilita azione
                        }
                        else
                        {

                            action.Disable();
                        }
                        action.started += Pressed;

                    }
                    if (cosaColpisco.GetComponent<ContaBandiere>().numerobandiere == 1)
                    {
                        action.Disable();
                    }
                }
                if (!Objects_where_spawn.Contains(res.collider.gameObject))
                {
                    action.Disable();
                }

            }
        }*/

        //Lettura input da tastiera e chiamata alla funzione SpawnObj per creare una bandierina
        if (Input.GetKeyDown(KeyCode.Z))
        {


            SpawnObj();
        }
        //Lettura input da controller e chiamata alla funzione SpawnObj per creare una bandierina
        if (OVRInput.Get(OVRInput.Button.One))
        {

            SpawnObj();
        }

        //Lettura input da tastiera e chiamata alla funzione DestroyObj per eliminare una bandierina   
        if (Input.GetKeyDown(KeyCode.X))
        {
            DestroyObj();


        }

        //Lettura input da controller e chiamata alla funzione DestroyObj per eliminare una bandierina  
        if (OVRInput.Get(OVRInput.Button.Two))
        {
            DestroyObj();
        }

    }

    //Crea un istanza della bandierina visibile a tutti gli utenti online
    public void SpawnObj()
    {
        Debug.Log(count);
        RaycastHit hit;
        if (Physics.Raycast(origin.position, origin.forward, out hit))
        {
            Debug.Log("bandiera");
            bandierina1 = PhotonNetwork.Instantiate(bandierina.name, hit.point, bandierina.transform.rotation, 0);
            
        }

        
    }
  
    //Se l'utente sta mirando una bandierina, questa viene eliminata
    public void DestroyObj()
    {
        Debug.Log(count);
        RaycastHit hit;
        
        if (Physics.Raycast(origin.position, origin.forward, out hit))
        {
            cosaColpisco = hit.collider.gameObject;
            Debug.Log(cosaColpisco.gameObject.name);
            if (cosaColpisco.gameObject.name == "Bandierina(Clone)")
            {
                PhotonNetwork.Destroy(cosaColpisco);
            }
                
        }

    }

    private void Pressed(InputAction.CallbackContext context)
    {
        OnPress.Invoke();
    }
}
