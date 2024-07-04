using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class LabelManager : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI textLabel;
    private bool placemark;
    private bool measurements;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void isPlacemark()
    {
       textLabel.text = "Placemark"; 
       
    }
    public void isMeasurments()
    {

        textLabel.text = "Measurements";
    }
}
