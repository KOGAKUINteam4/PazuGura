using UnityEngine;
using System.Collections;

public class DebugPrint : MonoBehaviour
{
    public string print;

    public void onPrint()
    {
        Debug.Log(print);
    }
}
