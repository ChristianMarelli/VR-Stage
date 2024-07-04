using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallRPCraggio : MonoBehaviour
{
    public RPCraggio XRray;
    
    public void callOn()
    {
        XRray.RayOn();
    }
    public void callOff()
    {
        XRray.RayOn();
    }
}
