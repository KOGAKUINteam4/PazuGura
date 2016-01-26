using UnityEngine;
using System.Collections;

public class WebviewSample : MonoBehaviour
{

    Vector3 defaultScale = Vector3.zero;

    public void Start()
    {
        defaultScale = transform.lossyScale;
    }

    public void Update()
    {
        Vector3 lossScale = transform.lossyScale;
        Vector3 localScale = transform.localScale;
        transform.localScale = new Vector3(
                localScale.x / lossScale.x * defaultScale.x,
                localScale.y / lossScale.y * defaultScale.y,
                localScale.z / lossScale.z * defaultScale.z);
    }



    // Use this for initialization
    public void GoURL()
    {
        Application.OpenURL("https://www.youtube.com/watch?v=7cwvUiGBMm4");
    }

}