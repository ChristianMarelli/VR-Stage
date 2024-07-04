using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Size : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 size;
    private MeshRenderer renderer;

    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();
        size = renderer.bounds.size;
        Debug.Log(size);
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
