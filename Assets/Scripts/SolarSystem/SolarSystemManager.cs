using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarSystemManager : MonoBehaviour
{
    // the GRAVITY const is higher than it should be to make the planets to move faster than reality
    readonly float GRAVITY = 5f;
    readonly string SUN = "Sun";
    GameObject[] celestials;
    // Start is called before the first frame update
    void Start()
    {
        celestials = GameObject.FindGameObjectsWithTag("Celestials");

        initialSpeed();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate() {
        gravity();
    }

    void gravity() {
        foreach(GameObject firstCel in celestials) {
            foreach(GameObject secondCel in celestials) {
                if (!firstCel.Equals(secondCel)) {
                    float massFirstCel = firstCel.GetComponent<Rigidbody>().mass;
                    float massSecondCel = secondCel.GetComponent<Rigidbody>().mass;
                    float distance = Vector3.Distance(firstCel.transform.position, secondCel.transform.position);

                    firstCel.GetComponent<Rigidbody>().AddForce((secondCel.transform.position - firstCel.transform.position).normalized * 
                        (GRAVITY * (massFirstCel * massSecondCel) / (distance * distance)));
                }
            }
        }
    }

    void initialSpeed() {
        foreach(GameObject firstCel in celestials) {
            foreach(GameObject secondCel in celestials) {
                if (!firstCel.Equals(secondCel)) {
                    
                    float massSecondCel = secondCel.GetComponent<Rigidbody>().mass;
                    float distance = Vector3.Distance(firstCel.transform.position, secondCel.transform.position);

                    firstCel.transform.LookAt(secondCel.transform);

                    // this line is useful to keep the Sun static
                    if (firstCel.name != SUN) {
                        firstCel.GetComponent<Rigidbody>().velocity += firstCel.transform.right * Mathf.Sqrt((GRAVITY * massSecondCel) / distance);
                    }
                }
            }
        }
    }
}
