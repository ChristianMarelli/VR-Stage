using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LimitRotation : MonoBehaviour
{

    public float rotationLimit = 90f; // Rotation limit in either direction

    private Image blackout;

    void Start()
    {
        blackout = GetComponentInChildren<Image>();
    }

    void Update()
    {
        float cameraRotationY = transform.localRotation.eulerAngles.y;

        if (cameraRotationY >= rotationLimit && cameraRotationY < (360 - rotationLimit))
        {
            blackout.color = Color.black;
        }
        else
        {
            blackout.color = Color.clear;
        }
    }
}