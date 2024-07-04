using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public GameObject head;
    public GameObject body;
    // Start is called before the first frame update
    void Start()
    {
        head = GameObject.Find("Main Camera");
    }
    
    // Update is called once per frame
    void Update()
    {
        
        //mapPositionBody(body.transform, head.transform);
        body.transform.position = new Vector3(head.transform.position.x, head.transform.position.y - 2, head.transform.position.z);
        Debug.Log("testa" + head.transform);
        //Debug.Log(body.transform);

    }
    void mapPositionBody(Transform target, Transform rigTransform)
    {
        target.position = rigTransform.position;

        //target.Rotate(-90, 0, 0);
    }
}
