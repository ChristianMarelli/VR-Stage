using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class UIMessageItem : MonoBehaviour
{
    
    public TMP_Text messageLine;

    
    public void SetLine(string message) {
        messageLine.text = message;
    }
}
