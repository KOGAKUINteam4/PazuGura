using UnityEngine;
using System.Collections;

public class LeanTweenSample : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        LeanTween.value(gameObject,
            value => print("currentValue=" + value)
            , 100f, 0f, 5f);

        LeanTween.value(gameObject, 100f, 0f, 5f)
            .setOnUpdate((float value) => { print("currentValue=" + value); })
            .setOnComplete(()=> print("Complete!"));
    }
    
}
