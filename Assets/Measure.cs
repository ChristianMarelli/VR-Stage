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
public class Measure : MonoBehaviour
{
    public LineRenderer linea;
    public TextMeshPro testo;
    private RaycastHit position;
    private RaycastHit movePosition;
    public Transform origin;
    private float length;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hitStart;
        RaycastHit hitMove;
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("Inizio Linea");
            //RaycastHit hitStart;
            
            if (Physics.Raycast(origin.position, origin.forward, out hitStart))
            {
                position = hitStart;
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            Debug.Log("Inizio Linea");
            if (Physics.Raycast(origin.position, origin.forward, out hitMove))
            {
                linea.gameObject.SetActive(true);
                movePosition = hitMove;
                linea.SetPosition(0, position.point);
                linea.SetPosition(1, hitMove.point);
                testo.transform.position = new Vector3(hitMove.point.x+9, hitMove.point.y, hitMove.point.z);
                
            }

            length = (float) Math.Sqrt(((movePosition.point.x - position.point.x) * (movePosition.point.x - position.point.x)) + ((movePosition.point.y - position.point.y) * (movePosition.point.y - position.point.y)));
      

            testo.gameObject.SetActive(true);
            testo.SetText(length.ToString() + "m");
           
        }
    }
}
